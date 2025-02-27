using Microsoft.AspNetCore.Identity;

namespace ITI_project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Address { set; get; }
    }
}
