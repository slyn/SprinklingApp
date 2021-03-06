﻿using System.Collections.Generic;
using SprinklingApp.Model.Entities.Concrete;

namespace SprinklingApp.Service.EntityServices.Abstract {

    public interface IValveGroupMappingService {
        ValveGroupMapping Get(long id);
        IEnumerable<ValveGroupMapping> GetList();
        IEnumerable<ValveGroupMapping> GetListByGroup(long groupid);
        IEnumerable<ValveGroupMapping> GetListByValve(long valveid);
        ValveGroupMapping Insert(ValveGroupMapping entity);
        ValveGroupMapping Update(ValveGroupMapping entity);
        void Delete(long id);
    }

}