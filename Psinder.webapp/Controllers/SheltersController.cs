using Microsoft.AspNetCore.Mvc;
using Psinder.Api.Data;

namespace Psinder.webapp.Controllers
{
    public class SheltersController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public SheltersController(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(string? name)
        {
            string uri;
            if (string.IsNullOrEmpty(name))
            {
                ViewData["Title"] = "Wszystkie zwierzęta";
                uri = "/Shelters/GetShelters/";
            }
            else
            {
                ViewData["Title"] = $"Zwierzęta o imieniu {name}";
                uri = $"/Shelters/GetShelters/?name={name}";
            }
            HttpClient client = _clientFactory.CreateClient(name: "Psinder.Api");
            HttpRequestMessage task = new(
                method: HttpMethod.Get,
                requestUri: uri
            );
            HttpResponseMessage response = await client.SendAsync(task);
            IEnumerable<Shelter>? model = await response.Content.ReadFromJsonAsync<IEnumerable<Shelter>>();
            return View(model);
        }
    }
}
