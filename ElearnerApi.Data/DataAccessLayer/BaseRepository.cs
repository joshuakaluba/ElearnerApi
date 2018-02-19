using ElearnerApi.Data.DataContext;

namespace ElearnerApi.Data.DataAccessLayer
{
    public abstract class BaseRepository
    {
        protected ApplicationDbContext _context = null;

        public BaseRepository()
        {
            _context = new ApplicationDbContext( );
        }
    }
}