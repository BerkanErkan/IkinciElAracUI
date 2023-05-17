using IkinciElAracUI.UI.ApiProvider;
using IkinciElAracUI.UI.Models.Core.DTO;
using IkinciElAracUI.UI.Models.VM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace IkinciElAracUI.UI.Controllers
{
    
    public class IhaleController : Controller
    {
        IhaleApiProvider _ihaleApiProvider;
        public IhaleController(IhaleApiProvider ihaleApiProvider)
        {
            _ihaleApiProvider = ihaleApiProvider;
        }
        [Authorize(Roles = "Admin,Kullanici,Kurumsal")]
        public async Task<IActionResult> Index()
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            ViewBag.kul = myuser;

            var benimIhalelerim = await _ihaleApiProvider.Ihalelerim(myuser.KullaniciID);

            ViewBag.BenimIhalelerim = benimIhalelerim;

            return View();
        }
        public async Task<IActionResult> IhaleIndex()
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            ViewBag.kul = myuser;

            var ihaleler = await _ihaleApiProvider.Ihaleler(myuser.RolID);

            ViewBag.Ihaleler = ihaleler;

            return View();
        }

        [HttpPost]
        public IActionResult IndexIhaleRol(int rolID)
        {
            


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexIhaleDetay(int ihaleID)
        {
           
            var ihaleVM = await _ihaleApiProvider.IhaleBilgisi(ihaleID);

            TempData["araIhale"] = JsonConvert.SerializeObject(ihaleVM);
            HttpContext.Session.SetString("kaydet", System.Text.Json.JsonSerializer.Serialize(2));
            return RedirectToAction("IhaleEkle");
        }

        [HttpPost]
        public IActionResult IndexIhaleEkle()
        {
            HttpContext.Session.SetString("kaydet", System.Text.Json.JsonSerializer.Serialize(1));

            return RedirectToAction("IhaleEkle");
        }

        [HttpGet]
        public IActionResult IhaleEkle()
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            ViewBag.kul = myuser;

            string araIhaleJson = TempData["araIhale"] as string;

            var aJson = HttpContext.Session.GetString("kaydet");

            var a = System.Text.Json.JsonSerializer.Deserialize<int>(aJson);

            

            ViewBag.Kaydet = a;

            if (araIhaleJson == null)
            {
                IhaleDTO ihale = new IhaleDTO();

                List<IhaleAracDTO> ihaleAracVMs = new List<IhaleAracDTO>();
                ihale.IhaleAracDTOs = ihaleAracVMs;
                //todo sayfa ya hiiden olarak kullnaıcıID yi ver.
                //ihale.KullaniciID = myuser.KullaniciID;
                return View(ihale);
            }
            
            var araIhale = JsonConvert.DeserializeObject<IhaleDTO>(araIhaleJson);
            if (araIhale.IhaleAracDTOs.Count <= 0)
            {
                araIhale.KullaniciID = myuser.KullaniciID;
                List<IhaleAracDTO> ihaleAracVMs = new List<IhaleAracDTO>();

                araIhale.IhaleAracDTOs = ihaleAracVMs;
            }

            araIhale.KullaniciID=myuser.KullaniciID;

            return View(araIhale);
        }

        [HttpPost]
        public IActionResult IhaleEkle(IhaleDTO vm, string name)
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            vm.KullaniciID = myuser.KullaniciID;
            //var sonIhale = TempData["araIhale"] as IhaleVM;
            if (name == "Kaydet")
            {
                var a = _ihaleApiProvider.IhaleKaydet(vm);
                TempData.Remove("araArac");
            }
            else if (name == "Guncelle")
            {
                var a = _ihaleApiProvider.IhaleGuncelle(vm);
                TempData.Remove("araArac");

            }
            if (name == "Ihaleye Arac Ekle")
            {
                if (vm.IhaleAracDTOs ==null)
                {
                    List<IhaleAracDTO> ihaleAracVMs = new List<IhaleAracDTO>();

                    vm.IhaleAracDTOs = ihaleAracVMs;


                }


                TempData["araIhale"] = JsonConvert.SerializeObject(vm);

                return RedirectToAction("IhaleAracEkle");
            }


            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult IhaleAracEkle()
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            ViewBag.kul = myuser;


            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IhaleAracEkle(IhaleAracDTO vm)
        {

            string ihaleJson = TempData["araIhale"].ToString();
            var ihale = JsonConvert.DeserializeObject<IhaleDTO>(ihaleJson);


            var arac = await _ihaleApiProvider.AracBilgi(vm.AracID);
            if (arac != null)
            {
                vm.AracMarkaID = arac.AracID;
                vm.AracMarkaAdi = arac.AracMarkaAdi;
                vm.AracModelID = arac.AracModelID;
                vm.AracModelAdi = arac.AracModelAdi;
                vm.KaydedenKullanici = arac.KaydedenKullanici;
                vm.KurumsalMi = arac.KurumsalMi;
                vm.KaydedilmeZamanı = arac.KaydedilmeZamanı;

                ihale.IhaleAracDTOs.Add(vm);
            }
            

            TempData["araIhale"] = JsonConvert.SerializeObject(ihale);



            return RedirectToAction("IhaleEkle");
        }

        [HttpPost]
        public async Task<IActionResult> IndexIhaleIcerik(int ihaleID)
        {

            var ihaleVM = await _ihaleApiProvider.IhaleBilgisi(ihaleID);

            TempData["araIhale"] = JsonConvert.SerializeObject(ihaleVM);
            return RedirectToAction("IhaleIcerik");
        }
        [HttpGet]
        public IActionResult IhaleIcerik()
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            ViewBag.kul = myuser;

            string araIhaleJson = TempData["araIhale"] as string;
            var araIhale = JsonConvert.DeserializeObject<IhaleDTO>(araIhaleJson);

            
            return View(araIhale);
        }
        [HttpPost]
        public IActionResult IndexAracTeklif(int ihaleAracID)
        {
            HttpContext.Session.SetString("ihaleAracID", System.Text.Json.JsonSerializer.Serialize(ihaleAracID));
            //var aracTeklif = await _ihaleApiProvider.AracTeklif(ihaleAracID);

            //TempData["araAracTeklif"] = JsonConvert.SerializeObject(aracTeklif);

            //HttpContext.Session.SetString("araAracTeklif", System.Text.Json.JsonSerializer.Serialize(aracTeklif));



            return RedirectToAction("AracTeklif");

        }
        [HttpGet]
        public async Task<IActionResult> AracTeklif()
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            ViewBag.kul = myuser;

            //var araAracTeklifJson = HttpContext.Session.GetString("araAracTeklif");

            //var aracTeklif = System.Text.Json.JsonSerializer.Deserialize<IhaleFiyatDTO>(araAracTeklifJson);


            //string araAracTeklifJson = TempData["araAracTeklif"] as string;
            //var aracTeklif = JsonConvert.DeserializeObject<IhaleFiyatDTO>(araAracTeklifJson);
            var ihaleAracIDJson = HttpContext.Session.GetString("ihaleAracID");

            var ihaleAracID = System.Text.Json.JsonSerializer.Deserialize<int>(ihaleAracIDJson);

            var aracTeklif = await _ihaleApiProvider.AracTeklif(ihaleAracID);

            return View(aracTeklif);

        }

        [HttpGet]
        public IActionResult IndexTeklif()
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            ViewBag.kul = myuser;

            

            //var aracTeklif = await _ihaleApiProvider.AracTeklif(ihaleFiyatID);


            return View();

        }
        [HttpPost]
        public async Task<IActionResult> IndexTeklif(FiyatDTO dto)
        {
            var userJson = HttpContext.Session.GetString("kullanici");

            var myuser = System.Text.Json.JsonSerializer.Deserialize<KullaniciDTO>(userJson);

            var ihaleAracIDJson = HttpContext.Session.GetString("ihaleAracID");

            var ihaleAracID = System.Text.Json.JsonSerializer.Deserialize<int>(ihaleAracIDJson);

            dto.KullaniciID = myuser.KullaniciID;
            dto.IhaleAracID= ihaleAracID;

            var aracTeklif = await _ihaleApiProvider.TeklifKaydet(dto);


            return RedirectToAction("AracTeklif");

        }
    }
}
