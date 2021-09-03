using System.Collections.Generic;

namespace VacationManager.Models.Requests
{
    public interface IRequest
    {
        public IEnumerable<string> Validate();
    }

    public interface IUpdateableRequest : IRequest
    {
        public int Id { get; set; }
    }
}