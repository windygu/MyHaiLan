using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonView.Views;
using HLACommonView.Model;
using System.Threading;
using HLACommonLib;
using System.Configuration;
using Stimulsoft.Report;
using System.Drawing.Printing;
using HLACommonLib.DAO;
using HLACommonLib.Model;
using Newtonsoft.Json;

namespace HLAZZZTest
{
    public partial class Form1 : CommonInventoryFormIMP
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //LoadConfig();
            //SAPDataService.Init();
        }

        public void LoadConfig()
        {
            SysConfig.DBUrl = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;

            //SAP相关配置
            SysConfig.AppServerHost = ConfigurationManager.AppSettings["AppServerHost"];
            SysConfig.SystemNumber = ConfigurationManager.AppSettings["SystemNumber"];
            SysConfig.User = ConfigurationManager.AppSettings["User"];
            SysConfig.Password = ConfigurationManager.AppSettings["Password"];
            SysConfig.Client = ConfigurationManager.AppSettings["Client"];
            SysConfig.Language = ConfigurationManager.AppSettings["Language"];
            SysConfig.PoolSize = ConfigurationManager.AppSettings["PoolSize"];
            SysConfig.PeakConnectionsLimit = ConfigurationManager.AppSettings["PeakConnectionsLimit"];
            SysConfig.IdleTimeout = ConfigurationManager.AppSettings["IdleTimeout"];


        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            int i = 9;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int i = 9;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            playSound(true);
        }
        void playSound(bool re)
        {
            try
            {

                {
                    AudioHelper.Play(".\\uploadError.wav");
                }
            }
            catch (Exception)
            { }
        }

    }
}
