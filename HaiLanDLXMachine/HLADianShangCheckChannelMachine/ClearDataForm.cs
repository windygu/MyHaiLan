﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HLACommonLib;

namespace HLAJiaoJieCheckChannelMachine
{
    public partial class ClearDataForm : Form
    {
        Form1 mParent;
        public ClearDataForm(Form1 pa)
        {
            InitializeComponent();
            mParent = pa;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_clearHu_Click(object sender, EventArgs e)
        {
            string hu = textBox1_hu.Text.Trim();
            if(string.IsNullOrEmpty(hu))
            {
                MessageBox.Show("请输入箱号");
                return;
            }

            mParent.clearHu(hu);
            string msg = "";
            SAPDataService.dianShangCGTDelHu(hu, out msg);
        }

        private void button1_clearDoc_Click(object sender, EventArgs e)
        {
            mParent.clearDoc(textBox2_doc.Text.Trim());
        }
    }
}
