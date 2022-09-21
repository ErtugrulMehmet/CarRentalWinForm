using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalWinForm
{
    public partial class CustomerForm : Form
    {
        public CustomerForm()
        {
            InitializeComponent();
        }
        CarRentalEntities db = new CarRentalEntities();

        public void Listele()
        {
            var musteri = db.Musteri.ToList();
            datagridMüsteri.DataSource = musteri.ToList();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void txtTcNoAra_TextChanged(object sender, EventArgs e)
        {
            string girilenDeger = txtTcNoAra.Text;
            var arananDeger = db.Musteri.Where(m => m.musteri_tc.Contains(girilenDeger)).ToList();
            datagridMüsteri.DataSource = arananDeger;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int tıklananDeger = Convert.ToInt16(datagridMüsteri.CurrentRow.Cells[0].Value);
            var istenenDeger = db.Musteri.Where(m => m.müsteri_id == tıklananDeger).FirstOrDefault();
            istenenDeger.musteri_ad = txtAd.Text;
            istenenDeger.musteri_soyad = txtSoyad.Text;
            istenenDeger.musteri_email = txtEposta.Text;
            istenenDeger.musteri_adres = txtAdres.Text;
            istenenDeger.musteri_tc = txtTcNo.Text;
            istenenDeger.musteri_tel = txtTel.Text;
            db.SaveChanges();
            MessageBox.Show("Müşteri Güncellendi");
            Listele();

        }

        private void datagridMüsteri_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int tıklananDeger = Convert.ToInt16(datagridMüsteri.CurrentRow.Cells[0].Value);
            var istenenDeger = db.Musteri.Where(m => m.müsteri_id == tıklananDeger).FirstOrDefault();
            if (istenenDeger != null)
            {
                txtTcNo.Text = istenenDeger.musteri_tc;
                txtAd.Text = istenenDeger.musteri_ad;
                txtSoyad.Text = istenenDeger.musteri_soyad;
                txtEposta.Text = istenenDeger.musteri_email;
                txtTel.Text = istenenDeger.musteri_tel;
                txtAdres.Text = istenenDeger.musteri_adres;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Emin misiniz?", "Sil", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int tıklananDeger = Convert.ToInt16(datagridMüsteri.CurrentRow.Cells[0].Value);
                var istenenDeger = db.Musteri.Where(m => m.müsteri_id == tıklananDeger).FirstOrDefault();
                db.Musteri.Remove(istenenDeger);
                db.SaveChanges();
                Listele();
            }


        }
    }
}
