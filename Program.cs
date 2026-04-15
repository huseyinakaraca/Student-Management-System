namespace OgrenciYonetimSistemi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> ogrenciler = new List<string>();
            Console.WriteLine("--- ÖĞRENCİ YÖNETİM SİSTEMİNE HOŞ GELDİN ---");
            while (true) 
            {
                Console.WriteLine("Seçim Yapınız:\n 1-Ögrenci Ekle: \n 2-Ögrenci Bilgilerini Getir:\n 3-Çıkış Yap:\n");
                string secim = Console.ReadLine();
                if (secim == "1")
                {
                    Console.Write("Ögrenci Adı:");
                    string isim = Console.ReadLine();
                    ogrenciler.Add(isim);
                    Console.WriteLine(isim + " başarıyla eklendi...\n");
                }
                else if (secim == "2")
                {
                    Console.WriteLine("\n--- Kayıtlı Ögrenciler ---");
                    foreach(string ogrenci in ogrenciler)
                    {
                        Console.WriteLine(ogrenci);
                    }
                    Console.WriteLine();
                }
                else if (secim == "3")
                {
                    Console.WriteLine("Çıkış Yapılıyor......");
                    break;
                }
                else
                    Console.WriteLine("Yanlış seçim yaptınız!!!!! Lütfen 1, 2 veya 3'e seçeneklerinden birini seçiniz:");
            }
        }
    }
}
