using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using OSharp.Utility.Extensions;
using ThingMagic;

namespace HLACommonLib.Model
{
    public enum READER_TYPE
    {
        Unknow,
        READER_IMP,
        READER_TM,
    }
    public class CBarQty
    {
        public string barcd;
        public int qty;
        public CBarQty(string bar,int q)
        {
            barcd = bar;
            qty = q;
        }
    }
    public class CMatQty:ICloneable
    {
        public string mat;
        public int qty;
        public CMatQty(string m, int q)
        {
            mat = m;
            qty = q;
        }

        public object Clone()
        {
            return new CMatQty(mat, qty);
        }
    }
    public class CCancelDocData
    {
        public string hu;
        public bool mIsHz;
        public bool mIsDd;
        public bool mIsCp;
        public bool mIsRFID;
        public List<CBarQty> barQty = new List<CBarQty>();

        public void addBarQty(string bar,int qty)
        {
            if (string.IsNullOrEmpty(bar))
                return;

            if(barQty.Exists(i=>i.barcd == bar))
            {
                barQty.FirstOrDefault(j => j.barcd == bar).qty += qty;
            }
            else
            {
                barQty.Add(new CBarQty(bar, qty));
            }
        }
    }
    public class CCancelDoc
    {
        public string doc;
        public List<CCancelDocData> docData = new List<CCancelDocData>();
    }
    public class CCancelUpload
    {
        public string lgnum;
        public string boxno;
        public string subuser;
        public bool inventoryRe;
        public string equipID;
        public string loucheng;
        public string docno;
        public string dianshuBoCi;

        public bool isHZ;

        public List<TagDetailInfo> tagDetailList = new List<TagDetailInfo>();
        public List<string> epcList = new List<string>();
    }

    public class ZLKBoxInfo
    {
        /// <summary>
        /// 箱码
        /// </summary>
        public string Hu { get; set; }
        /// <summary>
        /// 源存储类型
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 目标存储类型
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamps { get; set; }
        /// <summary>
        /// 海澜设备号
        /// </summary>
        public string EquipHla { get; set; }
        /// <summary>
        /// 信达设备号
        /// </summary>
        public string EquipXindeco { get; set; }
        /// <summary>
        /// 是否交接
        /// </summary>
        public byte IsHandover { get; set; }
        /// <summary>
        /// 是否满箱
        /// </summary>
        public byte IsFull { get; set; }
        /// <summary>
        /// 提交用户
        /// </summary>
        public string SubUser { get; set; }
        /// <summary>
        /// sap返回结果 S成功 E失败
        /// </summary>
        public string SapStatus { get; set; }
        /// <summary>
        /// sap返回信息
        /// </summary>
        public string SapRemark { get; set; }
        /// <summary>
        /// 楼层号
        /// </summary>
        public string LouCeng { get; set; }
        /// <summary>
        /// 包装材料
        /// </summary>
        public string PackMat { get; set; }
        /// <summary>
        /// 交接号（当目标存储类型为Y001时才需要打印该字段）
        /// </summary>
        public string LIFNR { get; set; }
        /// <summary>
        /// 箱明细
        /// </summary>
        public List<ZLKBoxDetailInfo> Details = new List<ZLKBoxDetailInfo>();

        public static ZLKBoxInfo BuildBoxInfo(DataRow row)
        {
            ZLKBoxInfo result = new ZLKBoxInfo();
            if (row != null)
            {
                result.EquipHla = row["EquipHla"] != null ? row["EquipHla"].ToString() : "";
                result.EquipXindeco = row["EquipXindeco"] != null ? row["EquipXindeco"].ToString() : "";
                result.Hu = row["Hu"] != null ? row["Hu"].ToString() : "";
                result.IsFull = row["IsFull"] != null ? byte.Parse(row["IsFull"].ToString()) : (byte)0;
                result.IsHandover = row["IsHandover"] != null ? byte.Parse(row["IsHandover"].ToString()) : (byte)0;
                result.Remark = row["Remark"] != null ? row["Remark"].ToString() : "";
                result.SapStatus = row["SapStatus"] != null ? row["SapStatus"].ToString() : "";
                result.SapRemark = row["SapRemark"] != null ? row["SapRemark"].ToString() : "";
                result.Source = row["Source"] != null ? row["Source"].ToString() : "";
                result.Status = row["Status"] != null ? row["Status"].ToString() : "";
                result.LouCeng = row["LouCeng"] != null ? row["LouCeng"].ToString() : "";
                result.SubUser = row["SubUser"] != null ? row["SubUser"].ToString() : "";
                result.Target = row["Target"] != null ? row["Target"].ToString() : "";
                result.PackMat = row["PackMat"] != null ? row["PackMat"].ToString() : "";
                result.LIFNR = row["LIFNR"] != null ? row["LIFNR"].ToString() : "";
                result.Timestamps = (row["Timestamps"] != null && row["Timestamps"].ToString().Trim() != "") ? DateTime.Parse(row["Timestamps"].ToString()) : DateTime.Now;
            }
            return result;
        }
    }

    public class ZLKBoxDetailInfo
    {
        public long Id { get; set; }
        public string Hu { get; set; }
        public string Epc { get; set; }

        public string Matnr { get; set; }

        public string Zsatnr { get; set; }
        public string Zcolsn { get; set; }
        public string Zsiztx { get; set; }
        public string Barcd { get; set; }
        public DateTime Timestamps { get; set; }
        public byte IsAdd { get; set; }

        public static ZLKBoxDetailInfo BuildBoxDetailInfo(DataRow row)
        {
            ZLKBoxDetailInfo result = new ZLKBoxDetailInfo();
            if (row != null)
            {
                result.Barcd = row["Barcd"] != null ? row["Barcd"].ToString() : "";
                result.Epc = row["Epc"] != null ? row["Epc"].ToString() : "";
                result.Hu = row["Hu"] != null ? row["Hu"].ToString() : "";
                result.Id = row["Id"] != null ? long.Parse(row["Id"].ToString()) : 0;
                result.IsAdd = row["IsAdd"].CastTo<byte>((byte)0);
                result.Matnr = row["Matnr"] != null ? row["Matnr"].ToString() : "";
                result.Zcolsn = row["Zcolsn"] != null ? row["Zcolsn"].ToString() : "";
                result.Zsatnr = row["Zsatnr"] != null ? row["Zsatnr"].ToString() : "";
                result.Zsiztx = row["Zsiztx"] != null ? row["Zsiztx"].ToString() : "";
                result.Timestamps = row["Timestamps"] != null ? DateTime.Parse(row["Timestamps"].ToString()) : DateTime.Now;
            }
            return result;
        }
    }

    public class CStoreQuery
    {
        public string barcd;
        public string storeid;
        public string status;
        public string msg;
        public string hu;
        public int pxqty_fh;
        public string flag;
        public string equip_hla;
        public string loucheng;
        public string date;
        public string time;
    }

    public class CDeliverStoreQuery
    {
        public string bar;
        public string store;
        public string mtr;
        public string msg;
        public string status;
        public string shipDate;
        public string hu;
    }

    public class CJieHuoDan
    {
        public string PICK_LIST = "";
        public List<CJieHuoDanDetail> mJieHuo = new List<CJieHuoDanDetail>();
        public string mStatus = "";
        public string mMsg = "";
    }
    public class CJieHuoDanDetail
    {
        public string PICK_LIST = "";
        public string PICK_LIST_ITEM = "";
        public string PRODUCTNO = "";
        public int QTY = 0;
        public string SHIP_DATE = "";
        public string PICK_DATE = "";
        public string WAVEID = "";
        public string EXPORT_NO = "";
        public string STOTYPE = "";
        public string STOBIN = "";
    }

    public class CJianHuoContrastRe
    {
        public string mat = "";
        public string barcd = "";
        public string p = "";
        public string s = "";
        public string g = "";
        public int shouldQty = 0;
        public int realQty = 0;
        public int shortQty = 0;
    }

    public class CJianHuoUpload
    {
        public string LGNUM = "";
        public string SHIP_DATE = "";
        public string HU = "";
        public string STATUS_IN = "";
        public string MSG_IN = "";
        public string SUBUSER = "";
        public string LOUCENG = "";
        public string EQUIP_HLA = "";

        public List<CJianHuoUploadBar> bars = new List<CJianHuoUploadBar>();
    }

    public class CJianHuoUploadBar
    {
        public string PICK_LIST = "";
        public string BARCD = "";
        public string QTY = "";
        public string DJ_QTY = "";
        public string ERR_QTY = "";
    }

    public class CJianHuoHu
    {
        public string hu = "";
        public string pick_list = "";
        public string mat = "";
        public string p = "";
        public string s = "";
        public string g = "";
        public string should_qty = "";
        public string real_qty = "";
        public string short_qty = "";
        public string opr_time = "";
    }

    public class CJiaoJieDanData
    {
        public string barcd = "";
        public string barcd_add = "";
        public int quan = 0;
        public CJiaoJieDanData() { }
        public CJiaoJieDanData(string bar,string bar_add,int qu)
        {
            barcd = bar;
            barcd_add = bar_add;
            quan = qu;
        }
    }
    public class CJiaoJieDan
    {
        public string doc = "";
        public Dictionary<string, List<CJiaoJieDanData>> huData = new Dictionary<string, List<CJiaoJieDanData>>();
    }

    public class CJJBox
    {
        public string doc = "";
        public string user = "";
        public string devno = "";
        public string loucheng = "";
        public string hu = "";
        public string inventoryRe = "";
        public string inventoryMsg = "";
        public string sapRe = "";
        public string sapMsg = "";
        public List<string> epc = new List<string>();
        public List<TagDetailInfo> tags = new List<TagDetailInfo>();
    }
    //电商接口参数
    public class CPPInfo
    {
        public string Inerfae_key;
        public string Secret;
        public string Interface_url;
        public CPPInfo(string key,string url,string sec)
        {
            Interface_url = url;
            Inerfae_key = key;
            Secret = sec;
        }
    }

    public class CDianShangDoc
    {
        public string doc = "";
        public List<CBarQty> dsData = new List<CBarQty>();
    }
    public class CDianShangQty
    {
        public string mat="";
        public int curQty = 0;
        public int hasQty=0;
        public int allQty=0;
    }
    public class CDianShangBox
    {
        public string doc = "";
        public string hu = "";
        public List<TagDetailInfo> tags = new List<TagDetailInfo>();
        public List<string> epc = new List<string>();

        public List<CDianShangQty> qty = new List<CDianShangQty>();

        public string inventoryRe = "";
        public string inventoryMsg = "";
        //SUCCESS FAILURE
        public string sapRe = "";
        public string sapMsg = "";
    }

    public class CTagDetail
    {
        public string bar;
        public string zsatnr;
        public string zcolsn;
        public string zsiztx;
        public string charg;
        public int quan;
    }
    public class CTagSum
    {
        public string mat;
        public string bar;
        public string barAdd = "";
        public string zsatnr;
        public string zcolsn;
        public string zsiztx;
        public int qty;
        public int qty_add = 0;

        public CTagSum(string m,string b,string badd,string zsa,string zcol,string zsize,int q,int q_a)
        {
            mat = m;
            bar = b;
            barAdd = badd;
            zsatnr = zsa;
            zcolsn = zcol;
            zsiztx = zsize;
            qty = q;
            qty_add = q_a;
        }
        public CTagSum()
        {

        }
    }
    public class CTagSumDif
    {
        public string mat;
        public string bar;
        public string barAdd = "";
        public string zsatnr;
        public string zcolsn;
        public string zsiztx;
        public int qty;
        public int qty_diff = 0;
        public int qty_add = 0;
        public int qty_add_diff = 0;

        public CTagSumDif(string m, string b, string badd, string zsa, string zcol, string zsize, int q, int q_a,int q_d,int q_a_d)
        {
            mat = m;
            bar = b;
            barAdd = badd;
            zsatnr = zsa;
            zcolsn = zcol;
            zsiztx = zsize;
            qty = q;
            qty_add = q_a;
            qty_diff = q_d;
            qty_add_diff = q_a_d;
        }
    }


    public class CPKCheckUpload
    {
        public string mHu;
        public string IV_UNAME;

        public List<string> mEpcList = new List<string>();
        public List<CPKCheckUploadData> mBars = new List<CPKCheckUploadData>();
    }
    public class CPKCheckUploadData
    {
        public string MATNR;
        public string BARCD;
        public string QTY;
        public string ZDJQTY;
    }
    public class CPKCheckHuInfo
    {
        public string HU;
        public string F_PACK;
        public string F_CHECK;
        public string F_MXBL;
        public string F_MX;
    }

    public class CPKCheckHuDetailInfo
    {
        public string mHu;
        public string DIVERT_FLAG;
        public string DIVERT_FLAGCN;
        public string DEST_ID;
        public string ZE_LANE_ID;
        public string WAVEID;

        public List<CPKCheckHuDetailInfoData> mDetail = new List<CPKCheckHuDetailInfoData>();
    }
    public class CPKCheckHuDetailInfoData
    {
        public string MATNR;
        public string ZSATNR;
        public string ZCOLSN;
        public string ZSIZTX;
        public int QTY;
        public CPKCheckHuDetailInfoData()
        {

        }
        public CPKCheckHuDetailInfoData(string m,string zsat,string zcol,string zsize,int q)
        {
            MATNR = m;
            ZSATNR = zsat;
            ZCOLSN = zcol;
            ZSIZTX = zsize;
            QTY = q;
        }
    }


    public class CDianShangOutCheckUploadData
    {
        public string inventoryRe;
        public string sapRe;
    }

    public class CDianShangOutDocInfo
    {
        public string mDoc = "";
        public string mDocTime = "";
        public string WHAreaId = "";
        public string OrigBillId = "";
        public List<string> mHu = new List<string>();

        public List<CMatQty> mMatQtyList = new List<CMatQty>();

        public Dictionary<string, List<TagDetailInfo>> mHuDetail = new Dictionary<string, List<TagDetailInfo>>();

        public List<CMatQty> getLeftMatQty()
        {
            List<CMatQty> re = (List<CMatQty>)CHelp.Clone(mMatQtyList);

            foreach(var v in mHuDetail.Values)
            {
                foreach(var j in v)
                {
                    if(!j.IsAddEpc && re.Exists(k=>k.mat == j.MATNR))
                    {
                        re.First(k => k.mat == j.MATNR).qty -= 1;
                    }
                }
            }

            return re;
        }
    }
}
