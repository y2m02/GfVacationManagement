using AutoMapper;
using VacationManagerApi.Helpers;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Requests;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Holiday, HolidayResponse>();
            CreateMap<CreateHolidayRequest, Holiday>();
            CreateMap<UpdateHolidayRequest, Holiday>();

            CreateMap<Vacation, VacationResponse>()
                .ForMember(
                    destination => destination.HolidayName,
                    member => member.MapFrom(field => field.Holiday.Id)
                )
                .ForMember(
                    destination => destination.IsLongWeekend,
                    member => member.MapFrom(
                        field => DateTimeHelper.ContainsLongWeekends(field.From, field.To)
                    )
                )
                .ForMember(
                    destination => destination.TotalWorkingDays,
                    member => member.MapFrom(
                        field => DateTimeHelper.GetTotalOfWorkingDays(field.From, field.To)
                    )
                )
                .ForMember(
                    destination => destination.TotalDays,
                    member => member.MapFrom(
                        field => DateTimeHelper.GetTotalOfDays(field.From, field.To)
                    )
                );
            CreateMap<CreateVacationRequest, Vacation>();
            CreateMap<UpdateVacationRequest, Vacation>();
        }
    }
}
