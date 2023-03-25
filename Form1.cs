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
        // Sql Bağlantı metni 
        static string conString = "Data Source=DESKTOP-73K0559;Initial Catalog=DbProje;Integrated Security=True";
        SqlConnection connect = new SqlConnection(conString);

        //TextBox1'in Renk değişimi kodları
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

        //TextBox2'in Renk değişimi kodları
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

        //TextBox1'in Tamamının seçip silme işlemi
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        //TextBox2'in Tamamının seçip silme işlemi
        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();
        }

        //Buton1 (sign in) Basma anında rengin siyahtan yeşile geçme kodu
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Black;
        }

        //Buton1 Buton üzerinden ayrılma renk değişikliği
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.ForeColor = Color.Lime;
        }

        //Sign in validasyonun gerçekleştiği yer
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
            // Şifre Kontrolu Burda Hint özelliği ve boş olma durumu kontrol edilir Daha sonrasında Sql deki tablo kontontrolu gerçekleşir
           
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

        //Formu kapatma butonu
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
           
            
        }
        
        // Sign up Butonun renk etkileşimi
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color.FromArgb(30,30,30) ;
            button3.ForeColor = Color.Lime;
        }


        //System.Windows.Forms.Button. = new System.Windows.Forms.Timer();

        // kayıt atma işlemi
        bool durum;
        void mukerrer()
        {
            connect.Open();
            SqlCommand komut3 = new SqlCommand("select * from Bilgi where kullanici_adi=@p1", connect);
            komut3.Parameters.AddWithValue("@p1", textBox3.Text);
            SqlDataReader dr = komut3.ExecuteReader();
           
            if (dr.Read())
            {
                durum= false;//veri tabanında var 
            }
            else
            {
                durum= true;
            }
            connect.Close();
        }

        // Kayıt ekleme ve kontrol validasyonları
        private void button3_Click(object sender, EventArgs e)
        {
            /*
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
            */

            if (textBox3.Text != "Enter username" && textBox4.Text!= "Enter Mail Address" && textBox5.Text != "Enter Password" && this.textBox4.Text.Contains('@'))
            {
                mukerrer();
                if (durum == true)
                {
                    connect.Open();
                    SqlCommand komut3 = new SqlCommand("insert into Bilgi (kullanici_adi,eposta,sifre) values (@p1,@p2,@p3)", connect);

                    komut3.Parameters.AddWithValue("@p1", textBox3.Text);
                    komut3.Parameters.AddWithValue("@p2", textBox4.Text);
                    komut3.Parameters.AddWithValue("@p3", textBox5.Text);
                    komut3.ExecuteNonQuery();
                    connect.Close();
                    MessageBox.Show("Kayıt eklendi");
                }

                else
                {
                    MessageBox.Show("Bu kayıt zaten var", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            else
            {
                if (textBox3.Text == "Enter username" || textBox3.Text == "")
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
                else if (!this.textBox4.Text.Contains('@'))
                {
                    MessageBox.Show("Mail adresi doğru formatta değil");
                }
                
                if (textBox5.Text == "Enter Password" || textBox4.Text == "")
                {
                    pnlPassword.Visible = true;
                    textBox5.Focus();
                    textBox5.SelectAll();
                    return;

                }
            } 




           
           


        }

        //  uyarıları vakitsel olarak göstermeyi sağlar
        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlUsername.Visible = pnlMailAddress.Visible = pnlPassword.Visible = false;
            // pnlCPassword.Visible

        }

        //  açılışı vaktini ayarlar 
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        //textbox3'un etkileşim halinde renk değişimi
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox3.ForeColor= Color.White;
        }
        
        //textbox4'un etkileşim halinde renk değişimi
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            textBox4.ForeColor = Color.White;
        }
        
        //textbox5'un etkileşim halinde renk değişimi
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            textBox5.ForeColor = Color.White;
        }

       
        //Buton3'un sign up'un basma halindeki renk validasyonu
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            button3.ForeColor = Color.Black;
            button3.BackColor = Color.Green;
        }

        
        //Linklabel3 renk validasyonu
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           // pnlsignup.Visible = false;
            pnlLogin.Visible = true;
            pnlLogin.Dock=DockStyle.Fill;
            pnlLogo.Dock=DockStyle.Left;

        }
      
        //Linklabel2 renk validasyonu
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pnlLogin.Visible = false;
            pnlsignup.Visible = true;
            pnlLogo.Dock = DockStyle.Right;

            pnlsignup.Dock = DockStyle.Fill;
            
        }

        //Şifre gösterme işlemi
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')
            {
                btnHide.BringToFront();
                textBox2.PasswordChar = '\0';
            }
        }

        //Şifre gizleme işlemi
        private void btnHide_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '\0')
            {
                btnShow.BringToFront();
                textBox2.PasswordChar = '*';
            }
        }

        //LinkLabel1'in yeni sign up ekranına yönlendirmesi
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '@')
            {
                e.Handled = false;
               
            }
        }
    }
}
