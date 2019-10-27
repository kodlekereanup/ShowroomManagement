using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace form1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Visible == true)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
            else if (pictureBox2.Visible == true)
            {
                pictureBox2.Visible = false;
                pictureBox3.Visible = true;
            }
            else if (pictureBox3.Visible == true)
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = true;
            }
            else if (pictureBox4.Visible == true)
            {
                pictureBox4.Visible = false;
                pictureBox1.Visible = true;
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowsFormsApp2.Inventory iv = new WindowsFormsApp2.Inventory();
            this.Hide();
            iv.ShowDialog();
            this.Close();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // log out from main menu
            Form2 ff = new Form2();
            this.Hide();
            ff.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 service = new Form4();
            this.Hide();
            service.ShowDialog();
            this.Close();
        }
    }
}
