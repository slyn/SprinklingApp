using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using System;

namespace SprinklingApp.Master.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        // grup aç kapat komutu
        [HttpPost]
        public ActionResult GroupOperationInsert([FromBody]InsertGroupOperationRequestModel requestModel)
        {
            //var result = _procedure.Insert(requestModel);
            //return result;
            return Ok(200);
        }
        // vana aç kapat komutu
        [HttpPost]
        public ActionResult ValveOperationInsert([FromBody]InsertValveOperationRequestModel requestModel)
        {
            //var result = _procedure.Insert(requestModel);
            //return result;
            return Ok(200);
        }
        // vanalar için planlanmış işleri ver

        [HttpGet]
        public ActionResult<OpenCloseOperationItem> GetMyWorkItems()
        {
            throw new NotImplementedException("Not Implemented!");
            // ip al
            // rasperry ye bağlı vanaları al
            // vanalar için group veya tekil işleri bul
            // return
        }
    }
}