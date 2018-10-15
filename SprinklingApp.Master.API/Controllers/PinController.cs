using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.Consts;
using SprinklingApp.Service.EntityServices.Abstract;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace SprinklingApp.Master.API.Controllers
{
    [Route(Routes.Pin)]
    public class PinController : BaseMasterController
    {
        private readonly IValveService _valveService;
        public PinController(IValveService valveService)
        {
            _valveService = valveService;
        }
        [HttpGet("/Open/{valveId}/{pin}")]
        public async Task<IActionResult> Open(long valveId, int pin)
        {
            var valveDto = _valveService.Get(valveId);

            var ip = valveDto?.Raspberry?.IPAddress;
            if (string.IsNullOrWhiteSpace(ip))
                throw new System.Exception(" IP address of Valve can not found!");

            var url = ip + "/api/Open/" + valveId + "/" + pin;

            await GetAsync(url);
            return Ok(200);
        }

        [HttpGet("/Close/{valveId}/{pin}")]
        public async Task<IActionResult> Close(long valveId, int pin)
        {
            var valveDto = _valveService.Get(valveId);

            var ip = valveDto?.Raspberry?.IPAddress;
            if (string.IsNullOrWhiteSpace(ip))
                throw new System.Exception("IP address of Valve  can not found!");

            var url = ip + "/api/Close/" + valveId + "/" + pin;

            await GetAsync(url);
            return Ok(200);
        }

        private async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}