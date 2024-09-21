using Microsoft.AspNetCore.Mvc;
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
    }
}
