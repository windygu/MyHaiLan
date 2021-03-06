﻿using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonView.Configs;
using HLACommonView.Model;
using HLACommonView.Views.Dialogs;
using HLACommonView.Views.Interfaces;
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

namespace HLACommonView.Views
{
    public partial class CommonInventoryFormIMPRealMat : MetroForm
    {
        #region 通用属性
        public UHFReader reader = null;
        public PLCController plc = null;
        public BarcodeDevice barcode1 = null;
        public BarcodeDevice barcode2 = null;
        public Queue<string> boxNoList = new Queue<string>();
        private string readerIp = SysConfig.ReaderIp;
        public bool isInventory = false;
        public DateTime lastReadTime = DateTime.Now;
        public List<string> epcList = new List<string>();
        #endregion

        public List<TagDetailInfo> tagDetailList = new List<TagDetailInfo>();

        public List<TagDetailInfo> tagAdd2DetailList = new List<TagDetailInfo>();

        public int errorEpcNumber = 0, mainEpcNumber = 0, addEpcNumber = 0;
        public List<HLATagInfo> hlaTagList = null;
        public List<MaterialInfo> materialList = null;

        public static int mGhost = 0;
        public static int mTrigger = 0;
        public static int mR6ghost = 0;

        private List<string> mIgnoreEpcs = new List<string>();

        Dictionary<string, MaterialInfo> mSapMaterial = new Dictionary<string, MaterialInfo>();

        //thing magic
        //500~3150
        public Reader mReaderTM = null;

        public CommonInventoryFormIMPRealMat()
        {
            InitializeComponent();
            mIgnoreEpcs = SAPDataService.getIngnoreEpcs();
        }
        public void startTarReader()
        {
            if (SysConfig.mReaderType == READER_TYPE.READER_IMP)
                reader.StartInventory(0, 0, 0);
            if (SysConfig.mReaderType == READER_TYPE.READER_TM)
                mReaderTM.StartReading();

        }
        public void stopTarReader()
        {
            if (SysConfig.mReaderType == READER_TYPE.READER_IMP)
                reader.StopInventory();
            if (SysConfig.mReaderType == READER_TYPE.READER_TM)
                mReaderTM.StopReading();
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


        public List<CTagDetail> getTags()
        {
            List<CTagDetail> re = new List<CTagDetail>();

            try
            {
                List<string> barList = tagDetailList.Select(i => i.BARCD).Distinct().ToList();
                foreach (var v in barList)
                {
                    TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.BARCD == v);

                    CTagDetail t = new CTagDetail();
                    t.bar = ti.BARCD;
                    t.zsatnr = ti.ZSATNR;
                    t.zcolsn = ti.ZCOLSN;
                    t.zsiztx = ti.ZSIZTX;
                    t.charg = ti.CHARG;
                    t.quan = tagDetailList.Count(i => i.BARCD == v && !i.IsAddEpc);

                    re.Add(t);
                }
            }

            catch (Exception)
            { }

            return re;
        }
        public List<CTagSum> getTagSum()
        {
            List<CTagSum> re = new List<CTagSum>();
            try
            {
                List<string> matList = tagDetailList.Select(i => i.MATNR).Distinct().ToList();
                foreach (var v in matList)
                {
                    TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.MATNR == v);

                    CTagSum t = new CTagSum();
                    t.mat = v;
                    t.bar = ti.BARCD;
                    t.barAdd = ti.BARCD_ADD;
                    t.zsatnr = ti.ZSATNR;
                    t.zcolsn = ti.ZCOLSN;
                    t.zsiztx = ti.ZSIZTX;
                    t.qty = tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc);
                    t.qty_add = tagDetailList.Count(i => i.MATNR == v && i.IsAddEpc);

                    re.Add(t);
                }
            }
            catch (Exception)
            {

            }
            return re;
        }
        public bool hasDif(List<CTagSumDif> td)
        {
            bool re = false;
            try
            {
                foreach (var v in td)
                {
                    if(v.qty_diff!=0 || v.qty_add_diff!=0)
                    {
                        re = true;
                        break;
                    }
                }
            }
            catch(Exception)
            {

            }
            return re;
        }
        public List<CTagSumDif> duibi(List<CMatQty> std)
        {
            List<CTagSumDif> re = new List<CTagSumDif>();

            if (std != null && std.Count > 0)
            {
                List<string> matList = tagDetailList.Select(i => i.MATNR).Distinct().ToList();
                foreach(var v in matList)
                {
                    TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.MATNR == v);

                    if (std.Exists(i=>i.mat == v))
                    {
                        bool hasAdd = !string.IsNullOrEmpty(ti.BARCD_ADD);
                        int stdQty = std.First(i => i.mat == v).qty;
                        CTagSumDif d = new CTagSumDif(ti.MATNR, ti.BARCD, ti.BARCD_ADD, ti.ZSATNR, ti.ZCOLSN, ti.ZSIZTX
                        , tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc), tagDetailList.Count(i => i.MATNR == v && i.IsAddEpc)
                        , tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc) - stdQty
                        , tagDetailList.Count(i => i.MATNR == v && i.IsAddEpc) - (hasAdd ? stdQty : 0));
                        re.Add(d);
                    }
                    else
                    {
                        CTagSumDif d = new CTagSumDif(ti.MATNR, ti.BARCD, ti.BARCD_ADD, ti.ZSATNR, ti.ZCOLSN, ti.ZSIZTX
                        , tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc), tagDetailList.Count(i => i.MATNR == v && i.IsAddEpc)
                        , tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc), tagDetailList.Count(i => i.MATNR == v && i.IsAddEpc));
                        re.Add(d);
                    }
                }

                foreach(var v in std)
                {
                    if (!matList.Exists(i => i == v.mat))
                    {
                        MaterialInfo mi = materialList.FirstOrDefault(i => i.MATNR == v.mat);
                        HLATagInfo ti = hlaTagList.FirstOrDefault(i => i.MATNR == v.mat);
                        bool hasAdd = false;
                        if (ti != null && !string.IsNullOrEmpty(ti.BARCD_ADD))
                            hasAdd = true;
                        CTagSumDif d = new CTagSumDif(v.mat, ti != null ? ti.BARCD : "", ti != null ? ti.BARCD_ADD : ""
                            , mi != null ? mi.ZSATNR : "", mi != null ? mi.ZCOLSN : "", mi != null ? mi.ZSIZTX : ""
                        , 0, 0
                        , 0 - v.qty, 0 - (hasAdd ? v.qty : 0));
                        re.Add(d);
                    }
                }
            }
            else
            {
                List<string> matList = tagDetailList.Select(i => i.MATNR).Distinct().ToList();
                foreach (var v in matList)
                {
                    TagDetailInfo ti = tagDetailList.FirstOrDefault(i => i.MATNR == v);

                    CTagSumDif d = new CTagSumDif(ti.MATNR, ti.BARCD, ti.BARCD_ADD, ti.ZSATNR, ti.ZCOLSN, ti.ZSIZTX
                        , tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc), tagDetailList.Count(i => i.MATNR == v && i.IsAddEpc)
                        , tagDetailList.Count(i => i.MATNR == v && !i.IsAddEpc), tagDetailList.Count(i => i.MATNR == v && i.IsAddEpc));
                    re.Add(d);
                }
            }

            return re;
        }

        public int checkAdd2()
        {
            List<TagDetailInfo> sum = new List<TagDetailInfo>();
            sum.AddRange(tagDetailList);
            sum.AddRange(tagAdd2DetailList);

            List<string> matList = sum.Select(i => i.MATNR).Distinct().ToList();
            foreach (string m in matList)
            {
                int mainEpc = sum.Count(i => i.MATNR == m && (i.EPC.Substring(0, i.EPC.Length < 14 ? i.EPC.Length : 14) == i.RFID_EPC.Substring(0, i.RFID_EPC.Length < 14 ? i.RFID_EPC.Length : 14) || i.EPC == i.BARCD));
                int addEpc = sum.Count(i => i.MATNR == m && (i.EPC.Substring(0, i.EPC.Length < 14 ? i.EPC.Length : 14) == i.RFID_ADD_EPC.Substring(0, i.RFID_ADD_EPC.Length < 14 ? i.RFID_ADD_EPC.Length : 14) || i.EPC == i.BARCD_ADD));
                int add2Epc = sum.Count(i => i.MATNR == m && (i.EPC.Substring(0, i.EPC.Length < 14 ? i.EPC.Length : 14) == i.RFID_ADD_EPC2.Substring(0, i.RFID_ADD_EPC2.Length < 14 ? i.RFID_ADD_EPC2.Length : 14) || i.EPC == i.BARCD_ADD2));

                if (sum.Exists(i => i.MATNR == m && !string.IsNullOrEmpty(i.RFID_ADD_EPC)))
                {
                    if (mainEpc != addEpc)
                    {
                        return 1;
                    }
                }
                if (sum.Exists(i => i.MATNR == m && !string.IsNullOrEmpty(i.RFID_ADD_EPC2)))
                {
                    if (mainEpc != add2Epc)
                        return 2;
                }
            }

            return 0;
        }

        public virtual CheckResult CheckData()
        {
            CheckResult result = new CheckResult();
            if (errorEpcNumber > 0)
            {
                result.UpdateMessage(Consts.Default.EPC_WEI_ZHU_CE);
                result.InventoryResult = false;
            }
            /*
            if (mainEpcNumber != addEpcNumber && tagDetailList.Exists(i => !string.IsNullOrEmpty(i.BARCD_ADD)))
            {
                result.UpdateMessage(Consts.Default.TWO_NUMBER_ERROR);
                result.InventoryResult = false;
            }
            */
            int checkAdd2Re = checkAdd2();
            if (checkAdd2Re == 1)
            {
                result.UpdateMessage("主副条码数量不一致");
                result.InventoryResult = false;
            }
            if (checkAdd2Re == 2)
            {
                result.UpdateMessage("主条码和副2条码数量不一致");
                result.InventoryResult = false;
            }

            if (boxNoList.Count > 0)
            {
                boxNoList.Clear();
                result.UpdateMessage(Consts.Default.XIANG_MA_BU_YI_ZHI);
                result.InventoryResult = false;
            }
            if (epcList.Count == 0)
            {
                result.UpdateMessage(Consts.Default.WEI_SAO_DAO_EPC);
                result.InventoryResult = false;
            }

            return result;
        }

        public CheckResult baseCheck()
        {
            CheckResult result = new CheckResult();
            if (errorEpcNumber > 0)
            {
                result.UpdateMessage(Consts.Default.EPC_WEI_ZHU_CE);
                result.InventoryResult = false;
            }
            if (boxNoList.Count > 0)
            {
                boxNoList.Clear();
                result.UpdateMessage(Consts.Default.XIANG_MA_BU_YI_ZHI);
                result.InventoryResult = false;
            }
            if (epcList.Count == 0)
            {
                result.UpdateMessage(Consts.Default.WEI_SAO_DAO_EPC);
                result.InventoryResult = false;
            }

            return result;
        }

        public virtual void UpdateView()
        {
        }

        MaterialInfo getMaterialFromSAP(string mat)
        {
            if(mSapMaterial.ContainsKey(mat))
            {
                return mSapMaterial[mat];
            }
            List<MaterialInfo> re = SAPDataService.GetMaterialInfoListByMATNR(SysConfig.LGNUM, mat);
            if(re!=null && re.Count>0)
            {
                mSapMaterial[mat] = re[0];
                materialList.RemoveAll(i => i.MATNR == mat);
                materialList.Add(re[0]);
                return re[0];
            }
            return null;
        }
        public TagDetailInfo GetTagDetailInfoByEpc(string epc,ref bool isAdd2)
        {
            if (string.IsNullOrEmpty(epc) || epc.Length < 20)
                return null;
            string rfidEpc = epc.Substring(0, 14) + "000000";
            string rfidAddEpc = rfidEpc.Substring(0, 14);
            if (hlaTagList == null || materialList == null)
                return null;
            List<HLATagInfo> tags = hlaTagList.FindAll(i => i.RFID_EPC == rfidEpc || i.RFID_ADD_EPC == rfidAddEpc || i.RFID_ADD_EPC2 == rfidAddEpc);
            if (tags == null || tags.Count == 0)
                return null;
            else
            {
                HLATagInfo tag = tags.First();
                //MaterialInfo mater = materialList.FirstOrDefault(i => i.MATNR == tag.MATNR);
                MaterialInfo mater = getMaterialFromSAP(tag.MATNR);

                if (mater == null)
                    return null;
                else
                {
                    if (tag.RFID_ADD_EPC2 == rfidAddEpc)
                        isAdd2 = true;

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

                    item.BARCD_ADD2 = tag.BARCD_ADD2;
                    item.RFID_ADD_EPC2 = tag.RFID_ADD_EPC2;

                    if (rfidEpc == item.RFID_EPC)
                        item.IsAddEpc = false;
                    else
                        item.IsAddEpc = true;
                    item.LIFNRS = new List<string>();
                    foreach(HLATagInfo t in tags)
                    {
                        if(!string.IsNullOrEmpty(t.LIFNR))
                        {
                            if(!item.LIFNRS.Contains(t.LIFNR))
                            {
                                item.LIFNRS.Add(t.LIFNR);
                            }
                        }
                    }
                    return item;
                }
            }
        }

        ProcessDialog pd = new ProcessDialog();

        public virtual void ShowLoading(string message)
        {
            Invoke(new Action(() => {
#if DEBUG

#else
                pd.Show();
#endif
                metroPanel1.Show();
                lblText.Text = message;
            }));
            
        }

        public virtual void HideLoading()
        {
            Invoke(new Action(() => {
                pd.Hide();
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
            if (SysConfig.mReaderType == READER_TYPE.READER_IMP)
                reader.Disconnect();
            if (SysConfig.mReaderType == READER_TYPE.READER_TM)
                mReaderTM.Destroy();
        }

        public virtual bool ConnectReader()
        {
            if (SysConfig.mReaderType == READER_TYPE.READER_IMP)
            {
                reader.OnTagReported += Reader_OnTagReported;
                bool result = reader.Connect(readerIp, Xindeco.Device.Model.ConnectType.TCP, WindowsFormsSynchronizationContext.Current);
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
            if (SysConfig.mReaderType == READER_TYPE.READER_TM)
            {
                bool re = false;
                try
                {
                    Reader.SetSerialTransport("tcp", SerialTransportTCP.CreateSerialReader);
                    mReaderTM = Reader.Create(string.Format("tcp://{0}", SysConfig.mReaderTmIp));
                    mReaderTM.TagRead += tagsReportedTM;

                    mReaderTM.Connect();

                    configTM(SysConfig.mReaderTmPower);

                    re = true;
                }
                catch (Exception)
                {
                    re = false;
                }

                return re;
            }
            return false;
        }
        public void configTM(int power = 3000)
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
            mReaderTM.ParamSet("/reader/radio/readPower", power);
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

        void getTagDetail()
        {
            try
            {
                foreach(string epc in epcList)
                {
                    bool isAdd2 = false;
                    TagDetailInfo tag = GetTagDetailInfoByEpc(epc,ref isAdd2);

                    if(isAdd2)
                    {
                        tagAdd2DetailList.Add(tag);
                        continue;
                    }

                    if (tag != null)   //合法EPC
                    {
                        tagDetailList.Add(tag);
                        if (!tag.IsAddEpc)   //主条码
                            mainEpcNumber++;
                        else
                            addEpcNumber++;
                    }
                    else
                    {
                        //累加非法EPC数量
                        errorEpcNumber++;
                    }
                }

                UpdateView();
            }
            catch(Exception)
            {

            }
        }
        public void Reader_OnTagReported(Xindeco.Device.Model.TagInfo taginfo)
        {
            if (!isInventory) return;
            if (taginfo == null || string.IsNullOrEmpty(taginfo.Epc) || ignore(taginfo.Epc)) return;
            if (!epcList.Contains(taginfo.Epc))
            {
                lastReadTime = DateTime.Now;
                epcList.Add(taginfo.Epc);

                /*
                TagDetailInfo tag = GetTagDetailInfoByEpc(taginfo.Epc);
                if (tag != null)   //合法EPC
                {
                    tagDetailList.Add(tag);
                    if (!tag.IsAddEpc)   //主条码
                        mainEpcNumber++;
                    else
                        addEpcNumber++;
                }
                else
                {
                    //累加非法EPC数量
                    errorEpcNumber++;
                }
                UpdateView();
                */
            }
        }

        public void tagsReportedTM(Object sender, TagReadDataEventArgs taginfo)
        {
            if (!isInventory) return;

            if (taginfo == null || taginfo.TagReadData == null || string.IsNullOrEmpty(taginfo.TagReadData.EpcString))
                return;

            string epc = taginfo.TagReadData.EpcString;

            if (!string.IsNullOrEmpty(epc))
            {
                epc = epc.ToUpper();
            }

            if (!epcList.Contains(epc))
            {
                lastReadTime = DateTime.Now;
                epcList.Add(epc);
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
                    getTagDetail();
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
