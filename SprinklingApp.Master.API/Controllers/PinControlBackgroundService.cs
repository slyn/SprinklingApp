using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SprinklingApp.Model.ApiResponseModels.Concrete;
using SprinklingApp.Model.Entities.Concrete;
using SprinklingApp.Service.Procedures.Abstract;

namespace SprinklingApp.Master.API.Controllers {

    public class PinControlBackgroundService : BackgroundService {
        private readonly IServiceScopeFactory scopeFactory;

        public PinControlBackgroundService(IServiceScopeFactory scopeFactory) {
            this.scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {

            using (IServiceScope scope = scopeFactory.CreateScope()) {
                //var valveService = scope.ServiceProvider.GetRequiredService<IValveProcedure>();
                var profileService = scope.ServiceProvider.GetRequiredService<IProfileProcedure>();
                var groupService = scope.ServiceProvider.GetRequiredService<IGroupProcedure>();
                var pinController = scope.ServiceProvider.GetRequiredService<PinController>();
                var settingsController = scope.ServiceProvider.GetRequiredService<MainController>();

                while (!stoppingToken.IsCancellationRequested) {
                    //todo check for is whole system is active
                    //if not, turn off all open valves

                    var settings = settingsController.Get().Value;
                    
                    if (settings.IsStarted) {
                        var profiles = profileService.GetList().ToList();

                        List<ProfileResponseModel> todaysProfiles = GetTodaysProfiles(profiles);
                        IOrderedEnumerable<ProfileResponseModel> orderedTodaysProfiles = todaysProfiles.OrderBy(o => o.StartHour).ThenBy(o => o.StartMinute);
                        foreach (ProfileResponseModel profile in orderedTodaysProfiles)
                        {
                            DateTime profileEndTime = GetProfileEndTime(profile);

                            /*check for is active

                            get today's profiles+
                                order by starting hour/minute+
                                add groups working time by one by
                                if it is smaller
                                    keep adding
                                else
                                    open groups valves
                                    turn off other open valves
                                    */

                            if (DateTime.Now.CompareTo(profileEndTime) >= 0)
                            {
                                continue;
                            }

                            List<Group> profileGroups = profile.Groups.ToList();
                            int leftMinutes = DateTime.Now.Hour * 60 + DateTime.Now.Minute - profile.StartHour * 60 - profile.StartMinute;
                            for (int i = profileGroups.Count - 1; i >= 0; i--)
                            {
                                var g = groupService.Get(profileGroups[i].Id);
                                if (leftMinutes < profileGroups[i].Duration)
                                {
                                    foreach (Valve valve in g.Valves)
                                    {
                                        pinController.Open(valve.Id);
                                    }
                                }
                                else
                                {
                                    foreach (Valve valve in g.Valves)
                                    {
                                        pinController.Close(valve.Id);
                                    }
                                }
                            }

                        }
                    }

                    await Task.Delay(1000 * 60, stoppingToken);
                }
            }
        }

        private static DateTime GetProfileEndTime(ProfileResponseModel profile) {
            var profileEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            profileEndTime = profileEndTime.AddHours(profile.StartHour).AddMinutes(profile.StartMinute);
            profileEndTime = profileEndTime.AddMinutes(profile.Groups.Sum(o => o.Duration));
            return profileEndTime;
        }

        private static List<ProfileResponseModel> GetTodaysProfiles(List<ProfileResponseModel> profiles) {
            List<ProfileResponseModel> todaysProfiles = new List<ProfileResponseModel>();

            foreach (ProfileResponseModel profile in profiles) {
                int nowDayOfWeek = (int) DateTime.Now.DayOfWeek;
                int profileDayOfWeek = (int) profile.DayOfWeek;
                if (nowDayOfWeek == profileDayOfWeek) {
                    todaysProfiles.Add(profile);
                }
            }

            return todaysProfiles;
        }
    }

}