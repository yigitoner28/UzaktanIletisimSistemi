using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;

namespace UzaktanIletisimSistemi
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }  
        // Sql Bağlantı metni 
        static string conString1 = "Data Source=DESKTOP-73K0559;Initial Catalog=DbProje;Integrated Security=True";
        SqlConnection connect1 = new SqlConnection(conString1);

        //TextBox1'in Renk değişimi kodları
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try// çalışması halinde  try içine giriliyor
            {

                if (textBox1.Text == "")//eğer textbox1 boş ise 
                {
                    textBox1.Text = "Enter username";//içerideki "Enter username" yazısı yazıyor
                    return;
                }
                textBox1.ForeColor = Color.White;// Yazı beyaz renkte
                panel5.Visible = false;//Panel 5 teki uyarı yazısı kapanıyor
            }
            catch
            {

            }
        }

        //TextBox1'in Renk değişimi kodları
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try// çalışması halinde  try içine giriliyor
            {

                if (textBox2.Text == "")//eğer textbox2 boş ise 
                {
                    textBox2.Text = "Password";//içerideki "Password" yazısı yazıyor
                    return;
                }
                textBox2.ForeColor = Color.White;// Yazı beyaz renkte
                textBox2.PasswordChar = '*';// şifre * karakteri şeklini almasını sağlıyor
                panel7.Visible = false;//panel 7 deki uyarı yazısı kapanıyor

            }
            catch
            {

            }
        }

        //TextBox1'in Tamamının seçip silme işlemi
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();// Etkileşim halinde textbox1 içindeki verilerin hepsini seçiyor
        }

        //TextBox2'in Tamamının seçip silme işlemi
        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.SelectAll();// Etkileşim halinde textbox2 içindeki verilerin hepsini seçiyor
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
            if (textBox1.Text == "" || textBox1.Text == "Enter username")// textbox1 doğru olmayan tipte ise 
            {
                panel5.Visible = true;
                textBox1.Focus();
                return;
            }
            if (textBox2.Text == "" || textBox2.Text == "Password")// textbox2 doğru olmayan tipte ise
            {
                panel7.Visible = true;
                textBox2.Focus();
                return;
            }
            connect1.Open();
            string userName;
            string password;
            userName = textBox1.Text;
            password = textBox2.Text;

            //Verinin veri tabanında olup olmadığını belirlemek için kullanıldı
            SqlCommand komut2 = new SqlCommand("select*from Yonetici where kullanici_adi_yonetici='" + userName + "' and sifre_yonetici='" + password + "'", connect1);
            SqlDataReader oku2 = komut2.ExecuteReader();


            if (oku2.Read())// komut doğru ise
            {
                MessageBox.Show("Hoşgeldiniz " + userName + "");
                Form5 frm5 = new Form5();
                Form1 frm1 = new Form1();
                frm5.Show();
                this.Hide();
                frm1.Close();
            }
            else//komut doğru değilse
            {
                MessageBox.Show("Hatalı kullanici adı veya şifre...");
            }
            connect1.Close();



        }

        //Formu kapatma butonu
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Normal kullanıcı giris ekranı
        private void linkLabelBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 form1 = new Form1();  
            form1.Show();
            this.Close();// giriş sayfasına yönlendirme
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

        //Şifre gösterme işlemi
        private void btnShow_Click(object sender, EventArgs e)
        {
            if (textBox2.PasswordChar == '*')// şifrenin doğru formatta gözükmesini sağlar
            {
                btnHide.BringToFront();
                textBox2.PasswordChar = '\0';
            }
        }
    }
}
