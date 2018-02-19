using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using ElearnerApi.Data.Models.Authentication;
using ElearnerApi.Data.DataContext;

namespace ElearnerApi.Controllers
{
    public abstract class BaseController : Controller
	{
        protected ApplicationDbContext _context = null;

		private UserManager<ApplicationUser> userManager = null;
        
        public BaseController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
        {
            this.userManager = userManager;
            this._context = dbContext;
        }

        protected string GetUserId ( )
		{
			var userId = this.User.FindFirstValue ( ClaimTypes.NameIdentifier );

			return userId;
		}

		protected async Task<ApplicationUser> GetUser ( )
		{
			var user = await userManager.FindByIdAsync ( this.GetUserId ( ) );

			return user;
		}
	}
}