using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.DTOs.Concrete;
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

        public Group ConvertToGroup(GroupDTO dtoItem)
        {
            var result = new Group()
            {
                Id = dtoItem.Id,
                IsActive = dtoItem.IsActive,
                Duration = dtoItem.Duration,
                Name = dtoItem.Name,
                Unit = dtoItem.Unit
                
            };

            return result;
        }
        public GroupDTO ConvertToGroupDTO(Group entity, IEnumerable<Valve> valves)
        {
            if (entity == null)
                return null;
            var result = new GroupDTO()
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                Duration = entity.Duration,
                Name = entity.Name,
                Unit = entity.Unit,
                Valves = valves
            };

            return result;
        }
        public GroupDTO ConvertToGroupDTO(InsertGroupRequestModel requestModel,IEnumerable<ValveDTO> valves)
        {
            var result = new GroupDTO()
            {
                IsActive = true,
                Duration = requestModel.Duration,
                Name = requestModel.Name,
                Unit = requestModel.Unit,
                Valves =valves.Select(x=>ConvertToValve(x))
            };

            return result;

        }
        public GroupDTO ConvertToGroupDTO(UpdateGroupRequestModel requestModel)
        {
            var result = new GroupDTO()
            {
                Id = requestModel.GroupId,
                IsActive = true,
                Duration = requestModel.Duration,
                Name = requestModel.Name,
                Unit = requestModel.Unit,
                //ValveList = requestModel.ValveList // TODO :: Get Valve List
            };

            return result;

        }
        public GroupResponseModel ConvertToGroupResponseModel(GroupDTO dtoItem)
        {
            if (dtoItem == null)
                return null;
            var result = new GroupResponseModel()
            {
                Id = dtoItem.Id,
                Duration = dtoItem.Duration,
                Name = dtoItem.Name,
                Unit = dtoItem.Unit,
                Valves = dtoItem.Valves
            };

            return result;
        }

        public Valve ConvertToValve(ValveDTO dtoItem)
        {
            var result = new Valve()
            {
                Id = dtoItem.Id,
                IsActive = dtoItem.IsActive,
                Name = dtoItem.Name,
                Pin = dtoItem.Pin,
                Pressure = dtoItem.Pressure,
                RaspberryId = dtoItem.RaspberryId
            };

            return result;
        }
        public ValveDTO ConvertToValveDTO(Valve entity, Raspberry raspberry)
        {
            var result = new ValveDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                IsActive = entity.IsActive,
                Pin = entity.Pin,
                Pressure = entity.Pressure,
                RaspberryId = entity.RaspberryId,
                Raspberry = raspberry
            };

            return result;
        }
        public ValveDTO ConvertToValveDTO(InsertValveRequestModel requestModel)
        {
            var result = new ValveDTO()
            {
                IsActive = true,
                Name = requestModel.Name,
                Pin = requestModel.Pin,
                Pressure = requestModel.Pressure,
                RaspberryId = requestModel.RaspberryId
            };

            return result;

        }
        public ValveDTO ConvertToValveDTO(UpdateValveRequestModel requestModel)
        {
            var result = new ValveDTO()
            {
                Id = requestModel.ValveId,
                Pin = requestModel.Pin,
                IsActive = true,
                Name = requestModel.Name,
                Pressure = requestModel.Pressure,
                RaspberryId =requestModel.RaspberryId
            };

            return result;
        }
        public ValveResponseModel ConvertToValveResponseModel(ValveDTO dtoItem)
        {
            if (dtoItem == null)
                return null;

            var result = new ValveResponseModel()
            {
                Id = dtoItem.Id,
                Name = dtoItem.Name,
                Pressure = dtoItem.Pressure,
                Pin = dtoItem.Pin,
                RaspberryId = dtoItem.RaspberryId
            };

            return result;
        }

        public Raspberry ConvertToRaspberry(RaspberryDTO dtoItem)
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
        public RaspberryDTO ConvertToRaspberryDTO(Raspberry entity, IEnumerable<Valve> valves)
        {
            if (entity == null)
                return null;

            var result = new RaspberryDTO()
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                Name = entity.Name,
                IPAddress = entity.IPAddress,
                Valves = valves
            };

            return result;
        }
        public RaspberryDTO ConvertToRaspberryDTO(InsertRaspberryRequestModel requestModel)
        {
            var result = new RaspberryDTO()
            {
                //Id = // sıraki Id atanacak
                IsActive = true,
                Name = requestModel.Name,
                IPAddress = requestModel.IPAddress,
                //Valves = requestModel.ValveIdList 
            };

            return result;

        }
        public RaspberryDTO ConvertToRaspberryDTO(UpdateRaspberryRequestModel requestModel)
        {
            var result = new RaspberryDTO()
            {
                Id = requestModel.RaspberryId,
                IsActive = true,
                Name = requestModel.Name,
                IPAddress = requestModel.IPAddress,
                //Valves todo GEt Valve List
            };

            return result;
        }
        public RaspberryResponseModel ConvertToRaspberryResponseModel(RaspberryDTO dtoItem)
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

        public Profile ConvertToProfile(ProfileDTO dtoItem)
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
        public ProfileDTO ConvertToProfileDTO(Profile entity, IEnumerable<Group> groups)
        {
            if (entity == null)
                return null;
            var result = new ProfileDTO()
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                Name = entity.Name,
                DayOfWeek = entity.DayOfWeek,
                StartHour = entity.StartHour,
                StartMinute = entity.StartMinute,
                Groups = groups
            };

            return result;
        }
        public ProfileDTO ConvertToProfileDTO(InsertProfileRequestModel requestModel)
        {
            var result = new ProfileDTO()
            {
                //Id = // sıraki Id atanacak
                IsActive = true,
                Name = requestModel.Name,
                DayOfWeek = requestModel.DayOfWeek,
                StartHour = requestModel.StartHour,
                StartMinute = requestModel.StartMinute,
                //Groups = requestModel.GroupIdList
            };

            return result;

        }
        public ProfileDTO ConvertToProfileDTO(UpdateProfileRequestModel requestModel)
        {
            var result = new ProfileDTO()
            {
                Id = requestModel.ProfileId,
                IsActive = true,
                Name = requestModel.Name,
                DayOfWeek = requestModel.DayOfWeek,
                StartHour = requestModel.StartHour,
                StartMinute = requestModel.StartMinute,
                //Groups = requestModel.GroupIdList
            };

            return result;
        }
        public ProfileResponseModel ConvertToProfileResponseModel(ProfileDTO dtoItem)
        {
            if (dtoItem == null)
                return null;
            var result = new ProfileResponseModel()
            {
                Id = dtoItem.Id,
                Name = dtoItem.Name,
                DayOfWeek = dtoItem.DayOfWeek,
                StartHour = dtoItem.StartHour,
                StartMinute = dtoItem.StartMinute,
                //Groups = 
            };

            return result;
        }

    }
}
