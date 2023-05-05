using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_PROJESİ.Classes.İmalat
{
    internal class gunlukislemraporları
    {
        public int islemID { get; set; }
        public int OperasyonID { get; set; }
        public int kullanılanmakineID { get; set; }
        public string urunadi { get; set; }
        public string calisanadi { get; set; }
        public bool sil { get; set; }
    }
}
