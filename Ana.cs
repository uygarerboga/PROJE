﻿using System;
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

namespace ERP_PROJESİ
{
    
    public partial class Ana : Form
    {

        SqlConnection SqlCon = new SqlConnection(@"Data Source=DESKTOP-PRMBC7J; initial Catalog = ERP; Integrated Security = True");
        public string selectedPage { get; set; }

        public string arama;
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
        }

        #endregion
        #region personel

        private void personeller_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "personeller";
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
        }

        private void cariler_Leave(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = null;
        }
        #endregion
        #region ÜRÜNLER
        private void ticariurunler_Enter(object sender, EventArgs e)
        {
            AnaTabControl.ContextMenuStrip = contextMenuStrip1;
            selectedPage = "ürünler";
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
                find.ShowDialog();
            }
        }

        private void ara_Click(object sender, EventArgs e)
        {
            Find find = new Find(this);
            find.ShowDialog();
        }

        private void arabtn_Click(object sender, EventArgs e)
        {

            Find find = new Find(this);
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
            arama = null;
        }
        #endregion

        #endregion

        #region Muhasebe

        #endregion

        #region URUN

        #endregion

        #region Satış

        #endregion

        #region Satın Alma

        #endregion

        #region Raporlar

        #endregion

        #region Personeller

        #endregion

        #region Cariler

        #endregion

        #endregion

        
    }
}
