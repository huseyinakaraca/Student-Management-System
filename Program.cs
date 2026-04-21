using System;
using System.Windows.Forms;
using Student_Management_System;
namespace OgrenciArayuzSistemi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}