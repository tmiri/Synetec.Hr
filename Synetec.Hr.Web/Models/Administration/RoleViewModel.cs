using System.ComponentModel.DataAnnotations;

namespace Synetec.Hr.Web.Models.Administration
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
