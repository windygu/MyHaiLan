using System;
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
using HLACommonLib.Model;
using System.Drawing.Printing;
using Stimulsoft.Report;

namespace HLADianShangOutCheckChannelMachine
{
    public partial class Form1 : CommonInventoryFormIMP, UploadMsgFormMethod
    {
        public const string SUCCESS = "SUCCESS";
        public const string FAILURE = "FAILURE";

        CDianShangOutDocInfo getDoc(string hu)
        {
            CDianShangOutDocInfo re = new CDianShangOutDocInfo();

            try
            {
                string msg = "";

                re = SAPDataService.getDianShangOutInfo(hu, out msg);

                if (re != null && !string.IsNullOrEmpty(re.mDoc))
                {
                    //load from sql
                    string sql = string.Format("select data from DianShangOutDoc where doc = '{0}'", re.mDoc);
                    string data = DBHelper.GetValue(sql, false)?.ToString();
                    if (!string.IsNullOrEmpty(data))
                    {
                        CDianShangOutDocInfo outDoc = Newtonsoft.Json.JsonConvert.DeserializeObject<CDianShangOutDocInfo>(data) as CDianShangOutDocInfo;
                        if (outDoc != null)
                            re = outDoc;
                    }
                }
            }
            catch(Exception)
            {

            }

            return re;
        }
        void saveDoc(CDianShangOutDocInfo doc)
        {
            try
            {
                string sql = string.Format("select count(*) from DianShangOutDoc where doc = '{0}'", doc.mDoc);
                if(int.Parse(DBHelper.GetValue(sql,false).ToString())>0)
                {
                    //update
                    sql = string.Format("update DianShangOutDoc set data='{0}',opTime = GETDATE() where doc='{1}'", Newtonsoft.Json.JsonConvert.SerializeObject(doc), doc.mDoc);
                    DBHelper.ExecuteSql(sql, false);
                }
                else
                {
                    //insert
                    sql = string.Format("insert into DianShangOutDoc (doc,data,opTime) values ('{0}','{1}',GETDATE())", doc.mDoc, Newtonsoft.Json.JsonConvert.SerializeObject(doc), doc.mDoc);
                    DBHelper.ExecuteSql(sql, false);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        bool isLastHu(CDianShangOutDocInfo doc,string hu)
        {
            bool re = false;

            if(doc.mHuDetail.Count+1 == doc.mHu.Count && !doc.mHuDetail.ContainsKey(hu) && doc.mHu.Exists(i=>i == hu))
            {
                re = true;
            }

            return re;
        }

        List<CTagSumDif> getChaYi(CDianShangOutDocInfo doc, string hu)
        {
            //总数量只能小于等于，最后一箱必须等于
            List<CTagSumDif> re = new List<CTagSumDif>();

            try
            {
                List<CTagSum> curTagSum = getTagSum();

                bool last = isLastHu(doc, hu);
                foreach(var v in curTagSum)
                {
                    if (doc.mMatQtyList.Exists(i => i.mat == v.mat))
                    {
                        int shouldQty = doc.mMatQtyList.First(i => i.mat == v.mat).qty;
                        int hasQty = doc.mHuDetail.Values.Sum(i => i.Count(j => j.MATNR == v.mat && !j.IsAddEpc));
                        if((last && hasQty + v.qty == shouldQty) || (!last && hasQty + v.qty <= shouldQty))
                        {
                            re.Add(new CTagSumDif(v.mat, v.bar, v.barAdd, v.zsatnr, v.zcolsn, v.zsiztx, v.qty, v.qty_add, 0, string.IsNullOrEmpty(v.barAdd) ? 0 : v.qty_add - v.qty));
                        }
                        else
                        {
                            CTagSumDif tsd = new CTagSumDif(v.mat, v.bar, v.barAdd, v.zsatnr, v.zcolsn, v.zsiztx, v.qty, v.qty_add, hasQty + v.qty - shouldQty, 0);
                            if (!string.IsNullOrEmpty(v.barAdd))
                            {
                                if (v.qty_add == v.qty)
                                    tsd.qty_add_diff = tsd.qty_diff;
                                else
                                    tsd.qty_add_diff = hasQty + v.qty_add - shouldQty;
                            }
                            re.Add(tsd);
                        }
                    }
                    else
                    {
                        re.Add(new CTagSumDif(v.mat, v.bar, v.barAdd, v.zsatnr, v.zcolsn, v.zsiztx, v.qty, v.qty_add, v.qty, v.qty_add));
                    }
                }
            }
            catch(Exception)
            {

            }

            return re;
        }

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
                materialList = LocalDataService.GetMaterialInfoList();

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
                hlaTagList = LocalDataService.GetAllRfidHlaTagList();

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
            }));

            thread.IsBackground = true;
            thread.Start();
        }
        public void clearHu(string hu)
        {
            
        }

        public void clearDoc(string doc)
        {
            
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

                if (SysConfig.IsTest)
                {
                    boxNoList.Enqueue("123451");
                }

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
        string getBoxNo()
        {
            return label17_currentHu.Text.Trim();
        }
        CheckResult check()
        {
            CheckResult re = baseCheck();

            try
            {
                string hu = getBoxNo();

                if(string.IsNullOrEmpty(hu))
                {
                    re.InventoryResult = false;
                    re.UpdateMessage("未扫到箱号");
                }

                CDianShangOutDocInfo doc = getDoc(hu);
                if(string.IsNullOrEmpty(doc.mDoc))
                {
                    re.InventoryResult = false;
                    re.UpdateMessage("箱号无绑定信息");
                }
                
                if(re.InventoryResult == false)
                {
                    grid.Rows.Insert(0, hu, doc.mDoc, "", "", "", "", "", "", "", re.Message);
                    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    return re;
                }

                //重投 商品已扫描
                if(doc.mHuDetail.ContainsKey(hu))
                {
                    re.InventoryResult = false;
                    re.UpdateMessage("重投");
                }
                foreach(var v in doc.mHuDetail.Values)
                {
                    if(v.Select(i=>i.EPC).ToList().Intersect(epcList).Count()>0)
                    {
                        re.InventoryResult = false;
                        re.UpdateMessage("商品已扫描");
                    }
                }
                if (re.InventoryResult == false)
                {
                    grid.Rows.Insert(0, hu, doc.mDoc, "", "", "", "", "", "", "", re.Message);
                    grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    return re;
                }

                List<CTagSumDif> tagDif = getChaYi(doc, hu);
                
                foreach(var v in tagDif)
                {
                    if (v.qty_diff != 0 || v.qty_add_diff != 0)
                    {
                        re.InventoryResult = false;
                        re.UpdateMessage("明细不一致");
                        break;
                    }
                }
                if(re.InventoryResult)
                {
                    re.UpdateMessage("正常");
                }

                //add grid
                foreach (var v in tagDif)
                {
                    grid.Rows.Insert(0, hu, doc.mDoc, v.zsatnr, v.zcolsn, v.zsiztx, v.qty, v.qty_diff, v.qty_add, v.qty_add_diff, re.Message);
                    if (v.qty_diff != 0 || v.qty_add_diff != 0)
                    {
                        grid.Rows[0].DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                }
                //if ok,save sql
                if (re.InventoryResult)
                {
                    doc.mHuDetail[hu] = tagDetailList.ToList();
                    saveDoc(doc);
                }
                //print
                if(re.InventoryResult)
                {
                    printOKLabel(hu, doc.WHAreaId, doc.mDocTime, doc.OrigBillId, doc.mDoc, mainEpcNumber.ToString());
                }
                else
                {
                    printErrorLabel(hu, tagDif);
                }
            }
            catch(Exception ex)
            {
                Log4netHelper.LogError(ex);
            }

            return re;
        }

        void printOKLabel(string hu,string kuqu,string date,string boci,string danhao,string count)
        {
            try
            {
                string filepath = Application.StartupPath + "\\LabelOk.mrt";
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                //设置报表内的参数值
                stiReport.Compile();
                stiReport["HU"] = hu;
                stiReport["COUNT"] = count;
                stiReport["JHKUQU"] = kuqu;
                stiReport["JHDATE"] = date;
                stiReport["JHBOCI"] = boci;
                stiReport["JHDANHAO"] = danhao;

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.Print(false, printerSettings);
            }
            catch (Exception)
            {

            }
        }
        void printErrorLabel(string hu, List<CTagSumDif> difs)
        {
            try
            {
                string filepath = Application.StartupPath + "\\LabelError.mrt";
                StiReport stiReport = new StiReport();
                stiReport.Load(filepath);
                stiReport.Compile();

                stiReport["HU"] = hu;

                DataTable content = new DataTable();
                content.Columns.Add(new DataColumn("PIN", System.Type.GetType("System.String")));
                content.Columns.Add(new DataColumn("SE", System.Type.GetType("System.String")));
                content.Columns.Add(new DataColumn("GUI", System.Type.GetType("System.String")));
                content.Columns.Add(new DataColumn("MAIN", System.Type.GetType("System.Int32")));
                content.Columns.Add(new DataColumn("ADD", System.Type.GetType("System.Int32")));

                foreach(var v in difs)
                {
                    DataRow row = content.NewRow();
                    row["PIN"] = v.zsatnr;
                    row["SE"] = v.zcolsn;
                    row["GUI"] = v.zsiztx;
                    row["MAIN"] = v.qty_diff;
                    row["ADD"] = v.qty_add_diff;
                    content.Rows.Add(row);
                }

                PrinterSettings printerSettings = new PrinterSettings();
                stiReport.RegData("CHAYIDATA", content);
                stiReport.Print(false, printerSettings);
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

                CheckResult checkResult = check();

                if (checkResult.InventoryResult)
                {
                    checkResult.UpdateMessage(Consts.Default.RIGHT);
                }

                Invoke(new Action(() => {
                    lblInventoryRe.Text = checkResult.Message;
                }));

                //play sound
                playSound(checkResult.InventoryResult);

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

        private void button3_start_Click(object sender, EventArgs e)
        {
            button3_start.Enabled = false;
            button2_stop.Enabled = true;

            openMachine();

            if (SysConfig.IsTest)
            {
                StartInventory();

                List<Xindeco.Device.Model.TagInfo> ti = new List<Xindeco.Device.Model.TagInfo>();
                Xindeco.Device.Model.TagInfo t = null;
                for (int i = 0; i < 3; i++)
                {
                    t = new Xindeco.Device.Model.TagInfo();
                    t.Epc = "50002A232508C00000009111" + i.ToString();
                    ti.Add(t);
                }

                for (int i = 0; i < 4; i++)
                {
                    t = new Xindeco.Device.Model.TagInfo();
                    t.Epc = "50002A233508C00000009111" + i.ToString();
                    ti.Add(t);
                }

                for (int i = 0; i < 0; i++)
                {
                    t = new Xindeco.Device.Model.TagInfo();
                    t.Epc = "500009D775000100000011" + i.ToString();
                    ti.Add(t);
                }
                for (int i = 0; i < 0; i++)
                {
                    t = new Xindeco.Device.Model.TagInfo();
                    t.Epc = "500009D775031500000011" + i.ToString();
                    ti.Add(t);
                }

                foreach (var v in ti)
                    Reader_OnTagReported(v);

                StopInventory();
            }
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
            UploadForm<CDianShangOutCheckUploadData> uf = new UploadForm<CDianShangOutCheckUploadData>(this);
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

        void UploadMsgFormMethod.Upload(CCmnUploadData ud)
        {
            CDianShangOutCheckUploadData box = ud.Data as CDianShangOutCheckUploadData;
        }

    }


}
