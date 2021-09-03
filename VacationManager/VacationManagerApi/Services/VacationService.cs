using AutoMapper;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Services
{
    public interface IVacationService : IBaseService { }

    public class VacationService :
        BaseService<Vacation, VacationResponse>,
        IVacationService
    {
        public VacationService(IMapper mapper) : base(mapper) { }
    }
}
