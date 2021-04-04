

using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories.Bases
{

    public class WriterRepository : WriterRepositoryBase
    {
        public WriterRepository(DbContext db) : base(db)
        {

        }
    }
}
