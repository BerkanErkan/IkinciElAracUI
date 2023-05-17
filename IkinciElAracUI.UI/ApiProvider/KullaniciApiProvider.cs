using IkinciElAracUI.UI.Models.Core.DTO;
using IkinciElAracUI.UI.Models.VM;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IkinciElAracUI.UI.ApiProvider
{
    public class KullaniciApiProvider
    {
        HttpClient _httpClient;
        public KullaniciApiProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<KullaniciDTO> Kontrol(KullaniciVM vm)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(vm));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("kullanici", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {

                return JsonConvert.DeserializeObject<KullaniciDTO>(await donenApiDegeri.Content.ReadAsStringAsync());
            }

            return null;

        }




    }
}
