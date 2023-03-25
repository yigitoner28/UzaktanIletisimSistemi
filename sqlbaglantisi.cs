using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace UzaktanIletisimSistemi
{
    class sqlbaglantisi
    {
        //Sql Bağlantısı ; Bu sınıfı ayrı tanımlama nedeni connection stringi Karışık kod satırları arasında bulunmaması gerekmesi
        public SqlConnection baglanti()
        {
            SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-73K0559;Initial Catalog=DbProje;Integrated Security=True");
            baglanti.Open();
            return baglanti;
        
        
        }
    }

       


}

