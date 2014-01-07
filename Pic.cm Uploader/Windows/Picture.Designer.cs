namespace Piccm_Uploader.Windows
{
    partial class Picture
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
            this.bleh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // bleh
            // 
            this.bleh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bleh.Location = new System.Drawing.Point(0, 0);
            this.bleh.Name = "bleh";
            this.bleh.Size = new System.Drawing.Size(284, 51);
            this.bleh.TabIndex = 0;
            this.bleh.UseVisualStyleBackColor = true;
            // 
            // Picture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 51);
            this.Controls.Add(this.bleh);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 90);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 90);
            this.Name = "Picture";
            this.Text = "Picture";
            this.Load += new System.EventHandler(this.Picture_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bleh;
    }
}