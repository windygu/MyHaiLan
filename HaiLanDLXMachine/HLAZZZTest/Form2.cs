using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HLAZZZTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("是的jksdf是的是的是的是的是的是的是的", "jksdf是的是的是的是的是的是的是的");
            dataGridView1.Rows.Add("是的", "jksdf");
            dataGridView1.Rows.Add("是的", "jksdf");
            dataGridView1.Rows.Add("是的", "jksdf");

        }
    }
}
