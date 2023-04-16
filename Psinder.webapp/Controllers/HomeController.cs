using Microsoft.AspNetCore.Mvc;
using Psinder.Api.Models;
using System.Diagnostics;
using System.Net.Http;

namespace Psinder.webapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            clientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Pets(string name)
        {
            string uri;
            if (string.IsNullOrEmpty(name))
            {
                ViewData["Title"] = "Wszystkie zwierzęta";
                uri = "pets/";
            }
            else
            {
                ViewData["Title"] = $"Zwierzęta o imieniu {name}";
                uri = $"pets/?name={name}";
            }
            HttpClient client = clientFactory.CreateClient(name: "Psinder.Api");
            HttpRequestMessage task = new(
                method: HttpMethod.Get, 
                requestUri: uri
            );
            HttpResponseMessage response = await client.SendAsync(task);
            IEnumerable<Pet>? model = await response.Content.ReadFromJsonAsync<IEnumerable<Pet>>();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}