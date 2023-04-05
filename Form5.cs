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


        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoster("Select * from Bilgi");
        }

        private void button2_Click(object sender, EventArgs e)
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

        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti1.Open();
            SqlCommand komut = new SqlCommand("delete from Bilgi where id=@id", baglanti1);
            komut.Parameters.AddWithValue("@id", textBox1.Text);
            komut.ExecuteNonQuery();
            verilerigoster("Select * from Bilgi ");
            baglanti1.Close();

            textBox1.Clear();
            
        }

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

        private void button5_Click(object sender, EventArgs e)
        {
            baglanti1.Open();
            SqlCommand komut = new SqlCommand(" update Bilgi set kullanici_adi='" + textBox2.Text + "',eposta='" + textBox3.Text + "',sifre='" + textBox4.Text + "' where id='" + dataGridView1.SelectedCells[0].Value.ToString().ToString() + "'", baglanti1);
            komut.ExecuteNonQuery();
            verilerigoster(" select * from Bilgi");
            
            baglanti1.Close();
        
        }
    }
}
