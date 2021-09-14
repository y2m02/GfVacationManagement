using System.Collections.Generic;

namespace VacationManagerApi.Models.Responses
{
    public class Unauthorized: IBaseResponse
    {
        public Unauthorized() { }

        public Unauthorized(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }
}
