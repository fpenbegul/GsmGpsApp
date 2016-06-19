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
using System.Windows;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Rehber : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-K12JLRE; Initial Catalog=rehberVT; Trusted_Connection=yes");
        public Rehber()
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
        public void Rehber_Load(object sender, EventArgs e)
        {
            Combaglan();
            if (serialPort1.IsOpen)
            {
                statusayar();
            }

            veritabani();
        }

        private void maskedTextBox2_Enter(object sender, EventArgs e)
        {
            
        }

        private void maskedTextBox2_MouseClick(object sender, MouseEventArgs e)
        {
            maskedTextBox2.SelectionStart = 0; // tıklandığında maskedtextbox'ın imleci başa getirir
        }

        private void maskedTextBox1_MouseClick(object sender, MouseEventArgs e)
        {
            maskedTextBox1.SelectionStart = 0; // tıklandığında maskedtextbox'ın imleci başa getirir
        }
        private void button1_Click(object sender, EventArgs e)
        {
           ErrorProvider provider = new ErrorProvider(); // Error provider kontrolünün tanımlanması


            if (maskedTextBox2.Text == "" || textBox1.Text=="") // Eğer txtbox1 ve maskedbox2 formları boş ise ;
            {
                provider.SetError(textBox1, "İsim Girişi Zorunludur!"); // Textbox1 formunun yanına uyarı mesajı
                provider.SetError(maskedTextBox2, "Numara Girişi Zorunludur!"); // Maskedbox2 formunun yanına uyarı mesajı
                provider.BlinkStyle = ErrorBlinkStyle.BlinkIfDifferentError; // Formların yanında çıkan icon'un yanıp sönme hızı
                
                MessageBox.Show("Yıldızlı Alanların doldurulması Zorunludur!"); // Hata Mesajı
            }
            else // Eğer txtbox1 ve maskedbox2 formları boş değil ise;
            {
                //TreeNode node; // içi boş düğüm oluşturuluyor

                //node = treeView1.Nodes.Add(textBox1.Text); // ebevyn düğümün değeri textbox1.Text'den ekliyor
                //node.Nodes.Add(maskedTextBox2.Text, maskedTextBox2.Text);  //Alt daldaki düğümün değeri ise maskedbox2.Text'den ekliyor
                //node.Name = maskedTextBox2.Text;

                baglanti.Open();
                SqlCommand kmt = new SqlCommand("insert into rehber(kisiAdi,telNo)values('" + textBox1.Text + "','" + maskedTextBox2.Text.ToString() + "')", baglanti);
                kmt.ExecuteNonQuery();
                baglanti.Close();
                veritabani();


            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
             treeView1.SelectedNode.Remove(); //Seçili düğümü kaldırır
            //baglanti.Open();
            //SqlCommand komut = new SqlCommand();
            //komut.Connection = baglanti;
            //komut.CommandText = "DELETE FROM rehber WHERE ID=@ID";
            //komut.Parameters.AddWithValue("@ID", treeView1.SelectedNode.Text);
            //komut.ExecuteNonQuery();
            //baglanti.Close();
            //baglanti.Open();
            //SqlCommand cmd = new SqlCommand("DELETE FROM rehber WHERE kisiAdi=" + treeView1.SelectedNode.Text + ";", baglanti);
            //cmd.ExecuteNonQuery();
            //baglanti.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            if (treeView1.SelectedNode!=null)
            {
                serialPort1.WriteLine("ATD" + treeView1.SelectedNode.Name + ";" + "\r");
                //MessageBox.Show(treeView1.SelectedNode.Name+"'lı numara Aranıyor...","Arama Yapılıyor");
                DialogResult secenek = MessageBox.Show(treeView1.SelectedNode.Name+"lı numara Aranıyor...\r\r"+"Aramayı Sonlandırmak İstiyor musunuz?", "Arama Yapılıyor", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (secenek == DialogResult.Yes)
                {
                    serialPort1.WriteLine("AT+CKPD=\"c\"\r");
                }
                else if (secenek == DialogResult.No)
                {
                    MessageBox.Show(treeView1.SelectedNode.Name + "'lı numara Aranıyor...\r\rÇağrıyı Sonlandırmayı Unutmayın!", "Arama Yapılıyor");
                }
            }
            
            else if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Seri porta bağlı değil");
            }
            else if (serialPort1.IsOpen && maskedTextBox1.Text!=null)
            {
                serialPort1.WriteLine("ATD" + maskedTextBox1.Text + ";"+ "\r");
                //MessageBox.Show(treeView1.SelectedNode.Name+"'lı numara Aranıyor...","Arama Yapılıyor");
                DialogResult secenek = MessageBox.Show(maskedTextBox1.Text + "lı numara Aranıyor...\r\r" + "Aramayı Sonlandırmak İstiyor musunuz?", "Arama Yapılıyor", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (secenek == DialogResult.Yes)
                {
                    serialPort1.WriteLine("AT+CKPD=\"c\"\r");
                }
                else if (secenek == DialogResult.No)
                {
                    MessageBox.Show(maskedTextBox1.Text + "'lı numara Aranıyor...\r\rÇağrıyı Sonlandırmayı Unutmayın!", "Arama Yapılıyor");
                }
            }
            else
            {
                MessageBox.Show("olmadı");
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                
                serialPort1.WriteLine("AT+CKPD=\"c\"\r");
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void Rehber_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }

        private void Rehber_FormClosed(object sender, FormClosedEventArgs e)
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
        public void veritabani()
        {
            treeView1.Nodes.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from rehber Order By kisiAdi asc", baglanti);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                TreeNode node = new TreeNode(oku["kisiAdi"].ToString());
                node.Nodes.Add(oku["telNo"].ToString(), oku["telNo"].ToString());
                treeView1.Nodes.Add(node);
                node.Name = oku["telNo"].ToString();

            }
            oku.Close();
            baglanti.Close();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
       

        }
    }
}
