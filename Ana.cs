﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using ERP_PROJESİ.Classes;
using ERP_PROJESİ.Classes.İmalat;
using ERP_PROJESİ.Classes.Muhasebe;
using ERP_PROJESİ.Classes.Satış;
using ERP_PROJESİ.Classes.Ürünler;

namespace ERP_PROJESİ
{
    
    public partial class Ana : Form
    {

        SqlConnection SqlCon = new SqlConnection(@"Data Source=UYGARERBOGA\UYGARERBOGA; initial Catalog = ERP; Integrated Security = True");
        public string selectedPage { get; set; }

        public int selectedid { get; set; }

        public string arama;

        public string urunturu;
        

        List<uretimemirleri> uretimemirlerilist = new List<uretimemirleri>();
        public Ana()
        {
            InitializeComponent();
            UretimEmriListesi();
            SatisSiparisleriListesi();

        }
        #region form load

        public void Ana_Load(object sender, EventArgs e)
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
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\home.png"));
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\tool.png"));
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\dollar.png"));
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\cubes.png"));
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\checkout.png"));
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\delivery-box.png"));
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\report.png"));
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\employees.png"));
            iconsList.Images.Add(Image.FromFile(@"..\..\Icons\customer-feedback.png"));

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
            HakedislerListesi();
        }

        #endregion
        #region Satın Alım Faturaları

        private void satinalimfaturası_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "satinalimfaturası";
            SatınAlımFaturalarıListele();
        }

        #endregion
        #region Satın Alım İade
        private void satinalimiadefaturasi_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "satinalimiade";
            SatınAlımİadeListele();
        }
        #endregion
        #region Satış Faturaları

        private void satısfaturaları_Enter(object sender, EventArgs e)
        {

            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "satisfaturalari";
            SatışFaturalarıListele();
        }


        #endregion
        #region Satış İade 
        private void satisiade_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "satisiade";
            SatışİadeListele();
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
        #region Kategori
        private void kategori_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "kategori";
            kategoriListele();
        }
        #endregion
        #endregion
        #region satış
        #region satış şiparişleri
        private void satısSiparisleri_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "satissiparisleri";
            SatisSiparisleriListesi();
        }
        #endregion
        #region onteklif
        private void onteklif_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "onteklif";
            ÖnTeklifListele();
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
   
    
        #endregion
        #region Satın Alma Siparişleri
        private void satınalmasiparişleri_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "satınalmasiparişleri";
        }
        #endregion
        #region Satın Alma İrsaliyeleri
        private void satınalmairsaliyeleri_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "satınalmairsaliyeleri";
        }
        #endregion

      
        #region işlevler (ekleme-çıkarma-arama)

        #region Ekleme Ekranı
        private void ekle_Click(object sender, EventArgs e)
        {
            if (selectedPage != "")
            {

                EklemeEkranı ekleekran = new EklemeEkranı(this);
                ekleekran.selectedPage = selectedPage;
                ekleekran.ShowDialog();
                
            }


        }
        private void eklebtn_Click(object sender, EventArgs e)
        {
            if (selectedPage != "")
            {

                EklemeEkranı ekleekran = new EklemeEkranı(this);
                ekleekran.selectedPage = selectedPage;
                ekleekran.ShowDialog();
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

            if (e.KeyCode == Keys.Delete)
            {
                delete_Click(this, null);
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

        public void refresh_Click(object sender, EventArgs e)
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
                    SatisSiparisleriListesi();
                    break;
                case "onteklif":
                    ÖnTeklifListele();
                    break;
                case "kategori":
                    kategoriListele();
                     break;
                case "hakedis":
                    HakedislerListesi();
                    break;
                case "satinalimfaturası":
                    SatınAlımFaturalarıListele();
                    break;
                case "satisalımiade":
                    SatınAlımİadeListele();
                    break;
                case "satisfaturalari":
                    SatışFaturalarıListele();
                    break;
                case "satisiade":
                    SatışİadeListele();
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
        #region Hakedişler
        public void HakedislerListesi()
        {
            List<Hakedisler> list = SqlCon.Query<Hakedisler>("select * from Hakedis where tarih like '%" + arama + "%'", SqlCon).ToList<Hakedisler>();
            hakedisdata.DataSource = list;
            hakedisdata.Columns[0].Visible = false;
            hakedisdata.Columns[1].HeaderText = "Tarih";
            hakedisdata.Columns[2].Visible = false;
            arama = null;
        }
        #endregion
        #region Satın Alım Faturaları
        public void SatınAlımFaturalarıListele()
        {
            List<satınalımfaturaları> list = SqlCon.Query<satınalımfaturaları>("select * from Satin_Alma_Faturalari where CariID like '%" + arama + "%'", SqlCon).ToList<satınalımfaturaları>();
            satinalimfaturasidata.DataSource = list;
            satinalimfaturasidata.Columns[0].Visible = false;
            satinalimfaturasidata.Columns[1].HeaderText = "Sipariş ID'si";
            satinalimfaturasidata.Columns[2].HeaderText = "Cari ID'si";
            satinalimfaturasidata.Columns[3].HeaderText = "Tarih";
            satinalimfaturasidata.Columns[4].HeaderText = "Tutar";
            satinalimfaturasidata.Columns[5].HeaderText = "Odeme Bilgisi";
            satinalimfaturasidata.Columns[6].HeaderText = "İade";
            satinalimfaturasidata.Columns[7].Visible = false;
            arama = null;
        }
        #endregion
        #region Satın Alım İade
        public void SatınAlımİadeListele()
        {
            List<satınalımiade> list = SqlCon.Query<satınalımiade>("select * from Satis_Faturalari where faturaID like '%" + arama + "%'", SqlCon).ToList<satınalımiade>();
            satinaidedata.DataSource = list;
            satinaidedata.Columns[0].Visible = false;
            satinaidedata.Columns[1].HeaderText = "Cari İD'si";
            satinaidedata.Columns[2].HeaderText = "Fatura Tarihi";
            satinaidedata.Columns[3].HeaderText = "Tutar";
            satinaidedata.Columns[4].HeaderText = "Odeme Bilgisi";
            satinaidedata.Columns[5].Visible = false;
            arama = null;
        }
        #endregion
        #region Satış Faturaları
        public void SatışFaturalarıListele()
        {
            List<satışfaturaları> list = SqlCon.Query<satışfaturaları>("select * from Satis_Faturalari where faturaID like '%" + arama + "%'", SqlCon).ToList<satışfaturaları>();
            satisfaturalaridata.DataSource = list;
            satisfaturalaridata.Columns[0].Visible = false;
            satisfaturalaridata.Columns[1].HeaderText = "Cari ID'si";
            satisfaturalaridata.Columns[2].HeaderText = "Fatura Tarihi";
            satisfaturalaridata.Columns[3].HeaderText = "Tutar";
            satisfaturalaridata.Columns[4].HeaderText = "Odeme Bilgisi";
            satisfaturalaridata.Columns[5].Visible = false;
            arama = null;
        }
        #endregion
        #region Satış İade
        public void SatışİadeListele()
        {
            List<satışiade> list = SqlCon.Query<satışiade>("select * from Satis_Iade_Irsaliyesi where satisiadeirsaliyesiID like '%" + arama + "%'", SqlCon).ToList<satışiade>();
            satisiadedata.DataSource = list;
            satisiadedata.Columns[0].Visible = false;
            satisiadedata.Columns[1].HeaderText = "Fatura ID'si";
            satisiadedata.Columns[2].HeaderText = "Tarih";
            satisiadedata.Columns[3].HeaderText = "Kargo Firması";
            satisiadedata.Columns[4].Visible = false;
            arama = null;
        }
        #endregion
        #endregion

        #region URUN
        #region urunler
        int tur;
        public void urunlistele(string tür)
        {
            List<ürünler> list = SqlCon.Query<ürünler>("select urunID,urunadi,urunacıklaması,uk.kategoriadi as [kategori], urunturu, rafkodu,stok_miktarı,u.sil,u.urunkategoriID from Urun_Tablosu u inner join Urun_Kategorileri uk on  uk.urunkategoriID = u.urunkategoriID where urunturu = '" + tür + "' and urunadi Like '%" + arama + "%' and u.sil = 'True'", SqlCon).ToList<ürünler>();
            if (urunturu == "Ticari")
            {
                
                Ticaridata.DataSource = list;
                Ticaridata.Columns[0].Visible = false;
                Ticaridata.Columns[1].HeaderText = "Ürünün Adı";
                Ticaridata.Columns[2].HeaderText = "Ürünün Açıklaması";
                Ticaridata.Columns[3].HeaderText = "Ürünün Kategorisi";
                Ticaridata.Columns[6].HeaderText = "Ürünün Stoğu";
                Ticaridata.Columns[7].Visible = false;
                Ticaridata.Columns[4].Visible = false;
                Ticaridata.Columns[8].Visible = false;
                tur= 0;
            }
            else if (urunturu == "Mamul")
            {
                mamuldata.DataSource = list;
                mamuldata.Columns[0].Visible = false;
                mamuldata.Columns[1].HeaderText = "Ürünün Adı";
                mamuldata.Columns[2].HeaderText = "Ürünün Açıklaması";
                mamuldata.Columns[3].HeaderText = "Ürünün Kategorisi";
                mamuldata.Columns[6].HeaderText = "Ürünün Stoğu";
                mamuldata.Columns[7].Visible = false;
                mamuldata.Columns[4].Visible = false;
                mamuldata.Columns[8].Visible = false;
                tur = 1;
            }
            else if (urunturu == "YarıMamul")
            {
                ymamuldata.DataSource = list;
                ymamuldata.Columns[0].Visible = false;
                ymamuldata.Columns[1].HeaderText = "Ürünün Adı";
                ymamuldata.Columns[2].HeaderText = "Ürünün Açıklaması";
                ymamuldata.Columns[3].HeaderText = "Ürünün Kategorisi";
                ymamuldata.Columns[6].HeaderText = "Ürünün Stoğu";
                ymamuldata.Columns[7].Visible = false;
                ymamuldata.Columns[4].Visible = false;
                ymamuldata.Columns[8].Visible = false;
                tur = 2;

            }
            else if (urunturu == "Hammadde")
            {
                hammaddedata.DataSource = list;
                hammaddedata.Columns[0].Visible = false;
                hammaddedata.Columns[1].HeaderText = "Ürünün Adı";
                hammaddedata.Columns[2].HeaderText = "Ürünün Açıklaması";
                hammaddedata.Columns[3].HeaderText = "Ürünün Kategorisi";
                hammaddedata.Columns[6].HeaderText = "Ürünün Stoğu";
                hammaddedata.Columns[7].Visible = false;
                hammaddedata.Columns[4].Visible = false;
                hammaddedata.Columns[8].Visible = false;
                tur = 3;
            }
                arama = null;
        }
        #endregion
        #region kategori
        public void kategoriListele()
        {
            List<kategori> list = SqlCon.Query<kategori>("select * from Urun_Kategorileri where kategoriadi Like '%" + arama + "%' and sil = 'True'", SqlCon).ToList<kategori>();   
            kategoridata.DataSource = list;
            kategoridata.Columns[0].Visible=false;
            kategoridata.Columns[1].HeaderText="Kategori Adı";
            kategoridata.Columns[2].HeaderText="Kategori Açıklaması";
            kategoridata.Columns[3].Visible=false;
        }
        #endregion
        #endregion

        #region Satış
        #region Satış siparisleri
        public void SatisSiparisleriListesi()
        {
            List<Satışsipariş> list = SqlCon.Query<Satışsipariş>("select * from Satis_Siparisleri where gidenSiparisID Like '%" + arama + "%'", SqlCon).ToList<Satışsipariş>();
            Satıssiparisdata.DataSource = list;
            Satıssiparisdata.Columns[0].HeaderText = "";



        }
        #endregion
        #region Ön Teklif
        public void ÖnTeklifListele()
        {
            List<Önteklif> list = SqlCon.Query<Önteklif>("select * from On_Teklif where onteklifID Like '%" + arama + "%'", SqlCon).ToList<Önteklif>();
            onteklifdata.DataSource = list;
            onteklifdata.Columns[0].HeaderText = "";



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
            
            
                List<Personel> list = SqlCon.Query<Personel>("select calisanid,calisanadi, calisansoyadi , u.unvanadi as [Ünvanı],isegiris, telefon, c.sil as [sil]  from Calisanlar_ c inner join Unvan u on u.UnvanID = c.ünvanID where calisanadi Like '%" + arama + "%' and c.sil = 'True'" , SqlCon).ToList<Personel>();
               
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


        #region güncelleme ekranı
        #region imalat
        #region uretimemri

        private void uretimemridata_DoubleClick(object sender, EventArgs e)
        {

            selectedid = int.Parse(uretimemridata.CurrentRow.Cells[0].Value.ToString());
            EklemeEkranı ekleekran = new EklemeEkranı(this);
            ekleekran.selectedPage = selectedPage;
            ekleekran.selectedid = selectedid;
            ekleekran.Show();
            this.Enabled = false;
            ekleekran.textBoxes[0].Text = uretimemridata.CurrentRow.Cells[1].Value.ToString();
        }
        #endregion
        #endregion
        #region Urun
        #region ticari urun double

        private void Ticaridata_DoubleClick(object sender, EventArgs e)
        {

            selectedid = int.Parse(Ticaridata.CurrentRow.Cells[0].Value.ToString());
            EklemeEkranı ekleekran = new EklemeEkranı(this);
            ekleekran.selectedPage = selectedPage;
            ekleekran.selectedid = selectedid;
            ekleekran.Show();
            ekleekran.textBoxes[0].Text = Ticaridata.CurrentRow.Cells[1].Value.ToString(); //ad
            ekleekran.textBoxes[1].Text = Ticaridata.CurrentRow.Cells[2].Value.ToString(); //aciklama
            ekleekran.textBoxes[2].Text = Ticaridata.CurrentRow.Cells[5].Value.ToString(); //raf kodu
            ekleekran.ComboBoxes[0].SelectedIndex = (int.Parse(Ticaridata.CurrentRow.Cells[8].Value.ToString())-1); //kategori id
            ekleekran.textBoxes[3].Text = Ticaridata.CurrentRow.Cells[6].Value.ToString(); //miktar
            ekleekran.radioButtons[0].Select(); //urunun türü
            ekleekran.Visible = false;
            ekleekran.ShowDialog();
        }
        #endregion
        #region mamul urun double
        private void mamuldata_DoubleClick(object sender, EventArgs e)
        {
            selectedid = int.Parse(mamuldata.CurrentRow.Cells[0].Value.ToString());
            EklemeEkranı ekleekran = new EklemeEkranı(this);
            ekleekran.selectedPage = selectedPage;
            ekleekran.selectedid = selectedid;
            ekleekran.Show();
            ekleekran.textBoxes[0].Text = mamuldata.CurrentRow.Cells[1].Value.ToString(); //ad
            ekleekran.textBoxes[1].Text = mamuldata.CurrentRow.Cells[2].Value.ToString(); //aciklama
            ekleekran.textBoxes[2].Text = mamuldata.CurrentRow.Cells[5].Value.ToString(); //raf kodu
            ekleekran.ComboBoxes[0].SelectedIndex = (int.Parse(mamuldata.CurrentRow.Cells[8].Value.ToString()) - 1); //kategori id
            ekleekran.textBoxes[3].Text = mamuldata.CurrentRow.Cells[6].Value.ToString(); //miktar
            ekleekran.radioButtons[1].Select(); //urunun türü
            ekleekran.Visible = false;
            ekleekran.ShowDialog();
        }
        #endregion

        #region yarı mamul double click
        private void ymamuldata_DoubleClick(object sender, EventArgs e)
        {
            selectedid = int.Parse(ymamuldata.CurrentRow.Cells[0].Value.ToString());
            EklemeEkranı ekleekran = new EklemeEkranı(this);
            ekleekran.selectedPage = selectedPage;
            ekleekran.selectedid = selectedid;
            ekleekran.Show();
            ekleekran.textBoxes[0].Text = ymamuldata.CurrentRow.Cells[1].Value.ToString(); //ad
            ekleekran.textBoxes[1].Text = ymamuldata.CurrentRow.Cells[2].Value.ToString(); //aciklama
            ekleekran.textBoxes[2].Text = ymamuldata.CurrentRow.Cells[5].Value.ToString(); //raf kodu
            ekleekran.ComboBoxes[0].SelectedIndex = (int.Parse(ymamuldata.CurrentRow.Cells[8].Value.ToString()) - 1); //kategori id
            ekleekran.textBoxes[3].Text = ymamuldata.CurrentRow.Cells[6].Value.ToString(); //miktar
            ekleekran.radioButtons[2].Select(); //urunun türü
            ekleekran.Visible = false;
            ekleekran.ShowDialog();
        }
        #endregion
        #region hammadde double
        private void hammaddedata_DoubleClick(object sender, EventArgs e)
        {
            
            selectedid = int.Parse(hammaddedata.CurrentRow.Cells[0].Value.ToString());
            EklemeEkranı ekleekran = new EklemeEkranı(this);
            ekleekran.selectedPage = selectedPage;
            ekleekran.selectedid = selectedid;
            ekleekran.Show();
            ekleekran.textBoxes[0].Text = hammaddedata.CurrentRow.Cells[1].Value.ToString(); //ad
            ekleekran.textBoxes[1].Text = hammaddedata.CurrentRow.Cells[2].Value.ToString(); //aciklama
            ekleekran.textBoxes[2].Text = hammaddedata.CurrentRow.Cells[5].Value.ToString(); //raf kodu
            ekleekran.ComboBoxes[0].SelectedIndex = (int.Parse(hammaddedata.CurrentRow.Cells[8].Value.ToString()) - 1); //kategori id
            ekleekran.textBoxes[3].Text = hammaddedata.CurrentRow.Cells[6].Value.ToString(); //miktar
            
            ekleekran.radioButtons[3].Select(); //urunun türü
            ekleekran.Visible = false;
            ekleekran.ShowDialog();
        }
        #endregion
        #region kategori double click
        private void kategoridata_DoubleClick(object sender, EventArgs e)
        {
            selectedid = int.Parse(kategoridata.CurrentRow.Cells[0].Value.ToString());
            EklemeEkranı ekleekran = new EklemeEkranı(this);
            ekleekran.selectedPage = selectedPage;
            ekleekran.selectedid = selectedid;
            ekleekran.Show();
            ekleekran.textBoxes[0].Text = kategoridata.CurrentRow.Cells[1].Value.ToString(); //ad
            ekleekran.textBoxes[1].Text = kategoridata.CurrentRow.Cells[2].Value.ToString(); //aciklama
            ekleekran.Visible = false;
            ekleekran.ShowDialog();

        }
        #endregion
        #endregion



        private void update_Click(object sender, EventArgs e)
        {
            EklemeEkranı ekleekran = new EklemeEkranı(this);
            switch (selectedPage)
            {
                case "üretimemri":
                    selectedid = int.Parse(uretimemridata.CurrentRow.Cells[0].Value.ToString());
                    ekleekran.selectedPage = selectedPage;
                    ekleekran.selectedid = selectedid;
                    ekleekran.ShowDialog();
                    ekleekran.EklemeEkranı_Load(this, null);
                    ekleekran.textBoxes[0].Text = uretimemridata.CurrentRow.Cells[1].Value.ToString();
                    break;
                #region ürünler

                case "ürünler":
                    selectedid = int.Parse(hammaddedata.CurrentRow.Cells[0].Value.ToString());
                    ekleekran.selectedPage = selectedPage;
                    ekleekran.selectedid = selectedid;
                    ekleekran.Show();
                    this.Enabled = false;
                    ekleekran.textBoxes[0].Text = hammaddedata.CurrentRow.Cells[1].Value.ToString(); //ad
                    ekleekran.textBoxes[1].Text = hammaddedata.CurrentRow.Cells[2].Value.ToString(); //aciklama
                    ekleekran.textBoxes[2].Text = hammaddedata.CurrentRow.Cells[5].Value.ToString(); //raf kodu
                    ekleekran.ComboBoxes[0].SelectedIndex = (int.Parse(hammaddedata.CurrentRow.Cells[8].Value.ToString()) - 1); //kategori id
                    ekleekran.textBoxes[3].Text = hammaddedata.CurrentRow.Cells[6].Value.ToString(); //miktar
                    switch (tur)
                    {
                        case 0:
                            ekleekran.radioButtons[0].Select(); //urunun türü
                            break;
                        case 1:
                            ekleekran.radioButtons[1].Select(); //urunun türü
                            break;
                        case 2:
                            ekleekran.radioButtons[2].Select(); //urunun türü
                            break;
                        case 3:
                            ekleekran.radioButtons[3].Select(); //urunun türü
                            break;
                        default:
                            break;
                    }
                    
                    break;
                #endregion
                default:
                    break;
            }
        }








        #endregion

        #region silme
        #region Ürünler
        #region kategoriler


        private void kategoridata_Click(object sender, EventArgs e)
        {

            selectedid = int.Parse(kategoridata.CurrentRow.Cells[0].Value.ToString());
        }

        public void kategorisilme()
        {

            
            DynamicParameters param = new DynamicParameters();
            param.Add("@kategoriid", selectedid);
            param.Add("@sil", "False");
            SqlCon.Execute("kategorisil", param, commandType: CommandType.StoredProcedure);


            
        }

        #endregion

        #endregion

        #region silme düğmesi
        public void delete_Click(object sender, EventArgs e)
        {
            if (SqlCon.State == ConnectionState.Closed)
            {
                SqlCon.Open();
            }
            switch (selectedPage)
            {
                case "kategori":
                    kategorisilme();
                    break;
                case "ürünler":
                    switch (urunturu)
                    {
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (SqlCon.State == ConnectionState.Open)
            {
                SqlCon.Close();
            }

            refresh_Click(this, null);
        }
        #endregion

        #region silme sağclick
        private void sil_Click(object sender, EventArgs e)
        {
            delete_Click(this, null);
        }

        #endregion

        #region delbutton

        #endregion

        #endregion

        private void satinaidedata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

      
    }
}
