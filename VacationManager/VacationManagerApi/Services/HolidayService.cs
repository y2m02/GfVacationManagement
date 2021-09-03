using AutoMapper;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Services
{
    public interface IHolidayService : IBaseService { }

    public class HolidayService :
        BaseService<Holiday, HolidayResponse>,
        IHolidayService
    {
        public HolidayService(IMapper mapper) : base(mapper) { }
    }
}
