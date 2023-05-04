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

namespace UzaktanIletisimSistemi
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        SqlConnection baglanti1 = new SqlConnection("Data Source=DESKTOP-73K0559;Initial Catalog=DbProje;Integrated Security=True");

        //Verileri Gösterme  DataGride
        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];


        }

        //Kullanıcıları Göster Butonu
        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("Select * from Yonetici");
        }


        bool durum;
        void mukerrer()// Tekrarı engellemeyi sağlayan metot
        {
            baglanti1.Open();
            SqlCommand komut3 = new SqlCommand("select * from Yonetici where kullanici_adi_yonetici=@p1", baglanti1);
            komut3.Parameters.AddWithValue("@p1", textBox1.Text);
            //Kullanıcı adını veri tabanında sorgulayıp eşleştirmeyi sağlamak içindir
            SqlDataReader dr = komut3.ExecuteReader();

            if (dr.Read())//komut 3 başarılı ise
            {
                durum = false;//veri tabanında var ise
            }
            else//komut 3 başarılı değil ise
            {
                durum = true;// veri tabanında yok ise
            }
            baglanti1.Close();
        }
       
        
        
        // Ad Filtreleme Butonu
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti1.Open();
            SqlCommand komut = new SqlCommand("Select * from Yonetici where kullanici_adi_yonetici like '%" + textBox5.Text + "%'", baglanti1);
            SqlDataAdapter daFiltre = new SqlDataAdapter(komut);
            DataSet dsFiltre = new DataSet();
            daFiltre.Fill(dsFiltre);
            dataGridView1.DataSource = dsFiltre.Tables[0];
            baglanti1.Close();




        }


        // Kayıt ekleme İşlemi Butonu
        private void button3_Click(object sender, EventArgs e)
        {


            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && this.textBox2.Text.Contains('@'))
            {
                mukerrer();
                if (durum == true)
                {
                    baglanti1.Open();
                    SqlCommand komut = new SqlCommand("insert into Yonetici (kullanici_adi_yonetici,eposta_yonetici,sifre_yonetici) values (@adiYonetici,@epostasiYonetici,@sifresiYonetici)", baglanti1);
                    komut.Parameters.AddWithValue("@adiYonetici", textBox1.Text);
                    komut.Parameters.AddWithValue("@epostasiYonetici", textBox2.Text);
                    komut.Parameters.AddWithValue("@sifresiYonetici", textBox3.Text);
                    komut.ExecuteNonQuery();
                    verilerigoster("Select * from Yonetici ");
                    baglanti1.Close();

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox2.Focus();
                    MessageBox.Show("Ekleme işlemi gerçekleşti");
                }
                else
                {
                    MessageBox.Show("Kayıt zaten mevcut");
                }
               
            }

                         

            else
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Lütfen Kullanıcı Adını Boş girmeyin");
                }


                else if (textBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Mail Adresini Boş girmeyin");
                }

                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Lütfen Şifreyi Boş girmeyin");
                }

                else if (!this.textBox2.Text.Contains('@'))
                {
                    MessageBox.Show("Lütfen Doğru Formatta Mail girin");
                }


                else
                {

                }
            }
        }

        // Kayıt silme 
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox4.Text != "")
            {
                baglanti1.Open();
                SqlCommand komut = new SqlCommand("delete from Yonetici where id=@id", baglanti1);
                komut.Parameters.AddWithValue("@id", textBox4.Text);
                komut.ExecuteNonQuery();
                verilerigoster("Select * from Yonetici ");
                baglanti1.Close();
                MessageBox.Show("Silme işlemi gerçekleşti");
                textBox1.Clear();
            }
            /*
            baglanti1.Open();
            SqlCommand komut = new SqlCommand("delete from Bilgi where id=@id", baglanti1);
            komut.Parameters.AddWithValue("@id", textBox1.Text);
            komut.ExecuteNonQuery();
            verilerigoster("Select * from Bilgi ");
            baglanti1.Close();

            textBox1.Clear();
            */
            else
            {
                MessageBox.Show("Lütfen Silme işlemini boş geçmeyin");
            }
        }


        // Kayıt Güncelleme
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && this.textBox2.Text.Contains('@'))
            {

                baglanti1.Open();
                SqlCommand komut = new SqlCommand(" update Yonetici set kullanici_adi_yonetici='" + textBox1.Text + "',eposta_yonetici='" + textBox2.Text + "',sifre_yonetici='" + textBox3.Text + "' where id='" + dataGridView1.SelectedCells[0].Value.ToString().ToString() + "'", baglanti1);
                komut.ExecuteNonQuery();
                verilerigoster(" select * from Yonetici");

                baglanti1.Close();
                MessageBox.Show("Kayıt Güncellendi");

            }

            else if (textBox1.Text == "" || textBox2.Text == "" || !this.textBox2.Text.Contains('@') || textBox3.Text == "")
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Kullanıcı adını boş girmeyin");
                    textBox1.Focus();
                }
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Epostayı  boş girmeyin");
                    textBox2.Focus();
                }
                if (!this.textBox2.Text.Contains('@'))
                {
                    MessageBox.Show("Epostayı doğru formatta giriniz");
                    textBox2.Focus();
                }
                if (textBox3.Text == "")
                {
                    MessageBox.Show("Kullanıcı şifresini  boş girmeyin");
                    textBox3.Focus();
                }


            }
        }

        // Data Grid
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seciliAlan = dataGridView1.SelectedCells[0].RowIndex;
            string adYonetici = dataGridView1.Rows[seciliAlan].Cells[1].Value.ToString();
            string epostaYonetici = dataGridView1.Rows[seciliAlan].Cells[2].Value.ToString();
            string sifreYonetici = dataGridView1.Rows[seciliAlan].Cells[3].Value.ToString();


            textBox1.Text = adYonetici;
            textBox2.Text = epostaYonetici;
            textBox3.Text = sifreYonetici;



        }

        // Onceki ekrana geçiş
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.Show();
            this.Close();
        }


        // Sistemi Kapatma
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
    }
}

