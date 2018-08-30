using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using OSharp.Utility.Extensions;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace HLAPKCheckChannelMachinePM
{
    public partial class InventoryForm : CommonPMInventoryForm,UploadMsgFormMethod
    {
        CLogManager mLog = new CLogManager(true);

        CPKCheckHuDetailInfo mBoxDetailInfo = new CPKCheckHuDetailInfo();

        const string HU_IS_NULL = "箱号为空";

        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(SysConfig.ReaderComPort, SysConfig.ScanComPort, SysConfig.ReaderPower);

        }

        private void InitView()
        {
            Invoke(new Action(() => {
                lblCurrentUser.Text = SysConfig.CurrentLoginUser != null ? SysConfig.CurrentLoginUser.UserId : "登录信息异常";
                lblReader.Text = "连接中...";
                label11_deviceNo.Text = SysConfig.DeviceInfo != null ? SysConfig.DeviceInfo.EQUIP_HLA : "设备信息异常";
            }));
        }
        private void InventoryForm_Shown(object sender, EventArgs e)
        {
#if DEBUG
            textBox1_boxno.Text = "123456";
            epcList = new List<string>();
            epcList.Add("1234");
            dmButton1_save_Click(null, null);
            List<string> r = getSaveEpcs("1234567");
#endif
        }
        public override void onScanBarcode(string barcode)
        {
            if(textBox1_boxno.Focused)
            {
                textBox1_boxno.Text = barcode;
                KeyPressEventArgs arg = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));
                textBox1_boxno_KeyPress(this, arg);
            }
            if (textBox1_bar.Focused)
            {
                textBox1_bar.Text = barcode;
                KeyPressEventArgs arg = new KeyPressEventArgs(Convert.ToChar(Keys.Enter));
                textBox1_bar_KeyPress(this, arg);
            }
        }
        void playSoundWarn()
        {
            try
            {
                AudioHelper.Play(".\\Res\\uploadError.wav");
            }
            catch (Exception)
            { }
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();

            Thread thread = new Thread(new ThreadStart(() => 
            {
                ShowLoading("正在连接读写器...");
                if (ConnectReader())
                    Invoke(new Action(() => { lblReader.Text = "正常"; lblReader.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed; }));


                bool closed = false;

                ShowLoading("正在下载物料数据...");
#if DEBUG
                materialList = SAPDataService.GetMaterialInfoList(SysConfig.LGNUM);
#else
                materialList = LocalDataService.GetMaterialInfoList();
#endif
                if (materialList == null || materialList.Count <= 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到物料主数据，请检查网络情况", "提示");

                        closed = true;
                        Close();
                    }));

                }

                if (closed) return;

                ShowLoading("正在下载吊牌数据...");
#if DEBUG
                hlaTagList = SAPDataService.GetTagInfoList(SysConfig.LGNUM);
#else
                hlaTagList = LocalDataService.GetAllRfidHlaTagList();
#endif
                if (hlaTagList == null || hlaTagList.Count <= 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        HideLoading();
                        MetroMessageBox.Show(this, "未下载到吊牌主数据，请检查网络情况", "提示");

                        closed = true;
                        Close();
                    }));
                }

                if (closed)
                    return;

                CSqliteDataService.delOldData();

                HideLoading();

            }));

            thread.IsBackground = true;
            thread.Start();
        }

        public override void UpdateView()
        {
            Invoke(new Action(() =>
            {
                lblMainNumber.Text = mainEpcNumber.ToString();

                foreach(DataGridViewRow v in grid.Rows)
                {
                    v.Cells["real_count"].Value = tagDetailList.Count(i => !i.IsAddEpc && i.MATNR == v.Tag.ToString());
                    if(v.Cells["real_count"].Value.ToString() != v.Cells["should_count"].Value.ToString())
                    {
                        v.DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                    else
                    {
                        v.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    }
                }
            }));
        }

        private void Start()
        {
            btnStart.Enabled = false;
            btn_tijiao.Enabled = true;
        }
        private void Reset()
        {
            btnStart.Enabled = true;
            btn_tijiao.Enabled = false;
        }
        void tijiao()
        {
            btn_tijiao.Enabled = false;
        }
        public override void StartInventory()
        {
            if (!isInventory)
            {
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();

                UpdateView();

                restoreEpc();

                base.StartInventory();
                isInventory = true;
            }
        }

        void restoreEpc()
        {
            try
            {
                List<string> epcs = getSaveEpcs(textBox1_boxno.Text.Trim());
                if(epcs!=null)
                {
                    foreach(string epc in epcs)
                    {
                        reportEpc(epc);
                    }
                }
            }
            catch(Exception)
            {

            }
        }
        bool hasDJ()
        {
            foreach(var v in mBoxDetailInfo.mDetail)
            {
                string mat = v.MATNR;
                if(tagDetailList.Count(i=>i.MATNR == mat && !i.IsAddEpc)<v.QTY)
                {
                    return true;
                }
            }
            
            return false;
        }
        public override void StopInventory()
        {
            if (isInventory)
            {
                isInventory = false;
                base.StopInventory();
            }

            CheckResult cr = CheckData();
            if (!cr.InventoryResult)
            {
                mErrorForm.showErrorInfo(tagDetailList, cr.Message);

                return;
            }

            //是否有短检
            bool dj = hasDJ();

            if (dj)
            {
                ConfirmForm cf = new ConfirmForm(mBoxDetailInfo, tagDetailList);
                if (cf.ShowDialog() == DialogResult.OK)
                {
                    //上传
                    string sapRe = "";
                    string sapMsg = "";
                    CPKCheckUpload up = getCurUpload();
                    //save to local
                    saveToLocal(up);
                    //upload to sap
                    uploadSAP(up, out sapRe, out sapMsg);
                    if (sapRe == "S")
                    {
                        playSound(true);
                    }
                    clearUi();
                }
                else
                {
                    btn_tijiao.Enabled = true;
                }
            }
            else
            {
                //上传
                string sapRe = "";
                string sapMsg = "";
                CPKCheckUpload up = getCurUpload();
                //save to local
                saveToLocal(up);
                //upload to sap
                uploadSAP(up, out sapRe, out sapMsg);
                if (sapRe == "S")
                {
                    playSound(true);
                }
                clearUi();
            }
        }
        void saveToLocal(CPKCheckUpload up)
        {
#if DEBUG
            return;
#endif
            try
            {
                string sql = string.Format("insert into PkCheck (boxNo,createTime,data) values ('{0}',GETDATE(),'{1}')", up.mHu, JsonConvert.SerializeObject(up));
                DBHelper.ExecuteSql(sql, false);
            }
            catch(Exception)
            {

            }
        }
        string getBoxNo()
        {
            return textBox1_boxno.Text.Trim();
        }
        public override CheckResult CheckData()
        {
            CheckResult result = base.CheckData();
           
            return result;
        }

        CPKCheckUpload getCurUpload()
        {
            CPKCheckUpload re = new CPKCheckUpload();

            try
            {
                re.mHu = getBoxNo();
                re.mEpcList = epcList.ToList();
                re.IV_UNAME = SysConfig.CurrentLoginUser.UserId;

                List<string> barcdList = tagDetailList.Select(i => i.BARCD).Distinct().ToList();
                foreach (var item in barcdList)
                {
                    CPKCheckUploadData d = new CPKCheckUploadData();
                    d.BARCD = item;
                    int realCount = tagDetailList.Count(i => i.BARCD == item && !i.IsAddEpc);
                    d.QTY = realCount.ToString();
                    string MAT = tagDetailList.FirstOrDefault(i => i.BARCD == item && !i.IsAddEpc).MATNR;
                    d.MATNR = MAT;

                    int shouldCount = mBoxDetailInfo.mDetail.FirstOrDefault(i => i.MATNR == MAT).QTY;
                    d.ZDJQTY = (shouldCount - realCount).ToString();

                    re.mBars.Add(d);
                }

                foreach(var v in mBoxDetailInfo.mDetail)
                {
                    if(!re.mBars.Exists(i=>i.MATNR == v.MATNR))
                    {
                        CPKCheckUploadData d = new CPKCheckUploadData();
                        d.MATNR = v.MATNR;
                        d.QTY = "0";
                        d.ZDJQTY = v.QTY.ToString();
                        d.BARCD = hlaTagList.FirstOrDefault(i => i.MATNR == v.MATNR).BARCD;

                        re.mBars.Add(d);
                    }
                }
            }
            catch(Exception)
            {

            }

            return re;
        }

        public void uploadSAP(CPKCheckUpload uploadData, out string sapRe, out string sapMsg)
        {
            sapRe = "";
            sapMsg = "";

            CCmnUploadData ud = new CCmnUploadData();
            ud.Guid = Guid.NewGuid().ToString();
            ud.Data = uploadData;
            ud.IsUpload = 0;
            ud.CreateTime = DateTime.Now;
            ud.HU = uploadData.mHu;
            CSqliteDataService.saveToSqlite(ud);

            //upload
            SAPDataService.uploadPKCheck(uploadData, out sapRe, out sapMsg);

            if (sapRe != "S")
            {
                CSqliteDataService.updateMsgToSqlite(ud.Guid, sapMsg);
                playSoundWarn();
                dmButton1_exception_query.BackColor = Color.OrangeRed;
            }
            else
            {
                CSqliteDataService.delUploadFromSqlite(ud.Guid);
            }
        }

        
        string getBarAdd(string bar)
        {
            TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.BARCD == bar);
            if (ti != null)
            {
                return ti.BARCD_ADD;
            }
            HLATagInfo t = hlaTagList.FirstOrDefault(i => i.BARCD == bar);
            if (t != null)
            {
                return t.BARCD_ADD;
            }
            return "";

        }

        string getBarAdd(string bar, out string pin, out string se, out string gui)
        {
            pin = "";
            se = "";
            gui = "";
            TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.BARCD == bar);
            if (ti != null)
            {
                pin = ti.ZSATNR;
                se = ti.ZCOLSN;
                gui = ti.ZSIZTX;
                return ti.BARCD_ADD;
            }
            HLATagInfo t = hlaTagList.FirstOrDefault(i => i.BARCD == bar);
            if (t != null)
            {
                MaterialInfo mi = materialList.FirstOrDefault(i => i.MATNR == t.MATNR);
                if (mi != null)
                {
                    pin = mi.ZSATNR;
                    se = mi.ZCOLSN;
                    gui = mi.ZSIZTX;
                }
                return t.BARCD_ADD;
            }
            return "";
        }
            
        void stopReader()
        {
            if (isInventory)
            {
                isInventory = false;
                base.StopInventory();
            }
        }

        void playSound(bool re)
        {
            try
            {
                if (re)
                {
                    AudioHelper.Play(".\\Res\\success.wav");
                }
                else
                {
                    AudioHelper.Play(".\\Res\\fail.wav");
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();

#if DEBUG
            StartInventory();

            string epc = hlaTagList.FirstOrDefault(i => i.MATNR == "HKCAD3A379ACP006").RFID_EPC;
            for(int j=0;j<5;j++)
            {
                reportEpc(epc + j.ToString());
            }

            epc = hlaTagList.FirstOrDefault(i => i.MATNR == "HNTAD1A225AQ1004").RFID_EPC;
            for (int j = 0; j < 9; j++)
            {
                reportEpc(epc + j.ToString());
            }

            epc = hlaTagList.FirstOrDefault(i => i.MATNR == "HNTBD2N301AT1002").RFID_EPC;
            for (int j = 0; j < 3; j++)
            {
                reportEpc(epc + j.ToString());
            }

            /*            
                        reportEpc("500011036500010000001");
                        reportEpc("500011036500010000002");
                        reportEpc("500011036500010000003");

                        reportEpc("500011037500010000001");

                        reportEpc("500011038500010000001");
                        */

            return;
#endif



            StartInventory();
        }

        private void dmButton3_Click(object sender, EventArgs e)
        {
            GxForm form = new GxForm();
            form.ShowDialog();
        }        

        private void InventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindow();
        }
       
        private void dmButton1_exception_query_Click(object sender, EventArgs e)
        {
            dmButton1_exception_query.BackColor = Color.WhiteSmoke;

            UploadForm<CPKCheckUpload> ef = new UploadForm<CPKCheckUpload>(this);
            ef.ShowDialog();
        }
        
        private void textBox1_bar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
                return;

            if (string.IsNullOrEmpty(textBox1_bar.Text.Trim()))
            {
                return;
            }

            onBarcodeScan();
        }

        void onBarcodeScan()
        {
            string bar = textBox1_bar.Text.Trim();

            reportBar(bar);
        }

        public override bool checkTagOK(TagDetailInfo tg, out string msg)
        {
            msg = "";
            if(tg == null)
            {
                msg = "商品未注册";
                return false;
            }

            if(tg.IsAddEpc)
            {
                //不处理辅条码
                return true;
            }

            if(!mBoxDetailInfo.mDetail.Exists(i=>i.MATNR == tg.MATNR))
            {
                msg = "不在本单";
                return false;
            }

            if(tagDetailList.Count(i=>!i.IsAddEpc && i.MATNR == tg.MATNR) >= mBoxDetailInfo.mDetail.First(i=>i.MATNR == tg.MATNR).QTY)
            {
                msg = "数量大于复核数量-" + tg.MATNR;
                return false;
            }

            return true;
        }

        private void textBox1_boxno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 13)
                return;

            string hu = textBox1_boxno.Text.Trim();
            if (string.IsNullOrEmpty(hu))
                return;

            //显示箱号信息Z_EW_RF_283
            string sapRe = "";
            string sapMsg = "";
            CPKCheckHuInfo huInfo = SAPDataService.getPKCheckHuInfo(hu, out sapRe, out sapMsg);

            HuInfoForm iForm = new HuInfoForm(this, huInfo, sapRe);
            if(DialogResult.OK == iForm.ShowDialog())
            {
                showBoxDetailInfo();
            }
        }

        void showBoxDetailInfo()
        {
            //show info
            label9_fenjianboci.Text = mBoxDetailInfo.WAVEID;
            lblzhuangxiangbz.Text = mBoxDetailInfo.DIVERT_FLAGCN;
            label9_dema.Text = mBoxDetailInfo.DEST_ID;
            lblfayunhuadao.Text = mBoxDetailInfo.ZE_LANE_ID;
            //show in grid
            grid.Rows.Clear();

            foreach(CPKCheckHuDetailInfoData di in mBoxDetailInfo.mDetail)
            {
                grid.Rows.Insert(0, mBoxDetailInfo.mHu, di.ZSATNR, di.ZCOLSN, di.ZSIZTX, di.QTY);
                grid.Rows[0].Tag = di.MATNR;
            }

            Reset();
        }

        public void setCurBoxDetail(CPKCheckHuDetailInfo box)
        {
            mBoxDetailInfo = box;
        }

        private void dmButton1_reset_Click(object sender, EventArgs e)
        {
            Reset();
            stopReader();
        }

        private void btn_tijiao_Click(object sender, EventArgs e)
        {
            tijiao();
            StopInventory();
        }

        public void Upload(CCmnUploadData ud)
        {
            string re = "";
            string msg = "";
            uploadSAP(ud.Data as CPKCheckUpload, out re, out msg);
        }

        void clearUi()
        {
            textBox1_boxno.Text = "";
            grid.Rows.Clear();
            textBox1_boxno.Focus();
        }

        List<string> getSaveEpcs(string boxNo)
        {
            List<string> re = new List<string>();
            try
            {
                string sql = string.Format("select saveEpcs from PkCheckSave where boxNo='{0}'", boxNo);
                string epcs = DBHelper.GetValue(sql, false)?.ToString();
                if(!string.IsNullOrEmpty(epcs))
                {
                    re = JsonConvert.DeserializeObject<IEnumerable<string>>(epcs) as List<string>;
                }
            }
            catch (Exception)
            {

            }

            return re;
        }
        private void dmButton1_save_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = string.Format("select boxNo from PkCheckSave where boxNo='{0}'", textBox1_boxno.Text.Trim());
                string boxno = DBHelper.GetValue(sql,false)?.ToString();
                if(string.IsNullOrEmpty(boxno))
                {
                    sql = string.Format("insert into PkCheckSave (boxNo,createTime,saveEpcs) values ('{0}',GETDATE(),'{1}')", textBox1_boxno.Text.Trim(), JsonConvert.SerializeObject(epcList));
                }
                else
                {
                    sql = string.Format("update PkCheckSave set createTime = GETDATE(),saveEpcs='{0}' where boxNo='{1}'", JsonConvert.SerializeObject(epcList), textBox1_boxno.Text.Trim());
                }
                DBHelper.ExecuteSql(sql, false);

                MetroMessageBox.Show(this, "保存成功");
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, "保存失败 " + ex.ToString(), "错误");
            }

            clearUi();
        }
    }

}
