using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.İmalat
{
    public class uretimemirleri
    {
        public int uretimemriID { get; set; }
        public string calisanadi { get; set; }

        public DateTime verilistarihi { get; set; }
        public DateTime baslangıctarihi { get; set; }
        public DateTime bitistarihi { get; set; }
        public DateTime planlananbaslangıctarihi { get; set; }
        public int siparisID { get; set; }
        public string urunadi { get; set; }
        public int rotaID { get; set; }
        public string uretimindurumu { get; set; }
        public bool sil { get; set; }


    }
}
