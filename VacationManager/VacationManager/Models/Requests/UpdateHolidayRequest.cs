using System.Collections.Generic;
using HelpersLibrary.Extensions;
using VacationManager.Models.Helpers;

namespace VacationManager.Models.Requests
{
    public class UpdateHolidayRequest : 
        CreateHolidayRequest, 
        IUpdateableRequest
    {
        public int Id { get; set; }

        public override IEnumerable<string> Validate()
        {
            if (Id < 1)
            {
                yield return ConsumerMessages.FieldRequired.Format(nameof(Id));
            }

            foreach (var validationError in base.Validate())
            {
                yield return validationError;
            }
        }
    }
}
