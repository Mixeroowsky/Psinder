using Microsoft.AspNetCore.Mvc;
using Psinder.Api.Models;

namespace Psinder.webapp.Controllers
{
    public class PetsController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        
        public PetsController(IHttpClientFactory httpClientFactory)
        {            
            _clientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index(string name)
        {            
            string uri;
            if (string.IsNullOrEmpty(name))
            {
                ViewData["Title"] = "Wszystkie zwierzęta";
                uri = "/Pets/GetPets/";
            }
            else
            {
                ViewData["Title"] = $"Zwierzęta o imieniu {name}";
                uri = $"/Pets/GetPets/?name={name}";
            }
            HttpClient client = _clientFactory.CreateClient(name: "Psinder.Api");
            HttpRequestMessage task = new(
                method: HttpMethod.Get,
                requestUri: uri
            );
            HttpResponseMessage response = await client.SendAsync(task);
            IEnumerable<Pet>? model = await response.Content.ReadFromJsonAsync<IEnumerable<Pet>>();
            return View(model);
            
        }
    }
}
