using System;
using Microsoft.Data.SqlClient;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;
namespace OgrenciArayuzSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void KutulariTemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }
        private void button4_Click(object sender, EventArgs e)   //Listele
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                string sqlListele = @"
        SELECT 
            Ogrenciler.Id, 
            Ogrenciler.Ad, 
            Ogrenciler.Numara, 
            Ogrenciler.Notu, 
            Bolumler.BolumAd 
        FROM Ogrenciler
        LEFT JOIN Bolumler ON Ogrenciler.BolumId = Bolumler.Id";
                SqlDataAdapter gemi = new SqlDataAdapter(sqlListele, baglanti);
                DataTable sanalTablo = new DataTable();
                gemi.Fill(sanalTablo);
                dataGridView1.DataSource = sanalTablo;
            }
        }
        private void button1_Click(object sender, EventArgs e)     //Ekle
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
                {
                    if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                        string.IsNullOrWhiteSpace(textBox2.Text) ||
                        string.IsNullOrWhiteSpace(textBox3.Text))
                    {
                        MessageBox.Show("Lütfen Ad, Numara ve Not alanlarýný eksiksiz doldurunuz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (textBox1.Text.Any(char.IsDigit))
                    {
                        MessageBox.Show("Ýsim alanýnda rakam bulunamaz! Lütfen geçerli bir ad giriniz.", "Hatalý Ýsim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    baglanti.Open();
                    SqlCommand kontrolKomutu = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler WHERE Numara = @kontrolNo", baglanti);
                    kontrolKomutu.Parameters.AddWithValue("@kontrolNo", int.Parse(textBox2.Text));
                    int kisiSayisi = (int)kontrolKomutu.ExecuteScalar();
                    if (kisiSayisi > 0)
                    {
                        MessageBox.Show("Bu numaraya sahip bir öðrenci zaten var! Lütfen farklý bir numara giriniz.", "Mükerrer Kayýt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string sqlEkle = "INSERT INTO Ogrenciler (Ad, Numara, Notu, BolumId) VALUES (@p1, @p2, @p3, @p4)";
                    SqlCommand komut = new SqlCommand(sqlEkle, baglanti);
                    komut.Parameters.AddWithValue("@p1", textBox1.Text);
                    komut.Parameters.AddWithValue("@p2", int.Parse(textBox2.Text));
                    komut.Parameters.AddWithValue("@p3", double.Parse(textBox3.Text));
                    komut.Parameters.AddWithValue("@p4", comboBox1.SelectedValue);
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Öðrenci Baþarýyla Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    KutulariTemizle();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen Numara ve Not alanlarýna sadece SAYI giriniz!", "Hatalý Giriþ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception hata)
            {
                MessageBox.Show("Beklenmeyen bir hata oluþtu: " + hata.Message, "Sistem Hatasý", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)   //Sil
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
            DialogResult cevap = MessageBox.Show("Bu öðrenciyi sistemden kalýcý olarak silmek istediðinize emin misiniz?", "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.No)
            {
                return;
            }
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                string sqlSil = "DELETE FROM Ogrenciler WHERE Numara = @p1";
                SqlCommand komut = new SqlCommand(sqlSil, baglanti);
                komut.Parameters.AddWithValue("@p1", int.Parse(textBox2.Text));
                komut.ExecuteNonQuery();
                MessageBox.Show("Öðrenci Sistemden Silindi!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button3_Click(object sender, EventArgs e)    //Güncelle
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                string sqlGuncelle = "UPDATE Ogrenciler SET Ad = @p1, Notu = @p2, BolumId = @p4 WHERE Numara = @p3";
                SqlCommand komut = new SqlCommand(sqlGuncelle, baglanti);
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", double.Parse(textBox3.Text));
                komut.Parameters.AddWithValue("@p3", int.Parse(textBox2.Text));
                komut.Parameters.AddWithValue("@p4", comboBox1.SelectedValue);
                komut.ExecuteNonQuery();
                MessageBox.Show("Öðrenci Bilgileri Baþarýyla Güncellendi!", "Sistem Mesajý", MessageBoxButtons.OK, MessageBoxIcon.Information);
                KutulariTemizle();
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                string sqlArama = "SELECT * FROM Ogrenciler WHERE Ad LIKE @aranan";
                SqlDataAdapter gemi = new SqlDataAdapter(sqlArama, baglanti);
                gemi.SelectCommand.Parameters.AddWithValue("@aranan", textBox4.Text + "%");
                DataTable sanalTablo = new DataTable();
                gemi.Fill(sanalTablo);
                dataGridView1.DataSource = sanalTablo;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                SqlDataAdapter gemi = new SqlDataAdapter("SELECT * FROM Bolumler", baglanti);
                DataTable sanalTablo = new DataTable();
                gemi.Fill(sanalTablo);
                comboBox1.DisplayMember = "BolumAd";
                comboBox1.ValueMember = "Id";
                comboBox1.DataSource = sanalTablo;
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int secilenSatir = e.RowIndex;
                textBox1.Text = dataGridView1.Rows[secilenSatir].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[secilenSatir].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[secilenSatir].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[secilenSatir].Cells[4].Value.ToString();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
