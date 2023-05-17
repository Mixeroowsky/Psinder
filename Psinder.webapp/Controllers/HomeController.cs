using Microsoft.AspNetCore.Mvc;
using Psinder.Api.Data;
using Psinder.Api.Models;
using System.Diagnostics;
using System.Net.Http;

namespace Psinder.webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            string uri;
            ViewData["Title"] = "Wszystkie zwierzęta";
            uri = "/Pets/GetAllPets/";            
            HttpClient client = _clientFactory.CreateClient(name: "Psinder.Api");
            HttpRequestMessage task = new(
                method: HttpMethod.Get,
                requestUri: uri
            );
            HttpResponseMessage response = await client.SendAsync(task);
            List<Pet>? model = await response.Content.ReadFromJsonAsync<List<Pet>>();
            return View(model);
        }
        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}