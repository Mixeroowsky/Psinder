using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Psinder.Api.Models;
using System.Threading.Tasks;

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
                uri = "/Pets/GetAllPets/";
            }
            else
            {
                ViewData["Title"] = $"Zwierzęta o imieniu {name}";
                uri = $"/Pets/SearchPetByName?name={name}";
            }
            HttpClient client = _clientFactory.CreateClient(name: "Psinder.Api");
            HttpRequestMessage task = new(
                method: HttpMethod.Get,
                requestUri: uri
            );
            HttpResponseMessage response = await client.SendAsync(task);
            List<PetModel>? model = await response.Content.ReadFromJsonAsync<List<PetModel>>();
            return View(model);            
        }
        public IActionResult Details(int id)
        {            
            TempData["PetId"] = id;
            return RedirectToAction("Profile");
        }
        public async Task<IActionResult> Profile()
        {
            int id = (int)TempData["PetId"];
            string uri;            
            
            ViewData["Title"] = "Wszystkie zwierzęta";
            uri = $"/Pets/GetPetById/{id}";
           
            HttpClient client = _clientFactory.CreateClient(name: "Psinder.Api");
            HttpRequestMessage task = new(
                method: HttpMethod.Get,
                requestUri: uri
            );
            HttpResponseMessage response = await client.SendAsync(task);
            PetModel pet = await response.Content.ReadFromJsonAsync<PetModel>();
            
            return View("Details", pet);
        }
        public async Task<IActionResult> Create()
        {
            ViewData["Title"] = "Dodaj nowe zwierzę";
            return View();
        }
    }
}
