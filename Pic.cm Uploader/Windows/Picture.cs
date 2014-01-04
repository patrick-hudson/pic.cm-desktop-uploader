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
    public partial class Picture : Form
    {
        public Picture(string id)
        {
            InitializeComponent();
            bleh.Text = id;
        }

        private void Picture_Load(object sender, EventArgs e)
        {

        }
    }
}
