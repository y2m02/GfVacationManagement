using AutoMapper;
using VacationManagerApi.Models.Dtos;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Repositories;

namespace VacationManagerApi.Services
{
    public interface IVacationService : IBaseService { }

    public class VacationService :
        BaseService<Vacation, VacationDto>,
        IVacationService
    {
        public VacationService(
            IMapper mapper,
            IVacationRepository repository
        ) : base(mapper) => Repository = repository;
    }
}
