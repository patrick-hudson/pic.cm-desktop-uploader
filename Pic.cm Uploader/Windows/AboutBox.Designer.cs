/*
* Copyright (c) 2013 Patrick Hudson
* 
* This file is part of Pic.cm Uploader
* Universal Chevereto Uploadr is a free software: you can redistribute it and/or modify it under the terms of the GNU General Public License
* as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
* Universal Chevereto Uploadr is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty
* of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
* You should have received a copy of the GNU General Public License along with Pic.cm Uploader If not, see http://www.gnu.org/licenses/.
*/

namespace Piccm_Uploader.Windows
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            this.labelProductName = new System.Windows.Forms.Label();
            this.labelVersionText = new System.Windows.Forms.Label();
            this.labelVersionData = new System.Windows.Forms.Label();
            this.labelCopyRight = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelProductName
            // 
            this.labelProductName.Location = new System.Drawing.Point(15, 9);
            this.labelProductName.Margin = new System.Windows.Forms.Padding(0);
            this.labelProductName.MaximumSize = new System.Drawing.Size(0, 17);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(0, 17);
            this.labelProductName.TabIndex = 21;
            this.labelProductName.Text = "Imgr Uploadr v 1.0";
            this.labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersionText
            // 
            this.labelVersionText.AutoSize = true;
            this.labelVersionText.Location = new System.Drawing.Point(12, 66);
            this.labelVersionText.Name = "labelVersionText";
            this.labelVersionText.Size = new System.Drawing.Size(45, 13);
            this.labelVersionText.TabIndex = 24;
            this.labelVersionText.Text = "Version:";
            // 
            // labelVersionData
            // 
            this.labelVersionData.AutoSize = true;
            this.labelVersionData.Location = new System.Drawing.Point(83, 66);
            this.labelVersionData.Name = "labelVersionData";
            this.labelVersionData.Size = new System.Drawing.Size(57, 13);
            this.labelVersionData.TabIndex = 25;
            this.labelVersionData.Text = "Loading....";
            // 
            // labelCopyRight
            // 
            this.labelCopyRight.AutoSize = true;
            this.labelCopyRight.Location = new System.Drawing.Point(12, 79);
            this.labelCopyRight.Name = "labelCopyRight";
            this.labelCopyRight.Size = new System.Drawing.Size(128, 13);
            this.labelCopyRight.TabIndex = 28;
            this.labelCopyRight.Text = "Copyright Pic.cm 2013-14";
            this.labelCopyRight.Click += new System.EventHandler(this.labelCopyRight_Click);
            // 
            // AboutBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 101);
            this.Controls.Add(this.labelCopyRight);
            this.Controls.Add(this.labelVersionData);
            this.Controls.Add(this.labelVersionText);
            this.Controls.Add(this.labelProductName);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(283, 358);
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.AboutBox_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.AboutBox_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersionText;
        private System.Windows.Forms.Label labelVersionData;
        private System.Windows.Forms.Label labelCopyRight;

    }
}
