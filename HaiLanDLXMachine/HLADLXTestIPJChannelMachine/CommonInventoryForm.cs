using DMSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xindeco.Device;
using Xindeco.Device.Model;
using ThingMagic;
using HLABoxCheckChannelMachine.Utils;

namespace HLABoxCheckChannelMachine
{
    public partial class CommonInventoryForm : MetroForm
    {
        #region 通用属性
        //public UHFReader reader = null;
        public Reader reader = null;
        public PLCController plc = null;
        public BarcodeDevice barcode1 = null;
        public BarcodeDevice barcode2 = null;
        public Queue<string> boxNoList = new Queue<string>();
        public CheckResult checkResult = new CheckResult();
        private string readerIp = SysConfig.ReaderIp;
        public bool isInventory = false;
        public DateTime lastReadTime = DateTime.Now;
        public List<string> epcList = new List<string>();
        #endregion

        public int errorEpcNumber = 0, mainEpcNumber = 0, addEpcNumber = 0;

        public static int mGhost = 0;
        public static int mTrigger = 0;
        public static int mR6ghost = 0;


        public CommonInventoryForm()
        {
            InitializeComponent();
        }



        public virtual CheckResult CheckData()
        {
            CheckResult result = new CheckResult();
            
            if (epcList.Count == 0)
            {
                result.UpdateMessage("未扫描到EPC");
                result.InventoryResult = false;
            }

            return result;
        }


        public virtual void UpdateView()
        {
            throw new NotImplementedException();
        }

        

        public virtual void ShowLoading(string message)
        {
            Invoke(new Action(() => {
                metroPanel1.Show();
                lblText.Text = message;
            }));
            
        }

        public virtual void HideLoading()
        {

            Invoke(new Action(() => {
                metroPanel1.Hide();
                lblText.Text = "";
            }));
        }


        public int inventoryResult = 0;
        public virtual void SetInventoryResult(int result)
        {
            inventoryResult = result;
        }

        private void CommonInventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseWindow();
        }
        #region 设备相关
        public virtual void InitDevice(UHFReaderType readerType, bool connectBarcode)
        {
            //reader = new UHFReader(readerType);
            Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader);
            reader = Reader.Create(string.Format("tcp://{0}", readerIp));

            plc = new PLCController(SysConfig.Port);
            if (connectBarcode)
            {
                barcode1 = new BarcodeDevice(SysConfig.ScannerPort_1);
                barcode2 = new BarcodeDevice(SysConfig.ScannerPort_2);
            }
        }

        public virtual void CloseWindow()
        {
            DisconnectReader();
            DisconnectPlc();
            DisconnectBarcode();
        }
        #region reader
        private void DisconnectReader()
        {
            reader.Destroy();
        }
        
        public virtual bool ConnectReader()
        {
            try
            {
                if (reader == null)
                    return false;

                reader.Connect();

                int[] antennaList = { 1, 2, 3, 4 }; //选择天线1,2,3,4

                reader.ParamSet("/reader/region/id", Reader.Region.NA);

                SimpleReadPlan plan = new SimpleReadPlan(antennaList, TagProtocol.GEN2, null, null, 1000); //设置天线和协议
                reader.ParamSet("/reader/read/plan", plan);

                //场景配置,用于隧道机
                Gen2.LinkFrequency blf = Gen2.LinkFrequency.LINK320KHZ;
                reader.ParamSet("/reader/gen2/BLF", blf);

                Gen2.Tari tari = Gen2.Tari.TARI_6_25US;
                reader.ParamSet("/reader/gen2/tari", tari);

                Gen2.TagEncoding tagncoding = Gen2.TagEncoding.FM0;
                reader.ParamSet("/reader/gen2/tagEncoding", tagncoding);

                Gen2.Session session = Gen2.Session.S1;
                reader.ParamSet("/reader/gen2/session", session);

                Gen2.Target target = Gen2.Target.A;
                reader.ParamSet("/reader/gen2/target", target);

                //500~3150
                reader.ParamSet("/reader/radio/readPower", SysConfig.mReaderPower);

                reader.TagRead += Reader_OnTagReported;
                reader.ReadException += new EventHandler<ReaderExceptionEventArgs>(r_ReadException);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void r_ReadException(object sender, ReaderExceptionEventArgs e)
        {
            MetroMessageBox.Show(this, e.ReaderException.Message,"Error");
        }

        public void Reader_OnTagReported(Object sender,TagReadDataEventArgs taginfo)
        {
            if (!isInventory) return;
            if (taginfo == null || taginfo.TagReadData == null || string.IsNullOrEmpty(taginfo.TagReadData.EpcString)) return;
            if (!epcList.Contains(taginfo.TagReadData.EpcString))
            {
                lastReadTime = DateTime.Now;
                epcList.Add(taginfo.TagReadData.EpcString);

                UpdateView();
            }
        }

        public virtual void StartInventory()
        {
            throw new NotImplementedException();
        }

        public virtual void StopInventory()
        {
            throw new NotImplementedException();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (isInventory)
            {
                //当前正在盘点，则判断上次读取时间和现在读取时间
                if ((DateTime.Now - lastReadTime).TotalMilliseconds >= SysConfig.DelayTime)
                {
                    StopInventory();
                }
            }
        }
        #endregion

        #region plc

        public virtual bool ConnectPlc()
        {
            plc.OnPLCDataReported += Plc_OnPLCDataReported;
            return plc.Connect();
        }

        private void DisconnectPlc()
        {
            plc.OnPLCDataReported -= Plc_OnPLCDataReported;
            plc.Disconnect();
        }
        private void Plc_OnPLCDataReported(Xindeco.Device.Model.PLCData plcData)
        {
            if (plcData.Command == Xindeco.Device.Model.PLCRequest.OPEN_RFID)
            {
                StartInventory();
            }
            else if (plcData.Command == Xindeco.Device.Model.PLCRequest.ASK_RESULT)
            {
                switch (inventoryResult)
                {
                    case 1://正常
                        StopInventory();
                        plc.SendCommand(Xindeco.Device.Model.PLCResponse.RIGHT);
                        break;
                    case 3://异常
                        StopInventory();
                        plc.SendCommand(Xindeco.Device.Model.PLCResponse.ERROR);
                        break;
                    default:

                        break;
                }
            }
        }
        #endregion

        #region barcode

        public virtual void ConnectBarcode()
        {
            barcode1.OnBarcodeReported += OnBarcodeReported;
            barcode1.Connect();
            barcode2.OnBarcodeReported += OnBarcodeReported;
            barcode2.Connect();
        }

        private void DisconnectBarcode()
        {
            barcode1.OnBarcodeReported -= OnBarcodeReported;
            barcode1.Disconnect();
            barcode2.OnBarcodeReported -= OnBarcodeReported;
            barcode2.Disconnect();
        }

        private void OnBarcodeReported(string barcode)
        {
            if (!boxNoList.Contains(barcode))
                boxNoList.Enqueue(barcode);
        }
        #endregion



        

        
        #endregion

    }
    
}
