using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VacationManagerApi.Helpers;
using VacationManagerApi.Models.Dtos;
using VacationManagerApi.Models.Entities;
using VacationManagerApi.Models.Requests;

namespace VacationManagerApi.Mappings
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Holiday, HolidayDto>();
            CreateMap<HolidayRequest, Holiday>();

            CreateMap<Vacation, VacationDto>()
                .ForMember(
                    destination => destination.HolidayName,
                    member => member.MapFrom(field => field.Holiday.Name)
                )
                .ForMember(
                    destination => destination.IsLongWeekend,
                    member => member.MapFrom(
                        field => DateTimeHelper.ContainsLongWeekends(field.From, field.To)
                    )
                )
                .ForMember(
                    destination => destination.Year,
                    member => member.MapFrom(field => field.From.Year)
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
            CreateMap<VacationRequest, Vacation>();

            CreateMap<RegisterUserRequest, ApplicationUser>();
            CreateMap<SingInRequest, ApplicationUser>();
        }
    }
}
