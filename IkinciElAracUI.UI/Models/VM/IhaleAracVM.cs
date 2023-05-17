using System;

namespace IkinciElAracUI.UI.Models.VM
{
    public class IhaleAracVM
    {
        public int IhaleAracID { get; set; }
        public int IhaleID { get; set; }
        public int AracID { get; set; }
        public decimal IhaleBaslangicFiyati { get; set; }
        public decimal MaxAlimFiyati { get; set; }
        public int AracModelID { get; set; }

        public string AracModelAdi { get; set; }
        public int AracMarkaID { get; set; }

        public string AracMarkaAdi { get; set; }
        public bool? KurumsalMi { get; set; }
        public string KaydedenKullanici { get; set; }
        public DateTime KaydedilmeZamanı { get; set; }

    }
}
