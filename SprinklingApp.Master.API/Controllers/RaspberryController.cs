using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Consts;
using SprinklingApp.Service.Procedures.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace SprinklingApp.Master.API.Controllers
{
    [Route(Routes.Raspberry)]
    public class RaspberryController : BaseMasterController
    {
        private readonly IRaspberryProcedure _procedure;
        public RaspberryController(IRaspberryProcedure procedure)
        {
            _procedure = procedure;
        }
        
        [HttpGet]
        public ActionResult<List<RaspberryResponseModel>> Get()
        {
            var result = _procedure.GetList()?.ToList();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<RaspberryResponseModel> Get(long id)
        {
            var result = _procedure.Get(id);
            return result;
        }

        [HttpPost]
        public ActionResult<RaspberryResponseModel> Post([FromBody]InsertRaspberryRequestModel requestModel)
        {
            var result = _procedure.Insert(requestModel);
            return result;
        }

        [HttpPut]
        public ActionResult<RaspberryResponseModel> Put([FromBody]UpdateRaspberryRequestModel requestModel)
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