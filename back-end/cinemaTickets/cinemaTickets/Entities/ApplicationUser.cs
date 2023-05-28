using Microsoft.AspNetCore.Identity;

namespace cinemaTickets.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName  { get; set; }
    }
}
