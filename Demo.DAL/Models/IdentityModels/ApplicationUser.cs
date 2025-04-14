using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Demo.DAL.Models.IdentityModels
{
    public class ApplicationUser: IdentityUser
    {
        public string FristName { get; set; } = null!;
        public string? LastName { get; set; }

    }
}
