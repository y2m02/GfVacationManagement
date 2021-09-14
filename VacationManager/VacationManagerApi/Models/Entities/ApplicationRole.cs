﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VacationManagerApi.Models.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
