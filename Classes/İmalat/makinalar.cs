using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes
{
    internal class Makineler
    {
        public int makineID { get; set; }
        public string makineadi { get; set; }
        public int makinestogu { get; set; }
        public DateTime bakımtarihi { get; set; }
        public string makineacıklaması { get; set; }
        public bool sil { get; set; }
    }
}
