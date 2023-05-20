using Microsoft.AspNetCore.Mvc;
using Psinder.Api.Models;

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
                ViewData["Title"] = "Wszystkie schroniska";
                uri = "/Shelters/GetShelters/";
            }
            else
            {
                ViewData["Title"] = $"Schroniska zawierające {name}";
                uri = $"/Shelters/GetShelters/?name={name}";
            }
            HttpClient client = _clientFactory.CreateClient(name: "Psinder.Api");
            HttpRequestMessage task = new(
                method: HttpMethod.Get,
                requestUri: uri
            );
            HttpResponseMessage response = await client.SendAsync(task);
            List<ShelterModel>? model = await response.Content.ReadFromJsonAsync<List<ShelterModel>>();
            return View(model);
        }
    }
}
