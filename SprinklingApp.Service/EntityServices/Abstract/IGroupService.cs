using System.Collections.Generic;
using SprinklingApp.Model.Entities.Concrete;

namespace SprinklingApp.Service.EntityServices.Abstract {

    public interface IGroupService {
        Group Get(long id);
        IEnumerable<Group> GetList();
        IEnumerable<Group> GetListByIds(IList<long> ids);
        Group Insert(Group entity);
        Group Update(Group entity);
        void Delete(long id);
    }

}