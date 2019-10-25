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
    public partial class Welcome : Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            // Button for redirecting to log in page
            // On press of this button, the employee will be redirected to log in page 

            Login ln = new Login();
            this.Hide();
            ln.ShowDialog();
            this.Close();

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //opens the sign up page
            SignUp register = new SignUp();
            this.Hide();
            register.ShowDialog();
            this.Close();

        }
    }
}
