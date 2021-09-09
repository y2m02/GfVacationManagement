using System.Collections.Generic;

namespace VacationManagerApi.Models.Responses
{
    public class Failure : IBaseResponse
    {
        public Failure(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}