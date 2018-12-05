using HLACommonLib;
using HLACommonView.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OSharp.Utility.Extensions;

namespace HLAWriteTagChannelMachine.Utils
{
    public class AppConfig
    {
        public static void Load()
        {
            SysConfig.DelayTime = ConfigurationManager.AppSettings["DelayTime"].CastTo<int>(700);
            SysConfig.Port = ConfigurationManager.AppSettings["Port"].CastTo("COM1");
            SysConfig.ScannerPort_1 = ConfigurationManager.AppSettings["ScannerPort_1"].CastTo("COM5");
            SysConfig.ScannerPort_2 = ConfigurationManager.AppSettings["ScannerPort_2"].CastTo("COM6");
        }
    }
}
