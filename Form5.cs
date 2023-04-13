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
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Contexts;

namespace UzaktanIletisimSistemi
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        
        SqlConnection baglanti1 = new SqlConnection("Data Source=DESKTOP-73K0559;Initial Catalog=DbProje;Integrated Security=True");


        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti1);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];

             
        }

        //Tüm Kullanıcıları Listele
        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("Select * from Bilgi");
        }



        bool durum;
        void mukerrer()// Tekrarı engellemeyi sağlayan metot
        {
            baglanti1.Open();
            SqlCommand komut3 = new SqlCommand("select * from Bilgi where kullanici_adi=@p1", baglanti1);
            komut3.Parameters.AddWithValue("@p1", textBox2.Text);
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


        // Kayıt Ekleme İşlemi
        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && this.textBox3.Text.Contains('@'))
            {
                mukerrer();
                if (durum == true)
                {
                    baglanti1.Open();
                    SqlCommand komut = new SqlCommand("insert into Bilgi (kullanici_adi,eposta,sifre) values (@adi,@epostasi,@sifresi)", baglanti1);
                    komut.Parameters.AddWithValue("@adi", textBox2.Text);
                    komut.Parameters.AddWithValue("@epostasi", textBox3.Text);
                    komut.Parameters.AddWithValue("@sifresi", textBox4.Text);
                    komut.ExecuteNonQuery();
                    verilerigoster("Select * from Bilgi ");
                    baglanti1.Close();

                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox2.Focus();
                    MessageBox.Show("Ekleme işlemi gerçekleşti");
                }
                else
                {
                    MessageBox.Show("Kayıt zaten mevcut");
                }
                /*
                baglanti1.Open();
                SqlCommand komut = new SqlCommand("insert into Bilgi (kullanici_adi,eposta,sifre) values (@adi,@epostasi,@sifresi)", baglanti1);
                komut.Parameters.AddWithValue("@adi", textBox2.Text);
                komut.Parameters.AddWithValue("@epostasi", textBox3.Text);
                komut.Parameters.AddWithValue("@sifresi", textBox4.Text);
                komut.ExecuteNonQuery();
                verilerigoster("Select * from Bilgi ");
                baglanti1.Close();

                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox2.Focus();
                MessageBox.Show("Ekleme işlemi gerçekleşti");
                */
            }


            /*
            baglanti1.Open();
            SqlCommand komut = new SqlCommand("insert into Bilgi (kullanici_adi,eposta,sifre) values (@adi,@epostasi,@sifresi)", baglanti1);
            komut.Parameters.AddWithValue("@adi", textBox2.Text);
            komut.Parameters.AddWithValue("@epostasi", textBox3.Text);
            komut.Parameters.AddWithValue("@sifresi", textBox4.Text);
            komut.ExecuteNonQuery();
            verilerigoster("Select * from Bilgi ");
            baglanti1.Close();

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox2.Focus();
            */


            else
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Lütfen Kullanıcı Adını Boş girmeyin");
                }


                else if (textBox3.Text == "")
                {
                    MessageBox.Show("Lütfen Mail Adresini Boş girmeyin");
                }

                else if (textBox4.Text == "")
                {
                    MessageBox.Show("Lütfen Şifreyi Boş girmeyin");
                }

                else if (!this.textBox3.Text.Contains('@'))
                {
                    MessageBox.Show("Lütfen Doğru Formatta Mail girin");
                }


                else
                {
                    
                }
            }



        }

        // Kullanıcı Sil
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                baglanti1.Open();
                SqlCommand komut = new SqlCommand("delete from Bilgi where id=@id", baglanti1);
                komut.Parameters.AddWithValue("@id", textBox1.Text);
                komut.ExecuteNonQuery();
                verilerigoster("Select * from Bilgi ");
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

        // Kullanıcıyı filtrele
        private void button4_Click(object sender, EventArgs e)
        {
            baglanti1.Open();
            SqlCommand komut = new SqlCommand("Select * from Bilgi where kullanici_adi like '%"+textBox5.Text+ "%'", baglanti1);
            SqlDataAdapter daFiltre = new SqlDataAdapter(komut);
            DataSet dsFiltre = new DataSet();
            daFiltre.Fill(dsFiltre);
            dataGridView1.DataSource = dsFiltre.Tables[0];
            baglanti1.Close();


        }

        //data Grid Seçme İşlemi
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int seciliAlan = dataGridView1.SelectedCells[0].RowIndex;
            string ad = dataGridView1.Rows[seciliAlan].Cells[1].Value.ToString();
            string eposta = dataGridView1.Rows[seciliAlan].Cells[2].Value.ToString();
            string sifre = dataGridView1.Rows[seciliAlan].Cells[3].Value.ToString();


            textBox2.Text = ad;
            textBox3.Text = eposta;
            textBox4.Text = sifre;
        }


        //Kullanıcıyı güncelle
        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox2.Text!=""&& textBox3.Text != "" && textBox4.Text != ""&& this.textBox3.Text.Contains('@'))
            {
               
                baglanti1.Open();
                SqlCommand komut = new SqlCommand(" update Bilgi set kullanici_adi='" + textBox2.Text + "',eposta='" + textBox3.Text + "',sifre='" + textBox4.Text + "' where id='" + dataGridView1.SelectedCells[0].Value.ToString().ToString() + "'", baglanti1);
                komut.ExecuteNonQuery();
                verilerigoster(" select * from Bilgi");

                baglanti1.Close();
                MessageBox.Show("Kayıt Güncellendi");
                
            }
           
            else if(textBox2.Text == "" || textBox3.Text == "" || !this.textBox3.Text.Contains('@') || textBox4.Text == "")
            {
                if (textBox2.Text == "")
                {
                    MessageBox.Show("Kullanıcı adını boş girmeyin");
                    textBox2.Focus();
                }
                if(textBox3.Text == "")
                {
                    MessageBox.Show("Epostayı  boş girmeyin");
                    textBox3.Focus();
                }
                if (!this.textBox3.Text.Contains('@'))
                {
                    MessageBox.Show("Epostayı doğru formatta giriniz");
                    textBox3.Focus();
                }
                if (textBox4.Text == "")
                {
                    MessageBox.Show("Kullanıcı şifresini  boş girmeyin");
                    textBox4.Focus();
                }


            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Exit();
        }
    }
}

