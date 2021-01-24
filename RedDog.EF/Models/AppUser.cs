using Microsoft.AspNetCore.Identity;
namespace RedDog.EF.Models
{
    //user identity
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
