using ElearnerApi.Data.DataAccessLayer;
using ElearnerApi.Data.Models;
using ElearnerApi.Data.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ElearnerApi.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void SerializeCourse()
        {
            string xml = File.ReadAllText( @"C:\Users\kalub\OneDrive\CIS\COMP466\ElearnerProject\ElearnerApi\wwwroot\assets\SampleCourse.xml" );

            Course entity = XmlParser.FromXml<Course>( xml );
        }

        [TestMethod]
        public async Task AddCourse()
        {
            string xml = File.ReadAllText( @"C:\Users\kalub\OneDrive\CIS\COMP466\ElearnerProject\ElearnerApi\wwwroot\assets\SampleCourse.xml" );

            Course entity = XmlParser.FromXml<Course>( xml );

            ICourseRepository courseRepository = new CourseRepository( );

            await courseRepository.CreateCourse( entity );
        }

        [TestMethod]
        public async Task GetCourses()
        {
            ICourseRepository courseRepository = new CourseRepository( );

            List<Course> courses = await courseRepository.GetCourses( ); ;
        }
    }
}