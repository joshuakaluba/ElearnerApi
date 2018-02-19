using ElearnerApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ElearnerApi.Data.DataAccessLayer
{
    public class LessonRepository : BaseRepository, ILessonRepository
    {
        public async Task<Lesson> GetLessonById(string id)
        {
            var guid = new Guid( id );

            var lesson
                = await _context.Lessons
                    .Where( l => l.Id == guid )
                       .Include( l => l.LearningObjects )
                            .FirstOrDefaultAsync( );

            return lesson;
        }
    }
}