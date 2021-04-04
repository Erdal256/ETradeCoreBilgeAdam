using Core.DataAccess.EntityFramework.Bases;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories.Bases
{
    public abstract class BookRepositoryBase : RepositoryBase<Book>
    {
        protected BookRepositoryBase(DbContext db) : base(db)
        {

        }
    }
}
