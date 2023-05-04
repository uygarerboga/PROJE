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
using Dapper;
using ERP_PROJESİ.Classes;
using ERP_PROJESİ.Classes.İmalat;
using ERP_PROJESİ.Classes.Ürünler;

namespace ERP_PROJESİ
{
    
    public partial class Ana : Form
    {
        
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-THFGP40; initial Catalog = ERP; Integrated Security = True");
        public string selectedPage { get; set; }

        public string arama;

        public string urunturu;

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
        #region tab geçişleri
        // sağ click için ekrana girmesi lazım bunu geçerli sayfalarda tekrarlıcaz fakat ilk girişte afallıyabiliyor. tekrar tab seçmen lazım.
        // selected page ayarlama
        #region üretim emirleri

        private void uretimEmirleri_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "üretimemri";
        }


        #endregion
        #region makinalar
        private void makinalar_Enter(object sender, EventArgs e)
        {

            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "makinalar";
            MakinaListesi();
            
        }

        #endregion
        #region rotalar
        private void rotalar_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "rotalar";
        }

        #endregion
        #region günlükrapor

        private void gunlukRaporEkle_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "günlükraporekle";
        }
        #endregion
        #region operasyon

        private void operasyonEkle_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "operasyonekle";
            OperasyonListesi();
        }

        #endregion
        #region personel

        private void personeller_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "personeller";
            personelListele();
            
        }
        #endregion
        #region Hakedişler
        private void hakedisler_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "hakedis";
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

        #endregion
        #region CARİLER
        private void cariler_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "cariler";
            CarileriListele();
        }

        private void cariler_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #region ÜRÜNLER
        private void urunler_Enter1(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
            urunturu = "Ticari";
            urunlistele(urunturu);
            
        }

        private void urunler_Enter2(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
            urunturu = "Mamul";
            urunlistele(urunturu);

        }

        private void urunler_Enter3(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
            urunturu = "YarıMamul";
            urunlistele(urunturu);

        }

        private void urunler_Enter4(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
            urunturu = "Hammadde";
            urunlistele(urunturu);

        }
        #endregion
        #region imalatçı
        private void imalatTabPage_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "imalatçı";
            İmalatciListesi();
        }
        #endregion

        #endregion
        #region işlevler (ekleme-çıkarma-arama)

        #region Ekleme Ekranı
        private void ekle_Click(object sender, EventArgs e)
        {
            if (selectedPage != "")
            {
                EklemeEkranı ekle = new EklemeEkranı();
                ekle.selectedPage = selectedPage;   
                ekle.ShowDialog();
            }


        }
        private void eklebtn_Click(object sender, EventArgs e)
        {
            if (selectedPage != "")
            {
                EklemeEkranı ekle = new EklemeEkranı();
                ekle.selectedPage = selectedPage;
                ekle.ShowDialog();

            }

        }
        #endregion
        #region find ekranı
        //find ekranını açar
        private void AnaTabControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.F)
            {
                Find find = new Find(this);
                find.urunturu = urunturu;
                find.ShowDialog();
            }
        }

        private void ara_Click(object sender, EventArgs e)
        {
            Find find = new Find(this);
            find.urunturu = urunturu;
            find.ShowDialog();
        }

        private void arabtn_Click(object sender, EventArgs e)
        {

            Find find = new Find(this);
            find.urunturu = urunturu;
            find.ShowDialog();
        }
        #endregion
        #region Arama

        private void refresh_Click(object sender, EventArgs e)
        {
            switch (selectedPage)
            {
                case "makinalar":
                    MakinaListesi();
                    break;
                case "imalatçı":
                    İmalatciListesi();
                    break;
                case "personeller":
                    personelListele(); 
                    break;
                case "cariler":
                    CarileriListele();
                    break;
                case "ürünler":
                    urunlistele(urunturu);
                    break;
                case "operasyonekle":
                    OperasyonListesi();
                    break;




            }
        }
        #endregion
        #region Saat

        private void timer1_Tick(object sender, EventArgs e)
        {

            label2.Text = DateTime.Now.ToLongTimeString();
        }
        #endregion
        #region çıkış
        private void cikis_Click(object sender, EventArgs e)
        {


        }
        #endregion
        #endregion





        #region SQL Listeleme
        #region İmalat
        #region Makinalar
        public void MakinaListesi()
        {
            List<Makineler> list = SqlCon.Query<Makineler>("select * from Makineler where makineadi like '%" + arama + "%'", SqlCon).ToList<Makineler>();
            makinadata.DataSource = list;
            makinadata.Columns[0].Visible = false;
            makinadata.Columns[5].Visible = false;
            makinadata.Columns[1].HeaderText = "Makinanın Adı";
            makinadata.Columns[2].HeaderText = "Makinanın Stoğu";
            makinadata.Columns[3].HeaderText = "Makinanın Bakım Tarihi";
            makinadata.Columns[4].HeaderText = "Makinanın Açıklaması";
            arama = null;
        }
        #endregion
        #region imalatçı
        public void İmalatciListesi()
        {
            List<İmalatçı> list = SqlCon.Query<İmalatçı>("select * from Calisanlar_ where ünvanID = 2 and calisanadi Like '%" + arama +"%'", SqlCon).ToList<İmalatçı>();
            imalatdata.DataSource = list;
            imalatdata.Columns[0].Visible = false;
            imalatdata.Columns[5].Visible = false;
            imalatdata.Columns[1].HeaderText = "Çalışanın Adı";
            imalatdata.Columns[2].HeaderText = "Çalışanın Soyadı";
            imalatdata.Columns[3].HeaderText = "İşe Giriş Tarihi";
            imalatdata.Columns[4].HeaderText = "Telefon";
            arama = null;
        }
        #endregion
        #region Operasyon
        public void OperasyonListesi()
        {
            List<Operasyonlar> list = SqlCon.Query<Operasyonlar>("select * from Operasyon where OperasyonAdi Like '%" + arama + "%'", SqlCon).ToList<Operasyonlar>();
            operasyondata.DataSource = list;
            operasyondata.Columns[0].Visible = false;
            operasyondata.Columns[1].HeaderText = "Operasyon Adı";
            operasyondata.Columns[2].Visible = false;
            arama = null;
        }
        #endregion


        #endregion

        #region Muhasebe

        #endregion

        #region URUN
        public void urunlistele(string tür)
        {
            List<ürünler> list = SqlCon.Query<ürünler>("select * from Urun_Tablosu where urunturu = '" + tür + "' and urunadi Like '%" + arama + "%'", SqlCon).ToList<ürünler>();
            if (urunturu == "Ticari")
            {
                
                Ticaridata.DataSource = list;
                Ticaridata.Columns[0].Visible = false;
                Ticaridata.Columns[1].HeaderText = "Ürünün Adı";
                Ticaridata.Columns[2].HeaderText = "Ürünün Açıklaması";
                Ticaridata.Columns[3].HeaderText = "Ürünün Kategorisi";
                Ticaridata.Columns[4].Visible = false;
                Ticaridata.Columns[5].Visible = false;
            }
            else if (urunturu == "Mamul")
            {
                mamuldata.DataSource = list;
                mamuldata.Columns[0].Visible = false;
                mamuldata.Columns[1].HeaderText = "Ürünün Adı";
                mamuldata.Columns[2].HeaderText = "Ürünün Açıklaması";
                mamuldata.Columns[3].HeaderText = "Ürünün Kategorisi";
                mamuldata.Columns[4].Visible = false;
                mamuldata.Columns[5].Visible = false;
            }
            else if (urunturu == "YarıMamul")
            {
                ymamuldata.DataSource = list;
                ymamuldata.Columns[0].Visible = false;
                ymamuldata.Columns[1].HeaderText = "Ürünün Adı";
                ymamuldata.Columns[2].HeaderText = "Ürünün Açıklaması";
                ymamuldata.Columns[3].HeaderText = "Ürünün Kategorisi";
                ymamuldata.Columns[4].Visible = false;
                ymamuldata.Columns[5].Visible = false;
                
            }
            else if (urunturu == "Hammadde")
            {
                hammaddedata.DataSource = list;
                hammaddedata.Columns[0].Visible = false;
                hammaddedata.Columns[1].HeaderText = "Ürünün Adı";
                hammaddedata.Columns[2].HeaderText = "Ürünün Açıklaması";
                hammaddedata.Columns[3].HeaderText = "Ürünün Kategorisi";
                hammaddedata.Columns[4].Visible = false;
                hammaddedata.Columns[5].Visible = false;
            }
                arama = null;
        }
        #endregion

        #region Satış

        #endregion

        #region Satın Alma

        #endregion

        #region Raporlar

        #endregion

        #region Personeller
        public void personelListele()
        {
            
            
                List<Personel> list = SqlCon.Query<Personel>("select calisanid,calisanadi, calisansoyadi , u.unvanadi as [Ünvanı],isegiris, telefon  from Calisanlar_ c inner join Unvan u on u.UnvanID = c.ünvanID where calisanadi Like '%" + arama + "%'", SqlCon).ToList<Personel>();
               
                personeldata.DataSource = list;
                personeldata.Columns[1].HeaderText = "Çalışanın Adı";
                personeldata.Columns[2].HeaderText = "Çalışanın Soyadı";
                personeldata.Columns[3].HeaderText = "Çalışanın Ünvanı";
                personeldata.Columns[4].HeaderText = "İşe Giriş Tarihi";
                personeldata.Columns[5].HeaderText = "Telefon";
                personeldata.Columns[0].Visible = false;
                personeldata.Columns[6].Visible = false;

            arama = null;
            

        }
        #endregion

        #region Cariler
        public void CarileriListele()
        {
            List<Cariler> list = SqlCon.Query<Cariler>("select * from Cari_Hesaplar where CariAdi Like '%" + arama + "%'", SqlCon).ToList<Cariler>();
            carihesapdata.DataSource = list;
            carihesapdata.Columns[0].Visible = false;
            carihesapdata.Columns[1].HeaderText = "Carinin Adı";
            carihesapdata.Columns[2].HeaderText = "Telefonu";
            carihesapdata.Columns[3].HeaderText = "Adresi";
            carihesapdata.Columns[4].HeaderText = "Hesap Numarası";
            carihesapdata.Columns[5].HeaderText = "Ülke";
            carihesapdata.Columns[6].HeaderText = "Şehir";
            carihesapdata.Columns[7].HeaderText = "Posta Kodu";
            carihesapdata.Columns[8].HeaderText = "Carinin Türü";
            carihesapdata.Columns[9].Visible = false;
            arama = null;
        }
        #endregion

        #endregion

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
