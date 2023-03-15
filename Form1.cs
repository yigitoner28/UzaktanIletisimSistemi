using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UzaktanIletisimSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static string conString = "Data Source=DESKTOP-73K0559;Initial Catalog=DbProje;Integrated Security=True";
        SqlConnection connect = new SqlConnection(conString);

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (textBox1.Text == "")
                {
                    textBox1.Text = "Enter username";
                    return;
                }
                textBox1.ForeColor= Color.White;
                panel5.Visible= false;
            }
            catch
            {

            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

            try
            {

                if (textBox2.Text == "")
                {
                    textBox2.Text = "Password";
                    return;
                }
                textBox2.ForeColor = Color.White;
                textBox2.PasswordChar= '*';
                panel7.Visible = false;

            }
            catch
            {

            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Lime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Enter username")
            {
                panel5.Visible = true;
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == "Password")
            {
                panel7.Visible = true;
                textBox2.Focus();
                return;

            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
           
            
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(30,30,30) ;
            button3.ForeColor = Color.Lime;
        }


        //System.Windows.Forms.Button. = new System.Windows.Forms.Timer();


        private void button3_Click(object sender, EventArgs e)
        {
            


            if(textBox3.Text == "Enter username")
            {
                pnlUsername.Visible = true;
                textBox3.Focus();
                textBox3.SelectAll();
                return;

            }

            if (textBox4.Text == "Enter Mail Address")
            {
                pnlMailAddress.Visible = true;
                textBox4.Focus();
                textBox4.SelectAll();
                return;

            }
            if (textBox5.Text == "Enter Password")
            {
                pnlPassword.Visible = true;
                textBox5.Focus();
                textBox5.SelectAll();
                return;

            }
            if (textBox6.Text == " Confirm Password")
            {
                pnlCPassword.Visible = true;
                textBox6.Focus();
                textBox6.SelectAll();
                return;

            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlUsername.Visible = pnlMailAddress.Visible = pnlPassword.Visible = pnlCPassword.Visible = false;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.ForeColor= Color.White;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.ForeColor = Color.White;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.ForeColor = Color.White;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            textBox6.ForeColor = Color.White;
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Black;
            button3.BackColor = Color.Green;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = true;
            pnlLogin.Dock=DockStyle.Fill;
            pnlsignup.Visible = false;
            pnlLogo.Dock=DockStyle.Left;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = false;
            pnlsignup.Visible = true;
            pnlLogo.Dock = DockStyle.Right;

            pnlsignup.Dock = DockStyle.Fill;
            
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                btnHide.BringToFront();
                textBox2.PasswordChar = '\0';
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '\0')
            {
                btnShow.BringToFront();
                textBox2.PasswordChar = '*';
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }
    }
}
