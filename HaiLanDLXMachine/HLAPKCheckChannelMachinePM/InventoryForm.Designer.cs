﻿namespace HLAPKCheckChannelMachinePM
{
    partial class InventoryForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dmButton1_save = new DMSkin.Controls.DMButton();
            this.dmButton1_reset = new DMSkin.Controls.DMButton();
            this.label9_dema = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9_fenjianboci = new System.Windows.Forms.Label();
            this.textBox1_bar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1_boxno = new System.Windows.Forms.TextBox();
            this.dmButton1_exception_query = new DMSkin.Controls.DMButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11_deviceNo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnStart = new DMSkin.Controls.DMButton();
            this.btn_tijiao = new DMSkin.Controls.DMButton();
            this.btnClose = new DMSkin.Controls.DMButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblfayunhuadao = new System.Windows.Forms.Label();
            this.lblzhuangxiangbz = new System.Windows.Forms.Label();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblReader = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMainNumber = new System.Windows.Forms.Label();
            this.grid = new DMSkin.Metro.Controls.MetroGrid();
            this.HU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MAINBAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADDBAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.should_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.real_count = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Teal;
            this.splitContainer1.Panel1.Controls.Add(this.dmButton1_save);
            this.splitContainer1.Panel1.Controls.Add(this.dmButton1_reset);
            this.splitContainer1.Panel1.Controls.Add(this.label9_dema);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label9_fenjianboci);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1_bar);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.textBox1_boxno);
            this.splitContainer1.Panel1.Controls.Add(this.dmButton1_exception_query);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label11_deviceNo);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.btnStart);
            this.splitContainer1.Panel1.Controls.Add(this.btn_tijiao);
            this.splitContainer1.Panel1.Controls.Add(this.btnClose);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.lblfayunhuadao);
            this.splitContainer1.Panel1.Controls.Add(this.lblzhuangxiangbz);
            this.splitContainer1.Panel1.Controls.Add(this.lblCurrentUser);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.lblReader);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lblMainNumber);
            this.splitContainer1.Panel2.Controls.Add(this.grid);
            this.splitContainer1.Size = new System.Drawing.Size(1221, 712);
            this.splitContainer1.SplitterDistance = 226;
            this.splitContainer1.TabIndex = 1;
            // 
            // dmButton1_save
            // 
            this.dmButton1_save.AutoEllipsis = true;
            this.dmButton1_save.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton1_save.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton1_save.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton1_save.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton1_save.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1_save.DM_Radius = 1;
            this.dmButton1_save.Enabled = false;
            this.dmButton1_save.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton1_save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton1_save.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.dmButton1_save.ForeColor = System.Drawing.Color.Teal;
            this.dmButton1_save.Image = null;
            this.dmButton1_save.Location = new System.Drawing.Point(597, 139);
            this.dmButton1_save.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton1_save.Name = "dmButton1_save";
            this.dmButton1_save.Size = new System.Drawing.Size(120, 30);
            this.dmButton1_save.TabIndex = 61;
            this.dmButton1_save.Text = "保存";
            this.dmButton1_save.UseVisualStyleBackColor = false;
            this.dmButton1_save.Click += new System.EventHandler(this.dmButton1_save_Click);
            // 
            // dmButton1_reset
            // 
            this.dmButton1_reset.AutoEllipsis = true;
            this.dmButton1_reset.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1_reset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton1_reset.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton1_reset.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton1_reset.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton1_reset.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1_reset.DM_Radius = 1;
            this.dmButton1_reset.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton1_reset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton1_reset.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.dmButton1_reset.ForeColor = System.Drawing.Color.Teal;
            this.dmButton1_reset.Image = null;
            this.dmButton1_reset.Location = new System.Drawing.Point(753, 95);
            this.dmButton1_reset.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton1_reset.Name = "dmButton1_reset";
            this.dmButton1_reset.Size = new System.Drawing.Size(120, 30);
            this.dmButton1_reset.TabIndex = 60;
            this.dmButton1_reset.Text = "重新扫描";
            this.dmButton1_reset.UseVisualStyleBackColor = false;
            this.dmButton1_reset.Click += new System.EventHandler(this.dmButton1_reset_Click);
            // 
            // label9_dema
            // 
            this.label9_dema.BackColor = System.Drawing.Color.White;
            this.label9_dema.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9_dema.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9_dema.Location = new System.Drawing.Point(401, 140);
            this.label9_dema.Name = "label9_dema";
            this.label9_dema.Size = new System.Drawing.Size(170, 32);
            this.label9_dema.TabIndex = 59;
            this.label9_dema.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(296, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 32);
            this.label5.TabIndex = 57;
            this.label5.Text = "分拣波次：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9_fenjianboci
            // 
            this.label9_fenjianboci.BackColor = System.Drawing.Color.White;
            this.label9_fenjianboci.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9_fenjianboci.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label9_fenjianboci.Location = new System.Drawing.Point(401, 94);
            this.label9_fenjianboci.Name = "label9_fenjianboci";
            this.label9_fenjianboci.Size = new System.Drawing.Size(170, 32);
            this.label9_fenjianboci.TabIndex = 58;
            this.label9_fenjianboci.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox1_bar
            // 
            this.textBox1_bar.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox1_bar.Location = new System.Drawing.Point(401, 186);
            this.textBox1_bar.Name = "textBox1_bar";
            this.textBox1_bar.Size = new System.Drawing.Size(170, 29);
            this.textBox1_bar.TabIndex = 56;
            this.textBox1_bar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_bar_KeyPress);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(295, 184);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 32);
            this.label2.TabIndex = 55;
            this.label2.Text = "扫描条码：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1_boxno
            // 
            this.textBox1_boxno.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.textBox1_boxno.Location = new System.Drawing.Point(703, 187);
            this.textBox1_boxno.Name = "textBox1_boxno";
            this.textBox1_boxno.Size = new System.Drawing.Size(170, 29);
            this.textBox1_boxno.TabIndex = 54;
            this.textBox1_boxno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_boxno_KeyPress);
            // 
            // dmButton1_exception_query
            // 
            this.dmButton1_exception_query.AutoEllipsis = true;
            this.dmButton1_exception_query.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1_exception_query.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dmButton1_exception_query.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.dmButton1_exception_query.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.dmButton1_exception_query.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.dmButton1_exception_query.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.dmButton1_exception_query.DM_Radius = 1;
            this.dmButton1_exception_query.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.dmButton1_exception_query.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dmButton1_exception_query.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.dmButton1_exception_query.ForeColor = System.Drawing.Color.Teal;
            this.dmButton1_exception_query.Image = null;
            this.dmButton1_exception_query.Location = new System.Drawing.Point(753, 139);
            this.dmButton1_exception_query.Margin = new System.Windows.Forms.Padding(0);
            this.dmButton1_exception_query.Name = "dmButton1_exception_query";
            this.dmButton1_exception_query.Size = new System.Drawing.Size(120, 30);
            this.dmButton1_exception_query.TabIndex = 29;
            this.dmButton1_exception_query.Text = "上传列表";
            this.dmButton1_exception_query.UseVisualStyleBackColor = false;
            this.dmButton1_exception_query.Click += new System.EventHandler(this.dmButton1_exception_query_Click);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(295, 140);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(100, 32);
            this.label12.TabIndex = 27;
            this.label12.Text = "德马目的：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(9, 183);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 32);
            this.label10.TabIndex = 25;
            this.label10.Text = "发运滑道：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11_deviceNo
            // 
            this.label11_deviceNo.BackColor = System.Drawing.Color.White;
            this.label11_deviceNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11_deviceNo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.label11_deviceNo.Location = new System.Drawing.Point(115, 94);
            this.label11_deviceNo.Name = "label11_deviceNo";
            this.label11_deviceNo.Size = new System.Drawing.Size(170, 32);
            this.label11_deviceNo.TabIndex = 26;
            this.label11_deviceNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(597, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 32);
            this.label8.TabIndex = 23;
            this.label8.Text = "扫描箱号：";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.AutoEllipsis = true;
            this.btnStart.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStart.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnStart.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btnStart.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btnStart.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnStart.DM_Radius = 1;
            this.btnStart.Enabled = false;
            this.btnStart.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnStart.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.Teal;
            this.btnStart.Image = null;
            this.btnStart.Location = new System.Drawing.Point(597, 50);
            this.btnStart.Margin = new System.Windows.Forms.Padding(0);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(120, 30);
            this.btnStart.TabIndex = 19;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btn_tijiao
            // 
            this.btn_tijiao.AutoEllipsis = true;
            this.btn_tijiao.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_tijiao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_tijiao.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btn_tijiao.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(93)))), ((int)(((byte)(203)))));
            this.btn_tijiao.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(123)))), ((int)(((byte)(203)))));
            this.btn_tijiao.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btn_tijiao.DM_Radius = 1;
            this.btn_tijiao.Enabled = false;
            this.btn_tijiao.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btn_tijiao.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_tijiao.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btn_tijiao.ForeColor = System.Drawing.Color.Teal;
            this.btn_tijiao.Image = null;
            this.btn_tijiao.Location = new System.Drawing.Point(597, 94);
            this.btn_tijiao.Margin = new System.Windows.Forms.Padding(0);
            this.btn_tijiao.Name = "btn_tijiao";
            this.btn_tijiao.Size = new System.Drawing.Size(120, 30);
            this.btn_tijiao.TabIndex = 20;
            this.btn_tijiao.Text = "提交";
            this.btn_tijiao.UseVisualStyleBackColor = false;
            this.btn_tijiao.Click += new System.EventHandler(this.btn_tijiao_Click);
            // 
            // btnClose
            // 
            this.btnClose.AutoEllipsis = true;
            this.btnClose.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.DM_DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(98)))), ((int)(((byte)(115)))));
            this.btnClose.DM_DownColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(20)))), ((int)(((byte)(0)))));
            this.btnClose.DM_MoveColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(60)))), ((int)(((byte)(0)))));
            this.btnClose.DM_NormalColor = System.Drawing.Color.WhiteSmoke;
            this.btnClose.DM_Radius = 1;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.Teal;
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(753, 50);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 30);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "退出";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(9, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 32);
            this.label6.TabIndex = 7;
            this.label6.Text = "设备号：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(10, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 32);
            this.label3.TabIndex = 8;
            this.label3.Text = "转向标识：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblfayunhuadao
            // 
            this.lblfayunhuadao.BackColor = System.Drawing.Color.White;
            this.lblfayunhuadao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblfayunhuadao.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblfayunhuadao.Location = new System.Drawing.Point(115, 183);
            this.lblfayunhuadao.Name = "lblfayunhuadao";
            this.lblfayunhuadao.Size = new System.Drawing.Size(170, 32);
            this.lblfayunhuadao.TabIndex = 9;
            this.lblfayunhuadao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblzhuangxiangbz
            // 
            this.lblzhuangxiangbz.BackColor = System.Drawing.Color.White;
            this.lblzhuangxiangbz.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblzhuangxiangbz.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblzhuangxiangbz.Location = new System.Drawing.Point(115, 139);
            this.lblzhuangxiangbz.Name = "lblzhuangxiangbz";
            this.lblzhuangxiangbz.Size = new System.Drawing.Size(170, 32);
            this.lblzhuangxiangbz.TabIndex = 10;
            this.lblzhuangxiangbz.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.BackColor = System.Drawing.Color.White;
            this.lblCurrentUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentUser.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblCurrentUser.Location = new System.Drawing.Point(115, 50);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(170, 32);
            this.lblCurrentUser.TabIndex = 11;
            this.lblCurrentUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 32);
            this.label4.TabIndex = 12;
            this.label4.Text = "登录工号：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(296, 51);
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
            this.lblReader.Location = new System.Drawing.Point(401, 50);
            this.lblReader.Name = "lblReader";
            this.lblReader.Size = new System.Drawing.Size(170, 32);
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
            this.label1.Size = new System.Drawing.Size(1221, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "出库复核";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMainNumber
            // 
            this.lblMainNumber.AutoSize = true;
            this.lblMainNumber.BackColor = System.Drawing.Color.Transparent;
            this.lblMainNumber.Font = new System.Drawing.Font("微软雅黑", 60F);
            this.lblMainNumber.ForeColor = System.Drawing.Color.Teal;
            this.lblMainNumber.Location = new System.Drawing.Point(798, 183);
            this.lblMainNumber.Name = "lblMainNumber";
            this.lblMainNumber.Size = new System.Drawing.Size(75, 119);
            this.lblMainNumber.TabIndex = 20;
            this.lblMainNumber.Text = "0";
            this.lblMainNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMainNumber.UseCompatibleTextRendering = true;
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
            this.HU,
            this.MAINBAR,
            this.ADDBAR,
            this.QTY,
            this.should_count,
            this.real_count});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grid.DefaultCellStyle = dataGridViewCellStyle2;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EnableHeadersVisualStyles = false;
            this.grid.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.grid.GridColor = System.Drawing.Color.DarkGray;
            this.grid.Location = new System.Drawing.Point(0, 0);
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
            this.grid.Size = new System.Drawing.Size(1221, 482);
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
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 772);
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "InventoryForm";
            this.Text = "InventoryForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InventoryForm_FormClosing);
            this.Load += new System.EventHandler(this.InventoryForm_Load);
            this.Shown += new System.EventHandler(this.InventoryForm_Shown);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblfayunhuadao;
        private System.Windows.Forms.Label lblzhuangxiangbz;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblReader;
        private DMSkin.Controls.DMButton btnStart;
        private DMSkin.Controls.DMButton btn_tijiao;
        private DMSkin.Controls.DMButton btnClose;
        private DMSkin.Metro.Controls.MetroGrid grid;
        private DMSkin.Controls.DMButton dmButton1_exception_query;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11_deviceNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox1_boxno;
        private System.Windows.Forms.TextBox textBox1_bar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9_fenjianboci;
        private System.Windows.Forms.Label lblMainNumber;
        private DMSkin.Controls.DMButton dmButton1_reset;
        private System.Windows.Forms.Label label9_dema;
        private System.Windows.Forms.DataGridViewTextBoxColumn HU;
        private System.Windows.Forms.DataGridViewTextBoxColumn MAINBAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADDBAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn QTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn should_count;
        private System.Windows.Forms.DataGridViewTextBoxColumn real_count;
        private DMSkin.Controls.DMButton dmButton1_save;
    }
}