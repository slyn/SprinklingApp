using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.Consts;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Master.API.Controllers {

    [Route(Routes.Pin)]
    public class PinController : BaseMasterController {
        private readonly IValveService _valveService;

        public PinController(IValveService valveService) {
            _valveService = valveService;
        }

        [HttpGet("Open/{valveId}")]
        public ActionResult Open(long valveId) {
            Valve valveDto = _valveService.Get(valveId);

            string ip = valveDto?.Raspberry?.IPAddress;
            if (string.IsNullOrWhiteSpace(ip)) {
                throw new Exception(" IP address of Valve can not found!");
            }

            string url = "http://" + ip + "/api/Open/" + valveDto.EnablePin;

            valveDto.IsActive = true;
            _valveService.Update(valveDto);

            Get(url);
            return Ok(200);
        }

        [HttpGet("Close/{valveId}")]
        public ActionResult Close(long valveId) {
            Valve valveDto = _valveService.Get(valveId);

            string ip = valveDto?.Raspberry?.IPAddress;
            if (string.IsNullOrWhiteSpace(ip)) {
                throw new Exception("IP address of Valve  can not found!");
            }

            valveDto.IsActive = false;
            _valveService.Update(valveDto);

            string url = "http://" + ip + "/api/Close/" + valveDto.DisablePin;

            Get(url);
            return Ok(200);
        }

        private string Get(string uri) {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }
    }

}