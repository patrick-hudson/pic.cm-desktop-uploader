using System;
using System.Text;
using System.Windows.Forms;

using Piccm_Uploader.Core;


namespace Piccm_Uploader.Windows
{
    public partial class UrlUpload : Form
    {
        public UrlUpload()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                if (Validity.CheckURL(textBox1.Text))
                {
                    Upload.uploadQueue.Enqueue(textBox1.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The URL you provided is not valid.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please provide a URL to upload.", "Invalid URL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
