﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HLAWebService.Model
{
    public class DeliverEpcDetailInfo
    {
        public string LGNUM { get; set; }
        public DateTime SHIP_DATE { get; set; }
        public string LOUCENG { get; set; }
        public string PARTNER { get; set; }
        public string HU { get; set; }
        public string EPC_SER { get; set; }
        public string Result { get; set; }
        public string BOXGUID { get; set; }
    }
}