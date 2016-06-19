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
using System.IO;
namespace WindowsFormsApplication1
{
    public partial class Arama : Form
    {
        public Arama()
        {
            InitializeComponent();
            
        }
        public void portkapat()
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
               
            }
            else if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Bağlantı Zaten Açık Değil");
            }

        }
        internal void Combaglan()
        {
            Anaekran anaekran = new Anaekran();
            string portadi = anaekran.portoku();
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
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

        private void Arama_Load(object sender, EventArgs e)
        {
            Combaglan();
            statusayar();

            label1.Parent = pictureBox1;
            label1.BackColor = Color.Transparent;

            label2.Parent = pictureBox1;
            label2.BackColor = Color.Transparent;

            label3.Parent = pictureBox1;
            label3.BackColor = Color.Transparent;

            label4.Parent = pictureBox1;
            label4.BackColor = Color.Transparent;

            label5.Parent = pictureBox1;
            label5.BackColor = Color.Transparent;

            label6.Parent = pictureBox1;
            label6.BackColor = Color.Transparent;

            label7.Parent = pictureBox1;
            label7.BackColor = Color.Transparent;

            label8.Parent = pictureBox1;
            label8.BackColor = Color.Transparent;

            label9.Parent = pictureBox1;
            label9.BackColor = Color.Transparent;

            label10.Parent = pictureBox1;
            label10.BackColor = Color.Transparent;

            label11.Parent = pictureBox1;
            label11.BackColor = Color.Transparent;

            label12.Parent = pictureBox1;
            label12.BackColor = Color.Transparent;

            label13.Parent = pictureBox1;
            label13.BackColor = Color.Transparent;

            label14.Parent = pictureBox1;
            label14.BackColor = Color.Transparent;

            label15.Parent = pictureBox1;
            label15.BackColor = Color.Transparent;

            label16.Parent = pictureBox1;
            label16.BackColor = Color.Transparent;

            label17.Parent = pictureBox1;
            label17.BackColor = Color.Transparent;

            label18.Parent = pictureBox1;
            label18.BackColor = Color.Transparent;

            label19.Parent = pictureBox1;
            label19.BackColor = Color.Transparent;

            label20.Parent = pictureBox1;
            label20.BackColor = Color.Transparent;

            label21.Parent = pictureBox1;
            label21.BackColor = Color.Transparent;

            label22.Parent = pictureBox1;
            label22.BackColor = Color.Transparent;

            label23.Parent = pictureBox1;
            label23.BackColor = Color.Transparent;

            label24.Parent = pictureBox1;
            label24.BackColor = Color.Transparent;
        }

      

     
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            if (serialPort1.IsOpen)
            {
                if (label27.Text== "Cihaz Kilitlendi")
                {
                    serialPort1.WriteLine("AT+CKPD=\":J\"\r");
                    Thread.Sleep(500);
                    serialPort1.WriteLine("AT+CKPD=\"*\"\r");
                    Thread.Sleep(500);
                    button1.BackgroundImage = Properties.Resources.kilit_acik1;
                    label27.Text = "Cihazın Kilidi Açıldı";
                    groupBox1.Enabled = true;
                    label25.Text = "";
                    label25.Visible = false;
                    label29.Visible = false;
                    label31.Visible = false;
                    label30.Visible = false;
                    
                }
                else if (label27.Text== "Cihazın Kilidi Açıldı")
                {
                    serialPort1.WriteLine("AT+CKPD=\"c\"\r");
                    Thread.Sleep(500);
                    serialPort1.WriteLine("AT+CKPD=\":J\"\r");
                    Thread.Sleep(500);
                    serialPort1.WriteLine("AT+CKPD=\"*\"\r");
                    Thread.Sleep(1500);
                    button1.BackgroundImage = Properties.Resources.kilit_kapali;
                    label27.Text = "Cihaz Kilitlendi";
                    groupBox1.Enabled = false;
                    label29.Visible = true;
                    label31.Visible = true;
                    label30.Visible = true;
                    

                }
            }
            
        }

        private void label22_Click(object sender, EventArgs e)
        {
        if (serialPort1.IsOpen)
            {
              
                serialPort1.WriteLine("AT+CKPD=\"U\"\r");
                label28.Text = "Ses Seviyesi Artırıldı";
            }
        }

        private void label23_Click(object sender, EventArgs e)
        {
       if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"D\"\r");
                label28.Text = "Ses Seviyesi Azaltıldı";

            }
        }
        
        public void kilitkontrol(string karakter)
        {
            
            if (serialPort1.IsOpen)
            {
                if (label27.Text == "Cihaz Kilitlendi")
                {
                    label25.Text = "Tuş Kilidini Açınız!";
                    label25.Visible = true;
                 
                }
                else if (label27.Text == "Cihazın Kilidi Açıldı")
                {
                    label25.Text = label25.Text + karakter;
                    label25.Visible = true;
                }
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"1\"\r");
                kilitkontrol("1");

            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"2\"\r");
                kilitkontrol("2");

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"3\"\r");
                kilitkontrol("3");

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"4\"\r");
                kilitkontrol("4");

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"5\"\r");
                kilitkontrol("5");

            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"6\"\r");
                kilitkontrol("6");

            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"7\"\r");
                kilitkontrol("7");

            }
        }
        private void label8_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"8\"\r");
                kilitkontrol("8");

            }
        }
        private void label9_Click(object sender, EventArgs e)
        {

            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"9\"\r");
                kilitkontrol("9");

            }
        }
        private void label10_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"*\"\r");
                kilitkontrol("*");

            }
        }
        private void label11_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"0\"\r");
                kilitkontrol("0");

            }
        }

        private void label12_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"#\"\r");
                kilitkontrol("#");

            }
        }

        private void label16_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"c\"\r");
                label25.Text = "";
                label25.Visible = false;
                string tarih = DateTime.Now.ToString("dd.MM.yyyy");
                string saat = DateTime.Now.ToString("HH.mm");
                label31.Text = tarih;
                label30.Text = saat;
            }
        }

        private void label14_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"]\"\r");
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"S\"\r");
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\"[\"\r");
            }
        }

        private void label24_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.WriteLine("AT+CKPD=\":C\"\r");
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void Arama_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("AT+CKPD=\":C\"\r");
            Thread.Sleep(750);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.WriteLine("AT+CKPD=\":J\"\r");
            Thread.Sleep(1050);

        }
        public void dosyadanOku()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "Resim Seç";
            //fd.Filter = "(*.jpg|*.jpg(*.png|*.png";
            if (fd.ShowDialog()==DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(fd.OpenFile());
            }
        }
        public void button4_Click(object sender, EventArgs e)
        {
            dosyadanOku();
            //pictureBox2.ImageLocation = ";

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            portkapat();
            statusayar();
            Thread.Sleep(1000);
            Combaglan();
            statusayar();
            Thread.Sleep(1000);
            //pictureBox2.ImageLocation = ";
        }

        private void Arama_FormClosed(object sender, FormClosedEventArgs e)
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
    }
}
