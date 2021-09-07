using AutoMapper;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Responses;
using VacationManagerApi.Repositories;

namespace VacationManagerApi.Services
{
    public interface IHolidayService : IBaseService { }

    public class HolidayService :
        BaseService<Holiday, HolidayResponse>,
        IHolidayService
    {
        public HolidayService(
            IMapper mapper,
            IHolidayRepository repository
        ) : base(mapper) => Repository = repository;
    }
}
