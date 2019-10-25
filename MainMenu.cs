using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Inventory iv = new Inventory();
            this.Hide();
            iv.ShowDialog();
            this.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            string msg = "Are you sure you want to log out of the application? All unsaved changes will be lost";
            string title = "Confirm Log Out";

            //MessageBoxButtons buttons = new MessageBoxButtons();
            DialogResult res = MessageBox.Show(msg, title, MessageBoxButtons.YesNoCancel);

            if(res == DialogResult.Yes)
            {
                Welcome wm = new Welcome();
                this.Hide();
                wm.ShowDialog();
                this.Close();
            }
        }
    }
}
