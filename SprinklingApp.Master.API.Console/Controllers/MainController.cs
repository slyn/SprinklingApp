using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SprinklingApp.Common.FileOperator;
using SprinklingApp.Model.ApiResponseModels.Abstract;
using SprinklingApp.Model.Consts;

namespace SprinklingApp.Master.API.Console.Controllers {

    [Route(Routes.GgSettings)]
    public class MainController : BaseMasterController {
        private readonly string path = Path.Combine(Environment.CurrentDirectory, "GgSettings.json");

        [HttpGet]
        public ActionResult<GgSettings> Get() {
            string json;
            if (!FileOps.IsExistingFile(path)) {
                json = JsonConvert.SerializeObject(
                    new GgSettings {
                        IsStarted = false
                    });
            } else {
                json = FileOps.ReadText(path);
            }

            return JsonConvert.DeserializeObject<GgSettings>(json);
        }

        [HttpPost]
        public ActionResult Post([FromBody] GgSettings settings) {
            FileOps.WriteFile(
                path,
                JsonConvert.SerializeObject(settings),
                new FileWriteOptions {
                    OverwriteFileIfExists = true,
                    CreateDirectoryIfNotExists = true
                });
            return Ok(200);
        }

        [HttpGet]
        [Route("SetManual")]
        public ActionResult SetManuel() {
            GgSettings settings = Get().Value;
            settings.IsStarted = false;
            Post(settings);

            return Ok(200);
        }

        [HttpGet]
        [Route("SetAutomatic")]
        public ActionResult SetAutomatic() {
            GgSettings settings = Get().Value;
            settings.IsStarted = true;
            Post(settings);
            return Ok(200);
        }
    }

    public class GgSettings : IApiResponse {
        public virtual bool IsStarted { get; set; }
        public virtual string Password { get; set; } = "1234";
    }

}