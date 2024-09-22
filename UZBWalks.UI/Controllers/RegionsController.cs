using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using UZBWalks.UI.Models.DTO;

namespace UZBWalks.UI.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RegionDto> response = new List<RegionDto>();

            try
            {
                // Get All region from web Api

                // Use the named client "RegionsClient"
                var client = _httpClientFactory.CreateClient("RegionsClient");

                var httpResponseMessage = await client.GetAsync("api/Regions"); // No need for the full URL

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception ex)
            {
                // Log the exception
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add() 
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionView)
        {
            var client = _httpClientFactory.CreateClient("RegionsClient");

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7054/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(addRegionView), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if(response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient("RegionsClient");

            var response = await client.GetFromJsonAsync<RegionDto>($"api/Regions/{id.ToString()}");

            if(response is not null) return View(response);

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto regionDto)
        {
            var client = _httpClientFactory.CreateClient("RegionsClient");

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7054/api/Regions/{regionDto.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(regionDto), Encoding.UTF8, "application/json")
            };

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null) return RedirectToAction("Index", "Regions");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto regionDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("RegionsClient");

                var httpRequestMessage = await client.DeleteAsync($"https://localhost:7054/api/Regions/{regionDto.Id}");

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception)
            {
                //Console
            }

            return View("Edit");
        }
    }
}
