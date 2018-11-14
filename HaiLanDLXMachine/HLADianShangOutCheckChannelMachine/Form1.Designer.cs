namespace HLADianShangOutCheckChannelMachine
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.button3_clearData = new System.Windows.Forms.Button();
            this.button4_cancel = new System.Windows.Forms.Button();
            this.button5_upload = new System.Windows.Forms.Button();
            this.button2_stop = new System.Windows.Forms.Button();
            this.button3_start = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17_currentHu = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11_status = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblInventoryRe = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPlc = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblReader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSATNR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZCOLSN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ZSIZTX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MSG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 30);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Teal;
            this.splitContainer1.Panel1.Controls.Add(this.button3_clearData);
            this.splitContainer1.Panel1.Controls.Add(this.button4_cancel);
            this.splitContainer1.Panel1.Controls.Add(this.button5_upload);
            this.splitContainer1.Panel1.Controls.Add(this.button2_stop);
            this.splitContainer1.Panel1.Controls.Add(this.button3_start);
            this.splitContainer1.Panel1.Controls.Add(this.label16);
            this.splitContainer1.Panel1.Controls.Add(this.label17_currentHu);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label11_status);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.lblInventoryRe);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.lblPlc);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.lblReader);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grid);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Size = new System.Drawing.Size(995, 728);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.TabIndex = 2;
            // 
            // button3_clearData
            // 
            this.button3_clearData.AutoEllipsis = true;
            this.button3_clearData.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3_clearData.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.button3_clearData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.button3_clearData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(239)))));
            this.button3_clearData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3_clearData.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.button3_clearData.ForeColor = System.Drawing.Color.Teal;
            this.button3_clearData.Location = new System.Drawing.Point(64, 524);
            this.button3_clearData.Name = "button3_clearData";
            this.button3_clearData.Size = new System.Drawing.Size(150, 40);
            this.button3_clearData.TabIndex = 46;
            this.button3_clearData.Text = "清除数据";
            this.button3_clearData.UseVisualStyleBackColor = false;
            this.button3_clearData.Click += new System.EventHandler(this.button3_clearData_Click);
            // 
            // button4_cancel
            // 
            this.button4_cancel.AutoEllipsis = true;
            this.button4_cancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button4_cancel.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.button4_cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.button4_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(239)))));
            this.button4_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4_cancel.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.button4_cancel.ForeColor = System.Drawing.Color.Teal;
            this.button4_cancel.Location = new System.Drawing.Point(64, 591);
            this.button4_cancel.Name = "button4_cancel";
            this.button4_cancel.Size = new System.Drawing.Size(150, 40);
            this.button4_cancel.TabIndex = 32;
            this.button4_cancel.Text = "退出";
            this.button4_cancel.UseVisualStyleBackColor = false;
            this.button4_cancel.Click += new System.EventHandler(this.button4_cancel_Click);
            // 
            // button5_upload
            // 
            this.button5_upload.AutoEllipsis = true;
            this.button5_upload.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button5_upload.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.button5_upload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.button5_upload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(239)))));
            this.button5_upload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5_upload.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.button5_upload.ForeColor = System.Drawing.Color.Teal;
            this.button5_upload.Location = new System.Drawing.Point(64, 462);
            this.button5_upload.Name = "button5_upload";
            this.button5_upload.Size = new System.Drawing.Size(150, 40);
            this.button5_upload.TabIndex = 31;
            this.button5_upload.Text = "上传列表";
            this.button5_upload.UseVisualStyleBackColor = false;
            this.button5_upload.Visible = false;
            this.button5_upload.Click += new System.EventHandler(this.button5_upload_Click);
            // 
            // button2_stop
            // 
            this.button2_stop.AutoEllipsis = true;
            this.button2_stop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button2_stop.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.button2_stop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.button2_stop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(239)))));
            this.button2_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2_stop.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.button2_stop.ForeColor = System.Drawing.Color.Teal;
            this.button2_stop.Location = new System.Drawing.Point(64, 399);
            this.button2_stop.Name = "button2_stop";
            this.button2_stop.Size = new System.Drawing.Size(150, 40);
            this.button2_stop.TabIndex = 30;
            this.button2_stop.Text = "暂停";
            this.button2_stop.UseVisualStyleBackColor = false;
            this.button2_stop.Click += new System.EventHandler(this.button2_stop_Click);
            // 
            // button3_start
            // 
            this.button3_start.AutoEllipsis = true;
            this.button3_start.BackColor = System.Drawing.Color.WhiteSmoke;
            this.button3_start.FlatAppearance.BorderColor = System.Drawing.Color.SkyBlue;
            this.button3_start.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(161)))), ((int)(((byte)(222)))));
            this.button3_start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(189)))), ((int)(((byte)(239)))));
            this.button3_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3_start.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.button3_start.ForeColor = System.Drawing.Color.Teal;
            this.button3_start.Location = new System.Drawing.Point(64, 337);
            this.button3_start.Name = "button3_start";
            this.button3_start.Size = new System.Drawing.Size(150, 40);
            this.button3_start.TabIndex = 29;
            this.button3_start.Text = "开始";
            this.button3_start.UseVisualStyleBackColor = false;
            this.button3_start.Click += new System.EventHandler(this.button3_start_Click);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(4, 215);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(100, 32);
            this.label16.TabIndex = 25;
            this.label16.Text = "当前箱号：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17_currentHu
            // 
            this.label17_currentHu.BackColor = System.Drawing.Color.White;
            this.label17_currentHu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17_currentHu.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label17_currentHu.Location = new System.Drawing.Point(107, 215);
            this.label17_currentHu.Name = "label17_currentHu";
            this.label17_currentHu.Size = new System.Drawing.Size(160, 32);
            this.label17_currentHu.TabIndex = 26;
            this.label17_currentHu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(4, 134);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 32);
            this.label10.TabIndex = 19;
            this.label10.Text = "工作状态：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11_status
            // 
            this.label11_status.BackColor = System.Drawing.Color.White;
            this.label11_status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11_status.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label11_status.Location = new System.Drawing.Point(107, 134);
            this.label11_status.Name = "label11_status";
            this.label11_status.Size = new System.Drawing.Size(160, 32);
            this.label11_status.TabIndex = 20;
            this.label11_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(4, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 32);
            this.label6.TabIndex = 7;
            this.label6.Text = "扫描结果：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInventoryRe
            // 
            this.lblInventoryRe.BackColor = System.Drawing.Color.White;
            this.lblInventoryRe.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInventoryRe.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblInventoryRe.Location = new System.Drawing.Point(107, 175);
            this.lblInventoryRe.Name = "lblInventoryRe";
            this.lblInventoryRe.Size = new System.Drawing.Size(160, 32);
            this.lblInventoryRe.TabIndex = 9;
            this.lblInventoryRe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(4, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 32);
            this.label5.TabIndex = 13;
            this.label5.Text = "PLC状态：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPlc
            // 
            this.lblPlc.BackColor = System.Drawing.Color.White;
            this.lblPlc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPlc.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblPlc.Location = new System.Drawing.Point(107, 90);
            this.lblPlc.Name = "lblPlc";
            this.lblPlc.Size = new System.Drawing.Size(160, 32);
            this.lblPlc.TabIndex = 14;
            this.lblPlc.Text = "连接中...";
            this.lblPlc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(4, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 32);
            this.label7.TabIndex = 15;
            this.label7.Text = "读写器：";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReader
            // 
            this.lblReader.BackColor = System.Drawing.Color.White;
            this.lblReader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReader.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblReader.Location = new System.Drawing.Point(107, 48);
            this.lblReader.Name = "lblReader";
            this.lblReader.Size = new System.Drawing.Size(160, 32);
            this.lblReader.TabIndex = 16;
            this.lblReader.Text = "连接中...";
            this.lblReader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.Teal;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "状态监控";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
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
            this.HU,
            this.Column3,
            this.ZSATNR,
            this.ZCOLSN,
            this.ZSIZTX,
            this.QTY,
            this.Column1,
            this.Column2,
            this.Column4,
            this.MSG});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle3;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.GridColor = System.Drawing.Color.DarkGray;
            this.grid.Location = new System.Drawing.Point(0, 40);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.grid.RowHeadersVisible = false;
            this.grid.RowHeadersWidth = 43;
            this.grid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.grid.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.RowTemplate.Height = 43;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(717, 688);
            this.grid.TabIndex = 19;
            // 
            // HU
            // 
            this.HU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.HU.FillWeight = 13F;
            this.HU.HeaderText = "箱号";
            this.HU.Name = "HU";
            this.HU.ReadOnly = true;
            this.HU.Width = 70;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column3.HeaderText = "拣货单号";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 88;
            // 
            // ZSATNR
            // 
            this.ZSATNR.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZSATNR.FillWeight = 15F;
            this.ZSATNR.HeaderText = "品号";
            this.ZSATNR.Name = "ZSATNR";
            this.ZSATNR.ReadOnly = true;
            this.ZSATNR.Width = 70;
            // 
            // ZCOLSN
            // 
            this.ZCOLSN.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZCOLSN.FillWeight = 7F;
            this.ZCOLSN.HeaderText = "色号";
            this.ZCOLSN.Name = "ZCOLSN";
            this.ZCOLSN.ReadOnly = true;
            this.ZCOLSN.Width = 70;
            // 
            // ZSIZTX
            // 
            this.ZSIZTX.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ZSIZTX.FillWeight = 13F;
            this.ZSIZTX.HeaderText = "规格";
            this.ZSIZTX.Name = "ZSIZTX";
            this.ZSIZTX.ReadOnly = true;
            this.ZSIZTX.Width = 70;
            // 
            // QTY
            // 
            this.QTY.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.QTY.FillWeight = 7F;
            this.QTY.HeaderText = "主实发";
            this.QTY.Name = "QTY";
            this.QTY.ReadOnly = true;
            this.QTY.Width = 88;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column1.HeaderText = "主差异";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 88;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column2.HeaderText = "辅实发";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 88;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Column4.HeaderText = "辅差异";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 88;
            // 
            // MSG
            // 
            this.MSG.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MSG.DefaultCellStyle = dataGridViewCellStyle2;
            this.MSG.FillWeight = 38F;
            this.MSG.HeaderText = "状态";
            this.MSG.Name = "MSG";
            this.MSG.ReadOnly = true;
            this.MSG.Width = 70;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Teal;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(717, 40);
            this.label2.TabIndex = 1;
            this.label2.Text = "复核明细";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 788);
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblInventoryRe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPlc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblReader;
        private System.Windows.Forms.Label label1;
        private DMSkin.Metro.Controls.MetroGrid grid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17_currentHu;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11_status;
        private System.Windows.Forms.Button button4_cancel;
        private System.Windows.Forms.Button button5_upload;
        private System.Windows.Forms.Button button2_stop;
        private System.Windows.Forms.Button button3_start;
        private System.Windows.Forms.Button button3_clearData;
        private System.Windows.Forms.DataGridViewTextBoxColumn HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSATNR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZCOLSN;
        private System.Windows.Forms.DataGridViewTextBoxColumn ZSIZTX;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn MSG;
    }
}

