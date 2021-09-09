using System.Collections.Generic;

namespace VacationManagerApi.Models.Requests
{
    public interface IRequest
    {
        IEnumerable<string> Validate();
    }
}
