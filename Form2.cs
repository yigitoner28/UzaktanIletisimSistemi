using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;


namespace UzaktanIletisimSistemi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
      
        //Back Labeli vesilesi ile ekranı kapatma işlemi 
        private void linkLabelBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        //Mail atma validasyonu Bu buton olayı sayesinde gmail domainine sahip kişilere şifre iletilerek Kullanıcının veri tabanına kayıtlı şifresi hatırlatılmaktadır
        private void button1_Click(object sender, EventArgs e)
        {

           
            //User name Yani Kullanıcı Adı Kısmının Boş olup olmadığını kontrol eder
            if (textBox2.Text == "")
                MessageBox.Show("Lütfen Kullanıcı adını  doldurunuz");

            //Mail Adresi kısmının boş olup olmadığını kontrol eder
            if (textBox1.Text == "")
                MessageBox.Show("Lütfen Mail adresi kısmını doldurunuz");

            //Eğer Boş değil ve veri tabanında mevcut ise Mail yollama yani Şifre hatırlatmayı hedef maile yollar(gmail)
            else if(textBox1.Text!="" && textBox2.Text!="" && this.textBox1.Text.Contains('@'))
            {
                //sql baglantisi sınıfından aldığımız ve olusturduğumuz bgln nesnesi ile veri tabanı bağlantımız oluştu
                sqlbaglantisi bgln = new sqlbaglantisi();
                SqlCommand komut = new SqlCommand("Select * From Bilgi Where kullanici_adi='" + textBox2.Text.ToString() +
                    "'and eposta= '" + textBox1.Text.ToString() + "'", bgln.baglanti());

                //Data reader sayesinde verilerimizi alabilmekle mütevellit kontrol de edebiliyoruz
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    try
                    {
                        if (bgln.baglanti().State == ConnectionState.Closed)
                        {
                            bgln.baglanti().Open();
                        }
                        SmtpClient smtpserver = new SmtpClient();
                        MailMessage mail = new MailMessage();
                        String tarih = DateTime.Now.ToLongDateString();
                        String mailadresin = ("mariusy28@gmail.com");
                        String sifre = ("y253910m");
                        String smptsrvr = "smtp.gmail.com";
                        String kime = (oku["eposta"].ToString());
                        String konu = ("Şifre Hatırlatma Maili");
                        String yaz = ("Sayın," + oku["kullanici_adi"].ToString() + "\n" + "Bizden" + tarih + " Tarihinde Şifre Hatırlatma Talebinde" +
                            " Bulundunuz" + "\n" + "Parolanız:" + oku["sifre"].ToString() + "\nİyi Günler");
                        smtpserver.Credentials = new NetworkCredential(mailadresin, sifre);
                        smtpserver.Port = 587;
                        smtpserver.Host = smptsrvr;
                        smtpserver.EnableSsl = true;
                        mail.From = new MailAddress(mailadresin);
                        mail.To.Add(kime);
                        mail.Subject = konu;
                        mail.Body = yaz;
                        smtpserver.Send(mail);
                        DialogResult bilgi = new DialogResult();
                        bilgi = MessageBox.Show("Girmiş Olduğunuz Bilgiler Uyuşuyor . Şifreniz Mail Adresinize Gönderilmiştir.");
                        this.Hide();

                    }
                    catch (Exception Hata)
                    {
                        MessageBox.Show("Mail Gönderme Hatası!", Hata.Message);// şu an hata vermesinin nedeni google yandex protonmail gibi düşük güvenlik özelliği desteğini kapatmasıdır.
                    }

                }
            }

            else
            {
                if (!this.textBox1.Text.Contains('@'))
                    MessageBox.Show("Lütfen Mailinizi düzgün giriniz ");
            }
        }
    }
}
