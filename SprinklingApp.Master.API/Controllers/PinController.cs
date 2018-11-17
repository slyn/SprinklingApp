using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.Consts;
using SprinklingApp.Service.EntityServices.Abstract;
using System.IO;
using System.Net;

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


        [HttpGet("Open/{valveId}")]
        public ActionResult Open(long valveId)
        {
            var valveDto = _valveService.Get(valveId);

            var ip = valveDto?.Raspberry?.IPAddress;
            if (string.IsNullOrWhiteSpace(ip))
                throw new System.Exception(" IP address of Valve can not found!");

            var url = "http://"+ip + "/api/Open/" + valveDto.EnablePin;

            Get(url);
            return Ok(200);
        }

        [HttpGet("Close/{valveId}")]
        public ActionResult Close(long valveId)
        {
            var valveDto = _valveService.Get(valveId);

            var ip = valveDto?.Raspberry?.IPAddress;
            if (string.IsNullOrWhiteSpace(ip))
                throw new System.Exception("IP address of Valve  can not found!");

            var url = "http://" + ip + "/api/Close/" + valveDto.DisablePin;

            Get(url);
            return Ok(200);
        }

        private string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}