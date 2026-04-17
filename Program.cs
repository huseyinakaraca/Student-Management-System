using System.ComponentModel;
using Microsoft.Data.SqlClient;
namespace OgrenciYonetimSistemi
{ 
    public class Ogrenci
    {
        public string Ad { get; set; }
        public int Numara { get; set; }
        public double Not { get; set; }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Ogrenci> ogrenciler = new List<Ogrenci>();
            Console.WriteLine("--- OGRENCI YONETIM SISTEMINE HOS GELDIN ---");
            string baglantiAdresi = "Server=DESKTOP-581KP98\\SQLEXPRESS;Database=OgrenciDB;Trusted_Connection=True;TrustServerCertificate=True;";
            while (true) 
            {
                Console.WriteLine("Secim Yapiniz:\n 1-Ogrenci Ekle: \n 2-Ogrenci Bilgilerini Getir:\n 3-Cikis Yap:\n");
                string secim = Console.ReadLine();
                if (secim == "1")
                {
                    Console.Write("Ogrenci Adi:");
                    string isim = Console.ReadLine();
                    Console.Write("Ogrenci Numarasi:");
                    int numara = int.Parse(Console.ReadLine());
                    Console.Write("Ogrenci Notu:");
                    double not = double.Parse(Console.ReadLine());
                    Ogrenci yeniOgrenci = new Ogrenci();
                    using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
                    {
                        baglanti.Open();
                        string sqlSorgusu = "INSERT INTO Ogrenciler (Ad, Numara, Notu) VALUES (@p1, @p2, @p3)";
                        SqlCommand komut = new SqlCommand(sqlSorgusu, baglanti);
                        komut.Parameters.AddWithValue("@p1", isim);
                        komut.Parameters.AddWithValue("@p2", numara);
                        komut.Parameters.AddWithValue("@p3", not);
                        komut.ExecuteNonQuery();
                    }
                    yeniOgrenci.Ad = isim;
                    yeniOgrenci.Numara = numara;
                    yeniOgrenci.Not = not;
                    ogrenciler.Add(yeniOgrenci);
                    Console.WriteLine(isim + " isimli ," + numara + " numarali , notu " + not + " olan ogrenci basariyla eklendi...\n");                   
                }
                else if (secim == "2")
                {
                    Console.WriteLine("\n--- Kayitli Ogrenciler ---");
                    using (SqlConnection baglanti = new SqlConnection(baglantiAdresi))
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("SELECT * FROM Ogrenciler", baglanti);
                        using (SqlDataReader okuyucu = komut.ExecuteReader())
                        {
                            while (okuyucu.Read())
                            {
                                string dbAd = okuyucu["Ad"].ToString();
                                string dbNumara = okuyucu["Numara"].ToString();
                                string dbNot = okuyucu["Notu"].ToString();
                                Console.WriteLine($"Adi: {dbAd}, Numarasi: {dbNumara}, Notu: {dbNot}");
                            }
                        }
                    }
                    Console.WriteLine();
                }
                else if (secim == "3")
                {
                    Console.WriteLine("Cikis Yapiliyor......");
                    break;
                }
                else
                    Console.WriteLine("Yanlis secim yaptiniz!!!!! Lutfen 1, 2 veya 3'e seceneklerinden birini seciniz!!!");
            }
        }
    }
}
