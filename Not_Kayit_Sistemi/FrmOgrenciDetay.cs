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
//using Microsoft.Data.SqlClient;

namespace Not_Kayit_Sistemi
{
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }

        public string numara;
        SqlConnection baglanti = new SqlConnection(@"Server=localhost;Database=DbNotKayıt;Integrated Security=True;Encrypt=False");

        //SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=DbNotKayıt;Integrated Security=True;Encrypt=False");

        //SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=DbNotKayıt;Integrated Security=True;Trust Server Certificate=True");
        // Data Source=localhost;Initial Catalog=DbNotKayıt;Integrated Security=True;Trust Server Certificate=True

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            LblNumara.Text = numara;

            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select * From TBLDERS where OGRNUMARA=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                LblAdSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                LblSınav1.Text = dr[4].ToString();
                LblSınav2.Text = dr[5].ToString();
                LblSınav3.Text = dr[6].ToString();
                LblOrtalama.Text = dr[7].ToString();


                object durumObj = dr[8];
                if (durumObj != DBNull.Value)
                {
                    bool durum = Convert.ToBoolean(durumObj);
                    if (durum)
                    {
                        LblDurum.Text = "Geçti";
                        LblDurum.ForeColor = Color.Green;
                    }
                    else
                    {
                        LblDurum.Text = "Kaldı";
                        LblDurum.ForeColor = Color.Red;
                    }
                }
                else
                {
                    LblDurum.Text = "Bilgi yok";
                    LblDurum.ForeColor = Color.Gray;
                }


            }
            baglanti.Close();

        }

      
    }
}
