using System;
using System.Data;
using Student_Management_System.Entity;
using Student_Management_System.DAL;
namespace Student_Management_System.BLL
{
    public class OgrenciBLL
    {
        public static DataTable OgrencileriListele()
        {
            return OgrenciDAL.OgrencileriGetir();
        }
        public static DataTable BolumleriListele()
        {
            return OgrenciDAL.BolumleriGetir();
        }
        public static bool OgrenciEkle(Ogrenci ogr)
        {
            if (OgrenciDAL.NumaraKontrol(ogr.Numara) > 0)
            {
                return false;
            }
            OgrenciDAL.OgrenciEkle(ogr);
            return true;
        }
        public static void OgrenciGuncelle(Ogrenci ogr)
        {
            OgrenciDAL.OgrenciGuncelle(ogr);
        }
        public static void OgrenciSil(int numara)
        {
            OgrenciDAL.OgrenciSil(numara);
        }
    }
}