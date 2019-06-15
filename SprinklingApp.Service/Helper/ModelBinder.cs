using System;
using System.Collections.Generic;
using System.Threading;
using SprinklingApp.Model.ApiRequestModels.Concrete;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Entities.Concrete;

namespace SprinklingApp.Service.Helper {

    public class ModelBinder {
        #region [ CTOR ]

        private ModelBinder() { }

        #endregion

        public Group ConvertToGroup(InsertGroupRequestModel requestModel) {
            Group result = new Group {
                IsActive = true,
                Duration = requestModel.Duration,
                Name = requestModel.Name,
                Unit = requestModel.Unit
            };

            return result;
        }

        public Group ConvertToGroup(UpdateGroupRequestModel requestModel) {
            Group result = new Group {
                Id = requestModel.Id,
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
        public GroupResponseModel ConvertToGroupResponseModel(Group entity, IEnumerable<Valve> valves) {
            if (entity == null) {
                return null;
            }

            GroupResponseModel result = new GroupResponseModel {
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
        public Valve ConvertToValve(InsertValveRequestModel requestModel) {
            Valve result = new Valve {
                IsActive = true,
                Name = requestModel.Name,
                EnablePin = requestModel.EnablePin,
                IsOpen = requestModel.IsOpen,
                DisablePin = requestModel.DisablePin,
                Pressure = requestModel.Pressure,
                RaspberryId = requestModel.RaspberryId,
                CloseDateTime = requestModel.CloseDateTime
            };

            return result;
        }

        public Valve ConvertToValve(UpdateValveRequestModel requestModel) {
            Valve result = new Valve {
                Id = requestModel.ValveId,
                EnablePin = requestModel.EnablePin,
                DisablePin = requestModel.DisablePin,
                IsActive = true,
                IsOpen = requestModel.IsOpen,
                Name = requestModel.Name,
                Pressure = requestModel.Pressure,
                RaspberryId = requestModel.RaspberryId,
                CloseDateTime = requestModel.CloseDateTime
            };

            return result;
        }

        public ValveResponseModel ConvertToValveResponseModel(Valve dtoItem) {
            if (dtoItem == null) {
                return null;
            }

            ValveResponseModel result = new ValveResponseModel {
                Id = dtoItem.Id,
                Name = dtoItem.Name,
                IsOpen = dtoItem.IsOpen,
                IsActive = dtoItem.IsActive,
                Pressure = dtoItem.Pressure,
                EnablePin = dtoItem.EnablePin,
                DisablePin = dtoItem.DisablePin,
                RaspberryId = dtoItem.RaspberryId,
                CloseDateTime = dtoItem.CloseDateTime
            };

            return result;
        }

        public Raspberry ConvertToRaspberry(Raspberry dtoItem) {
            Raspberry result = new Raspberry {
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

        public Raspberry ConvertToRaspberry(InsertRaspberryRequestModel requestModel, IEnumerable<Valve> valves) {
            Raspberry result = new Raspberry {
                IsActive = true,
                Name = requestModel.Name,
                IPAddress = requestModel.IPAddress,
                Valves = valves
            };

            return result;
        }

        public Raspberry ConvertToRaspberry(UpdateRaspberryRequestModel requestModel, IEnumerable<Valve> valves) {
            Raspberry result = new Raspberry {
                Id = requestModel.Id,
                IsActive = true,
                Name = requestModel.Name,
                IPAddress = requestModel.IPAddress,
                Valves = valves
            };

            return result;
        }

        public RaspberryResponseModel ConvertToRaspberryResponseModel(Raspberry dtoItem) {
            if (dtoItem == null) {
                return null;
            }

            RaspberryResponseModel result = new RaspberryResponseModel {
                Id = dtoItem.Id,
                Name = dtoItem.Name,
                IPAddress = dtoItem.IPAddress,
                Valves = dtoItem.Valves
            };

            return result;
        }

        public Profile ConvertToProfile(Profile dtoItem) {
            Profile result = new Profile {
                Id = dtoItem.Id,
                IsActive = dtoItem.IsActive,
                Name = dtoItem.Name,
                Monday =  dtoItem.Monday,
                Tuesday =  dtoItem.Tuesday,
                Wednesday =  dtoItem.Wednesday,
                Thursday =  dtoItem.Thursday,
                Friday =  dtoItem.Friday,
                Saturday =  dtoItem.Saturday,
                IsPassive = dtoItem.IsPassive,
                StartingDate = dtoItem.StartingDate,
                Sunday =  dtoItem.Sunday,
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
        public Profile ConvertToProfile(InsertProfileRequestModel requestModel) {
            Profile result = new Profile {
                //Id =
                IsActive = true,
                Name = requestModel.Name,
                Monday = requestModel.Monday,
                Tuesday = requestModel.Tuesday,
                Wednesday = requestModel.Wednesday,
                Thursday = requestModel.Thursday,
                Friday = requestModel.Friday,
                Saturday = requestModel.Saturday,
                Sunday = requestModel.Sunday,
                StartHour = requestModel.StartHour,
                IsPassive = requestModel.IsPassive,
                StartingDate = requestModel.StartingDate,
                StartMinute = requestModel.StartMinute
            };

            return result;
        }

        public Profile ConvertToProfile(UpdateProfileRequestModel requestModel) {
            Profile result = new Profile {
                Id = requestModel.Id,
                IsActive = true,
                Name = requestModel.Name,
                Monday = requestModel.Monday,
                Tuesday = requestModel.Tuesday,
                Wednesday = requestModel.Wednesday,
                Thursday = requestModel.Thursday,
                Friday = requestModel.Friday,
                Saturday = requestModel.Saturday,
                Sunday = requestModel.Sunday,
                StartHour = requestModel.StartHour,
                IsPassive = requestModel.IsPassive,
                StartingDate = requestModel.StartingDate,
                StartMinute = requestModel.StartMinute
            };

            return result;
        }

        public ProfileResponseModel ConvertToProfileResponseModel(Profile item, IEnumerable<Group> groups) {
            if (item == null) {
                return null;
            }

            ProfileResponseModel result = new ProfileResponseModel {
                Id = item.Id,
                Name = item.Name,
                Monday = item.Monday,
                Tuesday = item.Tuesday,
                Wednesday = item.Wednesday,
                Thursday = item.Thursday,
                Friday = item.Friday,
                Saturday = item.Saturday,
                Sunday = item.Sunday,
                StartHour = item.StartHour,
                StartMinute = item.StartMinute,
                IsPassive =  item.IsPassive,
                StartingDate = item.StartingDate,
                Groups = groups
            };

            return result;
        }

        #region [ Lazy-Singleton ]

        private static readonly Lazy<ModelBinder> _instance = new Lazy<ModelBinder>(() => new ModelBinder(), LazyThreadSafetyMode.ExecutionAndPublication);
        internal static ModelBinder Instance => _instance.Value;

        #endregion
    }

}