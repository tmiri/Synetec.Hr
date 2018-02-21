using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Synetec.Hr.Database.Entities
{
    public class User : IdentityUser<string>
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Position { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        [Range(0, 5)]
        public float DaysCarriedOver { get; set; }

        [Required]
        [Range(0, 50)]
        public float LeaveAllowance { get; set; }

        [Required]
        public string LineManager { get; set; }

        [Required]
        public bool ChangePassword { get; set; }
    }
}
