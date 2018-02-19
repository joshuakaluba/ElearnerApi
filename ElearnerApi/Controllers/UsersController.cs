using ElearnerApi.Data.DataContext;
using ElearnerApi.Data.Models;
using ElearnerApi.Data.Models.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ElearnerApi.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
            : base( userManager, dbContext )
        {
        }

        public async Task<IActionResult> Index( string name)
        {


            if (name != null)
            {
                TempData["SearchString"] = name;


                var users
                    = await _context.ApplicationUsers
                        .Where( user => user.Active == true && user.Email.ToLower().Contains(name.ToLower()) || user.PhoneNumber.ToLower().Contains(name.ToLower()))
                            .ToListAsync( );

                return View( users );
                
            }
            else
            {
                var users
                    = await _context.ApplicationUsers
                        .Where( user => user.Active == true )
                            .ToListAsync( );

                return View( users );
            }

            
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound( );
            }

            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync( m => m.Id == id );

            if (applicationUser == null)
            {
                return NotFound( );
            }

            return View( applicationUser );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind( "Id,Email,PhoneNumber" )] ApplicationUser applicationUser)
        {
            var dbUser = await _context.Users.Where( user => user.Id == id ).FirstOrDefaultAsync( );

            if ((dbUser == null) || (id != applicationUser.Id))
            {
                return NotFound( );
            }

            if (ModelState.IsValid)
            {
                try
                {
                    dbUser.ChangeEmail( applicationUser.Email );

                    dbUser.PhoneNumber = applicationUser.PhoneNumber;

                    _context.Update( dbUser );

                    await _context.SaveChangesAsync( );

                    TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Success, "Saved Successfully!" );
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!ApplicationUserExists( applicationUser.Id ))
                    {
                        return NotFound( );
                    }
                    else
                    {
                        throw ex;
                    }
                }
                return RedirectToAction( nameof( Index ) );
            }
            return View( applicationUser );
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound( );
            }

            var applicationUser = await _context.ApplicationUsers
                .SingleOrDefaultAsync( m => m.Id == id );
            if (applicationUser == null)
            {
                return NotFound( );
            }

            return View( applicationUser );
        }

        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUsers.SingleOrDefaultAsync( m => m.Id == id );
            _context.ApplicationUsers.Remove( applicationUser );
            await _context.SaveChangesAsync( );

            TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Success, "Deleted Successfully!" );

            return RedirectToAction( nameof( Index ) );
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUsers.Any( e => e.Id == id );
        }
    }
}