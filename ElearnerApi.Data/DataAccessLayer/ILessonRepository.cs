using ElearnerApi.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElearnerApi.Data.DataAccessLayer
{
    public interface ILessonRepository
    {
        Task<Lesson> GetLessonById(string id);
    }
}