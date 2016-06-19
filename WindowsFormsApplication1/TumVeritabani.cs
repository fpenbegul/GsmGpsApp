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

namespace WindowsFormsApplication1
{
    public partial class TumVeritabani : Form
    {
        public TumVeritabani()
        {
            InitializeComponent();
        }

        private void TumVeritabani_Load(object sender, EventArgs e)
        {
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            dataGridView1.Visible = true;
            string connection = "server=sql8.freesqldatabase.com; database=sql8118560;user=sql8118560; password=2itnDWnidk;";
            MySqlConnection con = new MySqlConnection(connection);
            con.Open();
            string komut = "SELECT * FROM tablo";
            MySqlDataAdapter da = new MySqlDataAdapter(komut, con);
            DataTable veritablo = new DataTable();
            da.Fill(veritablo);
            dataGridView1.DataSource = veritablo;
            con.Close();
        }
    }
}
