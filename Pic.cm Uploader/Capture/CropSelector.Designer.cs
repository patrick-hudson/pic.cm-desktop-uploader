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

namespace Piccm_Uploader.Capture
{
	partial class CropSelector
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel2 = new System.Windows.Forms.Panel();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.button5);
			this.panel2.Controls.Add(this.button6);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 235);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(327, 11);
			this.panel2.TabIndex = 4;
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.BackColor = System.Drawing.Color.White;
			this.button5.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
			this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button5.Location = new System.Drawing.Point(317, 0);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(10, 10);
			this.button5.TabIndex = 2;
			this.button5.UseVisualStyleBackColor = false;
			// 
			// button6
			// 
			this.button6.BackColor = System.Drawing.Color.White;
			this.button6.Cursor = System.Windows.Forms.Cursors.SizeNS;
			this.button6.Dock = System.Windows.Forms.DockStyle.Top;
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button6.Location = new System.Drawing.Point(0, 0);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(327, 10);
			this.button6.TabIndex = 2;
			this.button6.UseVisualStyleBackColor = false;
			// 
			// button8
			// 
			this.button8.BackColor = System.Drawing.Color.White;
			this.button8.Cursor = System.Windows.Forms.Cursors.SizeWE;
			this.button8.Dock = System.Windows.Forms.DockStyle.Right;
			this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button8.Location = new System.Drawing.Point(317, 0);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(10, 235);
			this.button8.TabIndex = 6;
			this.button8.UseVisualStyleBackColor = false;
			// 
			// CropSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button8);
			this.Controls.Add(this.panel2);
			this.Cursor = System.Windows.Forms.Cursors.Cross;
			this.Name = "CropSelector";
			this.Size = new System.Drawing.Size(327, 246);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Panel panel2;
	}
}
