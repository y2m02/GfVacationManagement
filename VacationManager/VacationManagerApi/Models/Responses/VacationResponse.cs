using System;

namespace VacationManagerApi.Models.Responses
{
    public class VacationResponse : BaseResponse
    {
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Year { get; set; }
        public bool IsLongWeekend { get; set; }
        public int TotalWorkingDays { get; set; }
        public int TotalDays { get; set; }
    }
}
