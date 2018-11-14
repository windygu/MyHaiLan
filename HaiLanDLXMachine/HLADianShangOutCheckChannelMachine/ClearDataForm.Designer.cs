namespace HLADianShangOutCheckChannelMachine
{
    partial class ClearDataForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2_doc = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1_clearDoc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(88, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "单号：";
            // 
            // textBox2_doc
            // 
            this.textBox2_doc.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2_doc.Location = new System.Drawing.Point(170, 101);
            this.textBox2_doc.Name = "textBox2_doc";
            this.textBox2_doc.Size = new System.Drawing.Size(257, 32);
            this.textBox2_doc.TabIndex = 4;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(464, 363);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 47);
            this.button2.TabIndex = 5;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1_clearDoc
            // 
            this.button1_clearDoc.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1_clearDoc.Location = new System.Drawing.Point(464, 93);
            this.button1_clearDoc.Name = "button1_clearDoc";
            this.button1_clearDoc.Size = new System.Drawing.Size(127, 47);
            this.button1_clearDoc.TabIndex = 6;
            this.button1_clearDoc.Text = "清除单号";
            this.button1_clearDoc.UseVisualStyleBackColor = true;
            this.button1_clearDoc.Click += new System.EventHandler(this.button1_clearDoc_Click);
            // 
            // ClearDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(661, 434);
            this.Controls.Add(this.button1_clearDoc);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2_doc);
            this.Controls.Add(this.label2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClearDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "清除数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2_doc;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1_clearDoc;
    }
}