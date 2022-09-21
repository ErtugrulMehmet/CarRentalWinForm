using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalWinForm.Login
{
    public partial class AddCustomer : Form
    {
        
        public AddCustomer()
        {
            InitializeComponent();
        }
        CarRentalEntities db = new CarRentalEntities();
        private void AddCustomer_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Musteri musteri = new Musteri();
            musteri.musteri_ad = txtAd.Text;
            musteri.musteri_soyad = txtSoyad.Text;
            musteri.musteri_email = txtMail.Text;
            musteri.musteri_tc = txtTc.Text;
            musteri.musteri_adres = txtAdres.Text;
            musteri.musteri_tel = txtTel.Text;
            db.Musteri.Add(musteri);
            db.SaveChanges();
            MessageBox.Show("Müşteri Eklendi");
            this.Close();

        }
    }
}
