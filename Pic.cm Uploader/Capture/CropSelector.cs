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

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Piccm_Uploader.Capture
{
	public partial class CropSelector : UserControl
	{
		Size OldSize;
		Point OldMouse, Dif;
		int mode, mouse_state;
		
		public CropSelector ()
		{
			InitializeComponent ();
			this.BackColor=Color.Pink;
			mode=0;
			this.MouseUp+=new MouseEventHandler (CropSelector_MouseUp);
			button5.MouseUp+=new MouseEventHandler (CropSelector_MouseUp);
			button6.MouseUp+=new MouseEventHandler (CropSelector_MouseUp);
			button8.MouseUp+=new MouseEventHandler (CropSelector_MouseUp);
			this.MouseDown+=new MouseEventHandler (CropSelector_MouseDown);
			button5.MouseDown+=new MouseEventHandler (CropSelector_MouseDown);
			button6.MouseDown+=new MouseEventHandler (CropSelector_MouseDown);
			button8.MouseDown+=new MouseEventHandler (CropSelector_MouseDown);
			this.MouseMove+=new MouseEventHandler (CropSelector_MouseMove);
			button5.MouseMove+=new MouseEventHandler (CropSelector_MouseMove);
			button6.MouseMove+=new MouseEventHandler (CropSelector_MouseMove);
			button8.MouseMove+=new MouseEventHandler (CropSelector_MouseMove);
			this.KeyUp+=new KeyEventHandler   (CropSelector_KeyUp);
			button5.KeyUp+=new KeyEventHandler   (CropSelector_KeyUp);
			button6.KeyUp+=new KeyEventHandler   (CropSelector_KeyUp);
			button8.KeyUp+=new KeyEventHandler   (CropSelector_KeyUp);
		}

		void CropSelector_KeyUp (object sender, KeyEventArgs e)
		{
            if (e.KeyCode==Keys.Escape)
                Program.MainClassInstance.resetScreen();
		}

		public int Absolute (int x)
		{
			if (x<0) return -x;
			return x;
		}
		
		void CropSelector_MouseUp (object sender, MouseEventArgs e)
		{
			mouse_state=0;
			mode=0;
		}

		void CropSelector_MouseDown (object sender, MouseEventArgs e)
		{
			mouse_state=1;
			Rectangle r=new Rectangle (MousePosition, new Size (10, 10));
			if (r.IntersectsWith (new Rectangle (button5.PointToScreen (Point.Empty), button5.Size)))
			{
				//v>
				mode=9;
				OldSize=this.Size;
			}
			else if (r.IntersectsWith (new Rectangle (button6.PointToScreen (Point.Empty), button6.Size)))
			{
				//vvv
				mode=2;
				OldSize=this.Size;
			}
			else if (r.IntersectsWith (new Rectangle (button8.PointToScreen (Point.Empty), button8.Size)))
			{
				//>>>
				mode=5;
				OldSize=this.Size;
			}
			else
			{
				mode=1;
				OldMouse=MousePosition;
				Dif=new Point (Absolute (OldMouse.X-this.Location.X), Absolute (OldMouse.Y-this.Location.Y));
			}
		}

		void CropSelector_MouseMove (object sender, MouseEventArgs e)
		{
			if (mouse_state==0) return;
			switch (mode)
			{
				case 1: this.Location=new Point (MousePosition.X-Dif.X, MousePosition.Y-Dif.Y); break;
				case 2: this.Size=new Size (OldSize.Width, MousePosition.Y-this.PointToScreen (Point.Empty).Y); break;
				case 9: this.Size=new Size (MousePosition.X-this.PointToScreen (Point.Empty).X, MousePosition.Y-this.PointToScreen (Point.Empty).Y); break;
				case 5: this.Size=new Size (MousePosition.X-this.PointToScreen (Point.Empty).X, OldSize.Height); break;
				default: break;
			}
		}
	}
}
