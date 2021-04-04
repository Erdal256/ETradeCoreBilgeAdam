using Core.DataAccess.EntityFramework.Bases;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework.Repositories.Bases
{
    public abstract class WriterRepositoryBase : RepositoryBase<Writer>
    {
        protected WriterRepositoryBase(DbContext db) : base(db)
        {

        }
    }
}
