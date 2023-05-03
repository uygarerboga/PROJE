using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_PROJESİ
{
    
    public partial class Ana : Form
    {
        public string selectedPage { get; set; }
        bool izin;
        public Ana()
        {
            InitializeComponent();
        }
        #region form load

        private void Ana_Load(object sender, EventArgs e)
        {
            timer1.Start();
            BackColor = ColorTranslator.FromHtml("#626262");
            AnaTabControl.Dock = DockStyle.Fill;
            //alttaki iki kod günü ve saati gösteriyor
            label1.Text = DateTime.Now.ToLongDateString();
            label2.Text = DateTime.Now.ToLongTimeString();
            label1.BackColor = ColorTranslator.FromHtml("#626262");
            label2.BackColor = ColorTranslator.FromHtml("#626262");
            

            #region Icon Ekleme
            ImageList iconsList = new ImageList();
            iconsList.TransparentColor = Color.Blue;
            iconsList.ColorDepth = ColorDepth.Depth32Bit;
            iconsList.ImageSize = new Size(25, 25);
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\home.png"));
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\tool.png"));
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\dollar.png"));
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\cubes.png"));
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\checkout.png"));
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\delivery-box.png"));
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\report.png"));
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\employees.png"));
            iconsList.Images.Add(Image.FromFile(@"C:\Users\mehme\OneDrive\Masaüstü\Projeler\ERP PROJESİ\Icons\customer-feedback.png"));

            #region Üst NavBar
            //yukarda eklenme sırasına göre görselleri koyuyor
            AnaTabControl.ImageList = iconsList;
            AnaSayfa.ImageIndex = 0;
            İmalat.ImageIndex = 1;
            Muhasebe.ImageIndex = 2;
            Urunler.ImageIndex = 3;
            Satış.ImageIndex = 4;
            SatınAlma.ImageIndex = 5;
            Raporlar.ImageIndex = 6;
            personeller.ImageIndex = 7;
            cariler.ImageIndex = 8;
            AnaTabControl.Multiline = true;
            #endregion
            #endregion
        }
        #endregion
        #region app closing

        //bug yaşanmasın diye formu kapatıyor
        private void Ana_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion
        #region seçilen ekran

        #endregion
        #region Right click
        // sağ click için ekrana girmesi lazım bunu geçerli sayfalarda tekrarlıcaz fakat ilk girişte afallıyabiliyor. tekrar tab seçmen lazım.

        #region üretim emirleri

        private void uretimEmirleri_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "üretimemri";
        }

        private void uretimEmirleri_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }

        #endregion
        #region makinalar
        private void makinalar_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "makinalar";
        }

        private void makinalar_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #region rotalar
        private void rotalar_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "rotalar";
        }

        private void rotalar_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #region günlükrapor

        private void gunlukRaporEkle_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "günlükraporekle";
        }
        private void gunlukRaporEkle_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #region operasyon

        private void operasyonEkle_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "operasyonekle";
        }

        private void operasyonEkle_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #region personel

        private void personeller_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "personeller";
        }
        private void personeller_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #region Hakedişler
        private void hakedisler_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "hakedis";
        }

        private void hakedisler_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #region Faturalar
        private void satısfaturaları_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "fatura";
        }

        private void satinalimfaturası_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "fatura";
        }

        private void satısfaturaları_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #endregion
        #region Ekleme Ekranı
        private void ekle_Click(object sender, EventArgs e)
        {
            EklemeEkranı ekle = new EklemeEkranı();
            ekle.selectedPage = selectedPage;
            ekle.Show();
            
        }
        private void eklebtn_Click(object sender, EventArgs e)
        {
            EklemeEkranı ekle = new EklemeEkranı();
            ekle.selectedPage = selectedPage;
            ekle.Show();
        }
        #endregion
        #region find ekranı
        //find ekranını açar
        private void AnaTabControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                Find find = new Find();
                find.Show();
            }
        }

        private void ara_Click(object sender, EventArgs e)
        {
            Find find = new Find();
            find.Show();
        }

        private void arabtn_Click(object sender, EventArgs e)
        {

            Find find = new Find();
            find.Show();
        }
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {

            label2.Text = DateTime.Now.ToLongTimeString();
        }


    }
}
