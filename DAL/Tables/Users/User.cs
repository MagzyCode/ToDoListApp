using Microsoft.AspNetCore.Identity;
using System;

namespace DAL.Tables.Users
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime DayOfBorn { get; set; }
    }
}
