using System.Collections.Generic;
using System;

namespace IkinciElAracUI.UI.Models.Core.DTO
{
    public class IhaleDTO
    {

        public int IhaleID { get; set; }
        public string IhaleAdi { get; set; }
        public int AracID { get; set; }

        public DateTime IhaleBaslangicTarihi { get; set; }

        public DateTime IhaleBitisTarihi { get; set; }

        public bool KurumsalMi { get; set; }
        public int KullaniciID { get; set; }

        public string KaydedenKullanici { get; set; }

        public int IhaleStatuID { get; set; }

        public string IhaleStatuAdi { get; set; }

        public DateTime KaydetmeZamani { get; set; }
        public decimal IhaleBaslangicFiyati { get; set; }
        public decimal MaxAlimFiyati { get; set; }

        public List<IhaleAracDTO> IhaleAracDTOs { get; set; }


    }
}
