using DMSkin;

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
using HLABoxCheckChannelMachine.Utils;

namespace HLABoxCheckChannelMachine
{

    public partial class InventoryForm : CommonInventoryFormIMP
    {
        string mCurBoxNo = "";
        Thread thread = null;
        public InventoryForm()
        {
            InitializeComponent();
            InitDevice(UHFReaderType.ImpinjR420, true);
        }
        private void InitView()
        {
            Invoke(new Action(() => {
                lblPlc.Text = "连接中...";
                lblReader.Text = "连接中...";
                lblWorkStatus.Text = "未开始工作";

            }));
        }
        public override void UpdateView()
        {
            Invoke(new Action(() => {
                lblQty.Text = epcList.Count.ToString();
            }));
        }
        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitView();

            btnStart.Enabled = false;
            thread = new Thread(new ThreadStart(() => {
                if (ConnectPlc())
                    Invoke(new Action(() => { lblPlc.Text = "正常"; lblPlc.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblPlc.Text = "异常"; lblPlc.ForeColor = Color.OrangeRed; }));

                ConnectBarcode();

                if (ConnectReader())
                    Invoke(new Action(() => { lblReader.Text = "正常"; lblReader.ForeColor = Color.Black; }));
                else
                    Invoke(new Action(() => { lblReader.Text = "异常"; lblReader.ForeColor = Color.OrangeRed; }));

                Invoke(new Action(() =>
                {
                    btnStart.Enabled = true;
                }));

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
                    label9_hu.Text = "";
                    if (btnStart.Enabled)
                    {
                        Start();
                    }
                    lblQty.Text = "0";
                }));
                SetInventoryResult(0);
                errorEpcNumber = 0;
                mainEpcNumber = 0;
                addEpcNumber = 0;
                epcList.Clear();

                mCurBoxNo = "";

                if (boxNoList.Count > 0)
                {
                    mCurBoxNo = boxNoList.Dequeue();
                }
                Invoke(new Action(() =>
                {
                    label9_hu.Text = mCurBoxNo;
                }));
                reader.StartInventory(0,0,0);
                isInventory = true;
                lastReadTime = DateTime.Now;

            }
        }
        public override CheckResult CheckData()
        {
            CheckResult result = base.CheckData();

            if (result.InventoryResult)
            {
                result.UpdateMessage("正常");
            }

            return result;
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

                //show in grid
                Invoke(new Action(() =>
                {
                    grid.Rows.Clear();
                    foreach(var v in epcList)
                    {
                        grid.Rows.Insert(0, v);
                    }
                }));
                //正常1
                SetInventoryResult(1);
                //异常3
                //SetInventoryResult(3);

            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            Pause();
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
