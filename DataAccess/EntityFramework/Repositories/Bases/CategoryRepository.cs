﻿using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework.Repositories.Bases
{
    public class CategoryRepository : CategoryRepositoryBase
    {
        public CategoryRepository(DbContext db) : base(db)
        {

        }
    }
}
