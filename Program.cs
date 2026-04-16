using System.ComponentModel;
using System.IO;
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
            if (File.Exists("ogrenciler.txt"))
            {
                string[] kayitlar = File.ReadAllLines("ogrenciler.txt");
                foreach (string kayit in kayitlar)
                {
                    string[] parcalar = kayit.Split(',');
                    Ogrenci eskiOgrenci = new Ogrenci();
                    eskiOgrenci.Ad = parcalar[0];
                    eskiOgrenci.Numara = int.Parse(parcalar[1]);
                    eskiOgrenci.Not = double.Parse(parcalar[2]);
                    ogrenciler.Add(eskiOgrenci);
                }
            }
            Console.WriteLine("--- OGRENCI YONETIM SISTEMINE HOS GELDIN ---");
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
                    yeniOgrenci.Ad = isim;
                    yeniOgrenci.Numara = numara;
                    yeniOgrenci.Not = not;
                    ogrenciler.Add(yeniOgrenci);
                    File.AppendAllText("ogrenciler.txt", $"{yeniOgrenci.Ad},{yeniOgrenci.Numara},{yeniOgrenci.Not}\n");
                    Console.WriteLine(isim + " isimli ," + numara + " numarali , notu " + not + " olan ogrenci basariyla eklendi...\n");                   
                }
                else if (secim == "2")
                {
                    Console.WriteLine("\n--- Kayitli Ogrenciler ---");
                    foreach(Ogrenci ogrenci in ogrenciler)
                    {
                        Console.WriteLine($"Ad= {ogrenci.Ad} , Numara= {ogrenci.Numara}, Not= {ogrenci.Not}" );
                    }
                    Console.WriteLine();
                }
                else if (secim == "3")
                {
                    Console.WriteLine("Cıkıs Yapiliyor......");
                    break;
                }
                else
                    Console.WriteLine("Yanlis secim yaptiniz!!!!! Lutfen 1, 2 veya 3'e seceneklerinden birini seciniz!!!");
            }
        }
    }
}
