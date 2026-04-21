using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Student_Management_System.Entity
{
    public class Ogrenci
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int Numara { get; set; }
        public double Notu { get; set; }
        public int BolumId { get; set; }
    }
}
