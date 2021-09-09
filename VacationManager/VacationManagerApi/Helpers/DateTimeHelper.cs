using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationManagerApi.Helpers
{
    public static class DateTimeHelper
    {
        public static int GetTotalOfDays(DateTime from, DateTime to)
        {
            var dates = GetDatesBetweenTwoDates(from, to).ToList();

            return GetTotalOfWorkingDays(dates) + GetWeekendsTotal(dates) * 2;
        }

        public static int GetTotalOfWorkingDays(IEnumerable<DateTime> dates)
        {
            return dates.Count(
                date => date.DayOfWeek is not (DayOfWeek.Saturday or DayOfWeek.Sunday)
            );
        }

        public static int GetTotalOfWorkingDays(DateTime from, DateTime to)
        {
            return GetTotalOfWorkingDays(GetDatesBetweenTwoDates(from, to));
        }

        public static bool ContainsLongWeekends(DateTime from, DateTime to)
        {
            return GetDatesBetweenTwoDates(from, to).Any(
                date => date.DayOfWeek is DayOfWeek.Monday or DayOfWeek.Friday
            );
        }

        public static int GetWeekendsTotal(IEnumerable<DateTime> dates)
        {
            var counter = 0;
            DateTime? friday = null;

            foreach (var date in dates)
            {
                switch (date.DayOfWeek)
                {
                    case DayOfWeek.Friday:
                        counter++;
                        friday = date;

                        break;

                    case DayOfWeek.Monday when friday is null:
                        counter++;

                        break;
                }
            }

            return counter;
        }

        public static int GetWeekendsTotal(DateTime from, DateTime to)
        {
            return GetWeekendsTotal(GetDatesBetweenTwoDates(from, to));
        }

        public static IEnumerable<DateTime> GetDatesBetweenTwoDates(DateTime from, DateTime to)
        {
            while (from.Date <= to.Date)
            {
                yield return from;

                from = from.AddDays(1);
            }
        }
    }
}
