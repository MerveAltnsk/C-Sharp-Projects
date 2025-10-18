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

namespace Not_Kayit_Sistemi 
{
    public partial class FrmOgretmenDetay : Form
    {
        public FrmOgretmenDetay()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Server=localhost;Database=DbNotKayıt;Integrated Security=True;Encrypt=False");

        //SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=DbNotKayıt;Integrated Security=True;Encrypt=False");

        private void FrmOgretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayıtDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);

            baglanti.Open();

            // Geçen sayısı (DURUM = 1)
            SqlCommand komut1 = new SqlCommand("SELECT COUNT(*) FROM TBLDERS WHERE DURUM = 1", baglanti);
            int gecenSayisi = (int)komut1.ExecuteScalar();
            LblGecenSayisi.Text = gecenSayisi.ToString();

            // Kalan sayısı (DURUM = 0)
            SqlCommand komut2 = new SqlCommand("SELECT COUNT(*) FROM TBLDERS WHERE DURUM = 0", baglanti);
            int kalanSayisi = (int)komut2.ExecuteScalar();
            LblKalanSayisi.Text = kalanSayisi.ToString();

            // Sınıf ortalaması
            SqlCommand komut3 = new SqlCommand("SELECT AVG(ORTALAMA) FROM TBLDERS", baglanti);
            object sonuc = komut3.ExecuteScalar();
            double sinifOrtalama = Convert.ToDouble(sonuc);
            LblOrtalama.Text = sinifOrtalama.ToString("0.00"); // 2 ondalıklı gösterim

            baglanti.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3)", baglanti);
            komut.Parameters.AddWithValue("@p1", MskNumara.Text);
            komut.Parameters.AddWithValue("@p2", TxtAd.Text);  
            komut.Parameters.AddWithValue("@p3", TxtSoyad.Text);
            komut.ExecuteNonQuery();  //İşlemleri, sorguyu gerçekleştirir.
            baglanti.Close();
            MessageBox.Show("Öğrenci Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            MskNumara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();

            TxtSinav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;

            s1 = Convert.ToDouble(TxtSinav1.Text);
            s2 = Convert.ToDouble(TxtSinav2.Text);
            s3 = Convert.ToDouble(TxtSinav3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            LblOrtalama.Text = ortalama.ToString();

            if (ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1=@p1, OGRS2=@p2, OGRS3=@p3, ORTALAMA=@p4, DURUM=@p5 where OGRNUMARA=@p6", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtSinav1.Text);
            komut.Parameters.AddWithValue("@p2", TxtSinav2.Text);
            komut.Parameters.AddWithValue("@p3", TxtSinav3.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(LblOrtalama.Text));
            komut.Parameters.AddWithValue("@p5", durum);
            komut.Parameters.AddWithValue("@p6", MskNumara.Text);
            komut.ExecuteNonQuery();  //İşlemleri, sorguyu gerçekleştirir.
            baglanti.Close();
            MessageBox.Show("Notlar Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
        }

        private void LblGecenSayisi_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM TBLDERS WHERE DURUM = 1", baglanti);
            int gecenSayisi = (int)komut.ExecuteScalar();
            LblGecenSayisi.Text = gecenSayisi.ToString();
            baglanti.Close();
        }

    }
}
