using System;
using System.Collections.Generic;

namespace IkinciElAracUI.UI.Models.VM
{
    public class IhaleVM
    {
        public int IhaleID { get; set; }
        public string IhaleAdi { get; set; }

        public DateTime IhaleBaslangicTarihi { get; set; }

        public DateTime IhaleBitisTarihi { get; set; }

        public bool KurumsalMi { get; set; }
        public int KullaniciID { get; set; }

        public string KaydedenKullanici { get; set; }

        public int IhaleStatuID { get; set; }

        public string IhaleStatuAdi { get; set; }

        public DateTime KaydetmeZamani { get; set; }
        public List<IhaleAracVM> IhaleAracVMs { get; set; }

        //public string IhaleBaslangicTarihi { get; set; }
        //public string IhaleBaslangicSaati { get; set; }
        //public string IhaleBitisTarihi { get; set; }
        //public string IhaleBitisSaati { get; set; }

    }
}
