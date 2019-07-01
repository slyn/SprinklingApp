using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Consts;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Master.API.Console.Controllers {

    [Route(Routes.Raspberry)]
    public class RaspberryController : BaseMasterController {
        private readonly IRaspberryProcedure _procedure;

        public RaspberryController(IRaspberryProcedure procedure) {
            _procedure = procedure;
        }

        [HttpGet]
        public ActionResult<List<RaspberryResponseModel>> Get() {
            List<RaspberryResponseModel> result = _procedure.GetList()?.ToList();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<RaspberryResponseModel> Get(long id) {
            RaspberryResponseModel result = _procedure.Get(id);
            return result;
        }

        [HttpPost]
        public ActionResult<RaspberryResponseModel> Post([FromBody] InsertRaspberryRequestModel requestModel) {
            RaspberryResponseModel result = _procedure.Insert(requestModel);
            return result;
        }

        [HttpPut]
        public ActionResult<RaspberryResponseModel> Put([FromBody] UpdateRaspberryRequestModel requestModel) {
            RaspberryResponseModel result = _procedure.Update(requestModel);
            return result;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id) {
            _procedure.Delete(id);
            return Ok(200);
        }
    }

}