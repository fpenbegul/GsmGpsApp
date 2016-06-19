using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class mesaj : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-K12JLRE; Initial Catalog=rehberVT; Trusted_Connection=yes");
        public mesaj()
        {
            InitializeComponent();
        }
        public void Combaglan()
        {
            Anaekran anaekran = new Anaekran();
            string portadi = anaekran.portoku();
            if (!serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            //SerialPort serialport1 = new SerialPort("COM5", 115200, Parity.None, 8, StopBits.One);
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Handshake = Handshake.RequestToSend;
            serialPort1.DtrEnable = true;
            serialPort1.PortName = portadi;
            serialPort1.Open();
        }
        public void statusayar()
        {
            toolStripStatusLabel6.Image = Properties.Resources.yes;
            toolStripStatusLabel5.Text = Convert.ToString(serialPort1.StopBits);
            toolStripStatusLabel2.Text = Convert.ToString(serialPort1.BaudRate);
            toolStripStatusLabel3.Text = Convert.ToString(serialPort1.DataBits);
            toolStripStatusLabel4.Text = Convert.ToString(serialPort1.Parity);
            toolStripStatusLabel6.Text = serialPort1.PortName + " Portun'a Baglandı";
        }
        public static string arndrma(string metin)
        {

            char[] türkcekarakterler = { 'ı', 'ğ', 'İ', 'Ğ', 'ç', 'Ç', 'ş', 'Ş', 'ö', 'Ö', 'ü', 'Ü' };
            char[] ingilizce = { 'i', 'g', 'I', 'G', 'c', 'C', 's', 'S', 'o', 'O', 'u', 'U' };//karakterler sırayla ingilizce karakter karşılıklarıyla yazıldı
            for (int i = 0; i < türkcekarakterler.Length; i++)
            {

                metin = metin.Replace(türkcekarakterler[i], ingilizce[i]);

            }
            return metin;
        }
        private void mesaj_Load(object sender, EventArgs e)
        {
            Combaglan();
            statusayar();
            kisiDoldur();
          
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int sayac1 = 0;
            string numara =maskedTextBox1.Text;
            maskedTextBox1.Clear();
            serialPort1.Write("at+cmgf=1\r\n");
            Thread.Sleep(500);

            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    maskedTextBox1.Text = maskedTextBox1.Text+ row.Cells[2].Value.ToString()+";";
                    serialPort1.Write("at+cmgs=" + row.Cells[3].Value + "\r\n");
                    Thread.Sleep(500);
                    serialPort1.Write(arndrma(richTextBox2.Text) + Char.ConvertFromUtf32(26) + "\r\n");
                    Thread.Sleep(550);
                    //richTextBox2.Text += komut_oku() + "\r";
                    MessageBox.Show(row.Cells[3].Value + "'lı numaraya \r\r\"" + arndrma(richTextBox2.Text) + "\" \r\r Mesajı Gönderildi");
                    sayac1++;
                }
            }
            if (sayac1==0)
            {
                maskedTextBox1.Text = numara;
                if (maskedTextBox1.Text!="")
                {
                    
                    //Firstly set the modem to text mode
                    serialPort1.Write("at+cmgf=1\r\n");
                    Thread.Sleep(500);
                    //Now enter message writing mode to send an sms to the number below
                    serialPort1.Write("at+cmgs=" + numara + "\r\n");
                    //Now send the contents of the message
                    Thread.Sleep(550);
                    serialPort1.Write(arndrma(richTextBox2.Text) + Char.ConvertFromUtf32(26) + "\r\n");
                    Thread.Sleep(550);
                    // richTextBox2.Text += komut_oku() + "\r";
                    MessageBox.Show(maskedTextBox1.Text + "'lı numaraya \r\r\"" + arndrma(richTextBox2.Text) + "\" \r\r Mesajı Gönderildi");

                }
                else
                {
                    MessageBox.Show("Lütfen Kişi Seçiniz veya Numara Giriniz!");
                }
            }
                }
        private void mesaj_FormClosing(object sender, FormClosingEventArgs e)
        {
           serialPort1.Close();

        }
        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            maskedTextBox1.SelectionStart = 0;
        }
        public void kisiDoldur()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();
            baglanti.Open();
            cmd.CommandText = "Select * From rehber ";
            da.SelectCommand = cmd;
            cmd.Connection = baglanti;
            da.Fill(ds, "rehber");

            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "rehber";
            baglanti.Close();
        }

        private void mesaj_FormClosed(object sender, FormClosedEventArgs e)
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

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}