using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;


namespace HLABoxCheckChannelMachine.Utils
{
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.ReaderIp = ConfigurationManager.AppSettings["ReaderIp"];
            SysConfig.mReaderPower = int.Parse(ConfigurationManager.AppSettings["ReaderPower"]);

            SysConfig.DelayTime = ConfigurationManager.AppSettings["DelayTime"] == null ? 700 : int.Parse(ConfigurationManager.AppSettings["DelayTime"]);
            SysConfig.Port = ConfigurationManager.AppSettings["Port"];
            SysConfig.ScannerPort_1 = ConfigurationManager.AppSettings["ScannerPort_1"];
            SysConfig.ScannerPort_2 = ConfigurationManager.AppSettings["ScannerPort_2"];

        }

    }
}
