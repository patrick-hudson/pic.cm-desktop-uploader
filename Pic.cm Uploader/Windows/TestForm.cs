using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Piccm_Uploader.Windows
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(keydown);
        }
        private void keydown(object sender, KeyEventArgs e)
        {
            MessageBox.Show(e.KeyCode.ToString() + " " + e.KeyValue.ToString());
        }
    }
}
