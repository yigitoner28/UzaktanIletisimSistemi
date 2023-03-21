﻿using System;
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

            /*
                 if (textBox1.Text == "Enter username")
                 {
                     panel5.Visible = true;
                     textBox1.Focus();
                     return;
                 }

                 if  (textBox2.Text == "Password")
                 {
                     panel7.Visible = true;
                     textBox2.Focus();
                     return;

                 }
             */
            // Şifre Kontrolu 
            /*
             if(textBox1.Text=="Enter username" || textBox2.Text == "Password")
             {
                 if (textBox1.Text == "Enter username")
                 {
                     panel5.Visible = true;
                     textBox1.Focus();
                     return;
                 }

                 else if (textBox2.Text == "Password")
                 {
                     panel7.Visible = true;
                     textBox2.Focus();
                     return;

                 }
             }

             else if(textBox1.Text!="Enter username"&& textBox2.Text != "Password")
             {
                 Form3 frm3 = new Form3();
                 frm3.Show();
             }

             */
            if (textBox1.Text == "" || textBox1.Text == "Enter username")
            {
                panel5.Visible = true;
                textBox1.Focus();
                return;
            }
            if (textBox2.Text== ""|| textBox2.Text == "Password")
            {
                panel7.Visible = true;
                textBox2.Focus();
                return;
            }

            connect.Open();
            string userName;
            string password;
            userName = textBox1.Text;
            password = textBox2.Text;

            SqlCommand komut2 = new SqlCommand("select*from Bilgi where kullanici_adi='" + userName + "' and sifre='" + password + "'", connect);
            SqlDataReader oku2 = komut2.ExecuteReader();
            if (oku2.Read())
            {
                MessageBox.Show("Hoşgeldiniz " + userName + "");
                Form3 frm3 = new Form3();
                frm3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı kullanici adı veya şifre...");
            }
            connect.Close();


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

        // kayıt atma işlemi
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                if(connect.State== ConnectionState.Closed)
                    connect.Open();
             
                string kayit = "insert into bilgi (kullanici_adi,eposta,sifre) values (@kullanici_ad,@epostaa,@sifree)";
                SqlCommand komut = new SqlCommand(kayit,connect);

                komut.Parameters.AddWithValue("@kullanici_ad", textBox3.Text);

                komut.Parameters.AddWithValue("@epostaa", textBox4.Text);

                komut.Parameters.AddWithValue("@sifree", textBox5.Text);

                komut.ExecuteNonQuery();

                connect.Close();
                MessageBox.Show("kayit eklendi");

               

            }
            catch(Exception hata)
            {
                MessageBox.Show("Hata meydana geldi" + hata.Message);
            }

            if(textBox3.Text == "Enter username" ||textBox3.Text=="")
            {
                pnlUsername.Visible = true;
                textBox3.Focus();
                textBox3.SelectAll();
                return;

            }

            if (textBox4.Text == "Enter Mail Address" || textBox4.Text == "")
            {
                pnlMailAddress.Visible = true;
                textBox4.Focus();
                textBox4.SelectAll();
                return;

            }
            if (textBox5.Text == "Enter Password" || textBox4.Text == "")
            {
                pnlPassword.Visible = true;
                textBox5.Focus();
                textBox5.SelectAll();
                return;

            }
            //if (textBox6.Text == " Confirm Password")
            //{
            //    pnlCPassword.Visible = true;
            //    textBox6.Focus();
            //    textBox6.SelectAll();
            //    return;

            //}


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlUsername.Visible = pnlMailAddress.Visible = pnlPassword.Visible = false;
            // pnlCPassword.Visible

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

        //private void textBox6_TextChanged(object sender, EventArgs e)
        //{
        //    textBox6.ForeColor = Color.White;
        //}

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
