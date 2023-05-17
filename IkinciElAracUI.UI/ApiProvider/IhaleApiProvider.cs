using IkinciElAracUI.UI.Models.Core.DTO;
using IkinciElAracUI.UI.Models.VM;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace IkinciElAracUI.UI.ApiProvider
{
    public class IhaleApiProvider
    {
        HttpClient _httpClient;
        public IhaleApiProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IhaleDTO>> Ihalelerim(int kullnaniciID)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(kullnaniciID));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("Ihale", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<IhaleDTO>>(await donenApiDegeri.Content.ReadAsStringAsync());


            }

            return null;
        }
        public async Task<List<IhaleDTO>> Ihaleler(int rolID)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(rolID));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("Ihale/ihaleler", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<IhaleDTO>>(await donenApiDegeri.Content.ReadAsStringAsync());


            }

            return null;
        }
        public async Task<IhaleAracDTO> AracBilgi(int aracID)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(aracID));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("Ihale/aracbilgi", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IhaleAracDTO>(await donenApiDegeri.Content.ReadAsStringAsync());


            }

            return null;
        }

        public async Task<bool> IhaleKaydet(IhaleDTO vm)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(vm));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("Ihale/ihalekaydet", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(await donenApiDegeri.Content.ReadAsStringAsync());


            }

            return false;
        }
        public async Task<IhaleDTO> IhaleBilgisi(int ihaleID)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(ihaleID));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("Ihale/ihalebilgi", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IhaleDTO>(await donenApiDegeri.Content.ReadAsStringAsync());


            }

            return null;
        }
        public async Task<bool> IhaleGuncelle(IhaleDTO vm)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(vm));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("Ihale/ihaleguncelle", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(await donenApiDegeri.Content.ReadAsStringAsync());


            }

            return false;
        }
        public async Task<IhaleFiyatDTO> AracTeklif(int ihaleAracID)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(ihaleAracID));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("Ihale/aracfiyat", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IhaleFiyatDTO>(await donenApiDegeri.Content.ReadAsStringAsync());


            }

            return null;
        }
        public async Task<bool> TeklifKaydet(FiyatDTO vm)
        {
            StringContent str = new StringContent(JsonConvert.SerializeObject(vm));
            str.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var donenApiDegeri = await _httpClient.PostAsync("Ihale/teklifkaydet", str);

            if (donenApiDegeri.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(await donenApiDegeri.Content.ReadAsStringAsync());


            }

            return false;
        }
    }
}
