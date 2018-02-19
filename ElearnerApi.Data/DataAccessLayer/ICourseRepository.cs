using ElearnerApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElearnerApi.Data.DataAccessLayer
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetCourses();

        Task<List<Course>> SearchForCourses(string name);

        Task<Course> GetCourseById(Guid courseId);

        Task CreateCourse(Course course);
    }
}