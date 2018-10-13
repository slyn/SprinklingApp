using SprinklingApp.Model.DTOs.Concrete;
using System.Collections.Generic;

namespace SprinklingApp.Service.EntityServices.Abstract
{
    public interface IGroupService
    {
        GroupDTO Get(long id);
        IEnumerable<GroupDTO> GetList();
        GroupDTO Insert(GroupDTO dtoItem);
        GroupDTO Update(GroupDTO dtoItem);
        void Delete(long id);
    }
}
