using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using HLACommon;

namespace HLAChannel
{ 
    public class AppConfig
    {
        public static void Load()
        {
            CConfig.mReaderType = (READER_TYPE)int.Parse(ConfigurationManager.AppSettings["ReaderType"]);
            CConfig.mPLCType = (PLC_TYPE)int.Parse(ConfigurationManager.AppSettings["PLCType"]);

            CConfig.mDBUrl = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
            CConfig.mLGNUM = ConfigurationManager.AppSettings["LGNUM"];
            CConfig.mDeviceNO = ConfigurationManager.AppSettings["DeviceNO"];

            CConfig.mDelayTime = int.Parse(ConfigurationManager.AppSettings["DelayTime"]);
            CConfig.mPLCComPort = ConfigurationManager.AppSettings["PLCPort"];
            CConfig.mScannerPort_1 = ConfigurationManager.AppSettings["ScannerPort_1"];
            CConfig.mScannerPort_2 = ConfigurationManager.AppSettings["ScannerPort_2"];

            CConfig.mAppServerHost = ConfigurationManager.AppSettings["AppServerHost"];
            CConfig.mSystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
            CConfig.mUser = ConfigurationManager.AppSettings["User"];
            CConfig.mPassword = ConfigurationManager.AppSettings["Password"];
            CConfig.mClient = ConfigurationManager.AppSettings["Client"];
            CConfig.mLanguage = ConfigurationManager.AppSettings["Language"];
            CConfig.mPoolSize = ConfigurationManager.AppSettings["PoolSize"];
            CConfig.mPeakConnectionsLimit = ConfigurationManager.AppSettings["PeakConnectionsLimit"];
            CConfig.mIdleTimeout = ConfigurationManager.AppSettings["IdleTimeout"];

            CConfig.mSearchMode = string.IsNullOrEmpty(ConfigurationManager.AppSettings["SearchMode"]) ? 1 : int.Parse(ConfigurationManager.AppSettings["SearchMode"]);
            CConfig.mSession = string.IsNullOrEmpty(ConfigurationManager.AppSettings["Session"]) ? (ushort)2 : ushort.Parse(ConfigurationManager.AppSettings["Session"]);

            if(CConfig.mReaderType == READER_TYPE.READER_IMP)
            {
                CConfig.mReaderIP = ConfigurationManager.AppSettings["ReaderIP_IMP"];
                CConfig.mReaderPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_IMP"]);
            }
            if (CConfig.mReaderType == READER_TYPE.READER_TM)
            {
                CConfig.mReaderIP = ConfigurationManager.AppSettings["ReaderIP_TM"];
                CConfig.mReaderPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_TM"]);
            }
            if (CConfig.mReaderType == READER_TYPE.READER_DLX_PM)
            {
                CConfig.mReaderPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_DLX_PM"]);
                CConfig.mReaderComPort = ConfigurationManager.AppSettings["ReaderCom_DLX_PM"];
            }
            if (CConfig.mReaderType == READER_TYPE.READER_XD_PM)
            {
                CConfig.mReaderIP = ConfigurationManager.AppSettings["ReaderIP_XD_PM"];
                CConfig.mReaderPower = double.Parse(ConfigurationManager.AppSettings["ReaderPower_XD_PM"]);
            }

        }
    }
}
