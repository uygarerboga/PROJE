using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.Ürünler
{
    internal class ürünler
    {
        public int urunID { get; set; }

        public string urunadi { get; set; }

        public string urunacıklaması { get; set; }

        public string kategori { get; set; }
        
        

        public string urunturu { get; set; }

        public int rafkodu { get; set; }

        public int stok_miktarı { get; set; }

        public bool sil { get; set; }

        public int urunkategoriID { get; set; }
    }
}
