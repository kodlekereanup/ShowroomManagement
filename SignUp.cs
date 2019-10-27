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
using System.Security.Cryptography;

namespace WindowsFormsApp2
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        public String createSalt(int size)
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buffer = new byte[size];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public String GenerateSHA512Hash(String input, String salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA512Managed sha512 =
                new System.Security.Cryptography.SHA512Managed();
            byte[] hash = sha512.ComputeHash(bytes);
            
            return BitConverter.ToString(hash).Replace("-", "");
        }

        public bool exists(String username)
        {
            bool flag = false;
            OracleConnection con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
            con.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from signup where username='" + textBox1.Text + "'";
            cmd.CommandType = CommandType.Text;
            OracleDataReader rd = cmd.ExecuteReader();
            
            while(rd.Read())
            {
                if (rd["username"].ToString() == textBox1.Text)
                {
                    flag = true;
                    break;
                }
                else flag = false;
            }
            rd.Close();
            con.Close();

            return flag;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Sign Up form button
            // Sign up rules:
            // Username should be atleast 6 characters long
            // password and confirm password should match and shouod be
            // at least 8 characters long

            // if username already exists, throw messagebox
            // new user is assigned an eid automatically

            String username = textBox1.Text;
            String password = textBox2.Text;
            String confirmPassword = textBox3.Text;

            //check for nulls
            if(username == "")
            {
                MessageBox.Show("Username cannot be empty!");
            }
            else if(password == "" || confirmPassword == "")
            {
                MessageBox.Show("Please Enter a Password");
            }
            else if(username.Length < 6)
            {
                MessageBox.Show("Username should be at least 6 characters long");
            }
            else if(password.Length < 8)
            {
                MessageBox.Show("Password should be atleast 8 characters long");
            }
            else if(password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match!");
            }
            else if(exists(username))
            {
                MessageBox.Show("Username already exists!");
            }
            else
            {
                //TODO:
                // add a salt to password
                // store the salt and the salted password in the signup database
                String salt = createSalt(10);
                String hashedPass = GenerateSHA512Hash(textBox2.Text, salt);
                OracleConnection con = new OracleConnection("Data Source=localhost;User Id=PROJECT;Password=anup;");
                con.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "insert into signup values(ei.nextval,'" + textBox1.Text + "'," + "'" + salt + "','" + hashedPass + "')";
                cmd.CommandType = CommandType.Text;
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

        private void Button2_Click(object sender, EventArgs e)
        {
            form1.Form2 fn = new form1.Form2();
            this.Hide();
            fn.ShowDialog();
            this.Close();
        }
    }
}
