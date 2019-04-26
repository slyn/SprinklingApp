using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Consts;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Master.API.Controllers {

    [Route(Routes.Valve)]
    public class ValveController : BaseMasterController {
        private readonly IValveProcedure _procedure;

        public ValveController(IValveProcedure procedure) {
            _procedure = procedure;
        }

        [HttpGet]
        public ActionResult<List<ValveResponseModel>> Get() {
            List<ValveResponseModel> result = _procedure.GetList()?.ToList();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<ValveResponseModel> Get(long id) {
            ValveResponseModel result = _procedure.Get(id);
            return result;
        }

        [HttpPost]
        public ActionResult<ValveResponseModel> Post([FromBody] InsertValveRequestModel requestModel) {
            ValveResponseModel result = _procedure.Insert(requestModel);
            return result;
        }

        [HttpPut]
        public ActionResult<ValveResponseModel> Put([FromBody] UpdateValveRequestModel requestModel) {
            ValveResponseModel result = _procedure.Update(requestModel);
            return result;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id) {
            _procedure.Delete(id);
            return Ok(200);
        }
    }

}