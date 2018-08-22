﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonView.Model;
using HLACommonView.Views;
using HLACommonView.Configs;
using Xindeco.Device.Model;
using System.Threading;
using DMSkin;
using HLACommonLib;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using HLACommonLib.Model.YK;
using System.Drawing.Printing;
using Stimulsoft.Report;

namespace HLAJiaoJieCheckChannelMachine
{
    public partial class Form1 : CommonInventoryFormIMP,UploadMsgFormMethod
    {
        public const string SUCCESS = "SUCCESS";
        public const string FAILURE = "FAILURE";

        public const string HAS_SAOMIAO_CHONGTOU = "重投";
        public const string WEI_SAO_DAO_XIANGHU = "未扫到箱号";
        public const string HAS_SAOMIAO_YICHANG = "收货记录异常";

        public CDianShangDoc mJiaoJieDan = new CDianShangDoc();
        public List<CDianShangBox> mCurDanBoxList = new List<CDianShangBox>();

        /*
        private object savingDataLockObject = new object();
        private Queue<CUploadData> savingData = new Queue<CUploadData>();
        private Thread savingDataThread = null;
        */
        public Form1()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);
        }

        private void InitView()
        {
            Invoke(new Action(() => {
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblInventoryRe.Text = "未开始工作";
                label11_status.Text = "未开始工作";
                button2_stop.Enabled = false;
            }));
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            InitView();
            Thread thread = new Thread(new ThreadStart(() => {
                ShowLoading("正在连接PLC...");
                if (ConnectPlc())
                    Invoke(new Action(() => { lblPlc.Text = "正常"; lblPlc.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblPlc.Text = "异常"; lblPlc.ForeColor = Color.OrangeRed; }));

                ShowLoading("正在连接条码扫描枪...");
                ConnectBarcode();

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
                HLATagInfo i1 = hlaTagList.FirstOrDefault(i => i.MATNR == "HKNAD3A212AB2001");
                HLATagInfo i2 = hlaTagList.FirstOrDefault(i => i.MATNR == "HKNAD3A212AB2002");
                HLATagInfo i3 = hlaTagList.FirstOrDefault(i => i.MATNR == "HKNAD3A212AB2003");
                HLATagInfo i4 = hlaTagList.FirstOrDefault(i => i.MATNR == "HKNAD3A212AB2004");

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

                HideLoading();

                /*
                this.savingDataThread = new Thread(new ThreadStart(savingDataThreadFunc));
                this.savingDataThread.IsBackground = true;
                this.savingDataThread.Start();
                */
            }));

            thread.IsBackground = true;
            thread.Start();
        }
        public void clearHu(string hu)
        {
            if(string.IsNullOrEmpty(hu))
            {
                MessageBox.Show("请输入箱号");
                return;
            }

            if (MessageBox.Show("确认删除该箱数据吗？", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LocalDataService.clearDianShangBox(mJiaoJieDan.doc, hu);
                mCurDanBoxList.RemoveAll(i => i.hu == hu);
                updateHuCount();
                MessageBox.Show("删除成功");
            }
        }

        public void clearDoc(string doc)
        {
            if (string.IsNullOrEmpty(doc))
            {
                MessageBox.Show("请输入单号");
                return;
            }

            if (doc != mJiaoJieDan.doc)
            {
                MessageBox.Show("输入的单号和当前交接单号不一致");
                return;
            }

            if(MessageBox.Show("确认删除该单号所有数据吗？","询问",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                LocalDataService.clearDianShangDoc(doc);
                mCurDanBoxList.Clear();
                updateHuCount();
                grid.Rows.Clear();
                MessageBox.Show("删除成功");
            }
        }

        public void loadDoc(CDianShangDoc jjd)
        {
            label13_jiaojiedocNO.Text = jjd.doc;

            mJiaoJieDan = jjd;
            mCurDanBoxList = LocalDataService.getDianShangBox(jjd.doc);

            grid.Rows.Clear();
            foreach(var v in mCurDanBoxList)
            {
                addgrid(v);
            }

            updateHuCount();
        }
        List<string> checkInDoc()
        {
            List<string> re = new List<string>();
            try
            {
                List<string> barList = tagDetailList.Select(i => i.MATNR).Distinct().ToList();

                foreach (var v in barList)
                {
                    if (!mJiaoJieDan.dsData.Exists(i => i.barcd == v))
                    {
                        re.Add(v);
                    }
                }
            }
            catch (Exception)
            {

            }

            return re;

        }
        Dictionary<string,int> checkTagQty()
        {
            Dictionary<string, int> re = new Dictionary<string, int>();
            try
            {
                List<string> barList = tagDetailList.Select(i => i.MATNR).Distinct().ToList();

                foreach (var v in barList)
                {
                    if (!mJiaoJieDan.dsData.Exists(i => i.barcd == v))
                    {
                        re[v] = tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc);
                    }
                    else
                    {
                        int curQty = 0;
                        foreach (var box in mCurDanBoxList)
                        {
                            if (box.inventoryRe == "S" && box.sapRe == SUCCESS)
                            {
                                curQty += box.tags.Count(j => j.MATNR == v && !j.IsAddEpc);
                            }
                        }
                        int shouldQty = mJiaoJieDan.dsData.FirstOrDefault(i => i.barcd == v).qty;
                        int curAllQty = curQty + tagDetailList.Count(j => j.MATNR == v && !j.IsAddEpc);
                        if (curAllQty > shouldQty)
                        {
                            re[v] = curAllQty - shouldQty;
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return re;
        }

        List<string> checkTag()
        {
            List<string> re = new List<string>();
            try
            {
                List<string> barList = tagDetailList.Select(i => i.BARCD).Distinct().ToList();

                foreach(var v in barList)
                {
                    if(!mJiaoJieDan.dsData.Exists(i=>i.barcd == v))
                    {
                        re.Add(v);
                    }
                    else
                    {
                        int curQty = 0;
                        foreach(var box in mCurDanBoxList)
                        {
                            if (box.inventoryRe == "S" && box.sapRe == SUCCESS)
                            {
                                curQty += box.tags.Count(j => j.BARCD == v && !j.IsAddEpc);
                            }
                        }
                        int shouldQty = mJiaoJieDan.dsData.FirstOrDefault(i => i.barcd == v).qty;
                        if (curQty + tagDetailList.Count(j => j.BARCD == v && !j.IsAddEpc) > shouldQty)
                        {
                            re.Add(v);
                        }
                    }
                }
            }
            catch(Exception)
            {
                
            }

            return re;
        }

        public void updateHuCount()
        {
            Invoke(new Action(() =>
            {
                label20_okHu.Text = mCurDanBoxList.Count(i => i.inventoryRe == "S" && i.sapRe == SUCCESS).ToString();
                label9_num.Text = mCurDanBoxList.FindAll(i => i.inventoryRe == "S" && i.sapRe == SUCCESS).Sum(j => j.tags.Count(k=>!k.IsAddEpc)).ToString();
            }));
        }

        private void btnInputDoc_Click(object sender, EventArgs e)
        {
            jiaojiedan f = new jiaojiedan(this);
            f.ShowDialog();
        }

        public override void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() => {
                    label11_status.Text = "开始扫描";
                    lblInventoryRe.Text = "";
                    label17_currentHu.Text = "";
                }));

                SetInventoryResult(0);
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();


#if DEBUG
                
                boxNoList.Enqueue("2001994695");
#endif

                if (boxNoList.Count > 0)
                {
                    Invoke(new Action(() => {
                        label17_currentHu.Text = boxNoList.Dequeue();
                    }));
                }
                reader.StartInventory(0, 0, 0);
                isInventory = true;
                lastReadTime = DateTime.Now;
            }
        }

        public void addgrid(CDianShangBox box)
        {
            try
            {
                List<string> mats = box.tags.Select(i => i.MATNR).Distinct().ToList();
                Invoke(new Action(() =>
                {
                    foreach (var item in mats)
                    {
                        int c = box.tags.Count(i => i.MATNR == item && !i.IsAddEpc);

                        CDianShangQty qty = new CDianShangQty();
                        if (box.qty.Exists(i => i.mat == item))
                        {
                            qty = box.qty.FirstOrDefault(i => i.mat == item);
                        }
                        TagDetailInfo ti = box.tags.First(i => i.MATNR == item && !i.IsAddEpc);
                        grid.Rows.Insert(0, box.hu, ti.ZSATNR, ti.ZCOLSN, ti.ZSIZTX, c, qty.hasQty, qty.allQty, box.inventoryMsg + " " + "SAP:" + box.sapMsg);
                        if (box.inventoryRe == "E" || box.sapRe == FAILURE)
                        {
                            grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                        }
                    }
                }));
            }
            catch (Exception)
            {

            }
        }
        void stopReader()
        {
            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    label11_status.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();
            }
        }
        public override void StopInventory()
        {
            if (isInventory)
            {
                Invoke(new Action(() => {
                    label11_status.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();

                List<CDianShangQty> qty = new List<CDianShangQty>();
                CheckResult checkResult = check(label17_currentHu.Text,out qty);

                if (checkResult.InventoryResult)
                {
                    checkResult.UpdateMessage(Consts.Default.RIGHT);
                }

                Invoke(new Action(() => {
                    lblInventoryRe.Text = checkResult.InventoryResult ? "正常" : "异常";
                }));

                CDianShangBox curBox = getCurBox(checkResult,qty);

                saveRecord(curBox);

                if (checkResult.InventoryResult)
                {
                    PrintRightTag(getTags(), label17_currentHu.Text);
                    //上传
                    saveAndUpdate(curBox);
                }

                //add grid
                addgrid(curBox);

                playSound(curBox.inventoryRe=="S" && curBox.sapRe == SUCCESS);

                if (checkResult.InventoryResult)
                {
                    SetInventoryResult(1);
                }
                else
                {
                    SetInventoryResult(1);
                }
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

        CheckResult check(string hu,out List<CDianShangQty> qtys)
        {
            CheckResult re = CheckData();
            qtys = new List<CDianShangQty>();

            if (button3_start.Enabled)
            {
                re.UpdateMessage("请点击开始按钮");
                re.InventoryResult = false;
            }

            if (string.IsNullOrEmpty(hu))
            {
                re.UpdateMessage(WEI_SAO_DAO_XIANGHU);
                re.InventoryResult = false;
                return re;
            }

            //是否已经扫描过了
            if (mCurDanBoxList.Exists(i=>i.hu == hu && i.inventoryRe == "S" && i.sapRe == SUCCESS))
            {
                if (boxSame(hu))
                {
                    re.UpdateMessage(HAS_SAOMIAO_CHONGTOU);
                    re.InventoryResult = false;
                }
                else
                {
                    re.UpdateMessage(HAS_SAOMIAO_YICHANG);
                    re.InventoryResult = false;
                }
            }
            //epc 是否扫描过了
            string sameEpcHu = "";
            if(sameEpc(out sameEpcHu))
            {
                re.UpdateMessage(string.Format("商品已扫描 {0}", sameEpcHu));
                re.InventoryResult = false;
            }

            List<string> notInDoc = checkInDoc();
            if(notInDoc.Count>0)
            {
                string msg = "";
                foreach (var v in notInDoc)
                {
                    msg += v;
                    msg += " ";
                }
                re.UpdateMessage(msg + " 不在本单");
                re.InventoryResult = false;
            }

            /*
            Dictionary<string, int> moreBar = checkTagQty();
            if(moreBar.Count>0)
            {
                string msg = "";
                foreach(var v in moreBar)
                {
                    msg += string.Format(v.Key + "超量" + v.Value);
                    msg += " ";
                }
                re.UpdateMessage(msg);
                re.InventoryResult = false;
            }*/
            List<string> matList = tagDetailList.Select(i => i.MATNR).Distinct().ToList();
            foreach (var v in matList)
            {
                int curQty = 0;
                foreach (var box in mCurDanBoxList)
                {
                    if (box.inventoryRe == "S" && box.sapRe == SUCCESS)
                    {
                        curQty += box.tags.Count(j => j.MATNR == v && !j.IsAddEpc);
                    }
                }

                CDianShangQty dsq = new CDianShangQty();
                dsq.mat = v;
                dsq.curQty = tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc);
                dsq.hasQty = curQty;
                if(mJiaoJieDan.dsData.Exists(i=>i.barcd == v))
                {
                    dsq.allQty = mJiaoJieDan.dsData.FirstOrDefault(i => i.barcd == v).qty;
                }
                qtys.Add(dsq);
            }

            foreach(var v in qtys)
            {
                if(v.allQty>0 && v.curQty+v.hasQty>v.allQty)
                {
                    re.UpdateMessage("超量");
                    re.InventoryResult = false;

                    break;
                }
            }

            return re;
        }
        bool sameEpc(out string hu)
        {
            hu = "";
            foreach (var i in mCurDanBoxList)
            {
                if (i.inventoryRe == "S" && i.sapRe == SUCCESS)
                {
                    foreach (var v in epcList)
                    {
                        if (i.epc.Exists(j => j == v))
                        {
                            hu = i.hu;
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        bool boxSame(string hu)
        {
            CDianShangBox box = mCurDanBoxList.First(i => i.hu == hu);
            return LocalDataService.compareListStr(box.epc, epcList);
        }

        CDianShangBox getCurBox(CheckResult cr,List<CDianShangQty> qty)
        {
            CDianShangBox re = new CDianShangBox();

            try
            {
                re.doc = mJiaoJieDan.doc;
                re.hu = label17_currentHu.Text;
                re.tags = tagDetailList.ToList();
                re.epc = epcList.ToList();

                re.inventoryRe = cr.InventoryResult ? "S" : "E";
                re.inventoryMsg = cr.Message;

                re.qty = qty;

            }
            catch (Exception e)
            {
                Log4netHelper.LogError(e);
            }

            return re;
        }
        /*
        private void savingDataThreadFunc()
        {
            while (true)
            {
                CUploadData upData = null;
                lock (savingDataLockObject)
                {
                    if (savingData.Count > 0)
                    {
                        upData = savingData.Dequeue();
                    }
                }
                if (upData != null)
                {
                    SaveData(upData);
                }

                Thread.Sleep(1000);
            }
        }
        */

        public void updateBoxList(CDianShangBox curBox)
        {
            if (curBox != null && !string.IsNullOrEmpty(curBox.hu))
            {
                mCurDanBoxList.RemoveAll(i => i.doc == mJiaoJieDan.doc && i.hu == curBox.hu);
                mCurDanBoxList.Add(curBox);
            }
        }
        public void saveAndUpdate(CDianShangBox box)
        {
            if (box == null)
                return;

            saveData(box);

            if (box.inventoryRe == "S" && box.sapRe == SUCCESS)
            {
                updateBoxList(box);
                updateHuCount();
            }
        }
        public void saveRecord(CDianShangBox box)
        {
            try
            {
                LocalDataService.saveDianShangBoxRecord(box);
            }
            catch(Exception)
            {

            }
        }
        public void saveData(CDianShangBox box)
        {
            if (box == null)
                return;
            try
            {
                CUploadData data = saveToSqlite(box);
                //uplad to sap
                string sapRe = "";
                string sapMsg = "";
                SAPDataService.uploadDianShangBox(box, ref sapRe, ref sapMsg);
                box.sapRe = sapRe;
                box.sapMsg = sapMsg;

                if (sapRe == SUCCESS)
                {               
                    //save to local
                    LocalDataService.saveDianShangBox(box);

                    SqliteDataService.delUploadFromSqlite(data.Guid);
                }
                else
                {
                    SqliteDataService.updateMsgToSqlite(data.Guid, sapMsg);
                }
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
            }
        }
        public static void PrintRightTag(List<CTagDetail> box, string curBoxNo)
        {
#if DEBUG
            return;
#endif
            try
            {
                if (box.Count <= 0)
                    return;

                int skuCount = 0;
                if (box != null && box.Count > 0)
                {
                    skuCount = box.Count();
                }
                string filepath = "";
                if (skuCount > 10)
                {
                    filepath = Application.StartupPath + "\\Label10Sku.mrt";
                }
                else
                {
                    if (skuCount > 1)
                    {
                        filepath = Application.StartupPath + "\\LabelMultiSku.mrt";
                    }
                    else
                    {
                        filepath = Application.StartupPath + "\\LabelSku.mrt";
                    }
                }
                StiReport report = new StiReport();
                report.Load(filepath);
                report.Compile();
                if (skuCount > 10)
                {
                    report["HU"] = curBoxNo;
                    report["SKUCOUNT"] = box.Count.ToString();
                    report["COUNT"] = box.Sum(i => i.quan).ToString();
                }
                else if (skuCount > 1)
                {
                    report["HU"] = curBoxNo;
                    string content = "";
                    foreach (var matnr in box)
                    {
                        string zsatnr = matnr.zsatnr;
                        string zcolsn = matnr.zcolsn;
                        string zsiztx = matnr.zsiztx;
                        int count = matnr.quan;
                        string newzsiztx = null;
                        if (zsiztx.Contains("/"))
                        {
                            try
                            {
                                newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
                            }
                            catch (Exception)
                            {
                                newzsiztx = zsiztx;
                            }
                        }
                        else
                        {
                            newzsiztx = zsiztx;
                        }

                        content += string.Format("{0}/{1}/{2}/{3}\r\n",
                            zsatnr, zcolsn, newzsiztx, count);
                    }
                    report["CONTENT"] = content;
                    report["COUNT"] = box.Sum(i => i.quan).ToString();

                }
                else
                {
                    CTagDetail matnr = box[0];
                    string zsatnr = matnr.zsatnr;
                    string zcolsn = matnr.zcolsn;
                    string zsiztx = matnr.zsiztx;
                    int count = matnr.quan;
                    string newzsiztx = null;
                    if (zsiztx.Contains("/"))
                    {
                        try
                        {
                            newzsiztx = zsiztx.Substring(zsiztx.IndexOf('(') + 1).TrimEnd(')');
                        }
                        catch (Exception)
                        {
                            newzsiztx = zsiztx;
                        }
                    }
                    else
                    {
                        newzsiztx = zsiztx;
                    }

                    report["HU"] = curBoxNo;
                    report["PINHAO"] = zsatnr;
                    report["SEHAO"] = zcolsn;
                    report["GUIGE"] = newzsiztx;
                    report["SHULIANG"] = count.ToString();
                }

                PrinterSettings printerSettings = new PrinterSettings();
                report.Print(false, printerSettings);

            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.ToString());
            }
        }

        public CUploadData saveToSqlite(CDianShangBox data)
        {
            CUploadData cu = new CUploadData();
            cu.Guid = Guid.NewGuid().ToString();
            cu.IsUpload = 0;
            cu.Data = data;
            cu.CreateTime = DateTime.Now;
            SqliteDataService.saveToSqlite(cu);
            return cu;
        }
        void openMachine()
        {
            try
            {
                if (plc != null)
                {
                    plc.SendCommand((PLCResponse)5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        void closeMachine()
        {
            try
            {
                if (plc != null)
                {
                    plc.SendCommand((PLCResponse)6);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button1_group_Click(object sender, EventArgs e)
        {
            HLACommonView.Views.GxForm form = new GxForm();
            form.ShowDialog();
        }

        private void button3_start_Click(object sender, EventArgs e)
        {
            button3_start.Enabled = false;
            button2_stop.Enabled = true;

            openMachine();

#if DEBUG
            
            StartInventory();

            List<Xindeco.Device.Model.TagInfo> ti = new List<Xindeco.Device.Model.TagInfo>();
            Xindeco.Device.Model.TagInfo t = null;
            for (int i=0;i<21;i++)
            {
                t = new Xindeco.Device.Model.TagInfo();
                t.Epc = "5000001000000B000000199" + i.ToString();
                ti.Add(t);

            }
            t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "5000000FE0000B0000001199";
            ti.Add(t);

            t = new Xindeco.Device.Model.TagInfo();
            t.Epc = "5000000FD0000B0000001199";
            ti.Add(t);

            foreach (var v in ti)
                Reader_OnTagReported(v);

            StopInventory();
#endif
        }

        private void button2_stop_Click(object sender, EventArgs e)
        {
            button3_start.Enabled = true;
            button2_stop.Enabled = false;

            stopReader();
            closeMachine();
        }

        private void button4_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_upload_Click(object sender, EventArgs e)
        {
            UploadMsgForm uf = new UploadMsgForm(this);
            uf.ShowDialog();
        }

        private void button3_clearData_Click(object sender, EventArgs e)
        {
            ClearDataForm cdf = new ClearDataForm(this);
            cdf.ShowDialog();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            SqliteDataService.delOldData();
        }

        public void Upload(CUploadData ud)
        {
            CDianShangBox box = ud.Data as CDianShangBox;
            saveAndUpdate(box);
            addgrid(box);
        }
    }


}
