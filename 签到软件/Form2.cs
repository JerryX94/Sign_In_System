using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 签到软件
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private string returnValue;
        public string Value
        {
            set { returnValue = value;}
            get { return returnValue; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            returnValue = textBox1.Text;
            textBox1.Clear();
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            returnValue = null;
            Close();
        }
    }
}
