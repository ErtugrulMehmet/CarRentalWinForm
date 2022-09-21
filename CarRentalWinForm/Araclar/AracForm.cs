using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalWinForm.Araclar
{
    public partial class AracForm : Form
    {
        public AracForm()
        {
            InitializeComponent();
        }
        CarRentalEntities db = new CarRentalEntities();
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarRental;Integrated Security=True");


        public void Temizle()
        {
            if (cmbMarka.Items!=null)
            {
                cmbMarka.Items.Clear();
                cmbSeri.Items.Clear();
                cmbYakit.Items.Clear();
            }
        }
        private void btnAracEkle_Click(object sender, EventArgs e)
        {
            CarAddForm carAdd = new CarAddForm();
            carAdd.Show();
        }

        public void Liste()
        {
            var gelenTablo = db.AracModel.ToList();
            dataGridView1.DataSource = gelenTablo.ToList();

        }
        

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Temizle();
            int tıklananDeger = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            var istenendeger = db.AracModel.Where(a => a.model_id == tıklananDeger).FirstOrDefault();
            txtFiyat.Text = istenendeger.model_fiyat;
            txtKM.Text = istenendeger.model_km;
            txtModel.Text = istenendeger.model_adi;
            txtPlaka.Text = istenendeger.model_plaka;
            txtRenk.Text = istenendeger.model_renk;
            cmbMarka.Items.Add(istenendeger.AracMarka.arac_marka);
            cmbMarka.Text = cmbMarka.Items[0].ToString();
            cmbSeri.Items.Add(istenendeger.model_seri);
            cmbSeri.Text = cmbSeri.Items[0].ToString();
            cmbYakit.Items.Add(istenendeger.Yakit.yakit_tur);
            cmbYakit.Text = cmbYakit.Items[0].ToString();

            if (pictureBox1.ImageLocation != null)
            {
                pictureBox1.ImageLocation = istenendeger.model_resim.ToString();
            }

        }

        private void AracForm_Load(object sender, EventArgs e)
        {
            Liste();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int tıklananDeger = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            var istenendeger = db.AracModel.Where(a => a.model_id == tıklananDeger).FirstOrDefault();
            DialogResult sonuc;
            sonuc =MessageBox.Show("Emin misiniz?", "Sil", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                db.AracModel.Remove(istenendeger);
                db.SaveChanges();
            }

            Liste();
        }

       

        

        private void cmbSeri_MouseClick(object sender, MouseEventArgs e)
        {
            cmbSeri.Items.Clear();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select * from AracModel", baglanti);

            SqlDataReader read1 = komut1.ExecuteReader();
            while (read1.Read())
            {
                cmbSeri.Items.Add(read1["model_seri"]);

            }
            read1.Close();
            baglanti.Close();
        }

        private void cmbYakit_MouseClick(object sender, MouseEventArgs e)
        {
            baglanti.Open();
            cmbYakit.Items.Clear();

            SqlCommand komut2 = new SqlCommand("select * from Yakit", baglanti);

            SqlDataReader read2 = komut2.ExecuteReader();
            while (read2.Read())
            {
                cmbYakit.Items.Add(read2["yakit_tur"]);

            }
            read2.Close();
            baglanti.Close();
        }

        private void cmbMarka_MouseClick(object sender, MouseEventArgs e)
        {
            cmbMarka.Items.Clear();
            baglanti.Open();


            SqlCommand komut = new SqlCommand("select * from AracMarka", baglanti);

            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                cmbMarka.Items.Add(read["arac_marka"]);

            }
            read.Close();
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tıklananDeger = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            var istenenDeger = db.AracModel.Where(m => m.model_id == tıklananDeger).FirstOrDefault();
            istenenDeger.model_plaka = txtPlaka.Text;
            istenenDeger.AracMarka.arac_marka = cmbMarka.Text;
            istenenDeger.model_renk = txtRenk.Text;
            istenenDeger.model_seri = cmbSeri.Text;
            istenenDeger.Yakit.yakit_tur = cmbYakit.Text;
            istenenDeger.model_fiyat = txtFiyat.Text;
            istenenDeger.model_km = txtKM.Text;
            istenenDeger.model_adi = txtModel.Text;
            istenenDeger.model_resim = pictureBox1.ImageLocation;
            db.SaveChanges();
            MessageBox.Show("Araç Güncellendi");
            Liste();


        }
    }
}
