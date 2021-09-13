using System;
using VacationManagerApi.Helpers.CustomValidations;

namespace VacationManagerApi.Models.Requests
{
    public class VacationRequest : IRequest
    {
        [HolidayIdMustBeGreaterThanZero]
        public int HolidayId { get; set; }

        public DateTime From { get; set; }

        [ToMustBeGreaterOrEqualToFrom]
        public DateTime To { get; set; }

        public int Year { get; set; }
    }
}
