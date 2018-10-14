using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Consts;
using SprinklingApp.Service.Procedures.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace SprinklingApp.Master.API.Controllers
{
    [Route(Routes.Profile)]
    public class ProfileController : BaseMasterController
    {
        private readonly IProfileProcedure _procedure;
        public ProfileController(IProfileProcedure procedure)
        {
            _procedure = procedure;
        }
        
        [HttpGet]
        public ActionResult<List<ProfileResponseModel>> Get()
        {
            var result = _procedure.GetList().ToList();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<ProfileResponseModel> Get(long id)
        {
            var result = _procedure.Get(id);
            return result;
        }

        [HttpPost]
        public ActionResult<ProfileResponseModel> Post([FromBody]InsertProfileRequestModel requestModel)
        {
            var result = _procedure.Insert(requestModel);
            return result;
        }

        [HttpPut]
        public ActionResult<ProfileResponseModel> Put([FromBody]UpdateProfileRequestModel requestModel)
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