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
using Impinj.OctaneSdk;
using UARTRfidLink.Extend;
using System.Configuration;
using UARTRfidLink.Exparam;
using Codetag.Rfid.ClassLibrary;
using System.IO.Ports;

namespace HLACommon.Views
{
    public partial class CommonInventoryFormYZ : MetroForm
    {
        ProcessDialog mPd = new ProcessDialog();
        private ErrorWarnForm mErrorForm = new ErrorWarnForm();

        public CReader mReader = null;
        public CPLC mPlc = null;
        public BarcodeDevice mBarcode1 = null;
        public BarcodeDevice mBarcode2 = null;
        public Queue<string> mBoxNoList = new Queue<string>();
        public bool mIsInventory = false;
        public DateTime mLastReadTime = DateTime.Now;
        public List<string> mEpcList = new List<string>();

        public List<TagDetailInfo> mTagDetailList = new List<TagDetailInfo>();
        public int mErrorEpcNumber = 0, mMainEpcNumber = 0, mAddEpcNumber = 0;
        public List<HLATagInfo> mHlaTagList = null;
        public List<MaterialInfo> mMaterialList = null;
        public int mInventoryResult = 0;

        private List<string> mIgnoreEpcs = new List<string>();

        public CommonInventoryFormYZ()
        {
            InitializeComponent();
            mIgnoreEpcs = SAPDataService.getIngnoreEpcs();
        }

        public List<CTagDetail> getTags()
        {
            List<CTagDetail> re = new List<CTagDetail>();
            try
            {
                List<string> barList = mTagDetailList.Select(i => i.BARCD).Distinct().ToList();
                foreach (var v in barList)
                {
                    TagDetailInfo ti = mTagDetailList.FirstOrDefault(i => i.BARCD == v);

                    CTagDetail t = new CTagDetail();
                    t.bar = ti.BARCD;
                    t.zsatnr = ti.ZSATNR;
                    t.zcolsn = ti.ZCOLSN;
                    t.zsiztx = ti.ZSIZTX;
                    t.charg = ti.CHARG;
                    t.quan = mTagDetailList.Count(i => i.BARCD == v && !i.IsAddEpc);

                    re.Add(t);
                }
            }
            catch (Exception)
            { }
            return re;
        }

        public virtual CCheckResult CheckData()
        {
            CCheckResult result = new CCheckResult();
            if (mErrorEpcNumber > 0)
            {
                result.updateMessage(CErrorMsg.EPC_WEI_ZHU_CE);
                result.mInventoryResult = false;
            }
            if (mMainEpcNumber != mAddEpcNumber && mAddEpcNumber > 0)
            {
                result.updateMessage(CErrorMsg.TWO_NUMBER_ERROR);
                result.mInventoryResult = false;
            }

            if (mAddEpcNumber == 0)
            {
                if (mTagDetailList.Exists(i => !string.IsNullOrEmpty(i.BARCD_ADD)))
                {
                    result.updateMessage(CErrorMsg.TWO_NUMBER_ERROR);
                    result.mInventoryResult = false;
                }
            }

            if (mBoxNoList.Count > 0)
            {
                mBoxNoList.Clear();
                result.updateMessage(CErrorMsg.XIANG_MA_BU_YI_ZHI);
                result.mInventoryResult = false;
            }
            if (mEpcList.Count == 0)
            {
                result.updateMessage(CErrorMsg.WEI_SAO_DAO_EPC);
                result.mInventoryResult = false;
            }

            return result;
        }


        public virtual void UpdateView()
        {
        }


        public TagDetailInfo GetTagDetailInfoByEpc(string epc)
        {
            if (string.IsNullOrEmpty(epc) || epc.Length < 20)
                return null;
            string rfidEpc = epc.Substring(0, 14) + "000000";
            string rfidAddEpc = rfidEpc.Substring(0, 14);
            if (mHlaTagList == null || mMaterialList == null)
                return null;
            List<HLATagInfo> tags = mHlaTagList.FindAll(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc);
            if (tags == null || tags.Count == 0)
                return null;
            else
            {
                HLATagInfo tag = tags.First();
                MaterialInfo mater = mMaterialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
                if (mater == null)
                    return null;
                else
                {
                    TagDetailInfo item = new TagDetailInfo();
                    item.EPC = epc;
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = string.IsNullOrEmpty(tag.RFID_ADD_EPC) ? "" : tag.RFID_ADD_EPC;
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.BARCD_ADD = tag.BARCD_ADD;
                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.ZCOLSN_WFG = mater.ZCOLSN_WFG;
                    item.PXQTY = mater.PXQTY;
                    item.PXQTY_FH = mater.PXQTY_FH;
                    item.PACKMAT = mater.PXMAT;
                    item.PACKMAT_FH = mater.PXMAT_FH;
                    item.PUT_STRA = mater.PUT_STRA;
                    item.BRGEW = mater.BRGEW;
                    item.MAKTX = mater.MAKTX;

                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    item.LIFNRS = new List<string>();
                    foreach (HLATagInfo t in tags)
                    {
                        if (!string.IsNullOrEmpty(t.LIFNR))
                        {
                            if (!item.LIFNRS.Contains(t.LIFNR))
                            {
                                item.LIFNRS.Add(t.LIFNR);
                            }
                        }
                    }
                    return item;
                }
            }
        }

        public virtual void ShowLoading(string message)
        {
            Invoke(new System.Action(() => {
#if DEBUG

#else
                mPd.Show();
#endif
                metroPanel1.Show();
                lblText.Text = message;
            }));

        }

        public virtual void HideLoading()
        {
            Invoke(new System.Action(() => {
                mPd.Hide();
                metroPanel1.Hide();
                lblText.Text = "";
            }));
        }

        public virtual void SetInventoryResult(int result)
        {

            mInventoryResult = result;
        }

        private void CommonInventoryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeWindow();
        }
        #region 设备相关
        public virtual void InitDevice(READER_TYPE readerType, PLC_TYPE plcType, bool connectBarcode)
        {
            mReader = new CReader(readerType);
            mPlc = new CPLC(plcType); 

            if (connectBarcode)
            {
                mBarcode1 = new BarcodeDevice(CConfig.mScannerPort_1);
                mBarcode2 = new BarcodeDevice(CConfig.mScannerPort_2);
            }

            if (plcType == PLC_TYPE.PLC_NONE)
            {
                mReader.OnTagReported += Reader_OnTagReportedPM;
            }
            else
            {
                mReader.OnTagReported += Reader_OnTagReported;
            }
        }

        public virtual void closeWindow()
        {
            disconnectReader();
            disconnectPlc();
            disconnectBarcode();
        }
        #region reader
        private void disconnectReader()
        {
            mReader.disConnect();
        }

        public virtual bool ConnectReader()
        {
            return mReader.connect();
        }
    
        public bool ignore(string epc)
        {
            try
            {
                if (mIgnoreEpcs != null && mIgnoreEpcs.Count > 0)
                {
                    if (!string.IsNullOrEmpty(epc) && epc.Length >= 14)
                    {
                        string rfidEpc = epc.Substring(0, 14);
                        if (mIgnoreEpcs.Exists(i => i.Contains(rfidEpc)))
                        {
                            return true;
                        }
                    }

                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
        public TagDetailInfo GetTagDetailInfoByBar(string bar)
        {
            if (string.IsNullOrEmpty(bar))
                return null;
            if (mHlaTagList == null || mMaterialList == null)
                return null;
            List<HLATagInfo> tags = mHlaTagList.FindAll(i => i.BARCD.ToUpper() == bar.ToUpper() || (i.BARCD_ADD.ToUpper() == bar.ToUpper()));

            if (tags == null || tags.Count == 0)
                return null;
            else
            {
                HLATagInfo tag = tags.First();
                MaterialInfo mater = mMaterialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
                if (mater == null)
                    return null;
                else
                {
                    TagDetailInfo item = new TagDetailInfo();
                    item.EPC = "";
                    item.RFID_EPC = tag.RFID_EPC;
                    item.RFID_ADD_EPC = tag.RFID_ADD_EPC;
                    item.CHARG = tag.CHARG;
                    item.MATNR = tag.MATNR;
                    item.BARCD = tag.BARCD;
                    item.BARCD_ADD = tag.BARCD_ADD;

                    item.ZSATNR = mater.ZSATNR;
                    item.ZCOLSN = mater.ZCOLSN;
                    item.ZSIZTX = mater.ZSIZTX;
                    item.ZCOLSN_WFG = mater.ZCOLSN_WFG;
                    item.PXQTY = mater.PXQTY;
                    item.PXQTY_FH = mater.PXQTY_FH;
                    item.PACKMAT = mater.PXMAT;
                    item.PACKMAT_FH = mater.PXMAT_FH;
                    item.PUT_STRA = mater.PUT_STRA;
                    item.BRGEW = mater.BRGEW;
                    item.MAKTX = mater.MAKTX;

                    if (bar.ToUpper() == item.BARCD.ToUpper())
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    item.LIFNRS = new List<string>();
                    foreach (HLATagInfo t in tags)
                    {
                        if (!string.IsNullOrEmpty(t.LIFNR))
                        {
                            if (!item.LIFNRS.Contains(t.LIFNR))
                            {
                                item.LIFNRS.Add(t.LIFNR);
                            }
                        }
                    }
                    return item;
                }
            }
        }

        public virtual void Reader_OnTagReportedPMBar(string bar)
        {
            TagDetailInfo tag = GetTagDetailInfoByBar(bar);

            string errorMsg = "";
            if (!tag.IsAddEpc && !checkTagOK(tag, out errorMsg))
            {
                mErrorForm.showErrorInfo(bar, tag, errorMsg);
                return;
            }

            if (tag != null)   //合法EPC
            {
                mTagDetailList.Add(tag);
                if (!tag.IsAddEpc)   //主条码
                    mMainEpcNumber++;
                else
                    mAddEpcNumber++;
            }
            else
            {
                mErrorEpcNumber++;
            }

            UpdateView();
        }

        public virtual void Reader_OnTagReportedPM(string epc)
        {
            if (!mIsInventory || string.IsNullOrEmpty(epc) || ignore(epc)) return;
            if (!mEpcList.Contains(epc))
            {
                mLastReadTime = DateTime.Now;

                TagDetailInfo tag = GetTagDetailInfoByEpc(epc);

                string errorMsg = "";
                if (!tag.IsAddEpc && !checkTagOK(tag, out errorMsg))
                {
                    mErrorForm.showErrorInfo(tag.EPC, tag, errorMsg);
                    return;
                }

                if (tag != null)   //合法EPC
                {
                    mTagDetailList.Add(tag);
                    if (!tag.IsAddEpc)   //主条码
                        mMainEpcNumber++;
                    else
                        mAddEpcNumber++;
                }
                else
                {
                    //累加非法EPC数量
                    mErrorEpcNumber++;
                }

                mEpcList.Add(epc);
                UpdateView();
            }

        }
        public virtual bool checkTagOK(TagDetailInfo tg, out string msg)
        {
            msg = "";
            return true;
        }

        public void Reader_OnTagReported(string epc)
        {
            if (!mIsInventory) return;
            if (string.IsNullOrEmpty(epc) || ignore(epc))
                return;
            if (!mEpcList.Contains(epc))
            {
                mLastReadTime = DateTime.Now;
                mEpcList.Add(epc);

                TagDetailInfo tag = GetTagDetailInfoByEpc(epc);
                if (tag != null)   //合法EPC
                {
                    mTagDetailList.Add(tag);
                    if (!tag.IsAddEpc)   //主条码
                        mMainEpcNumber++;
                    else
                        mAddEpcNumber++;
                }
                else
                {
                    //累加非法EPC数量
                    mErrorEpcNumber++;
                }
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
            if (mIsInventory)
            {
                //当前正在盘点，则判断上次读取时间和现在读取时间
                if ((DateTime.Now - mLastReadTime).TotalMilliseconds >= CConfig.mDelayTime)
                {
                    StopInventory();
                }
            }
        }
        #endregion

        #region plc

        public virtual bool connectPlc()
        {
            mPlc.OnPLCDataReported += Plc_OnPLCDataReported;
            mPlc.OnPLCDataReportedBJ += Plc_OnPLCDataReportedBJ;
            return mPlc.connect();
        }

        private void disconnectPlc()
        {
            mPlc.OnPLCDataReported -= Plc_OnPLCDataReported;
            mPlc.OnPLCDataReportedBJ -= Plc_OnPLCDataReportedBJ;

            mPlc.disConnect();
        }
        private void Plc_OnPLCDataReported(Xindeco.Device.Model.PLCData plcData)
        {
            if (plcData.Command == Xindeco.Device.Model.PLCRequest.OPEN_RFID)
            {
                StartInventory();
            }
        }

        private void Plc_OnPLCDataReportedBJ()
        {
            StartInventory();
        }

        #endregion

        #region barcode

        public virtual void ConnectBarcode()
        {
            mBarcode1.OnBarcodeReported += OnBarcodeReported;
            mBarcode1.Connect();
            mBarcode2.OnBarcodeReported += OnBarcodeReported;
            mBarcode2.Connect();
        }

        private void disconnectBarcode()
        {
            mBarcode1.OnBarcodeReported -= OnBarcodeReported;
            mBarcode1.Disconnect();
            mBarcode2.OnBarcodeReported -= OnBarcodeReported;
            mBarcode2.Disconnect();
        }

        private void OnBarcodeReported(string barcode)
        {
            if (!mBoxNoList.Contains(barcode))
                mBoxNoList.Enqueue(barcode);
        }
        #endregion

        #endregion

    }
    public class CPLC
    {
        public PLC_TYPE mPLCType;

        public PLCController mPlc = null;
        public CModbusRtuBasePLC mModbus = null;
        public event PLCDataReportedHandler OnPLCDataReported;

        public delegate void PLCDataBJ();
        public event PLCDataBJ OnPLCDataReportedBJ;

        public CPLC(PLC_TYPE pt)
        {
            mPLCType = pt;
        }

        public bool connect()
        {
            bool re = false;
            try
            {
                if (mPLCType == PLC_TYPE.PLC_BJ)
                {
                    mModbus = new CModbusRtuBasePLC();
                    if (mModbus.OpenPort(CConfig.mPLCComPort))
                    {
                        //软件准备
                        mModbus.WriteDO(5, false);
                        Thread.Sleep(500);
                        byte[] a = mModbus.ReadDIs();
                        if (a == null || a[4] == 0)
                        {

                        }
                        else
                        {
                            //软件启动
                            mModbus.WriteDO(3, true);
                            Thread th = new Thread(GetDIStatus);
                            th.IsBackground = true;
                            th.Start();
                            re = true;
                        }
                    }
                }
                if (mPLCType == PLC_TYPE.PLC_XD || mPLCType == PLC_TYPE.PLC_DLX)
                {
                    mPlc = new PLCController(CConfig.mPLCComPort);
                    mPlc.OnPLCDataReported += Plc_OnPLCDataReported;
                    re = mPlc.Connect();
                }
            }
            catch (Exception)
            {
                re = false;
            }
            return re;
        }
        public void disConnect()
        {
            if (mPLCType == PLC_TYPE.PLC_BJ)
            {
                mModbus.WriteDO(4, false);
            }
            if (mPLCType == PLC_TYPE.PLC_XD || mPLCType == PLC_TYPE.PLC_DLX)
            {
                mPlc.OnPLCDataReported -= Plc_OnPLCDataReported;
                mPlc.Disconnect();
            }
        }
        private void Plc_OnPLCDataReported(Xindeco.Device.Model.PLCData plcData)
        {
            OnPLCDataReported?.Invoke(plcData);
        }
        void GetDIStatus()
        {
            while (mModbus.Connected)
            {
                byte[] a = mModbus.ReadDIs();
                if (a == null)
                {
                    Thread.Sleep(100);
                    continue;
                }
                //开始读
                if (a[0] == 1)
                {
                    OnPLCDataReportedBJ?.Invoke();
                }
                //硬件启动
                else if (a[2] == 1)
                {
                }
                //硬件停止
                else if (a[3] == 1)
                {
                }
                else
                {
                }

                Thread.Sleep(100);
            }
        }

        public void setInventoryRe(bool ok)
        {
            if (mPLCType == PLC_TYPE.PLC_BJ)
            {
                if (ok)
                {
                    //ok
                    mModbus.WriteDO(1, true);
                }
                else
                {
                    mModbus.WriteDO(2, true);
                }
            }
            if(mPLCType == PLC_TYPE.PLC_XD)
            {
                if (ok)
                {
                    mPlc.SendCommand(Xindeco.Device.Model.PLCResponse.RIGHT);
                }
                else
                {
                    mPlc.SendCommand(Xindeco.Device.Model.PLCResponse.ERROR);
                }
            }
            if(mPLCType == PLC_TYPE.PLC_DLX)
            {
                mPlc.SendCommand(Xindeco.Device.Model.PLCResponse.RIGHT);
            }
        }
    }
    public class CReader
    {
        //1~32.5
        public ImpinjReader mReaderIMP = null;
        //500~3150
        public Reader mReaderTM = null;
        //10~230
        public RfidUARTLinkExtend mReaderDLXPM = null;
        public string mComPort;
        //10~325
        public RfidReader mReaderXDPM = null;

        public READER_TYPE mReaderType;

        public string mIp;
        public double mPower;

        public delegate void TagReportedHandler(string epc);
        public event TagReportedHandler OnTagReported;
        /*
         
        */
        public CReader(READER_TYPE rt)
        {
            mReaderType = rt;

            if(mReaderType == READER_TYPE.READER_IMP)
            {
                mIp = ConfigurationManager.AppSettings["ReaderIP_IMP_XD"];
                mPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_IMP_XD"]);
            }
            if (mReaderType == READER_TYPE.READER_TM)
            {
                mIp = ConfigurationManager.AppSettings["ReaderIP_TM"];
                mPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_TM"]);
            }
            if (mReaderType == READER_TYPE.READER_DLX_PM)
            {
                mPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_DLX_PM"]);
                mComPort = ConfigurationManager.AppSettings["ReaderCom_DLX_PM"];
            }
            if (mReaderType == READER_TYPE.READER_XD_PM)
            {
                mIp = ConfigurationManager.AppSettings["ReaderIP_XD_PM"];
                mPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_XD_PM"]);
            }
        }

        private void tagsReportedXDPM(object sender, Codetag.Rfid.ClassLibrary.TagsReportedEventArgs e)
        {
            if (e != null && !string.IsNullOrEmpty(e.Epc))
            {
                string epc = e.Epc.Replace(" ", "");
                if (!string.IsNullOrEmpty(epc))
                {
                    epc = epc.ToUpper();
                }
                this.OnTagReported?.Invoke(epc);
            }
        }
        private void tagsReportedIMP(ImpinjReader reader, TagReport report)
        {
            if (report != null && report.Tags != null)
            {
                foreach (Tag tag in report.Tags)
                {
                    string epc = tag.Epc.ToString().Replace(" ", "");
                    if (!string.IsNullOrEmpty(epc))
                    {
                        epc = epc.ToUpper();
                    }
                    this.OnTagReported?.Invoke(epc);
                }
            }
        }
        public void tagsReportedTM(Object sender, TagReadDataEventArgs taginfo)
        {
            if (taginfo == null || taginfo.TagReadData == null || string.IsNullOrEmpty(taginfo.TagReadData.EpcString))
                return;

            string epc = taginfo.TagReadData.EpcString;

            if (!string.IsNullOrEmpty(epc))
            {
                epc = epc.ToUpper();
            }
            this.OnTagReported?.Invoke(epc);
        }
        public void tagsReportedDLXPM(object sender, RadioInventoryEventArgs e)
        {
            string epc = "";
            try
            {
                for (int i = 0; i < e.tagInfo.epc.Length; i++)
                {
                    epc += string.Format("{0:X4}", e.tagInfo.epc[i]);
                }
            }
            catch (Exception) { }

            this.OnTagReported?.Invoke(epc);
        }
        public bool connect()
        {
#if DEBUG
            return false;
#endif
            bool re = false;
            try
            {
                if (mReaderType == READER_TYPE.READER_IMP)
                {

                    mReaderIMP = new ImpinjReader();
                    mReaderIMP.TagsReported += this.tagsReportedIMP;

                    mReaderIMP.Connect(mIp);
                    mReaderIMP.ApplyDefaultSettings();

                    configIMP(mPower, CConfig.mSearchMode, CConfig.mSession);

                    re = true;
                }

                if (mReaderType == READER_TYPE.READER_TM)
                {

                    Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader);
                    mReaderTM = Reader.Create(string.Format("tcp://{0}", mIp));
                    mReaderTM.TagRead += tagsReportedTM;

                    mReaderTM.Connect();

                    configTM(mPower);

                    re = true;
                }

                if (mReaderType == READER_TYPE.READER_DLX_PM)
                {
                    mReaderDLXPM = new RfidUARTLinkExtend();
                    mReaderDLXPM.RadioInventory += tagsReportedDLXPM;

                    if (mReaderDLXPM.ConnectRadio(mComPort, 115200) == operateResult.ok)
                    {
                        AntennaPortConfiguration portConfig = new AntennaPortConfiguration();
                        portConfig.powerLevel = (uint)mPower;
                        portConfig.numberInventoryCycles = 8192;
                        portConfig.dwellTime = 2000;
                        mReaderDLXPM.SetAntennaPortConfiguration(mComPort, 0, portConfig);
                        mReaderDLXPM.SetCurrentLinkProfile(mComPort, 1);
                        SingulationAlgorithmParms singParm = new SingulationAlgorithmParms();
                        singParm.singulationAlgorithmType = SingulationAlgorithm.Dynamicq;
                        singParm.startQValue = 4;
                        singParm.minQValue = 0;
                        singParm.maxQValue = 15;
                        singParm.thresholdMultiplier = 4;
                        singParm.toggleTarget = 1;
                        mReaderDLXPM.SetCurrentSingulationAlgorithm(mComPort, singParm);
                        mReaderDLXPM.SetTagGroupSession(mComPort, Session.S0);

                        re = true;
                    }
                }

                if (mReaderType == READER_TYPE.READER_DLX_PM)
                {
                    mReaderXDPM = new RfidReader();
                    mReaderXDPM.OnTagsReported += tagsReportedXDPM;
                    re = mReaderXDPM.OpenReader(mIp, 2048, SynchronizationContext.Current, "M6E").Success;
                    configXDPM(mPower,CConfig.mSearchMode,CConfig.mSession);
                }
            }
            catch (Exception)
            {
                re = false;
            }
            return re;
        }
        public void disConnect()
        {
            if (mReaderType == READER_TYPE.READER_IMP)
            {
                mReaderIMP.Disconnect();
            }
            if (mReaderType == READER_TYPE.READER_TM)
            {
                mReaderTM.Destroy();
            }
            if(mReaderType == READER_TYPE.READER_DLX_PM)
            {
                mReaderDLXPM.DisconnectRadio(mComPort);
            }
            if (mReaderType == READER_TYPE.READER_XD_PM)
            {
                mReaderXDPM.CloseReader();
            }

        }
        public void configXDPM(double power = 28, int searchMode = 1, ushort session = 2)
        {
            try
            {
                uint num = 0u;
                if (searchMode != 1)
                {
                    if (searchMode == 2)
                    {
                        num = 0u;
                    }
                }
                else
                {
                    num = 1u;
                }
                uint num2 = 0u;
                mReaderXDPM.SetSession(session, num2, num);

                for (uint i = 0; i <= 3; i++)
                {
                    mReaderXDPM.SetAntennaPort(i, true);
                    mReaderXDPM.SetAntennaPower((uint)mPower, i);
                }
            }
            catch (Exception)
            {
            }
        }
        public void configIMP(double power = 28, int searchMode = 1, ushort session = 2)
        {
            try
            {
                Settings settings = mReaderIMP.QueryDefaultSettings();
                settings.Report.Mode = ReportMode.Individual;
                settings.Report.IncludeAntennaPortNumber = true;
                settings.Report.IncludePeakRssi = true;
                switch (searchMode)
                {
                    case 1:
                        settings.SearchMode = SearchMode.DualTarget;
                        break;
                    case 2:
                        settings.SearchMode = SearchMode.SingleTarget;
                        break;
                }
                settings.Session = session;
                settings.Antennas.EnableAll();
                settings.Antennas.TxPowerInDbm = power;
                mReaderIMP.ApplySettings(settings);
            }
            catch (Exception)
            {
            }
        }
        public void configTM(double power = 28)
        {
            int[] antennaList = { 1, 2, 3, 4 }; //选择天线1,2,3,4

            mReaderTM.ParamSet("/reader/region/id", Reader.Region.NA);

            SimpleReadPlan plan = new SimpleReadPlan(antennaList, TagProtocol.GEN2, null, null, 1000); //设置天线和协议
            mReaderTM.ParamSet("/reader/read/plan", plan);

            //场景配置,用于隧道机
            Gen2.LinkFrequency blf = Gen2.LinkFrequency.LINK320KHZ;
            mReaderTM.ParamSet("/reader/gen2/BLF", blf);

            Gen2.Tari tari = Gen2.Tari.TARI_6_25US;
            mReaderTM.ParamSet("/reader/gen2/tari", tari);

            Gen2.TagEncoding tagncoding = Gen2.TagEncoding.FM0;
            mReaderTM.ParamSet("/reader/gen2/tagEncoding", tagncoding);

            Gen2.Session session = Gen2.Session.S1;
            mReaderTM.ParamSet("/reader/gen2/session", session);

            Gen2.Target target = Gen2.Target.A;
            mReaderTM.ParamSet("/reader/gen2/target", target);

            //500~3150
            mReaderTM.ParamSet("/reader/radio/readPower", (int)(power));
        }
        public void startRead()
        {
            try
            {
                if (mReaderType == READER_TYPE.READER_IMP)
                {
                    mReaderIMP.Start();
                }
                if (mReaderType == READER_TYPE.READER_TM)
                {
                    mReaderTM.StartReading();
                }
                if (mReaderType == READER_TYPE.READER_DLX_PM)
                {
                    mReaderDLXPM.StartInventory(mComPort, RadioOperationMode.Continuous, 1);
                }
                if (mReaderType == READER_TYPE.READER_XD_PM)
                {
                    mReaderXDPM.StartInventory(1000, 8192u, false);
                }
            }
            catch(Exception)
            {

            }
        }
        public void stopRead()
        {
            try
            {
                if (mReaderType == READER_TYPE.READER_IMP)
                {
                    mReaderIMP.Stop();
                }
                if (mReaderType == READER_TYPE.READER_TM)
                {
                    mReaderTM.StopReading();
                }
                if (mReaderType == READER_TYPE.READER_DLX_PM)
                {
                    mReaderDLXPM.StopInventory(mComPort);
                }
                if (mReaderType == READER_TYPE.READER_XD_PM)
                {
                    mReaderXDPM.StopInventory();
                }
            }
            catch(Exception)
            {

            }
        }
    }

    public class CModbusRtuBasePLC
    {
        SerialPort serialPort;
        object obj = new object();

        public bool Connected = false;
        public bool OpenPort(string com)
        {
            try
            {
                serialPort = new SerialPort();
                serialPort.PortName = com;
                serialPort.BaudRate = 19200;
                serialPort.StopBits = StopBits.One;
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.Even;
                serialPort.Open();

                Connected = true;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ClosePort()
        {
            bool ret = false;

            try
            {
                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                    Connected = false;
                    ret = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return ret;
        }

        public byte[] ReadDIs()
        {
            lock (obj)
            {
                try
                {
                    if (serialPort.IsOpen)
                    {
                        //01 02 00 96 00 08 99 E0 //读8个DI
                        byte[] cmd = new byte[] { 0x01, 0x02, 0x00, 0x96, 0x00, 0x08, 0x99, 0xE0 };

                        serialPort.Write(cmd, 0, cmd.Length);

                        Thread.Sleep(30);
                        byte[] response = new byte[20];
                        int responseLen = serialPort.Read(response, 0, response.Length);

                        if (CheckResponse(response, responseLen, 0x02))
                        {
                            return GetDIs(response[3]);
                        }

                    }
                }
                catch (Exception)
                {

                    //throw;
                }
                return null;
            }
        }

        public void WriteDOs(byte[] values)
        {
            //01 0F 00 20 00 08 01 00 7F 52
            lock (obj)
            {
                if (serialPort.IsOpen)
                {
                    byte[] cmd = new byte[10] { 0x01, 0x0f, 0, 0x20, 0, 8, 1, 0, 0, 0 };

                    string str = "";
                    for (int i = values.Length - 1; i >= 0; i--)
                    {
                        str += values[i];
                    }
                    cmd[7] = System.Convert.ToByte(str, 2);

                    byte[] a = new byte[8];
                    Array.Copy(cmd, a, 8);
                    byte[] crc = CRC16_C(a);

                    cmd[8] = crc[0];
                    cmd[9] = crc[1];

                    Console.Write("发送数据:");
                    for (int i = 0; i < cmd.Length; i++)
                    {
                        Console.Write(cmd[i].ToString("X2") + " ");
                    }
                    Console.WriteLine();

                    serialPort.Write(cmd, 0, cmd.Length);
                    Thread.Sleep(30);
                    byte[] response = new byte[20];
                    int responseLen = serialPort.Read(response, 0, response.Length);

                    Console.Write("接收数据:");
                    for (int i = 0; i < responseLen; i++)
                    {
                        Console.Write(response[i].ToString("X2") + " ");
                    }
                    Console.WriteLine();

                    if (CheckResponse(response, responseLen, 0x0F))
                    {

                    }
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="reset">是否需要复位</param>
        public void WriteDO(byte channel, bool reset)
        {
            WrieDo(channel, 1);

            if (reset)
            {
                Thread.Sleep(100);

                WrieDo(channel, 0);
            }
        }

        private void WrieDo(byte channel, byte value)
        {
            //01 0F 00 20 00 08 01 00 7F 52
            lock (obj)
            {
                if (serialPort.IsOpen)
                {
                    byte[] cmd = new byte[8] { 0x01, 0x05, 0, Convert.ToByte(0x20 + channel - 1), 0, 0, 0, 0 };

                    if (value == 1)
                    {
                        cmd[4] = 0xff;
                    }


                    byte[] a = new byte[6];
                    Array.Copy(cmd, a, 6);
                    byte[] crc = CRC16_C(a);

                    cmd[6] = crc[0];
                    cmd[7] = crc[1];

                    Console.Write("发送数据:");
                    for (int i = 0; i < cmd.Length; i++)
                    {
                        Console.Write(cmd[i].ToString("X2") + " ");
                    }
                    Console.WriteLine();

                    serialPort.Write(cmd, 0, cmd.Length);
                    Thread.Sleep(30);
                    byte[] response = new byte[20];
                    int responseLen = serialPort.Read(response, 0, response.Length);

                    Console.Write("接收数据:");
                    for (int i = 0; i < responseLen; i++)
                    {
                        Console.Write(response[i].ToString("X2") + " ");
                    }
                    Console.WriteLine();

                    if (CheckResponse(response, responseLen, 0x0F))
                    {

                    }
                }
            }
        }
        protected bool CheckResponse(byte[] buff, int len, byte functionCode)
        {
            byte[] a = new byte[len - 2];
            Array.Copy(buff, a, len - 2);
            byte[] crc = CRC16_C(a);
            if (buff[1] == functionCode && crc[0] == buff[len - 2] && crc[1] == buff[len - 1])
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算CRC校验码
        /// Cyclic Redundancy Check 循环冗余校验码
        /// 是数据通信领域中最常用的一种差错校验码
        /// 特征是信息字段和校验字段的长度可以任意选定
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected byte[] CRC16_C(byte[] data)
        {
            byte num = 0xff;
            byte num2 = 0xff;

            byte num3 = 1;
            byte num4 = 160;
            byte[] buffer = data;

            for (int i = 0; i < buffer.Length; i++)
            {
                //位异或运算
                num = (byte)(num ^ buffer[i]);

                for (int j = 0; j <= 7; j++)
                {
                    byte num5 = num2;
                    byte num6 = num;

                    //位右移运算
                    num2 = (byte)(num2 >> 1);
                    num = (byte)(num >> 1);

                    //位与运算
                    if ((num5 & 1) == 1)
                    {
                        //位或运算
                        num = (byte)(num | 0x80);
                    }
                    if ((num6 & 1) == 1)
                    {
                        num2 = (byte)(num2 ^ num4);
                        num = (byte)(num ^ num3);
                    }
                }
            }
            return new byte[] { num, num2 };
        }

        protected byte[] GetDIs(byte value)
        {
            byte[] data = new byte[8];
            try
            {
                string r = Convert.ToString(value, 2);
                int i = r.Length;
                while (i < 8)
                {
                    r = "0" + r;
                    i++;
                }
                for (int j = r.Length - 1; j >= 0; j--)
                {
                    data[r.Length - 1 - j] = Convert.ToByte(r[j].ToString());

                }
            }
            catch (Exception)
            {

                //throw;
            }
            return data;
        }
    }

}
