using ElearnerApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElearnerApi.Data.DataAccessLayer
{
    public class CourseRepository : BaseRepository, ICourseRepository
    {
        public CourseRepository()
        {
        }

        public async Task CreateCourse(Course course)
        {
            _context.Courses.Add( course );

            await _context.SaveChangesAsync( );
        }

        public async Task<Course> GetCourseById( Guid id)
        {
            var course 
                = await _context.Courses
                    .Where( c => c.Id == id )
                        .Include(c => c.Units)
                            .ThenInclude( unit => unit.Lessons )
                                .ThenInclude( lesson => lesson.LearningObjects )
                                    .FirstOrDefaultAsync( );
            return course;
        }

        public async Task<List<Course>> GetCourses()
        {
            return await _context.Courses
                    .OrderByDescending(course=>course.DateCreated)
                       .Include(course=>course.Units)
                            .ThenInclude(unit=>unit.Lessons)
                                .ThenInclude(lesson=>lesson.LearningObjects )
                                    .ToListAsync( );
        }

        public async Task<List<Course>> SearchForCourses(string name)
        {
            List<Course> courses
                    = await _context.Courses
                        .Where( c => c.Name.ToLower( ).Contains( name.ToLower( ) ) || c.Description.ToLower( ).Contains( name.ToLower( ) ) )
                            .OrderByDescending(c=>c.DateCreated)
                                .ToListAsync( );

            return courses;
        }
    }
}