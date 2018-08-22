using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMSkin;
using HLACommonLib.Model;
using HLACommonLib;

namespace HLAPKCheckChannelMachinePM
{
    public partial class HuInfoForm : MetroForm
    {
        CPKCheckHuInfo mHu;
        InventoryForm mParent = null;
        string mSapRe;
        public HuInfoForm(InventoryForm p, CPKCheckHuInfo hu,string sapRe)
        {
            mParent = p;
            mHu = hu;
            mSapRe = sapRe;
            DialogResult = DialogResult.Cancel;

            InitializeComponent();
        }

        private void button_fuhe_Click(object sender, EventArgs e)
        {
            //Z_EW_RF_145
            string sapRe = "";
            string sapMsg = "";
            CPKCheckHuDetailInfo boxDetail = SAPDataService.getPKCheckHuDetailInfo(mHu.HU, out sapRe, out sapMsg);

            if(sapRe == "S")
            {
                mParent.setCurBoxDetail(boxDetail);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(sapMsg);
            }
        }

        private void dmButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HuInfoForm_Load(object sender, EventArgs e)
        {
            lblBaoZhang.Text = mHu.F_PACK == "Y" ? "已包装" : "未包装";
            lblFuhe.Text = getfuhe(mHu.F_CHECK);
            lblmanxiang.Text = mHu.F_MXBL;
            lblshifou.Text = mHu.F_MX == "X" ? "满箱" : "尾箱";

            button_fuhe.Enabled = mSapRe == "S" && mHu.F_CHECK == "1";
        }

        string getfuhe(string fuhe)
        {
            if (fuhe == "1")
                return "有差异";
            if (fuhe == "2")
                return "无差异";
            if (fuhe == "3")
                return "箱号重复";

            return "";
        }
    }
}
