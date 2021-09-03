using AutoMapper;
using VacationManager.Models.Entities;
using VacationManager.Models.Responses;

namespace VacationManager.Services
{
    public interface IHolidayService : IBaseService { }

    public class HolidayService :
        BaseService<Holiday, HolidayResponse>,
        IHolidayService
    {
        public HolidayService(IMapper mapper) : base(mapper) { }
    }
}
