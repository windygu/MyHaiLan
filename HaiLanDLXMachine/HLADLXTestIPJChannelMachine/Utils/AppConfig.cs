﻿using System;
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
            SysConfig.loadConfig();

        }

    }
}
