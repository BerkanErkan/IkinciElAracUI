using IkinciElAracUI.UI.ApiProvider;
using IkinciElAracUI.UI.Models.Core.DTO;
using IkinciElAracUI.UI.Models.VM;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IkinciElAracUI.UI.Controllers
{
    public class GirisController : Controller
    {

        KullaniciApiProvider _kullaniciApiProvider;
        public GirisController(KullaniciApiProvider kullaniciApiProvider)
        {
            _kullaniciApiProvider = kullaniciApiProvider;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
               

                return RedirectToAction("Index", "Ihale");

            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(KullaniciVM vm)
        {

                var kullnici = await _kullaniciApiProvider.Kontrol(vm);
                ClaimsIdentity identity = null;
                bool isAuthenticate = false;
                if (kullnici != null)
                {
                    

                    identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                    identity.AddClaim(new Claim(ClaimTypes.Role, kullnici.RolAdi));

                    identity.AddClaim(new Claim(ClaimTypes.Name, kullnici.KullaniciAdi));
                    isAuthenticate = true;


                }
                if (isAuthenticate)
                {
                    var principal = new ClaimsPrincipal(identity);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("kullanici", JsonSerializer.Serialize(kullnici));
                    return RedirectToAction("Index", "Ihale");
                    
                }

            return View();

        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
