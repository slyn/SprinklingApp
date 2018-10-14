using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Consts;
using SprinklingApp.Service.Procedures.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace SprinklingApp.Master.API.Controllers
{
    [Route(Routes.Valve)]
    public class ValveController : BaseMasterController
    {
        private readonly IValveProcedure _procedure;
        public ValveController(IValveProcedure procedure)
        {
            _procedure = procedure;
        }
        
        [HttpGet]
        public ActionResult<List<ValveResponseModel>> Get()
        {
            var result = _procedure.GetList()?.ToList();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<ValveResponseModel> Get(long id)
        {
            var result = _procedure.Get(id);
            return result;
        }

        [HttpPost]
        public ActionResult<ValveResponseModel> Post([FromBody]InsertValveRequestModel requestModel)
        {
            var result = _procedure.Insert(requestModel);
            return result;
        }

        [HttpPut]
        public ActionResult<ValveResponseModel> Put([FromBody]UpdateValveRequestModel requestModel)
        {
            var result = _procedure.Update(requestModel);
            return result;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            _procedure.Delete(id);
            return Ok(200);
        }
    }
}