﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VacationManagerApi.Models.Entities;

namespace VacationManagerApi.Repositories
{
    public interface IVacationRepository : IBaseRepository<Vacation> { }

    public class VacationRepository : BaseRepository<Vacation>, IVacationRepository
    {
        public VacationRepository(VacationManagerContext context) : base(context) { }

        public override Task<List<Vacation>> GetAll()
        {
            return context.Vacations.Include(x => x.Holiday).ToListAsync();
        }

        public Task<int> Update(Vacation entity)
        {
            return Update(entity, new[] { "HolidayId", "Year", "To", "From" });
        }
    }
}