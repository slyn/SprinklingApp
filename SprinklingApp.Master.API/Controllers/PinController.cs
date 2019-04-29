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
        private readonly IRaspberryService raspberryService;

        public PinController(IValveService valveService, IRaspberryService raspberryService) {
            _valveService = valveService;
            this.raspberryService = raspberryService;
        }

        [HttpGet("Open/{valveId}")]
        public ActionResult Open(long valveId) {
            Valve valveDto = _valveService.Get(valveId);
            Raspberry raspberry = raspberryService.Get(valveDto.RaspberryId);
            string ip = raspberry.IPAddress;
            if (string.IsNullOrWhiteSpace(ip)) {
                throw new Exception(" IP address of Valve can not found!");
            }
            
            string url = $"http://{ip}/{valveDto.EnablePin}";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n\n\n\n{raspberry.IPAddress} valve:{valveDto.RaspberryId} opening\n{url}\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            valveDto.IsOpen = true;
            _valveService.Update(valveDto);

            Get(url);
            return Ok(200);
        }

        [HttpGet("Close/{valveId}")]
        public ActionResult Close(long valveId) {
            Valve valveDto = _valveService.Get(valveId);
            Raspberry raspberry = raspberryService.Get(valveDto.RaspberryId);
            string ip = raspberry.IPAddress;
            if (string.IsNullOrWhiteSpace(ip)) {
                throw new Exception("IP address of Valve  can not found!");
            }

            valveDto.IsOpen = false;
            valveDto.CloseDateTime = null;
            _valveService.Update(valveDto);

            string url = $"http://{ip}/{valveDto.DisablePin}";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\n\n\n{raspberry.IPAddress} valve:{valveDto.RaspberryId} closing\n{url}\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Get(url);
            return Ok(200);
        }

        [HttpGet("OpenWithTime/{valveId}/{workingTime}")]
        public ActionResult OpenWithTime(long valveId, int workingTime) {
            Valve valveDto = _valveService.Get(valveId);
            Raspberry raspberry = raspberryService.Get(valveDto.RaspberryId);
            string ip = raspberry.IPAddress;
            if (string.IsNullOrWhiteSpace(ip))
            {
                throw new Exception("IP address of Valve  can not found!");
            }

            valveDto.IsOpen = false;
            valveDto.CloseDateTime = DateTime.Now.AddMinutes(workingTime);
            _valveService.Update(valveDto);

            string url = $"http://{ip}/{valveDto.DisablePin}";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n\n\n\n{raspberry.IPAddress} valve:{valveDto.RaspberryId} closing\n{url}\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;
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