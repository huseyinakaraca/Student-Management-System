using System;
using System.Windows.Forms;
using Student_Management_System.BLL;
using Student_Management_System.Entity;
namespace Student_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DisplayMember = "BolumAd";
            comboBox1.ValueMember = "Id";
            comboBox1.DataSource = OgrenciBLL.BolumleriListele();
        }
        // LÝSTELE 
        private void buttonListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = OgrenciBLL.OgrencileriListele();
        }
        // EKLE
        private void buttonEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Ad boþ olamaz!");
                return;
            }
            Ogrenci yeniOgr = new Ogrenci();
            yeniOgr.Ad = textBox1.Text;
            yeniOgr.Numara = int.Parse(textBox2.Text);
            yeniOgr.Notu = double.Parse(textBox3.Text);
            yeniOgr.BolumId = (int)comboBox1.SelectedValue;
            bool sonuc = OgrenciBLL.OgrenciEkle(yeniOgr);
            if (sonuc)
            {
                MessageBox.Show("Öðrenci Baþarýyla Eklendi!");
                dataGridView1.DataSource = OgrenciBLL.OgrencileriListele();
            }
            else
            {
                MessageBox.Show("Bu numara zaten kayýtlý!");
            }
        }
    }
} 