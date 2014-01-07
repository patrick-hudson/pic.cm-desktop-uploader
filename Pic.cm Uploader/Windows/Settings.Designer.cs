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
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonCheckForUpdates = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnUploadWindow = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCoppedScreenshot = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUploadDesktop = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(6, 6);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(322, 17);
            this.checkBox7.TabIndex = 5;
            this.checkBox7.Text = "Check this if you connect to the internet through a proxy server";
            this.checkBox7.UseVisualStyleBackColor = true;
            this.checkBox7.CheckedChanged += new System.EventHandler(this.checkBox7_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(64, 68);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(99, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Adress:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(372, 222);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.buttonCheckForUpdates);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.checkBox4);
            this.tabPage1.Controls.Add(this.checkBox3);
            this.tabPage1.Controls.Add(this.checkBox1);
            this.tabPage1.Controls.Add(this.checkBox6);
            this.tabPage1.Controls.Add(this.checkBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(364, 196);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            // 
            // buttonCheckForUpdates
            // 
            this.buttonCheckForUpdates.Location = new System.Drawing.Point(3, 168);
            this.buttonCheckForUpdates.Name = "buttonCheckForUpdates";
            this.buttonCheckForUpdates.Size = new System.Drawing.Size(116, 25);
            this.buttonCheckForUpdates.TabIndex = 10;
            this.buttonCheckForUpdates.Text = "Check for updates";
            this.buttonCheckForUpdates.UseVisualStyleBackColor = true;
            this.buttonCheckForUpdates.Click += new System.EventHandler(this.ButtonCheckForUpdate_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 168);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(151, 25);
            this.button1.TabIndex = 9;
            this.button1.Text = "View Saved Screenshots";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(6, 98);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(113, 17);
            this.checkBox4.TabIndex = 8;
            this.checkBox4.Text = "Play Upload Jingle";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 75);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(111, 17);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "Save screenshots";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged_1);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(200, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "After upload, copy link(s) to clipboard";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(6, 29);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(177, 17);
            this.checkBox6.TabIndex = 1;
            this.checkBox6.Text = "Automatically check for updates";
            this.checkBox6.UseVisualStyleBackColor = true;
            this.checkBox6.CheckedChanged += new System.EventHandler(this.checkBox6_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(6, 52);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(169, 17);
            this.checkBox2.TabIndex = 4;
            this.checkBox2.Text = "Run this application on startup";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.checkBox7);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(364, 196);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Proxy";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Set here server\'s info:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.btnUploadWindow);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.btnCoppedScreenshot);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.btnUploadDesktop);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(364, 196);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Key binding";
            // 
            // btnUploadWindow
            // 
            this.btnUploadWindow.Location = new System.Drawing.Point(157, 109);
            this.btnUploadWindow.Name = "btnUploadWindow";
            this.btnUploadWindow.Size = new System.Drawing.Size(174, 23);
            this.btnUploadWindow.TabIndex = 5;
            this.btnUploadWindow.Text = "Control+D3";
            this.btnUploadWindow.UseVisualStyleBackColor = true;
            this.btnUploadWindow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUploadWindow_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Upload window screenshot";
            // 
            // btnCoppedScreenshot
            // 
            this.btnCoppedScreenshot.Location = new System.Drawing.Point(157, 17);
            this.btnCoppedScreenshot.Name = "btnCoppedScreenshot";
            this.btnCoppedScreenshot.Size = new System.Drawing.Size(174, 23);
            this.btnCoppedScreenshot.TabIndex = 3;
            this.btnCoppedScreenshot.Text = "Control+D1";
            this.btnCoppedScreenshot.UseVisualStyleBackColor = true;
            this.btnCoppedScreenshot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCoppedScreenshot_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Upload cropped screenshot";
            // 
            // btnUploadDesktop
            // 
            this.btnUploadDesktop.Location = new System.Drawing.Point(157, 63);
            this.btnUploadDesktop.Name = "btnUploadDesktop";
            this.btnUploadDesktop.Size = new System.Drawing.Size(174, 23);
            this.btnUploadDesktop.TabIndex = 1;
            this.btnUploadDesktop.Text = "Control+D2";
            this.btnUploadDesktop.UseVisualStyleBackColor = true;
            this.btnUploadDesktop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUploadDesktop_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Upload desktop screenshot";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(372, 222);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btnUploadDesktop;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnUploadWindow;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCoppedScreenshot;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonCheckForUpdates;
    }
}