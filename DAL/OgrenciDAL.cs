using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Student_Management_System.Entity;
namespace Student_Management_System.DAL
{
    public class OgrenciDAL
    {
        static string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
        //LİSTELEME VE BİRLEŞTİRME
        public static DataTable OgrencileriGetir()
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                string sql = @"SELECT Ogrenciler.Id, Ogrenciler.Ad, Ogrenciler.Numara, Ogrenciler.Notu, Bolumler.BolumAd
                       FROM Ogrenciler
                       LEFT JOIN Bolumler ON Ogrenciler.BolumId = Bolumler.Id
                       ORDER BY Ogrenciler.Ad ASC";
                SqlDataAdapter gemi = new SqlDataAdapter(sql, baglanti);
                DataTable tablo = new DataTable();
                gemi.Fill(tablo);
                return tablo;
            }
        }
        //BÖLÜMLERİ GETİRME 
        public static DataTable BolumleriGetir()
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                SqlDataAdapter gemi = new SqlDataAdapter("SELECT * FROM Bolumler", baglanti);
                DataTable tablo = new DataTable();
                gemi.Fill(tablo);
                return tablo;
            }
        }
        //EKLEME
        public static void OgrenciEkle(Ogrenci ogr)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                string sql = "INSERT INTO Ogrenciler (Ad, Numara, Notu, BolumId) VALUES (@p1, @p2, @p3, @p4)";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@p1", ogr.Ad);
                komut.Parameters.AddWithValue("@p2", ogr.Numara);
                komut.Parameters.AddWithValue("@p3", ogr.Notu);
                komut.Parameters.AddWithValue("@p4", ogr.BolumId);
                komut.ExecuteNonQuery();
            }
        }
        //GÜNCELLEME
        public static void OgrenciGuncelle(Ogrenci ogr)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                string sql = "UPDATE Ogrenciler SET Ad=@p1, Notu=@p2, BolumId=@p4 WHERE Numara=@p3";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@p1", ogr.Ad);
                komut.Parameters.AddWithValue("@p2", ogr.Notu);
                komut.Parameters.AddWithValue("@p3", ogr.Numara);
                komut.Parameters.AddWithValue("@p4", ogr.BolumId);
                komut.ExecuteNonQuery();
            }
        }
        //SİLME
        public static void OgrenciSil(int numara)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                string sql = "DELETE FROM Ogrenciler WHERE Numara=@p1";
                SqlCommand komut = new SqlCommand(sql, baglanti);
                komut.Parameters.AddWithValue("@p1", numara);
                komut.ExecuteNonQuery();
            }
        }
        public static int NumaraKontrol(int numara)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler WHERE Numara=@p1", baglanti);
                komut.Parameters.AddWithValue("@p1", numara);
                return (int)komut.ExecuteScalar();
            }
        }
        //ARAMA MOTORU
        public static DataTable OgrenciAra(string aranan)
        {
            using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
            {
                string sql = @"SELECT Ogrenciler.Id, Ogrenciler.Ad, Ogrenciler.Numara, Ogrenciler.Notu, Bolumler.BolumAd
                       FROM Ogrenciler
                       LEFT JOIN Bolumler ON Ogrenciler.BolumId = Bolumler.Id
                       WHERE Ogrenciler.Ad LIKE @aranan";
                SqlDataAdapter gemi = new SqlDataAdapter(sql, baglanti);
                gemi.SelectCommand.Parameters.AddWithValue("@aranan", aranan + "%");
                DataTable tablo = new DataTable();
                gemi.Fill(tablo);
                return tablo;
            }
        }
    }
}
