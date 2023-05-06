using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.Muhasebe
{
    internal class satışfaturaları
    {
        public int faturaID { get; set; }
        public int CariID { get; set; }
        public DateTime faturatarihi { get; set; }
        public int Tutar { get; set; }
        public string odemebilgisi { get; set; }
        public bool iade { get; set; }
        public bool sil { get; set; }
    }
}
