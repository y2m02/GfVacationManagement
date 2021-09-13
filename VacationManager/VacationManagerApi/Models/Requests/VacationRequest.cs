using System;

namespace VacationManagerApi.Models.Requests
{
    public class VacationRequest : IRequest
    {
        public int HolidayId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Year { get; set; }
    }
}
