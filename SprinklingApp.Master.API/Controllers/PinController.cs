using System;
using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SprinklingApp.DataAccess.ORM.EFCore;
using SprinklingApp.Model.Consts;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.EntityServices.Abstract;

namespace SprinklingApp.Master.API.Controllers {

    [Route(Routes.Pin)]
    public class PinController : BaseMasterController {
        private readonly IValveService _valveService;
        private readonly IRaspberryService raspberryService;
        private readonly SpringklingContext _dbContext;

        public PinController(IValveService valveService, IRaspberryService raspberryService, SpringklingContext dbContext) {
            _valveService = valveService;
            this.raspberryService = raspberryService;
            _dbContext = dbContext;
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
            
            
            ValveLog log = new ValveLog();
            log.Operation = "Open";
            log.OperationTime = DateTime.Now;
            log.Tonnage = valveDto.Pressure;
            log.ValveName = valveDto.Name;
            log.ValveId = (int) valveDto.Id;
            log.IsActive = true;
            log.RaspberryId = raspberry.Id;
            log.RaspberryName = raspberry.Name;
            try
            {
                Get(url);
                log.Status = "Success";
            }
            catch
            {
                log.Status = "Fail";
            }
            //_dbContext.ValveLog.Add(log);
            System.IO.File.AppendAllText("log.txt", JsonConvert.SerializeObject(log));
            _valveService.Update(valveDto);
            _dbContext.SaveChanges();
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

            ValveLog log = new ValveLog();
            log.Operation = "Close";
            log.OperationTime = DateTime.Now;
            log.Tonnage = valveDto.Pressure;
            log.ValveName = valveDto.Name;
            log.ValveId = (int)valveDto.Id;
            log.IsActive = true;
            log.RaspberryId = raspberry.Id;
            log.RaspberryName = raspberry.Name;
            try
            {
                Get(url);
                log.Status = "Success";
            }
            catch {
                log.Status = "Fail";
            }

            System.IO.File.AppendAllText("log.txt", JsonConvert.SerializeObject(log));
            //_dbContext.ValveLog.Add(log);
            int changes = _dbContext.SaveChanges();
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

            valveDto.IsOpen = true;
            valveDto.CloseDateTime = DateTime.Now.AddMinutes(workingTime);
            

            string url = $"http://{ip}/{valveDto.EnablePin}";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n\n\n\n{raspberry.IPAddress} valve:{valveDto.RaspberryId} opening\n{url}\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;


            ValveLog log = new ValveLog();
            log.Operation = "Open with time";
            log.OperationTime = DateTime.Now;
            log.Tonnage = valveDto.Pressure;
            log.ValveName = valveDto.Name;
            log.ValveId = (int)valveDto.Id;
            log.IsActive = true;
            log.RaspberryId = raspberry.Id;
            log.RaspberryName = raspberry.Name;
            try
            {
                Get(url);
                log.Status = "Success";
            }
            catch
            {
                log.Status = "Fail";
            }
            System.IO.File.AppendAllText("log.txt", JsonConvert.SerializeObject(log));
            //_dbContext.ValveLog.Add(log);
            _valveService.Update(valveDto);
            _dbContext.SaveChanges();
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