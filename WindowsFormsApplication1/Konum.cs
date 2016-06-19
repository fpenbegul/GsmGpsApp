using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Konum : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-K12JLRE; Initial Catalog=rehberVT; Trusted_Connection=yes");
        public Konum()
        {
            InitializeComponent();
        }
        public void Combaglan()
        {
            Anaekran anaekran = new Anaekran();
            string portadi = anaekran.portoku();
            serialPort1.Close();
            //SerialPort serialport1 = new SerialPort("COM5", 115200, Parity.None, 8, StopBits.One);
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.RequestToSend;
            serialPort1.DtrEnable = true;
            serialPort1.PortName = portadi;
            serialPort1.Open();
            serialPort1.Write("at+cmgf=1\r\n");
            Thread.Sleep(500);
        }

        public void sql_baglan()
        {
            string connection = "server=sql8.freesqldatabase.com; database=sql8118560;user=sql8118560; password=2itnDWnidk;";
            MySqlConnection con = new MySqlConnection(connection);
        }
        public void sql_kapat()
        {
            string connection = "server=sql8.freesqldatabase.com; database=sql8118560;user=sql8118560; password=2itnDWnidk;";
            MySqlConnection conn = new MySqlConnection(connection);
            conn.Close();
        }
        string yol1, yol2;
        string saat, tarih, adres;
        public void button1_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            comboBox2.Enabled = true;
            linkLabel1.Enabled = true;
            linkLabel2.Enabled = true;
            string isim;
            isim = comboBox1.Text;
            this.Text = isim;
            string connection = "server=sql8.freesqldatabase.com; database=sql8118560;user=sql8118560; password=2itnDWnidk;";
            MySqlConnection con = new MySqlConnection(connection);
            con.Open();
            MySqlCommand cmd =new MySqlCommand("SELECT resimyol,adres FROM tablo WHERE ad='" + isim + "' ORDER BY id ASC LIMIT 1",con);
            MySqlDataReader dr = cmd.ExecuteReader();
            
            while (dr.Read())
            {
                yol1 = (dr["resimyol"]).ToString();
                label3.Text = "İlk Görüldüğü Adres: \r" + (dr["adres"]).ToString();
            }
            con.Close();
            con.Open();
            MySqlCommand cmdd = new MySqlCommand("SELECT resimyol,adres,hour,day FROM tablo WHERE ad='" + isim + "' ORDER BY id DESC LIMIT 1",con);
            MySqlDataReader drr = cmdd.ExecuteReader();
            
            while (drr.Read())
            {
                yol2 = (drr["resimyol"].ToString());
                label4.Text = "Son Görüldüğü Adres: \r" + (drr["adres"].ToString());
                adres= (drr["adres"].ToString());
                saat = (drr["hour"].ToString());
                tarih = (drr["day"].ToString());
            }
            con.Close();
            pictureBox1.ImageLocation = yol1;
            pictureBox2.ImageLocation = yol2;

        }
        public void combo_doldur() {
            string connection = "server=sql8.freesqldatabase.com; database=sql8118560;user=sql8118560; password=2itnDWnidk;";
            MySqlConnection con = new MySqlConnection(connection);
            con.Open();
            string komut = "SELECT ad FROM tablo GROUP BY ad";
            MySqlDataAdapter da = new MySqlDataAdapter(komut, con);
            DataTable veritablo = new DataTable();
            da.Fill(veritablo);
            comboBox1.DataSource = veritablo;
            comboBox1.DisplayMember = "ad";
            con.Close();
        }
        public void liste_doldur()
        {
            
                
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from rehber Order By kisiAdi asc", baglanti);
                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                comboBox2.Items.Add(oku["kisiAdi"].ToString());
                }
                oku.Close();
                baglanti.Close();
            



        }
        private void Konum_Load(object sender, EventArgs e)
        {
            combo_doldur();
            Combaglan();
            liste_doldur();
            comboBox2.SelectedIndex = 0;
        }
        public void button2_Click(object sender, EventArgs e)
        {
            TumVeritabani veritabanı = new TumVeritabani();
            veritabanı.Show();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string key = @"http\shell\open\command";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);
            // Varsayılan tarayıcının yolunu al.
            // Aldığımız değer salt olarak programın
            // yolu olmadığı için biraz işlememiz gerekiyor.
            string defaultbrowserpath = ((string)registryKey.GetValue(null, null)).Split('"')[1];
            //Process.Start ile başlat
            Process.Start(defaultbrowserpath, yol2);
        }
        public static string arndrma(string metin)
        {

            char[] türkcekarakterler = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö','ü','Ü'};
            char[] ingilizce = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };//karakterler sırayla ingilizce karakter karşılıklarıyla yazıldı
            for (int i = 0; i < türkcekarakterler.Length; i++)
            {

                metin = metin.Replace(türkcekarakterler[i], ingilizce[i]);

            }
            return metin;
        }

        private void Konum_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
            foreach (Form forms in Application.OpenForms)
            {
                if (forms.Name == "Anaekran")
                {
                    Anaekran anaekran = (Anaekran)forms;
                    this.Hide();
                    anaekran.Combaglan();
                    anaekran.statusayar();
                    
                }
            }
        }

        string telefon_no;

        public void button3_Click(object sender, EventArgs e)
        {
            string adi=comboBox2.Text;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select telNo from rehber WHERE kisiAdi='" + adi + "'", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                telefon_no=(oku["telNo"].ToString());
            }
            oku.Close();
            baglanti.Close();
            string mesaj_icerik = tarih + " tarihinde saat " + saat + "'te " + comboBox1.Text + " " + adres + " adresinde goruldu ";
            mesaj_icerik = arndrma(mesaj_icerik);
            serialPort1.Write("at+cmgs=" +telefon_no + "\r\n");
            Thread.Sleep(500);
            serialPort1.Write(mesaj_icerik+  Char.ConvertFromUtf32(26) + "\r\n");
            Thread.Sleep(550);
            
            //richTextBox2.Text += komut_oku() + "\r";
            MessageBox.Show(telefon_no+ "'lı numaraya \r\r\"" + mesaj_icerik + "\" \r\r Mesajı Gönderildi");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string key = @"http\shell\open\command";
            RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(key, false);
            // Varsayılan tarayıcının yolunu al.
            // Aldığımız değer salt olarak programın
            // yolu olmadığı için biraz işlememiz gerekiyor.
            string defaultbrowserpath =
            ((string)registryKey.GetValue(null, null)).Split('"')[1];
            //Process.Start ile başlat
            Process.Start(defaultbrowserpath, yol1);
        }
    }
}
