using AutoMapper;
using VacationManagerApi.Models.Dtos;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Repositories;

namespace VacationManagerApi.Services
{
    public interface IHolidayService : IBaseService { }

    public class HolidayService :
        BaseService<Holiday, HolidayDto>,
        IHolidayService
    {
        public HolidayService(
            IMapper mapper,
            IHolidayRepository repository
        ) : base(mapper) => Repository = repository;
    }
}
