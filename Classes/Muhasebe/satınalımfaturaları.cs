using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP_PROJESİ.Classes.Muhasebe
{
    internal class satınalımfaturaları
    {
        public int faturaID { get; set; }
        public int CariID { get; set; }
        public int siparisID { get; set; }
        public DateTime tarih { get; set; }
        public decimal tutar { get; set; }
        public string odemebilgisi { get; set; }
        public bool iade { get; set; }
        public bool sil { get; set;}
    }
}
