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

namespace CarRentalWinForm.SatisYap
{
    public partial class SatisYapForm : Form
    {
        public SatisYapForm()
        {
            InitializeComponent();
        }
        CarRentalEntities db = new CarRentalEntities();
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarRental;Integrated Security=True");

        public void Listele()
        {
            var listeDeger = db.Satis.ToList();
            dataGridView1.DataSource = listeDeger.ToList();
        }
        public void Temizle()
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox)
                {
                    TextBox textKontrol = (TextBox)item;
                    textKontrol.Clear();
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from AracModel,AracMarka where model_plaka like '"+comboBox1.Text+"' ",baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtMarka.Text = read["arac_marka"].ToString();
                txtModel.Text = read["model_adi"].ToString();
                txtRenk.Text = read["model_renk"].ToString();
                txtSeri.Text = read["model_seri"].ToString();
                txtAracModel.Text = read["model_id"].ToString() ; 
            }
            read.Close();
            baglanti.Close();
        }

        private void SatisYapForm_Load(object sender, EventArgs e)
        {
            Listele();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from AracModel where model_durum =1", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["model_plaka"]);
            }
            read.Close();
            baglanti.Close();
           
        }

        private void txtTcNOAra_TextChanged(object sender, EventArgs e)
        {
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Musteri where musteri_tc like '%" + txtTcNOAra.Text + "%' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtTC.Text = read["musteri_tc"].ToString();
                txtAd.Text = read["musteri_ad"].ToString();
                txtSoyad.Text = read["musteri_soyad"].ToString();
                txtTel.Text = read["musteri_tel"].ToString();
                txtMusteriid.Text = read[0].ToString();
            }

            read.Close();
            baglanti.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Satis satis = new Satis();
            satis.müsteri_id = Convert.ToInt16(txtMusteriid.Text);
            satis.model_id = Convert.ToInt16(txtAracModel.Text);
            satis.satis_ehliyet = txtEhliyetNo.Text;
            satis.satis_ehliyetTarih = txtEhliyetTar.Text;
            satis.satis_ehliyetVerYeri = txtEhlVer.Text;
            satis.satis_kiratip=comboBox1.Text;
            satis.satis_verilis = Convert.ToDateTime(dtpCıkıs.Text);
            satis.satis_alis = Convert.ToDateTime(dtpDönüs.Text);
            satis.satis_gun = Convert.ToInt16(txtGün.Text);
            satis.satis_kira = Convert.ToInt16(txtFiyat.Text);
            satis.satis_toplam = Convert.ToInt16(txtFiyat.Text) * Convert.ToInt16(txtGün.Text);
            db.Satis.Add(satis);
            db.SaveChanges();
            MessageBox.Show("Satış Tamamlandı");
            Listele();

        }

        private void dtpDönüs_ValueChanged(object sender, EventArgs e)
        {
            DateTime dtp1 =dtpCıkıs.Value.Date ;
            DateTime dtp2 = dtpDönüs.Value.Date;
            TimeSpan span = dtp2.Subtract(dtp1);

            txtGün.Text = span.TotalDays.ToString();

            int s1 = int.Parse(txtGün.Text);
            int s2 = int.Parse(txtFiyat.Text);
            int s3 = s1 * s2;
            txtTutar.Text = s3.ToString();


        }

        

       

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Günlük")
            {
                txtFiyat.Text = 200.ToString();
            }
            else if (comboBox2.Text =="Haftalık")
            {
                txtFiyat.Text = 150.ToString();
            }
            else if (comboBox2.Text == "Aylık")
            {
                txtFiyat.Text = 120.ToString();
            }
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tiklananDeger =Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value) ;
            var istenenDeger = db.Satis.Where(s => s.satis_id == tiklananDeger).FirstOrDefault();
            istenenDeger.Musteri.müsteri_id = Convert.ToInt16(txtMusteriid.Text);
            istenenDeger.AracModel.model_id= Convert.ToInt16(txtAracModel.Text);
            istenenDeger.satis_ehliyet=txtEhliyetNo.Text;
            istenenDeger.satis_ehliyetTarih = txtEhliyetTar.Text;
            istenenDeger.satis_ehliyetVerYeri = txtEhlVer.Text;
            istenenDeger.satis_kiratip = comboBox2.Text;
            istenenDeger.satis_verilis = Convert.ToDateTime(dtpCıkıs.Text);
            istenenDeger.satis_alis = Convert.ToDateTime(dtpDönüs.Text);
            istenenDeger.satis_gun = Convert.ToInt16(txtGün.Text);
            istenenDeger.satis_kira = Convert.ToInt16(txtFiyat.Text);
            istenenDeger.satis_toplam = Convert.ToInt16(txtTutar.Text);
            db.SaveChanges();
            MessageBox.Show("Güncellendi!");
            Listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int tiklananDeger = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            var istenenDeger = db.Satis.Where(s => s.satis_id == tiklananDeger).FirstOrDefault();
            txtEhliyetNo.Text = istenenDeger.satis_ehliyet;
            txtEhliyetTar.Text = istenenDeger.satis_ehliyetTarih;
            txtEhlVer.Text = istenenDeger.satis_ehliyetVerYeri;
            comboBox2.Text = istenenDeger.satis_kiratip;
            dtpCıkıs.Text = istenenDeger.satis_verilis.ToString();
            dtpDönüs.Text = istenenDeger.satis_alis.ToString();
            txtGün.Text =istenenDeger.satis_gun.ToString();
            txtFiyat.Text = istenenDeger.satis_kira.ToString();
            txtTutar.Text = istenenDeger.satis_toplam.ToString();
            comboBox1.Text = istenenDeger.AracModel.model_plaka;
            txtTcNOAra.Text = istenenDeger.Musteri.musteri_tc;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int tiklananDeger = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            var istenenDeger = db.Satis.Where(s => s.satis_id == tiklananDeger).FirstOrDefault();
            db.Satis.Remove(istenenDeger);
            db.SaveChanges();
            MessageBox.Show("Arac Teslim Alındı");
            Listele();
        }
    }
}
