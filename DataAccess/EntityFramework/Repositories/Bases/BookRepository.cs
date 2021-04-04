using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories.Bases
{
    public class BookRepository : BookRepositoryBase
    {
        public BookRepository(DbContext db) : base(db)
        {

        }
    }
}
