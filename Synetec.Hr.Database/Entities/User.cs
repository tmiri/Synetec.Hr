using System;
using Microsoft.AspNetCore.Identity;

namespace Synetec.Hr.Database.Entities
{
    public class User : IdentityUser<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
