using cinemaTickets.Data;
using cinemaTickets.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace cinemaTickets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser>  _signInManager;
        private readonly UserManager<ApplicationUser>    _userManager;
        private readonly AppDbContext _context;
        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            AppDbContext context
            )
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
    }
}
