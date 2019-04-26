using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Consts;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Master.API.Controllers {

    [Route(Routes.Group)]
    public class GroupController : BaseMasterController {
        private readonly IGroupProcedure _procedure;

        public GroupController(IGroupProcedure procedure) {
            _procedure = procedure;
        }

        [HttpGet]
        public ActionResult<List<GroupResponseModel>> Get() {
            List<GroupResponseModel> result = _procedure.GetList()?.ToList();
            return result;
        }

        [HttpGet("{id}")]
        public ActionResult<GroupResponseModel> Get(long id) {
            GroupResponseModel result = _procedure.Get(id);
            return result;
        }

        [HttpPost]
        public ActionResult<GroupResponseModel> Post([FromBody] InsertGroupRequestModel requestModel) {
            GroupResponseModel result = _procedure.Insert(requestModel);
            return result;
        }

        [HttpPut]
        public ActionResult<GroupResponseModel> Put([FromBody] UpdateGroupRequestModel requestModel) {
            GroupResponseModel result = _procedure.Update(requestModel);
            return result;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id) {
            _procedure.Delete(id);
            return Ok(200);
        }
    }

}