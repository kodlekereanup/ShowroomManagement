using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.OleDb;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace WindowsFormsApp2
{
    public partial class InsertInventory : Form
    {

        bool insert = false, update = false, delete = false, change = false;
       
        public InsertInventory()
        {
            InitializeComponent();
           
        }
        
        private bool searchValueInDatabase(int id)
        {
            OracleConnection con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "select count(*) from inventory where id=" + id;
            cmd.CommandType = CommandType.Text;
            string recordExists = cmd.ExecuteScalar().ToString();
            con.Close();
            return (recordExists == "1") ? true : false;
        }

        public void changeFieldState(bool state1 = true, bool state2 = true, bool state3 = true, bool state4 = true)
        {
            textBox1.ReadOnly = state1;
            textBox2.ReadOnly = state2;
            textBox3.ReadOnly = state3;
            textBox4.ReadOnly = state4;

        }

        
        public void insertRecords()
        {
            // button for inserting data into database
            OracleConnection con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "insert into inventory values(id.nextval,'" + textBox2.Text + "'," + "'" + textBox3.Text + "'," + textBox4.Text + ")";
            //cmd.CommandType = CommandType.Text;
            int rw = cmd.ExecuteNonQuery();
            if (rw == 0)
            {
                MessageBox.Show("Unsuccessfull");
            }
            else
            {
                MessageBox.Show("Successfull!");
            }
            con.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // insert records button was pressed
            insert = true;
            update = false;
            delete = false; 

            changeFieldState(true, false, false, false);
                        
        }

        private void InsertInventory_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet1.INVENTORY' table. You can move, or remove it, as needed.
            this.iNVENTORYTableAdapter.Fill(this.dataSet1.INVENTORY);

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            InsertInventory_Load(sender, e);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            MainMenu mm = new MainMenu();
            this.Hide();
            mm.ShowDialog();
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            // takes in the empid and updates records
            update = true;
            insert = false;
            delete = false;

            MessageBox.Show("Please enter the ID of the row you want to update");
            changeFieldState(false);
           
            
        }

        public void updateRecords(int id)
        {
            OracleConnection con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "update inventory set model='" + textBox2.Text + "',type='" + textBox3.Text + "',amount=" + textBox4.Text + " where id=" + textBox1.Text;
            //cmd.CommandType = CommandType.Text;
            int rw = cmd.ExecuteNonQuery();
            if (rw == 0)
            {
                MessageBox.Show("Unsuccessfull");
            }
            else
            {
                MessageBox.Show("Successfull!");
            }
            con.Close();
            // "update inventory set model=textbox2 
        }

        public void deleteRecords(int id)
        {
            string msg = "Are you sure you want to delete record?";
            string title = "Delete Data";

            //MessageBoxButtons buttons = new MessageBoxButtons();
            DialogResult res = MessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel);

            if (res == DialogResult.Yes)
            {
                OracleConnection con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "delete from inventory where id=" + textBox1.Text;
                //cmd.CommandType = CommandType.Text;
                int rw = cmd.ExecuteNonQuery();
                if (rw == 0)
                {
                    MessageBox.Show("Unsuccessfull");
                }
                else
                {
                    MessageBox.Show("Successfull!");
                }
                con.Close();
            }

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (insert && !update && !delete)
            {
                insertRecords();

            }
            else if (update && !insert && !delete)
            {
                int id = Int16.Parse(textBox1.Text);
                updateRecords(id);

            }
            else if (delete && !insert && !update)
            {
                int id = Int16.Parse(textBox1.Text);
                deleteRecords(id);

            }


            changeFieldState();
            change = false;
        }

        public void enableButton()
        {
            if(textBox2.Text.Length > 0 && textBox3.Text.Length > 0 && textBox4.Text.Length > 0)
            {
                change = true;
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            
            if(textBox1.Text.Length > 0)
            {
                button7.Enabled = true;
            }
            // check if id exists in the ID column of the inventory table
            // if it exists, make the other textboxes visible
            // else show a messagebox saying not found
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            int id = Int16.Parse(textBox1.Text);
            if (searchValueInDatabase(id) && update)
            {

                MessageBox.Show("Record Found! Edit Textboxes to insert the new values");
                changeFieldState(true, false, false, false);

                // now retrieve the data for respective textboxes from the database and insert into respective textboxes
                OracleConnection con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from inventory where id=" + id;
                cmd.CommandType = CommandType.Text;
                OracleDataReader rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    textBox2.Text = rd["MODEL"].ToString();
                    textBox3.Text = rd["TYPE"].ToString();
                    textBox4.Text = rd["AMOUNT"].ToString();
                }
                con.Close();
            }
            else if(searchValueInDatabase(id) && delete)
            {
                MessageBox.Show("Record Found! Press Delete to remove record from Database");
                button6.Text = "DELETE";
                button6.Enabled = true;

            }
            else MessageBox.Show("Entered ID doesn't exist in table.");
           
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            update = false;
            insert = false;
            delete = true;

            MessageBox.Show("Please enter the ID of the row you want to delete");
            changeFieldState(false);
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            enableButton();
        }
        
        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            enableButton();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            enableButton();
            if (change)
            {
                if(insert && !update && !delete)
                {
                    button6.Text = "INSERT";
                    button6.Enabled = true;

                }
                else if(update && !insert && !delete)
                {
                    button6.Text = "UPDATE";
                    button6.Enabled = true;

                }
                
            }
        }


        // string test = "insert into inventory values(id.nextval, 'textbox1.text', 'textbox2.text', textbox3.text)";
    }
}

// enable button function checks if all three textboxes are having some data