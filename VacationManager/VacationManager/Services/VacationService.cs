using AutoMapper;
using VacationManager.Models.Entities;
using VacationManager.Models.Responses;

namespace VacationManager.Services
{
    public interface IVacationService : IBaseService { }

    public class VacationService :
        BaseService<Vacation, VacationResponse>,
        IVacationService
    {
        public VacationService(IMapper mapper) : base(mapper) { }
    }
}
