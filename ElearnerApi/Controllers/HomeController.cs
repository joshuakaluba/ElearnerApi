using ElearnerApi.Data.DataAccessLayer;
using ElearnerApi.Data.DataContext;
using ElearnerApi.Data.Models;
using ElearnerApi.Data.Models.Authentication;
using ElearnerApi.Data.Models.ExceptionHandling;
using ElearnerApi.Data.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElearnerApi.Controllers
{
    public class HomeController : BaseController
    {        
        public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext)
            :base(userManager, dbContext)
        {            
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult UploadCourse()
        {
            return View(  );
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UploadCourse(string xmlTextInput, List<IFormFile> files)
        {
            try
            {
                Course course;

                if (files.Count > 0)
                {
                    IFormFile file = Request.Form.Files[0];

                    var allowedExtensions = new[] { ".xml", ".txt" };

                    if (!allowedExtensions.Contains( Path.GetExtension( file.FileName ).ToLower( ) ))
                    {
                        TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Danger, "Only .xml and .txt files accepted" );

                        return View( );
                    }

                    string textContent = string.Empty;

                    using (var reader = new StreamReader( file.OpenReadStream( ) ))
                    {
                        textContent = reader.ReadToEnd( );
                    }

                    course = XmlParser.FromXml<Course>( textContent );
                }
                else
                {
                    course = XmlParser.FromXml<Course>( xmlTextInput );
                }

                var courseRepository = new CourseRepository( );

                await courseRepository.CreateCourse( course );

                TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Success, "Course created successfully;" );
            }
            catch (Exception ex)
            {
                TempData["CustomResponseAlert"] = CustomResponseAlert.GetStringResponse( ResponseStatusEnum.Danger, ex.Message );
            }

            return View( );
        }

        [Authorize]
        public async Task<IActionResult> Course( string id)
        {
            if(id == null)
            {
                return NotFound( );
            }

            ICourseRepository courseRepository = new CourseRepository( );

            var course = await courseRepository.GetCourseById( new Guid( id) );

            return View( course );
        }
        
        [HttpGet]
        public async Task<IActionResult> Lesson(string id)
        {
            try
            {
                ILessonRepository lessonRepository = new LessonRepository( );

                var lesson = await lessonRepository.GetLessonById( id );

                return Ok( lesson );
            }
            catch (Exception ex)
            {
                return BadRequest( new ErrorMessage( ex ) );
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}