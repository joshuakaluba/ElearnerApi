using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElearnerApi.Data.DataContext;
using ElearnerApi.Data.Models;
using Microsoft.AspNetCore.Authorization;
using ElearnerApi.Data.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using ElearnerApi.Data.DataAccessLayer;

namespace ElearnerApi.Controllers
{
    [Authorize]
    public class CoursesController : BaseController
    {
        public CoursesController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
            : base( userManager, dbContext )
        {
        }

        public async Task<IActionResult> Index(string name)
        {
            ICourseRepository courseRepository = new CourseRepository( );

            if(name!=null)
            {
                var courses = await courseRepository.SearchForCourses( name );

                TempData["SearchString"] = name;

                return View( courses );
            }
            else
            {
                var courses = await courseRepository.GetCourses( );

                return View( courses );
            }
           
        }

        
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,Description,Id,Name")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();

                    TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Success, "Saved Successfully!" );
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .SingleOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var course = await _context.Courses.SingleOrDefaultAsync(m => m.Id == id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Success, "Deleted Successfully!" );

            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
