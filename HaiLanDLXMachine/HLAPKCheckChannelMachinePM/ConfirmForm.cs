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
    public partial class ConfirmForm : MetroForm
    {
        CPKCheckHuDetailInfo mBox;
        List<TagDetailInfo> mTags;
        public ConfirmForm(CPKCheckHuDetailInfo boxD,List<TagDetailInfo> t)
        {
            mBox = boxD;
            mTags = t;

            DialogResult = DialogResult.Cancel;
            InitializeComponent();

        }

        private void dmButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button_sure_Click(object sender, EventArgs e)
        {
            bool checkRe = true;

#if DEBUG
#else
            //检验密码
            string sapRe = "";
            string sapMsg = "";
            SAPDataService.pkCheckPw(textBox1_zhanghao.Text.Trim(), textBox1_mima.Text.Trim(), out sapRe, out sapMsg);
            if (sapRe == "S")
                checkRe = true;
            else
            {
                MetroMessageBox.Show(this, sapMsg, "账号或密码错误");
                return;
            }
#endif
            if (checkRe)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ConfirmForm_Load(object sender, EventArgs e)
        {
            try
            {
                grid.Rows.Clear();
                foreach (CPKCheckHuDetailInfoData di in mBox.mDetail)
                {
                    grid.Rows.Insert(0, di.ZSATNR, di.ZCOLSN, di.ZSIZTX, di.QTY);
                    grid.Rows[0].Tag = di.MATNR;
                }

                foreach (DataGridViewRow v in grid.Rows)
                {
                    v.Cells["real_count"].Value = mTags.Count(i => !i.IsAddEpc && i.MATNR == v.Tag.ToString());

                    int djCount = int.Parse(v.Cells["should_count"].Value.ToString()) - int.Parse(v.Cells["real_count"].Value.ToString());
                    v.Cells["dj_count"].Value = djCount.ToString();

                    if (djCount > 0)
                    {
                        v.DefaultCellStyle.BackColor = Color.OrangeRed;
                    }
                    else
                    {
                        v.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    }
                }
            }
            catch(Exception)
            {

            }
        }
    }
}
