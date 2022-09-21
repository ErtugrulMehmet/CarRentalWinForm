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


namespace CarRentalWinForm.Araclar
{
    public partial class CarAddForm : Form
    {
        public CarAddForm()
        {
            InitializeComponent();
        }
        

        CarRentalEntities db = new CarRentalEntities();
        SqlConnection baglanti = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarRental;Integrated Security=True");
       
        private void button1_Click(object sender, EventArgs e)
        {
            
            AracModel aracModel = new AracModel();
            AracMarka aracMarka = new AracMarka();
            Yakit yakit = new Yakit();
            aracModel.model_plaka = txtPlaka.Text;
            aracMarka.arac_marka = cmbMarka.SelectedItem.ToString();
            aracModel.model_seri = cmbSeri.SelectedItem.ToString() ;
            aracModel.model_adi=txtModel.Text;
            aracModel.model_renk = txtRenk.Text;
            aracModel.model_km = txtKM.Text; 
            yakit.yakit_tur = cmbYakit.SelectedItem.ToString();
            aracModel.model_fiyat = txtFiyat.Text;
            aracModel.model_resim=pictureBox1.ImageLocation;

           
            db.AracModel.Add(aracModel);
            db.Yakit.Add(yakit);
            db.AracMarka.Add(aracMarka);
            db.SaveChanges();
            MessageBox.Show("Arac Eklendi");
        }

        private void CarAddForm_Load(object sender, EventArgs e)
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
            cmbYakit.Items.Clear();
           
            SqlCommand komut2 = new SqlCommand("select * from Yakit", baglanti);

            SqlDataReader read2 = komut2.ExecuteReader();
            while (read2.Read())
            {
                cmbYakit.Items.Add(read2["yakit_tur"]);

            }
            read2.Close();
           
            
            cmbMarka.Items.Clear();
            
            SqlCommand komut = new SqlCommand("select * from AracMarka", baglanti);

            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                cmbMarka.Items.Add(read["arac_marka"]);

            }
            read.Close();
            baglanti.Close();

        }
             

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            pictureBox1.ImageLocation = dlg.FileName;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
