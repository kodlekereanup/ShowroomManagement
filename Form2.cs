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

namespace form1
{
    public partial class Form2 : Form
    {
        OracleConnection con;
        OracleCommand cmd;
        OracleDataReader rd;
        string query;

        public Form2()
        {
            InitializeComponent();
            con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
        }

        String getSalt(String username)
        {
            OracleConnection con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "select salt from signup where username='" + username + "'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader rd = cmd.ExecuteReader();
            rd.Read();
            return rd["salt"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form3 main = new Form3();
            //this.Hide();
            //main.ShowDialog(this);
            WindowsFormsApp2.SignUp su = new WindowsFormsApp2.SignUp();
            if (textBox1.Text == "")
            {
                MessageBox.Show("Username cannot be empty!");

            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Password cannot be empty!");
            }
            else
            {
                int flag = 0;
                String databasePass = "";
                query = "select * from signup where username='" + textBox1.Text + "' ";
                cmd = new OracleCommand(query, con);

                con.Open();
                rd = cmd.ExecuteReader();

                while (rd.Read())
                {
                    databasePass = rd["password"].ToString();
                    flag = 1;
                }

                rd.Close();
                con.Close();

                if (flag == 0)
                {
                    MessageBox.Show("Username doesn't exist! Please try again!");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();

                }
                // change this to accomodate ith hashing
                // textbox2.text should be hashed and matched with str
                // 1. Find the username and his corresponding salt
                // 2. concatenate the salt with textbox pass and hash
                // 3. compare hash with signup hash
                else if (su.GenerateSHA512Hash(textBox2.Text, getSalt(textBox1.Text)) == databasePass)
                {

                    MessageBox.Show("Login Successfull!");
                    Form3 mm = new Form3();
                    this.Hide();
                    mm.ShowDialog();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Username and Password doesn't match! Please enter correct username and password");
                }

            }
        }
    }
}
