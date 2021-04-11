using AppCore.Business.Models.Results;
using AppCore.Business.Models.Results.Bases;
using Business.Models;
using Business.Services.Bases;
using DataAccess.EntityFramework.Repositories.Bases;
using Entities.Entities;
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
            try
            {
                if (_categoryRepository.GetEntityQuery().Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim()))
                    return new ErrorResult("Category with the same name exists!");
                var entity = new Category()
                {
                    Description = model.Description?.Trim(),
                    Name = model.Name.Trim()
                };
                _categoryRepository.Add(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public Result Delete(int id)
        {
            try
            {
                var category = _categoryRepository.GetEntityQuery(c => c.Id == id, "Products").SingleOrDefault();
                if (category.Products != null && category.Products.Count > 0)
                    return new ErrorResult("Category has products so it can't be deleted!");
                _categoryRepository.Delete(category);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _categoryRepository.Dispose();
        }

        public Result<IQueryable<CategoryModel>> GetQuery()
        {
            try
            {
                var query = _categoryRepository.GetEntityQuery("Products").OrderBy(c => c.Name).Select(c => new CategoryModel()
                {
                    Id = c.Id,
                    Guid = c.Guid,
                    Name = c.Name,
                    Description = c.Description,
                    ProductCount = c.Products.Count
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
            try
            {
                if (_categoryRepository.GetEntityQuery().Any(c => c.Name.ToUpper() == model.Name.ToUpper().Trim() && c.Id != model.Id))
                    return new ErrorResult("Category with the same name exists!");
                var entity = _categoryRepository.GetEntityQuery(c => c.Id == model.Id).SingleOrDefault();
                entity.Description = model.Description?.Trim();
                entity.Name = model.Name.Trim();
                _categoryRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }
    }
}