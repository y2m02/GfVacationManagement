using System;
using System.Collections.Generic;
using HelpersLibrary.Extensions;
using VacationManagerApi.Models.Helpers;

namespace VacationManagerApi.Models.Requests
{
    public class VacationRequest : IRequest
    {
        public int HolidayId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Year { get; set; }

        public virtual IEnumerable<string> Validate()
        {
            if (HolidayId < 1)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(HolidayId));
            }

            if (From > To)
            {
                yield return ConsumerMessages
                    .FieldMustBeGreaterOrEqualsTo
                    .Format(nameof(To), nameof(From));
            }

            const int year = 2020;

            if (Year < year)
            {
                yield return ConsumerMessages
                    .FieldMustBeGreaterOrEqualsTo
                    .Format(nameof(Year), year);
            }
        }
    }
}
