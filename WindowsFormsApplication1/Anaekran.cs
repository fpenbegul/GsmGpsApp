using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports; // System i/o Ports kütüphanesini ekledik ( Serialportu kullanabilmek için )
using System.IO; // Girdi- Çıktı metin belgesine okuma yazma güncelleme için

namespace WindowsFormsApplication1
{
    public partial class Anaekran : Form
    {
        public Anaekran()
        {
            InitializeComponent(); // Formu getirir
            // Combobox'ın gösterilen değerleri index'i 0 olanlar seçildi
            comboBox1.SelectedIndex = 0;// Combobox'ın gösterilen değerleri index'i 0 olanlar seçildi
            comboBox2.SelectedIndex = 0;// Combobox'ın gösterilen değerleri index'i 0 olanlar seçildi
            comboBox3.SelectedIndex = 0;// Combobox'ın gösterilen değerleri index'i 0 olanlar seçildi
            comboBox4.SelectedIndex = 0;// Combobox'ın gösterilen değerleri index'i 0 olanlar seçildi
        }
        public void portkapat() // Port Kapat Metodu Başlangıç
        {
            if (serialPort1.IsOpen)//Eğer Port Açıksa Gir
            {
                serialPort1.Close(); // Portu Kapat
                groupBox2.Enabled = false; // Groupbox2 kullanılabilirliğini pasif hale getirir.
            }
            else if (!serialPort1.IsOpen) // Port Kapalıysa
            {
                MessageBox.Show("Bağlantı Zaten Açık Değil"); // Uyarı Mesajı verir
            }

        }

        string yol = Application.StartupPath + "\\portisim.txt";
        FileStream dosya;
        public void port_isim(string portisim)// port ismini metin belgesine yazar
        {
            dosya = new FileStream(yol, FileMode.Create, FileAccess.Write);
            StreamWriter yazma;
            yazma = new StreamWriter(dosya);
            yazma.Write(portisim);
            yazma.Close();

        }
        public string portoku()
        {
            dosya = new FileStream(yol, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader okuma;
            okuma = new StreamReader(dosya);
            string portadi = okuma.ReadLine();
            okuma.Close();
            return portadi;
        }
        private void Anaekran_Load(object sender, EventArgs e)
        {
            porttara(); // Program ilk yüklediğinde porttara metodunu çalıştırır
        }
        public void porttara()
        {
            comboBox5.Items.Clear(); // Combobox'ta yüklü item varsa Temizler
            try
            {
                // Port isimlerini combobox içine doldur.
                comboBox5.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());


                // Listeden ilk elemanı seç.
                comboBox5.SelectedIndex = 1; // İlk açılışta com4 yerine com5 portu gözükmesi için
            }
            catch (Exception ex)
            {
                // Portlar sorgulanırken hata oluştu.
                MessageBox.Show(ex.Message);
            }
        }
        public void Combaglan()
        { //Basit uygulamalar için sadece seri port baundrate, portname'i belirtmemiz yeterlidir
         
            serialPort1.BaudRate = int.Parse(comboBox1.Text); // combobox1'in textini seriportun transfer hızı olarak ayarlar
            serialPort1.DataBits = int.Parse(comboBox2.Text); // combobox2'nin textini seriportun veribiti olarak ayarlar
            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), comboBox3.Text); //combobox3'ün textini seriportun eşlik biti olarak ayarlar
            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), comboBox4.Text); //combobox4'ün textini seriportun dur biti olarak ayarlar
            //serialPort1.Handshake = Handshake.RequestToSend; // Gönderilen verinin duraklatılması için seri bağlantı noktası ile el sıkışmaya “handshake” ihtiyaç duyuyor olabilir. 
            serialPort1.DtrEnable = true; //Veri terminalini hazırlar
            string portadi=portoku();
            serialPort1.PortName = portadi; // Port Adını Verir
            serialPort1.Open(); // Portu Açar
        }
        public void statusayar()
        {
            if (serialPort1.IsOpen)//Eğer Port Açıksa Gir
            {
                groupBox2.Enabled = true; // Groupbox2 Aktifliğini true yapar yani aktif hale getirir
                toolStripStatusLabel1.Image = Properties.Resources.yes; // statuslabel1'in image'ını yes yapar
                toolStripStatusLabel5.Text = Convert.ToString(serialPort1.StopBits); // statuslabel5'in textini dur biti yapar
                toolStripStatusLabel2.Text = Convert.ToString(serialPort1.BaudRate); // statuslabel2'nin Textini veri hızı yapar
                toolStripStatusLabel3.Text = Convert.ToString(serialPort1.DataBits); // statuslabel3'ün Textini veri biti yapar
                toolStripStatusLabel4.Text = Convert.ToString(serialPort1.Parity); // statuslabel4'ün textini eşlik biti yapar
                toolStripStatusLabel1.Text = serialPort1.PortName + " Portun'a Baglandı"; // statuslabel1'in textini portadı + bağlandı şeklinde gösterir

            }
            if (!serialPort1.IsOpen)//Eğer Port Kapalıysa Gir
            {
                toolStripStatusLabel1.Image = Properties.Resources.no; // statuslabel1'in image'ını no yapar
                toolStripStatusLabel1.Text = serialPort1.PortName + "  Baglantı sonlandırıldı"; // statuslabel1'in textini portadı + bağlantı sonlandırıldı şeklinde gösterir
            }
        }
        internal void button1_Click(object sender, EventArgs e)
        {
            portkapat(); // portkapat metodunu çalıştır
            Arama arama = new Arama(); // Arama sınıfından arama diye yeni bir form türettik
            statusayar(); // statusayar metodunu çalıştır
            arama.ShowDialog(); //türettiğimiz arama formunu showdialog ile gösterdik


        }

        internal void button2_Click(object sender, EventArgs e)
        {
            portkapat(); // portkapat metodunu çalıştır
            Rehber rehber = new Rehber(); // Rehber sınıfından rehber diye yeni bir form türettik
            statusayar(); // statusayar metodunu çalıştır
            rehber.ShowDialog(); //türettiğimiz rehber formunu showdialog ile gösterdik

        }

        private void button3_Click(object sender, EventArgs e)
        {
            portkapat(); // portkapat metodunu çalıştır
            mesaj mesaj = new mesaj(); // mesaj sınıfından mesaj diye yeni bir form türettik
            statusayar(); // statusayar metodunu çalıştır
            mesaj.ShowDialog(); //türettiğimiz mesaj formunu showdialog ile gösterdik
        }
        private void button7_Click(object sender, EventArgs e)
        {
            portkapat();
            Konum konum = new Konum();
            statusayar();
            konum.Show();

        }

        public void button5_Click(object sender, EventArgs e) // Bağlan Butonu
        {
            if (comboBox5.SelectedItem == null) // Eğer combobox5te seçili item yoksa gir
            {
                MessageBox.Show("Bir Com# seçmek zorundasınız !"); // Mesajbox ile port seçilmesi gerektiği uyarısını ver
            }
            else // Yukarıdaki şart sağlanmazsa gir
            {
                if (serialPort1.IsOpen)// Eğer port açıksa gir
                {
                    MessageBox.Show("Bağlantı zaten kurulu"); // mesaj kutusu ile bağlantı zaten kurulu uyarısı ver 
                }
                else // Yukarıdaki şart sağlanmazsa gir
                {
                    if (comboBox5.SelectedItem != null) // Combobox5'te item seçiliyse gir
                    {
                        port_isim(comboBox5.Text);
                        Combaglan(); // Combaglan metodunu çalıştır
                        statusayar(); // statusayar metodunu çalıştır
                        

                    }
                }
            }



        }
        public void button6_Click(object sender, EventArgs e) // Bağlantıyı kes butonu
        {
            portkapat(); // portkapat metodunu çalıştır
            statusayar(); // statusayar metodunu çalıştır
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
