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
using Xindeco.Device.Model;
using System.IO.Ports;

namespace HLABoxCheckChannelMachine
{
    public partial class InventoryForm : CommonInventoryFormIMP
    {
        CLogManager mLog = new CLogManager(true);
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(true);
        }
        private void InitView()
        {
            Invoke(new Action(() => {
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";

            }));
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
        bool initReader()
        {
            bool re = false;
            try
            {

            }
            catch(Exception)
            {

            }

            return re;
        }
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();

            btnStart.Enabled = false;
            Thread thread = new Thread(new ThreadStart(() => {
                ShowLoading("正在连接PLC...");
                if (ConnectPlc())
                    Invoke(new Action(() => { lblPlc.Text = "正常"; lblPlc.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblPlc.Text = "异常"; lblPlc.ForeColor = Color.OrangeRed; }));

                ShowLoading("正在连接条码扫描枪...");
                ConnectBarcode();

                ShowLoading("正在连接读写器...");
                if (initReader())
                    Invoke(new Action(() => { lblReader.Text = "正常"; lblReader.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed; }));

                Invoke(new Action(() =>
                {
                    btnStart.Enabled = true;
                }));
                HideLoading();

            }));

            thread.IsBackground = true;
            thread.Start();
        }
        private void Start()
        {
            btnStart.Enabled = false;
            btnPause.Enabled = true;
        }
        private void Pause()
        {
            btnStart.Enabled = true;
            btnPause.Enabled = false;
        }
        public override void StartInventory()
        {
            if (!isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "开始扫描";
                    if (btnStart.Enabled)
                    {
                        Start();
                    }
                }));
                SetInventoryResult(0);
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();
                tagDetailList.Clear();

                reader.StartInventory(0, 0, 0);
                isInventory = true;
                lastReadTime = DateTime.Now;

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
        public override void StopInventory()
        {
            if (isInventory)
            {
                Invoke(new Action(() =>
                {
                    lblWorkStatus.Text = "停止扫描";
                }));
                isInventory = false;
                reader.StopInventory();

                playSound(true);

                SetInventoryResult(1);


            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
            openMachine();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
            StopInventory();
            closeMachine();
        }

        List<CTagDetail> getTags()
        {
            List<CTagDetail> re = new List<CTagDetail>();

            try
            {
                if (tagDetailList != null && tagDetailList.Count > 0)
                {
                    foreach (var v in tagDetailList)
                    {
                        if (!v.IsAddEpc)
                        {
                            if (!re.Exists(i => i.proNo == v.MATNR))
                            {
                                CTagDetail t = new CTagDetail();
                                t.proNo = v.MATNR;
                                t.zsatnr = v.ZSATNR;
                                t.zcolsn = v.ZCOLSN;
                                t.zsiztx = v.ZSIZTX;
                                t.charg = v.CHARG;
                                t.quan = 1;

                                re.Add(t);
                            }
                            else
                            {
                                re.FirstOrDefault(i => i.proNo == v.MATNR).quan += 1;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { }

            return re;
        }

        private void InventoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseWindow();
        }
    }
    public class CTagDetail
    {
        public string proNo;
        public string zsatnr;
        public string zcolsn;
        public string zsiztx;
        public string charg;
        public int quan;
    }

}
