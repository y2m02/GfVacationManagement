using System.Collections.Generic;

namespace VacationManagerApi.Models.Responses
{
    public class Validation : IBaseResponse
    {
        public Validation(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}