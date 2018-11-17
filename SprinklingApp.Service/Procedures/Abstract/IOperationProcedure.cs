using System;
using System.Collections.Generic;
using System.Text;
using SprinklingApp.Model.Entities.Concrete;

namespace SprinklingApp.Service.Procedures.Abstract
{
    public interface IOperationProcedure : IProcedure
    {
        IEnumerable<OpenCloseOperationItem> AddOperationForValves(IEnumerable<Valve> valves);
    }
}
