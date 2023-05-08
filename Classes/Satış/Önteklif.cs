using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.Satış
{
    internal class Önteklif
    {
        public int onteklifID { get; set; }
        public int cariID { get; set; }
        public DateTime tarih { get; set; }
        public int totaltutar { get; set; }
        public string Onaydurumu { get; set; }
        public bool sil { get; set; }
    }
}
