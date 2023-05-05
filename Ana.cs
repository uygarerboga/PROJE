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
using ERP_PROJESİ.Classes.Satış;
using ERP_PROJESİ.Classes.Ürünler;

namespace ERP_PROJESİ
{
    
    public partial class Ana : Form
    {
        
        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-PRMBC7J; initial Catalog = ERP; Integrated Security = True");
        public string selectedPage { get; set; }

        public int selectedid { get; set; }

        public string arama;

        public string urunturu;

        EklemeEkranı Eklemeekranı = new EklemeEkranı();
        List<uretimemirleri> uretimemirlerilist = new List<uretimemirleri>();
        public Ana()
        {
            InitializeComponent();
            UretimEmriListesi();
            satissiparisleriListesi();
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
        #region ana sayfa
        private void AnaTabControl_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
        }
        #endregion
        // sağ click için ekrana girmesi lazım bunu geçerli sayfalarda tekrarlıcaz fakat ilk girişte afallıyabiliyor. tekrar tab seçmen lazım.
        // selected page ayarlama
        #region İmalat
        #region üretim emirleri

        private void uretimEmirleri_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "üretimemri";
            UretimEmriListesi();
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
            rotaListele();
        }

        #endregion
        #region günlükrapor

        private void gunlukRaporEkle_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "günlükraporekle";
            gunlukislemListesi();
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
        #region imalatçı
        
        private void imalatcı_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
            selectedPage = "imalatçı";
            İmalatciListesi();
        }
        #endregion
        #endregion
        #region Muhasebe

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
        #endregion
        #region ÜRÜNLER
        #region Ticari ürünler

        private void urunler_Enter1(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
            urunturu = "Ticari";
            urunlistele(urunturu);

        }

        #endregion
        #region Mamul

        private void urunler_Enter2(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
            urunturu = "Mamul";
            urunlistele(urunturu);

        }
        #endregion
        #region Yarı Mamul

        private void urunler_Enter3(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
            urunturu = "YarıMamul";
            urunlistele(urunturu);

        }
        #endregion
        #region Hammadde

        private void urunler_Enter4(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
            urunturu = "Hammadde";
            urunlistele(urunturu);

        }
        #endregion
        #endregion
        #region personel

        private void personeller_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "personeller";
            personelListele();
            
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
        #region Satış siparişleri
        private void satısTabControl_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "satissipariş";
            satissiparisleriListesi();
        }
        #endregion

        #endregion
        #region işlevler (ekleme-çıkarma-arama)

        #region Ekleme Ekranı
        private void ekle_Click(object sender, EventArgs e)
        {
            if (selectedPage != "")
            {

                Eklemeekranı.selectedid = selectedid;
                Eklemeekranı.selectedPage = selectedPage;
                Eklemeekranı.ShowDialog();
                Eklemeekranı.uretimemirleri = uretimemirlerilist;
                
            }


        }
        private void eklebtn_Click(object sender, EventArgs e)
        {
            if (selectedPage != "")
            {
                
                Eklemeekranı.selectedid = selectedid;
                Eklemeekranı.selectedPage = selectedPage;
                Eklemeekranı.ShowDialog();
                Eklemeekranı.uretimemirleri = uretimemirlerilist;
            }

        }

        #region veri seçme

        #endregion
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
        #region Yenileme

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
                case "rotalar":
                    rotaListele();
                    break;
                case "üretimemri":
                    UretimEmriListesi();
                    break;
                case "günlükraporekle":
                    gunlukislemListesi();
                    break;
                case "satissipariş":
                    satissiparisleriListesi();
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
        #region Üretim Emri
        public void UretimEmriListesi()
        {
            List<uretimemirleri> list = SqlCon.Query<uretimemirleri>("select * from Uretim_Emirleri u inner join Calisanlar_ c on c.calisanid = u.calısanID inner join Urun_Tablosu urun on urun.urunID = u.cıkanurunID where uretimemriID like '%" + arama + "%'", SqlCon).ToList<uretimemirleri>();
            uretimemridata.DataSource = list;
            uretimemirlerilist = list;
            uretimemridata.Columns[0].HeaderText = "Kodu";
            uretimemridata.Columns[1].HeaderText = "Çalışan";
            uretimemridata.Columns[2].HeaderText = "Veriliş Tarihi";
            uretimemridata.Columns[3].HeaderText = "Başlangıç Tarihi";
            uretimemridata.Columns[4].HeaderText = "Bitiş Tarihi";
            uretimemridata.Columns[5].HeaderText = "Planlanan Bitiş Tarihi";
            uretimemridata.Columns[6].HeaderText = "Siparişin ID'si";
            uretimemridata.Columns[7].HeaderText = "Ürün Adı";
            uretimemridata.Columns[8].HeaderText = "Rotanın ID'si";
            uretimemridata.Columns[9].HeaderText = "Üretimin Durumu";
            uretimemridata.Columns[10].Visible = false;
            arama = null;
        }
        #endregion
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
        #region Rota
        public void rotaListele()
        {
            List<Rota> list = SqlCon.Query<Rota>("select * from Rota where rotaID Like '%" + arama + "%'", SqlCon).ToList<Rota>();

            rotadata.DataSource = list;
            rotadata.Columns[2].Visible = false;
            rotadata.Columns[0].HeaderText = "Rotanın ID'si";
            rotadata.Columns[1].HeaderText = "Rotanın Açıklaması";

            arama = null;
        }
        #endregion
        #region Günlük işlem Raporları
        public void gunlukislemListesi()
        {
            List<gunlukislemraporları> list = SqlCon.Query<gunlukislemraporları>("select * from [İSLEM RAPORU] ir \r\ninner join Urun_Tablosu urun on urun.urunID = ir.kullanılanmalzemeID \r\ninner join Calisanlar_ c on c.calisanid = ir.calisanid where islemID Like '%" + arama + "%'", SqlCon).ToList<gunlukislemraporları>();

            gunlukdata.DataSource = list;
            gunlukdata.Columns[0].HeaderText = "İşlem ID'si";
            gunlukdata.Columns[1].HeaderText = "Operasyon ID'si";
            gunlukdata.Columns[2].HeaderText = "Kullanılan Makina";
            gunlukdata.Columns[3].HeaderText = "Ürün Adı";
            gunlukdata.Columns[4].HeaderText = "Çalışan";
            gunlukdata.Columns[5].Visible = false;

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
        #region Satış siparisleri
        public void satissiparisleriListesi()
        {
            List<Satışsipariş> list = SqlCon.Query<Satışsipariş>("select * from Satis_Siparisleri where gidenSiparisID Like '%" + arama + "%'", SqlCon).ToList<Satışsipariş>();
            Satıssiparisdata.DataSource = list;
            Satıssiparisdata.Columns[0].HeaderText = "";

        }
        #endregion
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
        //Personeller tamam
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

        //Cariler tamam
        #endregion

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void uretimemridata_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            
        }

        private void uretimemridata_Click(object sender, EventArgs e)
        {
           
        }

        private void uretimemridata_DoubleClick(object sender, EventArgs e)
        {
            //selected id öbür tarafa geçmiyor anasını sikim

            selectedid = int.Parse(uretimemridata.CurrentRow.Cells[0].Value.ToString());

            if (selectedPage != "")
            {
                EklemeEkranı aa = new EklemeEkranı();
                aa.selectedid = selectedid;
                aa.selectedPage = selectedPage;
                aa.ShowDialog();
                aa.uretimemirleri = uretimemirlerilist;
                
            }
        }
    }
}
