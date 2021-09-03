using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using VacationManager.Models.Entities;
using VacationManager.Models.Requests;
using VacationManager.Models.Responses;

namespace VacationManager.Mappings
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
                    member => member.MapFrom(x => false)
                )
                .ForMember(
                    destination => destination.TotalDays,
                    member => member.MapFrom(field => (field.To - field.From).TotalDays)
                )
                .ForMember(
                    destination => destination.TotalDays,
                    member => member.MapFrom(field => (field.To.Date - field.From.Date).TotalDays)
                );
        }

        public bool Test(Vacation vacation)
        {
            var fromDate = vacation.From.Date;

            var a = new List<DateTime>
            {
                fromDate,
            };

            while (fromDate <= vacation.To.Date)
            {
                a.Add(fromDate);
                fromDate = fromDate.AddDays(1);
            }

            var daysOfWeek = a.Select(x => x.DayOfWeek);

            
            if (daysOfWeek.Any(x=> x == DayOfWeek.Sunday || x == DayOfWeek.Saturday)) { }
        }
    }
}
