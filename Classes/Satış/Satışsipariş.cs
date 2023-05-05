using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.Satış
{
    internal class Satışsipariş
    {
        public int gidenSiparisID { get; set; }
        public int CariID { get; set; }
        public int Calısanid { get; set; }
        public DateTime Siparistarihi { get; set; }
        public DateTime Teslimtarihi { get; set; }
        public string gönderilenkargofirması { get; set; }
    }
}
