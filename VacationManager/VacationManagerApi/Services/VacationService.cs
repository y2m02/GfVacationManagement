using AutoMapper;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Responses;
using VacationManagerApi.Repositories;

namespace VacationManagerApi.Services
{
    public interface IVacationService : IBaseService { }

    public class VacationService :
        BaseService<Vacation, VacationResponse>,
        IVacationService
    {
        public VacationService(
            IMapper mapper,
            IVacationRepository repository
        ) : base(mapper) => Repository = repository;
    }
}
