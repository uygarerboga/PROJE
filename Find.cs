using ERP_PROJESİ.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_PROJESİ
{

    public partial class Find : Form
    {

        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-PRMBC7J; initial Catalog = ERP; Integrated Security = True");

        Ana Ana;
        public string arama { get; set; }
        public Find()
        {
            InitializeComponent();
        }

        //aşağıdaki komut find ekranını kapatır
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();

            }
        }

        private void aramatxt_TextChanged(object sender, EventArgs e)
        {

        }
        public Find(Ana ana)
        {
            this.Ana = ana;
            InitializeComponent();
        }
        private void Find_FormClosing(object sender, FormClosingEventArgs e)
        {
            Ana.arama = aramatxt.Text;
            switch (Ana.selectedPage)
            {
                case "makinalar":
                    Ana.MakinaListesi();
                    break;
            }
        }
    }
}
