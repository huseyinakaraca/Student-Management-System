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
        private void button4_Click(object sender, EventArgs e)
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
            SqlConnection baglanti = new SqlConnection(baglantiAdresi);
            SqlDataAdapter gemi = new SqlDataAdapter("SELECT * FROM Ogrenciler", baglanti);
            DataTable sanalTablo = new DataTable();
            gemi.Fill(sanalTablo);
            dataGridView1.DataSource = sanalTablo;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
                {
                    if (textBox1.Text.Any(char.IsDigit))
                    {
                        MessageBox.Show("Ýsim alanýnda rakam bulunamaz! Lütfen geçerli bir ad giriniz.", "Hatalý Ýsim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    baglanti.Open();
                    string sqlEkle = "INSERT INTO Ogrenciler (Ad, Numara, Notu) VALUES (@p1, @p2, @p3)";
                    SqlCommand komut = new SqlCommand(sqlEkle, baglanti);
                    komut.Parameters.AddWithValue("@p1", textBox1.Text);
                    komut.Parameters.AddWithValue("@p2", int.Parse(textBox2.Text));
                    komut.Parameters.AddWithValue("@p3", double.Parse(textBox3.Text));
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
        private void button2_Click(object sender, EventArgs e)
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";

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
        private void button3_Click(object sender, EventArgs e)
        {
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                string sqlGuncelle = "UPDATE Ogrenciler SET Ad = @p1, Notu = @p2 WHERE Numara = @p3";
                SqlCommand komut = new SqlCommand(sqlGuncelle, baglanti);
                komut.Parameters.AddWithValue("@p1", textBox1.Text);
                komut.Parameters.AddWithValue("@p2", double.Parse(textBox3.Text));
                komut.Parameters.AddWithValue("@p3", int.Parse(textBox2.Text));
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
    }
}
