using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SprinklingApp.Service.Helper
{
    public class ModelBinder
    {
        #region [ CTOR ]
        private ModelBinder()
        {
        }
        #endregion

        #region [ Lazy-Singleton ]
        private static readonly Lazy<ModelBinder> _instance = new Lazy<ModelBinder>(() => new ModelBinder(), LazyThreadSafetyMode.ExecutionAndPublication);
        internal static ModelBinder Instance => _instance.Value;
        #endregion

        public Group ConvertToGroup(InsertGroupRequestModel requestModel)
        {
            var result = new Group()
            {
                IsActive = true,
                Duration = requestModel.Duration,
                Name = requestModel.Name,
                Unit = requestModel.Unit
            };

            return result;
        }
        public Group ConvertToGroup(UpdateGroupRequestModel requestModel)
        {
            var result = new Group()
            {
                Id = requestModel.GroupId,
                IsActive = true,
                Duration = requestModel.Duration,
                Name = requestModel.Name,
                Unit = requestModel.Unit
            };

            return result;
        }
        //public GroupDTO ConvertToGroupDTO(Group entity, IEnumerable<Valve> valves)
        //public GroupDTO ConvertToGroupDTO(Group entity, IEnumerable<ValveDTO> valves)
        //{
        //    if (entity == null)
        //        return null;
        //    var result = new GroupDTO()
        //    {
        //        Id = entity.Id,
        //        IsActive = entity.IsActive,
        //        Duration = entity.Duration,
        //        Name = entity.Name,
        //        Unit = entity.Unit,
        //        Valves = valves
        //    };

        //    return result;
        //}
        //public GroupDTO ConvertToGroupDTO(InsertGroupRequestModel requestModel,IEnumerable<ValveDTO> valves)
        //{
        //    var result = new GroupDTO()
        //    {
        //        IsActive = true,
        //        Duration = requestModel.Duration,
        //        Name = requestModel.Name,
        //        Unit = requestModel.Unit,
        //        Valves = valves
        //        //Valves =valves.Select(x=>ConvertToValve(x))
        //    };

        //    return result;

        //}
        //public GroupDTO ConvertToGroupDTO(UpdateGroupRequestModel requestModel, IEnumerable<ValveDTO> valves)
        //{
        //    var result = new GroupDTO()
        //    {
        //        Id = requestModel.GroupId,
        //        IsActive = true,
        //        Duration = requestModel.Duration,
        //        Name = requestModel.Name,
        //        Unit = requestModel.Unit,
        //        Valves = valves
        //        //Valves = requestModel.ValveIdList != null ? 
        //        //                    valves.Where(x => x.IsActive && requestModel.ValveIdList.Contains(x.Id)).Select(x => ConvertToValve(x)) : 
        //        //                    new List<Valve>()
        //    };

        //    return result;

        //}
        public GroupResponseModel ConvertToGroupResponseModel(Group entity,IEnumerable<Valve> valves)
        {
            if (entity == null)
                return null;
            var result = new GroupResponseModel()
            {
                Id = entity.Id,
                Duration = entity.Duration,
                Name = entity.Name,
                Unit = entity.Unit,
                Valves = valves
            };

            return result;
        }

        //public Valve ConvertToValve(ValveDTO dtoItem)
        //{
        //    var result = new Valve()
        //    {
        //        Id = dtoItem.Id,
        //        IsActive = dtoItem.IsActive,
        //        Name = dtoItem.Name,
        //        ActivatePin = dtoItem.ActivatePin,
        //        DisabledPin = dtoItem.DisabledPin,
        //        Pressure = dtoItem.Pressure,
        //        RaspberryId = dtoItem.RaspberryId
        //    };

        //    return result;
        //}
        //public ValveDTO ConvertToValveDTO(Valve entity, Raspberry raspberry)
        //{
        //    var result = new ValveDTO()
        //    {
        //        Id = entity.Id,
        //        Name = entity.Name,
        //        IsActive = entity.IsActive,
        //        ActivatePin = entity.ActivatePin,
        //        DisabledPin = entity.DisabledPin,
        //        Pressure = entity.Pressure,
        //        RaspberryId = entity.RaspberryId,
        //        //Raspberry = raspberry
        //    };

        //    return result;
        //}
        public Valve ConvertToValve(InsertValveRequestModel requestModel)
        {
            var result = new Valve()
            {
                IsActive = true,
                Name = requestModel.Name,
                EnablePin = requestModel.EnablePin,
                DisablePin = requestModel.DisablePin,
                Pressure = requestModel.Pressure,
                RaspberryId = requestModel.RaspberryId
            };

            return result;

        }
        public Valve ConvertToValve(UpdateValveRequestModel requestModel)
        {
            var result = new Valve()
            {
                Id = requestModel.ValveId,
                EnablePin = requestModel.EnablePin,
                DisablePin = requestModel.DisablePin,
                IsActive = true,
                Name = requestModel.Name,
                Pressure = requestModel.Pressure,
                RaspberryId =requestModel.RaspberryId
            };

            return result;
        }
        public ValveResponseModel ConvertToValveResponseModel(Valve dtoItem)
        {
            if (dtoItem == null)
                return null;

            var result = new ValveResponseModel()
            {
                Id = dtoItem.Id,
                Name = dtoItem.Name,
                Pressure = dtoItem.Pressure,
                EnablePin = dtoItem.EnablePin,
                DisablePin = dtoItem.DisablePin,
                RaspberryId = dtoItem.RaspberryId
            };

            return result;
        }

        public Raspberry ConvertToRaspberry(Raspberry dtoItem)
        {
            var result = new Raspberry()
            {
                Id = dtoItem.Id,
                IsActive = dtoItem.IsActive,
                Name = dtoItem.Name,
                IPAddress = dtoItem.IPAddress,
                Valves = dtoItem.Valves
            };

            return result;
        }
        
        //public Raspberry ConvertToRaspberryDTO(Raspberry entity, IEnumerable<Valve> valves)
        //{
        //    if (entity == null)
        //        return null;

        //    var result = new Raspberry()
        //    {
        //        Id = entity.Id,
        //        IsActive = entity.IsActive,
        //        Name = entity.Name,
        //        IPAddress = entity.IPAddress,
        //        Valves = valves
        //    };

        //    return result;
        //}

        public Raspberry ConvertToRaspberry(InsertRaspberryRequestModel requestModel,IEnumerable<Valve> valves)
        {
            var result = new Raspberry()
            {
                IsActive = true,
                Name = requestModel.Name,
                IPAddress = requestModel.IPAddress,
                Valves = valves
            };

            return result;

        }
        public Raspberry ConvertToRaspberry(UpdateRaspberryRequestModel requestModel, IEnumerable<Valve> valves)
        {
            var result = new Raspberry()
            {
                Id = requestModel.RaspberryId,
                IsActive = true,
                Name = requestModel.Name,
                IPAddress = requestModel.IPAddress,
                Valves = valves
            };

            return result;
        }
        public RaspberryResponseModel ConvertToRaspberryResponseModel(Raspberry dtoItem)
        {
            if (dtoItem == null)
                return null;
            var result = new RaspberryResponseModel()
            {
                Id = dtoItem.Id,
                Name = dtoItem.Name,
                IPAddress = dtoItem.IPAddress,
                Valves = dtoItem.Valves
            };

            return result;
        }

        public Profile ConvertToProfile(Profile dtoItem)
        {
            var result = new Profile()
            {
                Id = dtoItem.Id,
                IsActive = dtoItem.IsActive,
                Name = dtoItem.Name,
                DayOfWeek = dtoItem.DayOfWeek,
                StartHour = dtoItem.StartHour,
                StartMinute = dtoItem.StartMinute
            };

            return result;
        }
        ////public ProfileDTO ConvertToProfileDTO(Profile entity, IEnumerable<Group> groups)
        //public Profile ConvertToProfileDTO(Profile entity, IEnumerable<Group> groups)
        //{
        //    if (entity == null)
        //        return null;
        //    var result = new Profile()
        //    {
        //        Id = entity.Id,
        //        IsActive = entity.IsActive,
        //        Name = entity.Name,
        //        DayOfWeek = entity.DayOfWeek,
        //        StartHour = entity.StartHour,
        //        StartMinute = entity.StartMinute,
        //        Groups = groups
        //    };

        //    return result;
        //}
        public Profile ConvertToProfile(InsertProfileRequestModel requestModel)
        {
            var result = new Profile()
            {
                //Id =
                IsActive = true,
                Name = requestModel.Name,
                DayOfWeek = requestModel.DayOfWeek,
                StartHour = requestModel.StartHour,
                StartMinute = requestModel.StartMinute
            };
            
            return result;

        }
        public Profile ConvertToProfile(UpdateProfileRequestModel requestModel)
        {
            var result = new Profile()
            {
                Id = requestModel.ProfileId,
                IsActive = true,
                Name = requestModel.Name,
                DayOfWeek = requestModel.DayOfWeek,
                StartHour = requestModel.StartHour,
                StartMinute = requestModel.StartMinute                
            };

            return result;
        }
        public ProfileResponseModel ConvertToProfileResponseModel(Profile item, IEnumerable<Group> groups)
        {
            if (item == null)
                return null;
            var result = new ProfileResponseModel()
            {
                Id = item.Id,
                Name = item.Name,
                DayOfWeek = item.DayOfWeek,
                StartHour = item.StartHour,
                StartMinute = item.StartMinute,
                Groups = groups
            };

            return result;
        }

    }
}
