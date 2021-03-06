﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            //return;
            using (IServiceScope scope = scopeFactory.CreateScope()) {
                var valveService = scope.ServiceProvider.GetRequiredService<IValveProcedure>();
                IProfileProcedure profileService = scope.ServiceProvider.GetRequiredService<IProfileProcedure>();
                IGroupProcedure groupService = scope.ServiceProvider.GetRequiredService<IGroupProcedure>();
                //PinController pinController = scope.ServiceProvider.GetRequiredService<PinController>();
                MainController settingsController = scope.ServiceProvider.GetRequiredService<MainController>();

                valveService.GetList().ToList().ForEach(
                    valve => {
                        EasyRequest($"http://localhost:5000/api/v1/Pin/Close/{valve.Id}");
                    });
                Console.Clear();

                while (!stoppingToken.IsCancellationRequested) {
                    //todo check for is whole system is active
                    //if not, turn off all open valves

                    GgSettings settings = settingsController.Get().Value;

                    if (settings.IsStarted) {
                        List<ProfileResponseModel> profiles = profileService.GetList().ToList();

                        List<ProfileResponseModel> todaysProfiles = GetTodaysProfiles(profiles);
                        List<ProfileResponseModel> startTimePassedProfiles = GetStartTimePassedProfiles(todaysProfiles);
                        List<ProfileResponseModel> totalTimeIsNotPassedProfiles = GetTotalTimeIsNotPassedProfiles(startTimePassedProfiles);
                        IOrderedEnumerable<ProfileResponseModel> orderedTodaysProfiles = totalTimeIsNotPassedProfiles.OrderBy(o => o.StartHour).ThenBy(o => o.StartMinute);

                        List<Valve> valvesToBeOpen = new List<Valve>();
                        
                        foreach (ProfileResponseModel profile in orderedTodaysProfiles) {
                            //DateTime profileEndTime = GetProfileEndTime(profile);
                            //if (DateTime.Now.CompareTo(profileEndTime) >= 0) {
                            //    continue;
                            //}

                            List<Group> profileGroups = profile.Groups.ToList();
                            int passedTime =  (DateTime.Now.Hour * 60 + DateTime.Now.Minute - profile.StartHour * 60 - profile.StartMinute);

                            for (int i = profileGroups.Count - 1; i >= 0; i--) {
                                GroupResponseModel g = groupService.Get(profileGroups[i].Id);
                                if (passedTime < profileGroups[i].Duration) {
                                    valvesToBeOpen.AddRange(g.Valves);
                                    break;
                                } else {
                                    //foreach (Valve valve in g.Valves) {
                                    //    //EasyRequest($"http://localhost:5000/api/v1/Pin/Close/{valve.Id}");
                                    //    //pinController.Close(valve.Id);
                                    //}
                                    passedTime -= profileGroups[i].Duration;
                                }
                            }
                        }

                        valvesToBeOpen.ForEach(valve => EasyRequest($"http://localhost:5000/api/v1/Pin/Open/{valve.Id}"));
                        await Task.Delay(1000 * 5, stoppingToken);
                        List<ValveResponseModel> valves = valveService.GetList().ToList();
                        foreach (ValveResponseModel valve in valves) {
                            if (!valvesToBeOpen.Exists(o => o.Id == valve.Id)) {
                                EasyRequest($"http://localhost:5000/api/v1/Pin/Close/{valve.Id}");
                            }
                        }
                    }

                    await Task.Delay(1000 * 10, stoppingToken);
                }
            }
        }

        private List<ProfileResponseModel> GetTotalTimeIsNotPassedProfiles(IEnumerable<ProfileResponseModel> profiles) {
            List<ProfileResponseModel> totalTimeIsNotPassedProfiles = new List<ProfileResponseModel>();
            
            foreach (ProfileResponseModel profile in profiles) {
                int leftMinutes = DateTime.Now.Hour * 60 + DateTime.Now.Minute - profile.StartHour * 60 - profile.StartMinute;
                if (leftMinutes > 0 && leftMinutes < profile.Groups.Sum(o => o.Duration)) {
                    totalTimeIsNotPassedProfiles.Add(profile);
                }
            }

            return totalTimeIsNotPassedProfiles;
        }

        private static List<ProfileResponseModel> GetStartTimePassedProfiles(IEnumerable<ProfileResponseModel> profiles) {
            List<ProfileResponseModel> startTimePassedProfiles = new List<ProfileResponseModel>();

            foreach (ProfileResponseModel profile in profiles) {
                if (DateTime.Now.Hour >= profile.StartHour && DateTime.Now.Minute >= profile.StartMinute) {
                    startTimePassedProfiles.Add(profile);
                }
            }

            return startTimePassedProfiles;
        }

        private static DateTime GetProfileEndTime(ProfileResponseModel profile) {
            DateTime profileEndTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            profileEndTime = profileEndTime.AddHours(profile.StartHour).AddMinutes(profile.StartMinute);
            profileEndTime = profileEndTime.AddMinutes(profile.Groups.Sum(o => o.Duration));
            return profileEndTime;
        }

        private static List<ProfileResponseModel> GetTodaysProfiles(IReadOnlyCollection<ProfileResponseModel> profiles) {
            List<ProfileResponseModel> todaysProfiles = new List<ProfileResponseModel>();

            IEnumerable<ProfileResponseModel> collection = null;
            switch (DateTime.Now.DayOfWeek) {
                case DayOfWeek.Friday: {
                    try {
                        collection = profiles.Where(o => o.Friday);
                    }
                    catch { }

                    break;
                }

                case DayOfWeek.Monday: {
                    try {
                        collection = profiles.Where(o => o.Monday);
                    }
                    catch { }

                    break;
                }

                case DayOfWeek.Saturday: {
                    try {
                        collection = profiles.Where(o => o.Saturday);
                    }
                    catch { }

                    break;
                }

                case DayOfWeek.Sunday: {
                    try {
                        collection = profiles.Where(o => o.Sunday);
                    }
                    catch { }

                    break;
                }

                case DayOfWeek.Thursday: {
                    try {
                        collection = profiles.Where(o => o.Thursday);
                    }
                    catch { }

                    break;
                }

                case DayOfWeek.Tuesday: {
                    try {
                        collection = profiles.Where(o => o.Tuesday);
                    }
                    catch { }

                    break;
                }

                case DayOfWeek.Wednesday: {
                    try {
                        collection = profiles.Where(o => o.Wednesday);
                    }
                    catch { }

                    break;
                }
            }

            if (collection != null) {
                todaysProfiles.AddRange(collection);
            }
            //foreach (ProfileResponseModel profile in profiles) {
            //    int nowDayOfWeek = (int) DateTime.Now.DayOfWeek;
            //    int profileDayOfWeek = (int) profile.DayOfWeek;
            //    if (nowDayOfWeek == profileDayOfWeek) {
            //        todaysProfiles.Add(profile);
            //    }
            //}

            return todaysProfiles;
        }

        public static void EasyRequest(string url) {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = "GET";
            WebResponse webResponse = request.GetResponse();
            Stream webStream = webResponse.GetResponseStream();
            if (webStream == null) {
                return;
            }

            StreamReader responseReader = new StreamReader(webStream);
            responseReader.ReadToEnd();
            responseReader.Close();
        }
    }

}