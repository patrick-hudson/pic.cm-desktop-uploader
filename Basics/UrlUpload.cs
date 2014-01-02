using System;
using System.Text;
using System.Windows.Forms;

/*
 * Temporary Patch
 * TODO Rewrite URL upload class, Possible merge into Capture.cs
 */


namespace Piccm_Uploader
{
    public partial class UrlUpload : Form
    {
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                Uri uriResult = null;
                bool result = Uri.TryCreate(textBox1.Text, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
            }
            this.Close();
            Uploadr.StartUpload(null, textBox1.Text);
        }
    }
}
