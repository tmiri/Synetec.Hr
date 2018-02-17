using Microsoft.AspNetCore.Identity;

namespace Synetec.Hr.Database.Entities
{
    public class Role : IdentityRole<string>
    {
        public string Description { get; set; }
    }
}
