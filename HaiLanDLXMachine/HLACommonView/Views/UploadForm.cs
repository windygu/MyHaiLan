using DMSkin;
using HLACommonLib;
using HLACommonLib.Model;
using HLACommonLib.Model.PK;
using HLACommonView.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HLACommonView.Views
{
    public partial class UploadForm<T> : MetroForm
    {
        UploadMsgFormMethod mUploadMethod = null;
        ProcessDialog pd = new ProcessDialog();

        bool mSelAll = false;
        public UploadForm(UploadMsgFormMethod p)
        {
            mUploadMethod = p;
            InitializeComponent();
        }
        
        private void initData()
        {
            Invoke(new Action(() => {
                grid.Rows.Clear();

                List<CCmnUploadData> list = CSqliteDataService.GetAllUploadFromSqlite<T>();
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if (item != null)
                        {
                            grid.Rows.Insert(0, false, item.HU, item.IsUpload == 0 ? "未上传" : "已经上传", item.MSG);
                            grid.Rows[0].Tag = item;
                        }
                    }
                }
            }));
        }
        private void UploadMgForm_Load(object sender, EventArgs e)
        {
            initData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = GetCheckedRows();
            if (rows != null && rows.Count > 0)
            {
                if (MetroMessageBox.Show(this, "确认要清除记录吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        CCmnUploadData box = row.Tag as CCmnUploadData;
                        CSqliteDataService.delUploadFromSqlite(box.Guid);
                    }
                }
            }
            if (rows != null && rows.Count > 0)
            {
                MetroMessageBox.Show(this, "成功清除", "提示");
                initData();
            }
        }

        private void grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex>=0 && e.RowIndex<grid.Rows.Count)
            {
                grid.Rows[e.RowIndex].Cells[0].Value = !(Boolean)(grid.Rows[e.RowIndex].Cells[0].Value);
            }
        }
        public virtual void ShowLoading(string message)
        {
            Invoke(new Action(() => {
                pd.Show();
                metroPanel1.Show();
                lblText.Text = message;
            }));

        }

        public virtual void HideLoading()
        {
            Invoke(new Action(() => {
                pd.Hide();
                metroPanel1.Hide();
                lblText.Text = "";
            }));
        }
        private void btnReupload_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rows = GetCheckedRows();
            if (rows!=null && rows.Count>0)
            {
                Thread t = new Thread(new ThreadStart(() =>
                {
                    ShowLoading("正在上传...");
                    foreach (DataGridViewRow row in rows)
                    {
                        CCmnUploadData box = row.Tag as CCmnUploadData;
                        CSqliteDataService.delUploadFromSqlite(box.Guid);
                        mUploadMethod.Upload(box);
                    }
                    initData();
                    HideLoading();
                }));
                t.IsBackground = true;
                t.Start();

            }

        }

        private List<DataGridViewRow> GetCheckedRows()
        {
            List<DataGridViewRow> result = new List<DataGridViewRow>(); 
            if (grid.Rows.Count>0)
            {
                foreach(DataGridViewRow row in grid.Rows)
                {
                    if((Boolean)row.Cells[0].Value)
                    {
                        result.Add(row);
                    }
                }
            }
            return result;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            mSelAll = !mSelAll;
            grid.Focus();

            if(mSelAll)
            {
                grid.SelectAll();
            }
            else
            {
                grid.ClearSelection();
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                row.Cells[0].Value = mSelAll;
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            initData();
        }

        private void metroButton3_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public interface UploadMsgFormMethod
    {
        void Upload(CCmnUploadData ud);
    }
}
