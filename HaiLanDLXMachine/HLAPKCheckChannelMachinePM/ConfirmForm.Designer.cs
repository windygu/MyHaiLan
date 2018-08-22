namespace HLAPKCheckChannelMachinePM
{
    partial class ConfirmForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.MAINBAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADDBAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.should_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.real_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dj_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dmButton1 = new DMSkin.Controls.DMButton();
            this.button_sure = new DMSkin.Controls.DMButton();
            this.textBox1_mima = new System.Windows.Forms.TextBox();
            this.textBox1_zhanghao = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grid.ColumnHeadersHeight = 65;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MAINBAR,
            this.ADDBAR,
            this.QTY,
            this.should_count,
            this.real_count,
            this.dj_count});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.GridColor = System.Drawing.Color.DarkGray;
            this.grid.Location = new System.Drawing.Point(17, 13);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 43;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.grid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(578, 276);
            this.grid.TabIndex = 20;
            // 
            // MAINBAR
            // 
            this.MAINBAR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.MAINBAR.FillWeight = 15F;
            this.MAINBAR.HeaderText = "品号";
            this.MAINBAR.Name = "MAINBAR";
            this.MAINBAR.ReadOnly = true;
            this.MAINBAR.Width = 70;
            // 
            // ADDBAR
            // 
            this.ADDBAR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ADDBAR.FillWeight = 7F;
            this.ADDBAR.HeaderText = "色号";
            this.ADDBAR.Name = "ADDBAR";
            this.ADDBAR.ReadOnly = true;
            this.ADDBAR.Width = 70;
            // 
            // QTY
            // 
            this.QTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.QTY.FillWeight = 13F;
            this.QTY.HeaderText = "规格";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 70;
            // 
            // should_count
            // 
            this.should_count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.should_count.FillWeight = 7F;
            this.should_count.HeaderText = "发货数量";
            this.should_count.Name = "should_count";
            this.should_count.ReadOnly = true;
            this.should_count.Width = 88;
            // 
            // real_count
            // 
            this.real_count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.real_count.HeaderText = "复核数量";
            this.real_count.Name = "real_count";
            this.real_count.ReadOnly = true;
            this.real_count.Width = 88;
            // 
            // dj_count
            // 
            this.dj_count.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dj_count.FillWeight = 13F;
            this.dj_count.HeaderText = "短捡数量";
            this.dj_count.Name = "dj_count";
            this.dj_count.ReadOnly = true;
            this.dj_count.Width = 88;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Teal;
            this.panel1.Controls.Add(this.dmButton1);
            this.panel1.Controls.Add(this.button_sure);
            this.panel1.Controls.Add(this.textBox1_mima);
            this.panel1.Controls.Add(this.textBox1_zhanghao);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.grid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 411);
            this.panel1.TabIndex = 21;
            // 
            // dmButton1
            // 
            this.dmButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.dmButton1.AutoEllipsis = true;
            this.dmButton1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton1.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton1.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton1.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton1.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1.DM_Radius = 1;
            this.dmButton1.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.dmButton1.ForeColor = System.Drawing.Color.Teal;
            this.dmButton1.Image = null;
            this.dmButton1.Location = new System.Drawing.Point(475, 356);
            this.dmButton1.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton1.Name = "dmButton1";
            this.dmButton1.Size = new System.Drawing.Size(120, 40);
            this.dmButton1.TabIndex = 60;
            this.dmButton1.Text = "关闭";
            this.dmButton1.UseVisualStyleBackColor = false;
            this.dmButton1.Click += new System.EventHandler(this.dmButton1_Click);
            // 
            // button_sure
            // 
            this.button_sure.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.button_sure.AutoEllipsis = true;
            this.button_sure.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button_sure.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_sure.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.button_sure.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.button_sure.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.button_sure.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.button_sure.DM_Radius = 1;
            this.button_sure.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.button_sure.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_sure.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.button_sure.ForeColor = System.Drawing.Color.Teal;
            this.button_sure.Image = null;
            this.button_sure.Location = new System.Drawing.Point(319, 356);
            this.button_sure.Margin = new System.Windows.Forms.Padding(0);
            this.button_sure.Name = "button_sure";
            this.button_sure.Size = new System.Drawing.Size(120, 40);
            this.button_sure.TabIndex = 59;
            this.button_sure.Text = "确认";
            this.button_sure.UseVisualStyleBackColor = false;
            this.button_sure.Click += new System.EventHandler(this.button_sure_Click);
            // 
            // textBox1_mima
            // 
            this.textBox1_mima.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1_mima.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox1_mima.Location = new System.Drawing.Point(425, 308);
            this.textBox1_mima.Name = "textBox1_mima";
            this.textBox1_mima.PasswordChar = '*';
            this.textBox1_mima.Size = new System.Drawing.Size(170, 29);
            this.textBox1_mima.TabIndex = 58;
            // 
            // textBox1_zhanghao
            // 
            this.textBox1_zhanghao.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.textBox1_zhanghao.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox1_zhanghao.Location = new System.Drawing.Point(123, 309);
            this.textBox1_zhanghao.Name = "textBox1_zhanghao";
            this.textBox1_zhanghao.Size = new System.Drawing.Size(170, 29);
            this.textBox1_zhanghao.TabIndex = 57;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(319, 307);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 32);
            this.label6.TabIndex = 27;
            this.label6.Text = "主管密码：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(17, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 32);
            this.label4.TabIndex = 29;
            this.label4.Text = "主管账号：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConfirmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 491);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfirmForm";
            this.Text = "短捡确认";
            this.Load += new System.EventHandler(this.ConfirmForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DMSkin.Metro.Controls.MetroGrid grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAINBAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDBAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn should_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn real_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn dj_count;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1_mima;
        private System.Windows.Forms.TextBox textBox1_zhanghao;
        private DMSkin.Controls.DMButton dmButton1;
        private DMSkin.Controls.DMButton button_sure;
    }
}