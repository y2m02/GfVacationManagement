using System.Collections.Generic;

namespace VacationManager.Models.Requests
{
    public interface IRequest
    {
        IEnumerable<string> Validate();
    }

    public interface IUpdateableRequest : IRequest
    {
        int Id { get; set; }
    }
}
