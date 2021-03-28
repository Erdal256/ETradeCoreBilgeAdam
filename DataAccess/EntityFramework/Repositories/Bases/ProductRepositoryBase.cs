using AppCore.DataAccess.Bases;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories.Bases
{
    public abstract class ProductRepositoryBase : RepositoryBase<Product>
    {
        protected ProductRepositoryBase(DbContext db) : base(db)
        {

        }
    }
}
