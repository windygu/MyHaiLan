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
    public partial class CommonInventoryFormIMP : MetroForm
    {
        #region 通用属性
        public UHFReader reader = null;
        public PLCController plc = null;
        public BarcodeDevice barcode1 = null;
        public BarcodeDevice barcode2 = null;
        public Queue<string> boxNoList = new Queue<string>();
        public bool isInventory = false;
        public DateTime lastReadTime = DateTime.Now;
        public List<string> epcList = new List<string>();
        #endregion



        public int errorEpcNumber = 0, mainEpcNumber = 0, addEpcNumber = 0;

        public CommonInventoryFormIMP()
        {
            InitializeComponent();
        }
        public void openMachineCommon()
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
        public void closeMachineCommon()
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
            reader = new UHFReader(readerType);
            plc = new PLCController(SysConfig.Port);
            if (connectBarcode)
            {
                barcode1 = new BarcodeDevice(SysConfig.ScannerPort_1);
                barcode2 = new BarcodeDevice(SysConfig.ScannerPort_2);
            }
        }

        public virtual void InitDevice(bool connectBarcode)
        {
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
            reader.Disconnect();
        }

        public virtual bool ConnectReader()
        {
            reader.OnTagReported += Reader_OnTagReported;
            bool result = reader.Connect(SysConfig.ReaderIp, Xindeco.Device.Model.ConnectType.TCP, WindowsFormsSynchronizationContext.Current);
            if (result)
            {
                Xindeco.Device.Model.ReaderConfig config = new Xindeco.Device.Model.ReaderConfig();
                config.SearchMode = SysConfig.ReaderConfig.SearchMode;
                config.Session = SysConfig.ReaderConfig.Session;
                if (config.AntennaList == null) config.AntennaList = new List<Xindeco.Device.Model.ReaderAntenna>();
                if (SysConfig.ReaderConfig.UseAntenna1)
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(1, true, SysConfig.ReaderConfig.AntennaPower1));
                else
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(1, false, SysConfig.ReaderConfig.AntennaPower1));

                if (SysConfig.ReaderConfig.UseAntenna2)
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(2, true, SysConfig.ReaderConfig.AntennaPower2));
                else
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(2, false, SysConfig.ReaderConfig.AntennaPower2));

                if (SysConfig.ReaderConfig.UseAntenna3)
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(3, true, SysConfig.ReaderConfig.AntennaPower3));
                else
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(3, false, SysConfig.ReaderConfig.AntennaPower3));

                if (SysConfig.ReaderConfig.UseAntenna4)
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(4, true, SysConfig.ReaderConfig.AntennaPower4));
                else
                    config.AntennaList.Add(new Xindeco.Device.Model.ReaderAntenna(4, false, SysConfig.ReaderConfig.AntennaPower4));
                reader.SetParameter(config);
            }
            return result;
        }
    

        public void Reader_OnTagReported(Xindeco.Device.Model.TagInfo taginfo)
        {
            if (!isInventory) return;
            if (taginfo == null || string.IsNullOrEmpty(taginfo.Epc)) return;
            if (!epcList.Contains(taginfo.Epc))
            {
                lastReadTime = DateTime.Now;
                epcList.Add(taginfo.Epc);
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

        public void DisconnectBarcode()
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
