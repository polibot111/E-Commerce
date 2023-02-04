﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurName { get; set; }
        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
