//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.IO.Ports;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Text.RegularExpressions;

//namespace WindowsFormsApplication1
//{
//    public class Comayar
//    {
//        public void ayar()
//        {
//            Anaekran anaekran = new Anaekran();
//            //anaekran.portkapat();
//            serialPort1.BaudRate = int.Parse(anaekran.comboBox1.Text);
//            serialPort1.DataBits = int.Parse(anaekran.comboBox2.Text);
//            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), anaekran.comboBox3.Text);
//            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), anaekran.comboBox4.Text);
//            serialPort1.Handshake = Handshake.RequestToSend;
//            serialPort1.DtrEnable = true;
//            serialPort1.PortName = anaekran.comboBox5.Text;
//            serialPort1.Open();

//        }
//    }

//    internal class serialPort1
//    {
//        public static int BaudRate { get; internal set; }
//        public static int DataBits { get; internal set; }
//        public static bool DtrEnable { get; internal set; }
//        public static Handshake Handshake { get; internal set; }
//        public static Parity Parity { get; internal set; }
//        public static string PortName { get; internal set; }
//        public static StopBits StopBits { get; internal set; }

//        public static void Open()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
