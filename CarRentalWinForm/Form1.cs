using CarRentalWinForm.Araclar;
using CarRentalWinForm.Login;
using CarRentalWinForm.SatisYap;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        CarRentalEntities db = new CarRentalEntities();
        CustomerForm customerForm;

        public void Getir(Form form)
        {
            panel3.Controls.Clear();
            panel3.Controls.Add(form);
        }
       
        private void btnMüsteri_Click(object sender, EventArgs e)
        {
            
            customerForm = new CustomerForm();
            customerForm.MdiParent = this;
            Getir(customerForm);
            
            
            customerForm.Show();
        }

        private void btnMüsteriEkle_Click(object sender, EventArgs e)
        {
           AddCustomer addCustomer = new AddCustomer();
           addCustomer.Show(); 
        }
        AracForm aracForm;
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            
            aracForm = new AracForm();
            aracForm.MdiParent = this;
            Getir(aracForm);
            aracForm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        SatisYapForm satisYap;
        private void button3_Click(object sender, EventArgs e)
        {
            satisYap = new SatisYapForm();
            satisYap.MdiParent = this;
            Getir(satisYap);
            satisYap.Show();
        }
    }
}
