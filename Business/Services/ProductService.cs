using AppCore.Business.Models.Results;
using AppCore.Business.Models.Results.Bases;
using AppCore.Business.Services.Bases;
using Business.Models;
using Business.Services.Bases;
using DataAccess.EntityFramework.Repositories.Bases;
using System;
using System.Linq;

namespace Business.Services
{
    public class ProductService : IProductService
    {
        private readonly ProductRepositoryBase _productRepository;
        public ProductService(ProductRepositoryBase productRepository)
        {
            _productRepository = productRepository;
        }
        public Result Add(ProductModel model)
        {
            throw new System.NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Result<IQueryable<ProductModel>> GetQuery()
        {
            try
            {
                var query = _productRepository.GetEntityQuery("Category").OrderBy(p => p.Name).Select(p => new ProductModel()
                {
                    Id = p.Id,
                    Guid = p.Guid,
                    Name = p.Name,
                    UnitPrice = p.UnitPrice,
                    StockAmount = p.StockAmount,
                    ExpirationDate = p.ExpirationDate,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Category = new CategoryModel()
                    {
                        Id = p.Category.Id,
                        Guid = p.Category.Guid,
                        Name = p.Category.Name,
                        Description = p.Category.Description
                    }

                });
                return new SuccessResult<IQueryable<ProductModel>>(query);
            }
            catch (Exception exc)
            {
                return new ExceptionResult<IQueryable<ProductModel>>(exc);
            }
        }

        public Result Update(ProductModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}
