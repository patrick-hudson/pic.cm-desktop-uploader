using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Piccm_Uploader.History;

namespace Piccm_Uploader.Windows
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }

        private void History_Load(object sender, EventArgs e)
        {
            try
            {
                SQLiteDatabase sqldb = new SQLiteDatabase();
                DataTable history;
                String query = "SELECT image_name, image_type, image_width, image_height, image_bytes, image_id_public, image_delete_hash, image_date FROM history;";
                history = sqldb.GetDataTable(query);
                // The results can be directly applied to a DataGridView control
                historyDataGrid.DataSource = history;
                /*
                // Or looped through for some other reason
                foreach (DataRow r in recipe.Rows)
                {
                    MessageBox.Show(r["Name"].ToString());
                    MessageBox.Show(r["Description"].ToString());
                    MessageBox.Show(r["Prep Time"].ToString());
                    MessageBox.Show(r["Cooking Time"].ToString());
                }
	
                */
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
                this.Close();
            }

        }
    }
}
