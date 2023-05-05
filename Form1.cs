using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Dapper;

namespace ERP_PROJESİ
{
    public partial class Form1 : Form
    {

        class _kullanıcı
        {
            public int id { get; set; }
            public string kullaniciadi { get; set; }
            public string parola { get; set; }
            public int ünvan { get; set; }
        }

        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-THFGP40; initial Catalog = ERP; Integrated Security = True");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '*';
            panel1.BackColor = ColorTranslator.FromHtml("#626262");
            panel2.BackColor = ColorTranslator.FromHtml("#626262");
        }

        private void button1_Click(object sender, EventArgs e)
        {


                Login(textBox1.Text.Trim(),textBox2.Text.Trim());
                
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Login(string username, string password)
        {
            bool kullanıcı = false;
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@kullaniciadi", username);

            var reader = SqlCon.ExecuteReader("kullanici", parameters, commandType:CommandType.StoredProcedure);
            if (reader.Read())
            {
                kullanıcı=true;
            }
            reader.Dispose();
            reader.Close();
            DynamicParameters param = new DynamicParameters();
            param.Add("@kullanici", username);
            param.Add("@parola", password);
            if (kullanıcı)
            {
                reader = SqlCon.ExecuteReader("Parola", param, commandType: CommandType.StoredProcedure);
                if (reader.Read())
                {
                    if (reader.GetString(0) == username)
                    {
                        if (reader.GetString(1) == password)
                        {
                            Ana anaa = new Ana();
                            this.Hide();
                            anaa.ShowDialog();
                        }

                    }


                }else { MessageBox.Show("Şifre hatalı"); }
            }
            
            else { MessageBox.Show("Kullanıcı bulunamadı"); }
            reader.Dispose();
            reader.Close();

        }
        #region classlar


        #endregion
    }
}
