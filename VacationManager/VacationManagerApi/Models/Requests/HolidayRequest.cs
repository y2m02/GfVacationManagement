using System.Collections.Generic;
using HelpersLibrary.Extensions;
using VacationManagerApi.Models.Helpers;

namespace VacationManagerApi.Models.Requests
{
    public class HolidayRequest : IRequest
    {
        public string Name { get; set; }
        public int TotalDays { get; set; }

        public virtual IEnumerable<string> Validate()
        {
            if (Name.IsEmpty())
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Name));
            }

            if (TotalDays < 1)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(TotalDays));
            }
        }
    }
}
