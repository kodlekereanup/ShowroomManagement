using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace WindowsFormsApp2
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // when this button is pressed, the app will show a list of all the items in inventory
            string oradb = "Data Source=localhost;User Id=PROJECT;Password=anup;";
            OracleConnection conn = new OracleConnection(oradb);  // C#
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select * from inventory";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            
            using (OracleDataAdapter oda = new OracleDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                dataGridView1.Visible = true;
                oda.Fill(dt);
                dataGridView1.DataSource = dt;
                
            }

            
            dr.Read();
            
            conn.Dispose();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // add cars
            // This button will most likely use the insert command 
            // update command will be used in service page where the last date of service will be updated
            // delete can be used in inventory and user details

            InsertInventory iv = new InsertInventory();
            this.Hide();
            iv.ShowDialog();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            form1.Form3 f3 = new form1.Form3();
            this.Hide();
            f3.ShowDialog();
            this.Close();
        }
    }
}
