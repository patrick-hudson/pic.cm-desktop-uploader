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
    partial class PhotoViewer
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
        	this.button1 = new System.Windows.Forms.Button();
        	this.button2 = new System.Windows.Forms.Button();
        	this.button3 = new System.Windows.Forms.Button();
        	this.groupBox1 = new System.Windows.Forms.GroupBox();
        	this.pictureBox1 = new System.Windows.Forms.PictureBox();
        	this.label3 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.label1 = new System.Windows.Forms.Label();
        	this.groupBox3 = new System.Windows.Forms.GroupBox();
        	this.textBox5 = new System.Windows.Forms.TextBox();
        	this.label8 = new System.Windows.Forms.Label();
        	this.textBox4 = new System.Windows.Forms.TextBox();
        	this.label7 = new System.Windows.Forms.Label();
        	this.textBox3 = new System.Windows.Forms.TextBox();
        	this.label6 = new System.Windows.Forms.Label();
        	this.textBox2 = new System.Windows.Forms.TextBox();
        	this.label5 = new System.Windows.Forms.Label();
        	this.textBox1 = new System.Windows.Forms.TextBox();
        	this.label4 = new System.Windows.Forms.Label();
        	this.groupBox4 = new System.Windows.Forms.GroupBox();
        	this.textBox6 = new System.Windows.Forms.TextBox();
        	this.label9 = new System.Windows.Forms.Label();
        	this.textBox7 = new System.Windows.Forms.TextBox();
        	this.label10 = new System.Windows.Forms.Label();
        	this.textBox8 = new System.Windows.Forms.TextBox();
        	this.label11 = new System.Windows.Forms.Label();
        	this.button4 = new System.Windows.Forms.Button();
        	this.groupBox1.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.groupBox3.SuspendLayout();
        	this.groupBox4.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// button1
        	// 
        	this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.button1.Location = new System.Drawing.Point(6, 255);
        	this.button1.Name = "button1";
        	this.button1.Size = new System.Drawing.Size(75, 23);
        	this.button1.TabIndex = 10;
        	this.button1.Text = "Download";
        	this.button1.UseVisualStyleBackColor = true;
        	this.button1.Click += new System.EventHandler(this.Button1Click);
        	// 
        	// button2
        	// 
        	this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.button2.Location = new System.Drawing.Point(87, 255);
        	this.button2.Name = "button2";
        	this.button2.Size = new System.Drawing.Size(116, 23);
        	this.button2.TabIndex = 11;
        	this.button2.Text = "Delete from server";
        	this.button2.UseVisualStyleBackColor = true;
        	this.button2.Click += new System.EventHandler(this.button2_Click);
        	// 
        	// button3
        	// 
        	this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.button3.Location = new System.Drawing.Point(209, 255);
        	this.button3.Name = "button3";
        	this.button3.Size = new System.Drawing.Size(116, 23);
        	this.button3.TabIndex = 12;
        	this.button3.Text = "Delete from history";
        	this.button3.UseVisualStyleBackColor = true;
        	this.button3.Click += new System.EventHandler(this.Button3Click);
        	// 
        	// groupBox1
        	// 
        	this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
        	        	        	| System.Windows.Forms.AnchorStyles.Left) 
        	        	        	| System.Windows.Forms.AnchorStyles.Right)));
        	this.groupBox1.Controls.Add(this.pictureBox1);
        	this.groupBox1.Controls.Add(this.button3);
        	this.groupBox1.Controls.Add(this.label3);
        	this.groupBox1.Controls.Add(this.button2);
        	this.groupBox1.Controls.Add(this.label2);
        	this.groupBox1.Controls.Add(this.button1);
        	this.groupBox1.Controls.Add(this.label1);
        	this.groupBox1.Location = new System.Drawing.Point(12, 12);
        	this.groupBox1.Name = "groupBox1";
        	this.groupBox1.Size = new System.Drawing.Size(335, 286);
        	this.groupBox1.TabIndex = 4;
        	this.groupBox1.TabStop = false;
        	this.groupBox1.Text = "Photo info";
        	this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.Location = new System.Drawing.Point(6, 32);
        	this.pictureBox1.Name = "pictureBox1";
        	this.pictureBox1.Size = new System.Drawing.Size(323, 191);
        	this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        	this.pictureBox1.TabIndex = 13;
        	this.pictureBox1.TabStop = false;
        	// 
        	// label3
        	// 
        	this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.label3.AutoSize = true;
        	this.label3.Location = new System.Drawing.Point(6, 239);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(70, 13);
        	this.label3.TabIndex = 4;
        	this.label3.Text = "Server name:";
        	// 
        	// label2
        	// 
        	this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(6, 226);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(65, 13);
        	this.label2.TabIndex = 3;
        	this.label2.Text = "Local name:";
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.ForeColor = System.Drawing.Color.Black;
        	this.label1.Location = new System.Drawing.Point(6, 16);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(48, 13);
        	this.label1.TabIndex = 2;
        	this.label1.Text = "Preview:";
        	this.label1.Click += new System.EventHandler(this.label1_Click);
        	// 
        	// groupBox3
        	// 
        	this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.groupBox3.Controls.Add(this.textBox5);
        	this.groupBox3.Controls.Add(this.label8);
        	this.groupBox3.Controls.Add(this.textBox4);
        	this.groupBox3.Controls.Add(this.label7);
        	this.groupBox3.Controls.Add(this.textBox3);
        	this.groupBox3.Controls.Add(this.label6);
        	this.groupBox3.Controls.Add(this.textBox2);
        	this.groupBox3.Controls.Add(this.label5);
        	this.groupBox3.Controls.Add(this.textBox1);
        	this.groupBox3.Controls.Add(this.label4);
        	this.groupBox3.Location = new System.Drawing.Point(353, 12);
        	this.groupBox3.Name = "groupBox3";
        	this.groupBox3.Size = new System.Drawing.Size(262, 124);
        	this.groupBox3.TabIndex = 6;
        	this.groupBox3.TabStop = false;
        	this.groupBox3.Text = "Picture links";
        	// 
        	// textBox5
        	// 
        	this.textBox5.Location = new System.Drawing.Point(69, 96);
        	this.textBox5.Name = "textBox5";
        	this.textBox5.Size = new System.Drawing.Size(186, 20);
        	this.textBox5.TabIndex = 6;
        	// 
        	// label8
        	// 
        	this.label8.AutoSize = true;
        	this.label8.Location = new System.Drawing.Point(6, 99);
        	this.label8.Name = "label8";
        	this.label8.Size = new System.Drawing.Size(49, 13);
        	this.label8.TabIndex = 10;
        	this.label8.Text = "BBCode:";
        	// 
        	// textBox4
        	// 
        	this.textBox4.Location = new System.Drawing.Point(69, 76);
        	this.textBox4.Name = "textBox4";
        	this.textBox4.Size = new System.Drawing.Size(186, 20);
        	this.textBox4.TabIndex = 5;
        	// 
        	// label7
        	// 
        	this.label7.AutoSize = true;
        	this.label7.Location = new System.Drawing.Point(6, 79);
        	this.label7.Name = "label7";
        	this.label7.Size = new System.Drawing.Size(40, 13);
        	this.label7.TabIndex = 8;
        	this.label7.Text = "HTML:";
        	// 
        	// textBox3
        	// 
        	this.textBox3.Location = new System.Drawing.Point(69, 56);
        	this.textBox3.Name = "textBox3";
        	this.textBox3.Size = new System.Drawing.Size(186, 20);
        	this.textBox3.TabIndex = 4;
        	// 
        	// label6
        	// 
        	this.label6.AutoSize = true;
        	this.label6.Location = new System.Drawing.Point(6, 59);
        	this.label6.Name = "label6";
        	this.label6.Size = new System.Drawing.Size(42, 13);
        	this.label6.TabIndex = 6;
        	this.label6.Text = "Viewer:";
        	// 
        	// textBox2
        	// 
        	this.textBox2.Location = new System.Drawing.Point(69, 36);
        	this.textBox2.Name = "textBox2";
        	this.textBox2.Size = new System.Drawing.Size(186, 20);
        	this.textBox2.TabIndex = 3;
        	// 
        	// label5
        	// 
        	this.label5.AutoSize = true;
        	this.label5.Location = new System.Drawing.Point(6, 39);
        	this.label5.Name = "label5";
        	this.label5.Size = new System.Drawing.Size(60, 13);
        	this.label5.TabIndex = 4;
        	this.label5.Text = "Short URL:";
        	// 
        	// textBox1
        	// 
        	this.textBox1.Location = new System.Drawing.Point(69, 16);
        	this.textBox1.Name = "textBox1";
        	this.textBox1.Size = new System.Drawing.Size(185, 20);
        	this.textBox1.TabIndex = 2;
        	this.textBox1.TextChanged += new System.EventHandler(this.TextBox1TextChanged);
        	// 
        	// label4
        	// 
        	this.label4.AutoSize = true;
        	this.label4.Location = new System.Drawing.Point(6, 19);
        	this.label4.Name = "label4";
        	this.label4.Size = new System.Drawing.Size(57, 13);
        	this.label4.TabIndex = 0;
        	this.label4.Text = "Direct link:";
        	// 
        	// groupBox4
        	// 
        	this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.groupBox4.Controls.Add(this.textBox6);
        	this.groupBox4.Controls.Add(this.label9);
        	this.groupBox4.Controls.Add(this.textBox7);
        	this.groupBox4.Controls.Add(this.label10);
        	this.groupBox4.Controls.Add(this.textBox8);
        	this.groupBox4.Controls.Add(this.label11);
        	this.groupBox4.Location = new System.Drawing.Point(353, 142);
        	this.groupBox4.Name = "groupBox4";
        	this.groupBox4.Size = new System.Drawing.Size(262, 95);
        	this.groupBox4.TabIndex = 7;
        	this.groupBox4.TabStop = false;
        	this.groupBox4.Text = "Thumbnail links";
        	// 
        	// textBox6
        	// 
        	this.textBox6.Location = new System.Drawing.Point(68, 56);
        	this.textBox6.Name = "textBox6";
        	this.textBox6.Size = new System.Drawing.Size(186, 20);
        	this.textBox6.TabIndex = 9;
        	// 
        	// label9
        	// 
        	this.label9.AutoSize = true;
        	this.label9.Location = new System.Drawing.Point(5, 59);
        	this.label9.Name = "label9";
        	this.label9.Size = new System.Drawing.Size(49, 13);
        	this.label9.TabIndex = 16;
        	this.label9.Text = "BBCode:";
        	// 
        	// textBox7
        	// 
        	this.textBox7.Location = new System.Drawing.Point(68, 36);
        	this.textBox7.Name = "textBox7";
        	this.textBox7.Size = new System.Drawing.Size(186, 20);
        	this.textBox7.TabIndex = 8;
        	// 
        	// label10
        	// 
        	this.label10.AutoSize = true;
        	this.label10.Location = new System.Drawing.Point(5, 39);
        	this.label10.Name = "label10";
        	this.label10.Size = new System.Drawing.Size(40, 13);
        	this.label10.TabIndex = 14;
        	this.label10.Text = "HTML:";
        	// 
        	// textBox8
        	// 
        	this.textBox8.Location = new System.Drawing.Point(69, 16);
        	this.textBox8.Name = "textBox8";
        	this.textBox8.Size = new System.Drawing.Size(185, 20);
        	this.textBox8.TabIndex = 7;
        	// 
        	// label11
        	// 
        	this.label11.AutoSize = true;
        	this.label11.Location = new System.Drawing.Point(6, 19);
        	this.label11.Name = "label11";
        	this.label11.Size = new System.Drawing.Size(32, 13);
        	this.label11.TabIndex = 12;
        	this.label11.Text = "URL:";
        	// 
        	// button4
        	// 
        	this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.button4.Location = new System.Drawing.Point(516, 269);
        	this.button4.Name = "button4";
        	this.button4.Size = new System.Drawing.Size(99, 23);
        	this.button4.TabIndex = 13;
        	this.button4.Text = "View on browser";
        	this.button4.UseVisualStyleBackColor = true;
        	this.button4.Click += new System.EventHandler(this.button4_Click);
        	// 
        	// PhotoViewer
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(625, 304);
        	this.Controls.Add(this.button4);
        	this.Controls.Add(this.groupBox4);
        	this.Controls.Add(this.groupBox3);
        	this.Controls.Add(this.groupBox1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        	this.Name = "PhotoViewer";
        	this.Text = "Photo Viewer";
        	this.groupBox1.ResumeLayout(false);
        	this.groupBox1.PerformLayout();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.groupBox3.ResumeLayout(false);
        	this.groupBox3.PerformLayout();
        	this.groupBox4.ResumeLayout(false);
        	this.groupBox4.PerformLayout();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.PictureBox pictureBox1;

        

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button4;

        
        
    }
}