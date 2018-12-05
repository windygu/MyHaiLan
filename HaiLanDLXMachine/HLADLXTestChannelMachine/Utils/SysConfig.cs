using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Net;

namespace HLABoxCheckChannelMachine.Utils
{
    public enum RunMode
    {
        平库 = 1,
        高位库 = 2,
        电商库 = 3
    }
    public enum ERRORDATATYPE
    {
        读写器未连接 = 1,
        未扫描到箱号,
        正常
    }
    public class CReadParam
    {
        public static int ghost = 3;
        public static int trigger = 280;
        public static int r6ghost = 3;

    }
    public class SysConfig
    {
        //仓库编号
        public static string LGNUM = "HL01";
        //软件版本号
        public static string Version = Application.ProductVersion;
        //RSSI限制值
        public static double RssiLimit = 0;
        //延迟时间设置
        public static int DelayTime = 0;
        //延迟时间设置2
        public static int DelayTime2 = 1000;
        //串口地址
        public static string Port = "COM1";
        //条码扫描枪串口地址
        public static string ScannerPort_1 = "COM4";
        public static string ScannerPort_2 = "COM5";
        //读写器IP
        public static string ReaderIp = "192.168.8.166:8086";

        public static string SearchMode = "1";
        public static string Session = "2";

        public static int mReaderPower = 2700;
        //读写器参数
        /// <summary>
        /// 所在楼层对应的设备终端编码
        /// </summary>
        /// <summary>
        /// 当前运行模式 默认平库
        /// </summary>
        public static RunMode RunningModel = RunMode.平库;

        //当前登录用户

        //当前连接的数据库的IP
        public static string DBUrl = "localhost";

        //当前运行的设备类型
        //public static string Device = "TDJ-ZZ-1";

        /// <summary>
        /// 设备编号
        /// </summary>
        public static string DeviceNO = "10E901";

        /// <summary>
        /// 是否支持hub
        /// </summary>
        public static int IsHub = 0;

        /// <summary>
        /// 设备基础信息
        /// </summary>
        /// <summary>
        /// 下架单类型(箱装或挂装)
        /// </summary>
        public static string sKF_LX = null;
        /// <summary>
        /// 是否正式环境
        /// </summary>
        public static bool UseFormal = true;
        //SAP相关配置
        public static string AppServerHost = "172.18.200.14";
        public static string SystemNumber = "00";
        public static string User = "093482";
        public static string Password = "sunrain";
        public static string Client = "300";
        public static string Language = "ZH";
        public static string PoolSize = "5";
        public static string PeakConnectionsLimit = "10";
        public static string IdleTimeout = "60";

        public static string UseGroupLogon = "1";
        public static string LogonGroup = "EWM_PRD";
        public static string SystemID = "EWP";
        public static string MessageServerHost = "172.18.200.81";
        public static string MessageServerService = "3600";
        //打印相关配置
        public static string PrinterName = "ZDesigner GX430t";
        public static string RightPrintTemplate = "";
        public static string ErrorPrintTemplate = "";

        public static bool IsTest = false;
        public static bool IsUseTestSAP = false;
        public static bool IsLog = false;

        /// <summary>
        /// 单位中英对照表
        /// </summary>
        public static Dictionary<string, string> UOMDic = new Dictionary<string, string>();

        #region 挂装机收货相关代码
        public static int TrackNum = 2;
        public static int EachTrackNum = 10;
        #endregion
        public static int BoxQty = 0;

        public static string ReaderComPort = "COM3";
        public static string ReaderPower = "5";
        public static string ScanComPort = "COM4";

        public static string HttpKey = "";
        public static string HttpUrl = "";
        public static string HttpSec = "";
        /// <summary>
        /// 初始化单位中英对照表
        /// </summary>

        /// <summary>
        /// 设置配置文件的值
        /// </summary>
        /// <param name="AppKey"></param>
        /// <param name="AppValue"></param>

        /// <summary>
        /// 获取当前局域网IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            string sClientIp = ConfigurationManager.AppSettings["ReaderIp"].ToString();

            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    string sAddressIP = _IPAddress.ToString();//当前IP

                    if (!string.IsNullOrEmpty(sClientIp))
                    {
                        string[] IPstring = sClientIp.Split('.');

                        if (sAddressIP.Contains(IPstring[0] + "." + IPstring[1] + "." + IPstring[2]))
                        {
                            continue;
                        }
                    }
                    AddressIP = sAddressIP;
                    break;
                }
            }
            return AddressIP;
        }
    }

    public class YZConfig
    {
        public static string mIp;
        public static double mPower;
        public static string mCom;

        public static void loadConfig()
        {

        }
    }
}
