using AppCore.Business.Models.Results;
using AppCore.Business.Models.Results.Bases;
using Business.Models;
using Business.Services.Bases;
using DataAccess.EntityFramework.Repositories.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepositoryBase _categoryRepository;
        public CategoryService(CategoryRepositoryBase categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Result Add(CategoryModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            return null;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public Result<IQueryable<CategoryModel>> GetQuery()
        {
            try
            {
                var query = _categoryRepository.GetEntityQuery().OrderBy(c => c.Name).Select(c => new CategoryModel()
                {
                    Id = c.Id,
                    Guid = c.Guid,
                    Name = c.Name,
                    Description = c.Description
                });
                return new SuccessResult<IQueryable<CategoryModel>>(query);
            }
            catch (Exception exc)
            {

                return new ExceptionResult<IQueryable<CategoryModel>>(exc);
            }
        }

        public Result Update(CategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}
