using SprinklingApp.Model.Enums;
using System;

namespace SprinklingApp.Service.Helper
{
    public static class ExtensionManager
    {
        public static Days ConvertToDays(this DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return Days.Monday;

                case DayOfWeek.Tuesday:
                    return Days.Tuesday;

                case DayOfWeek.Wednesday:
                    return Days.Wednesday;

                case DayOfWeek.Thursday:
                    return Days.Thursday;

                case DayOfWeek.Friday:
                    return Days.Friday;

                case DayOfWeek.Saturday:
                    return Days.Saturday;

                case DayOfWeek.Sunday:
                    return Days.Sunday;

                default:
                    throw new ArgumentOutOfRangeException("Haftanın günü belirlenemedi!");
            }
        }

        public static bool IsDays(this DateTime date, Days days)
        {
            var weekDay = date.DayOfWeek.ConvertToDays();

            return days.HasFlag(weekDay);
        }

    }
}