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
    public class SysConfig
    {
        //仓库编号
        public static string LGNUM = "HL01";
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
        public static ReaderConfig ReaderConfig = new ReaderConfig();

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

        public static void loadConfig()
        {
            SysConfig.ReaderIp = ConfigurationManager.AppSettings["ReaderIp"];
            SysConfig.ReaderConfig.SearchMode = int.Parse(ConfigurationManager.AppSettings["SearchMode"]);
            SysConfig.ReaderConfig.Session = ushort.Parse(ConfigurationManager.AppSettings["Session"]);
            SysConfig.ReaderConfig.UseAntenna1 = int.Parse(ConfigurationManager.AppSettings["UseAntenna1"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna2 = int.Parse(ConfigurationManager.AppSettings["UseAntenna2"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna3 = int.Parse(ConfigurationManager.AppSettings["UseAntenna3"]) == 0 ? false : true;
            SysConfig.ReaderConfig.UseAntenna4 = int.Parse(ConfigurationManager.AppSettings["UseAntenna4"]) == 0 ? false : true;
            SysConfig.ReaderConfig.AntennaPower1 = double.Parse(ConfigurationManager.AppSettings["AntennaPower1"]);
            SysConfig.ReaderConfig.AntennaPower2 = double.Parse(ConfigurationManager.AppSettings["AntennaPower2"]);
            SysConfig.ReaderConfig.AntennaPower3 = double.Parse(ConfigurationManager.AppSettings["AntennaPower3"]);
            SysConfig.ReaderConfig.AntennaPower4 = double.Parse(ConfigurationManager.AppSettings["AntennaPower4"]);

            SysConfig.DelayTime = ConfigurationManager.AppSettings["DelayTime"] == null ? 700 : int.Parse(ConfigurationManager.AppSettings["DelayTime"]);

            //通道机硬件设备相关配置
            SysConfig.Port = ConfigurationManager.AppSettings["Port"];
            SysConfig.ScannerPort_1 = ConfigurationManager.AppSettings["ScannerPort_1"];
            SysConfig.ScannerPort_2 = ConfigurationManager.AppSettings["ScannerPort_2"];
        }

    }
    public class ReaderConfig
    {
        public int SearchMode { get; set; }
        public ushort Session { get; set; }

        public bool UseAntenna1 { get; set; }
        public bool UseAntenna2 { get; set; }
        public bool UseAntenna3 { get; set; }
        public bool UseAntenna4 { get; set; }
        public bool UseAntenna5 { get; set; }
        public bool UseAntenna6 { get; set; }
        public bool UseAntenna7 { get; set; }
        public bool UseAntenna8 { get; set; }

        public double AntennaPower1 { get; set; }
        public double AntennaPower2 { get; set; }
        public double AntennaPower3 { get; set; }
        public double AntennaPower4 { get; set; }
        public double AntennaPower5 { get; set; }
        public double AntennaPower6 { get; set; }
        public double AntennaPower7 { get; set; }
        public double AntennaPower8 { get; set; }
    }
}
