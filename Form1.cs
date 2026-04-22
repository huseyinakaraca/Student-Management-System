using Student_Management_System.BLL;
using Student_Management_System.Entity;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
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
        private void KutularýTemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox1.SelectedIndex = -1;
            textBox2.Enabled = true;
        }
        private void buttonListele_Click(object sender, EventArgs e) //Listele
        {
            dataGridView1.DataSource = OgrenciBLL.OgrencileriListele();
        }
        private void buttonEkle_Click(object sender, EventArgs e)    //Ekle
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen tüm alanlarý (Ad, Numara, Not, Bölüm) eksiksiz doldurun!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult cevap = MessageBox.Show("Bu öðrenciyi sisteme eklemek istediðinize emin misiniz?", "Kayýt Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                Ogrenci yeniOgr = new Ogrenci();
                yeniOgr.Ad = textBox1.Text;
                yeniOgr.Numara = int.Parse(textBox2.Text);
                yeniOgr.Notu = double.Parse(textBox3.Text);
                yeniOgr.BolumId = (int)comboBox1.SelectedValue;
                bool sonuc = OgrenciBLL.OgrenciEkle(yeniOgr);
                if (sonuc == true)
                {
                    MessageBox.Show("Öðrenci Baþarýyla Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    buttonListele_Click(null, null);
                    KutularýTemizle();
                }
                else
                {
                    MessageBox.Show("Bu öðrenci numarasý zaten sistemde kayýtlý! Lütfen farklý bir numara girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonGuncelle_Click(object sender, EventArgs e)         //Güncelle
        {
            if (textBox2.Enabled == true || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen önce tablodan güncelleyeceðiniz kiþiye týklayýn!", "Kiþi Seçilmedi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Ad, Not ve Bölüm alanlarý boþ býrakýlamaz!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult cevap = MessageBox.Show("Bu öðrencinin bilgilerini güncellemek istediðinize emin misiniz?", "Güncelleme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                Ogrenci guncelOgr = new Ogrenci();
                guncelOgr.Ad = textBox1.Text;
                guncelOgr.Numara = int.Parse(textBox2.Text);
                guncelOgr.Notu = double.Parse(textBox3.Text);
                guncelOgr.BolumId = (int)comboBox1.SelectedValue;
                OgrenciBLL.OgrenciGuncelle(guncelOgr);
                MessageBox.Show("Öðrenci Bilgileri Baþarýyla Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonListele_Click(null, null);
                KutularýTemizle();
            }
        }
        private void buttonSil_Click(object sender, EventArgs e)           //Sil
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Lütfen tablodan silmek istediðiniz kiþiyi seçin!", "Uyarý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult cevap = MessageBox.Show("Bu öðrenciyi silmek istediðinize emin misiniz? Bu iþlem geri alýnamaz!", "Silme Onayý", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (cevap == DialogResult.Yes)
            {
                int numara = int.Parse(textBox2.Text);
                OgrenciBLL.OgrenciSil(numara);
                MessageBox.Show("Öðrenci Baþarýyla Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonListele_Click(null, null);
                KutularýTemizle();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)                 //Tablo
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = satir.Cells[1].Value.ToString();
                textBox2.Text = satir.Cells[2].Value.ToString();
                textBox3.Text = satir.Cells[3].Value.ToString();
                comboBox1.Text = satir.Cells[4].Value.ToString().Trim();
                textBox2.Enabled = false;
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)                         //Arama Çubuðu
        {
            // Kutuya yazýlan yazýyý Aþçýya gönder ve dönen listeyi tabloya bas
            dataGridView1.DataSource = OgrenciBLL.OgrenciAra(textBox4.Text);
        }
        private void buttonTemizle_Click(object sender, EventArgs e)                          //Temizle
        {
            KutularýTemizle();
        }
        private void buttonCikis_Click(object sender, EventArgs e)                        //Çýkýþ butonu
        {
            DialogResult cikisCevabi = MessageBox.Show("Programdan çýkmak istediðinize emin misiniz?", "Çýkýþ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cikisCevabi == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
