using System.Collections.Generic;

namespace IkinciElAracUI.UI.Models.Core.DTO
{
    public class IhaleFiyatDTO
    {
        public int IhaleFiyatID { get; set; }
        public int IhaleID { get; set; }
        public string AracModelAdi { get; set; }
        public string AracMarkaAdi { get; set; }
        public string Plaka { get; set; }

        public List<FiyatDTO> FiyatList { get; set; }
    }
}
