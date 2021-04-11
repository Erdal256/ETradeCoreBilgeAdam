using AppCore.Business.Models.Results;
using AppCore.Business.Models.Results.Bases;
using AppCore.Business.Services.Bases;
using Business.Models;
using Business.Services.Bases;
using DataAccess.EntityFramework.Repositories.Bases;
using Entities.Entities;
using System;
using System.Globalization;
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
            try
            {
                //var product = _productRepository.GetEntityQuery().SingleOrDefault(p => p.Name.ToUpper() == model.Name.ToUpper().Trim());
                //var product = _productRepository.GetEntityQuery(p => p.Name.ToUpper() == model.Name.ToUpper().Trim()).SingleOrDefault();
                //if (product != null)
                //    return new ErrorResult("Product with the same name exists!");

                if (_productRepository.GetEntityQuery().Any(p => p.Name.ToUpper() == model.Name.ToUpper().Trim()))
                    return new ErrorResult("Product with the same name exists!");

                double unitPrice;
                //unitPrice = Convert.ToDouble(model.UnitPriceText.Trim().Replace(",", "."), new CultureInfo("en"));
                //unitPrice = Convert.ToDouble(model.UnitPriceText.Trim().Replace(",", "."), CultureInfo.InvariantCulture);
                //if (!double.TryParse(model.UnitPriceText.Trim().Replace(",", "."), NumberStyles.Any, new CultureInfo("en"), out unitPrice))
                if (!double.TryParse(model.UnitPriceText.Trim().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out unitPrice))
                    return new ErrorResult("Unit price must be a decimal number!");

                model.UnitPrice = unitPrice;
                model.ExpirationDate = null;
                if (!string.IsNullOrWhiteSpace(model.ExpirationDateText))
                    model.ExpirationDate = DateTime.Parse(model.ExpirationDateText, new CultureInfo("en"));
                var entity = new Product()
                {
                    CategoryId = model.CategoryId,

                    //Description = model.Description == null ? null : model.Description.Trim(),
                    Description = model.Description?.Trim(),

                    ExpirationDate = model.ExpirationDate,
                    Name = model.Name.Trim(),
                    StockAmount = model.StockAmount,
                    UnitPrice = model.UnitPrice
                };
                _productRepository.Add(entity);
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
                _productRepository.Delete(id);
                return new SuccessResult();
            }
            catch (Exception exc)
            {

                return new ExceptionResult(exc);
            }
        }

        public void Dispose()
        {
            _productRepository.Dispose();
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
                    Description = p.Description,
                    UnitPrice = p.UnitPrice,
                    UnitPriceText = p.UnitPrice.ToString(new CultureInfo("en")),
                    StockAmount = p.StockAmount,
                    ExpirationDate = p.ExpirationDate,
                    ExpirationDateText = p.ExpirationDate.HasValue ? p.ExpirationDate.Value.ToString("MM/dd/yyyy", new CultureInfo("en")) : "",
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
            try
            {
                //var product = _productRepository.GetEntityQuery().SingleOrDefault(p => p.Name.ToUpper() == model.Name.ToUpper().Trim() && p.Id != model.Id);
                //var product = _productRepository.GetEntityQuery(p => p.Name.ToUpper() == model.Name.ToUpper().Trim() && p.Id != model.Id).SingleOrDefault();
                //if (product != null)
                //    return new ErrorResult("Product with the same name exists!");

                if (_productRepository.GetEntityQuery().Any(p => p.Name.ToUpper() == model.Name.ToUpper().Trim() && p.Id != model.Id))
                    return new ErrorResult("Product with the same name exists!");

                double unitPrice;
                //unitPrice = Convert.ToDouble(model.UnitPriceText.Trim().Replace(",", "."), new CultureInfo("en"));
                //unitPrice = Convert.ToDouble(model.UnitPriceText.Trim().Replace(",", "."), CultureInfo.InvariantCulture);
                //if (!double.TryParse(model.UnitPriceText.Trim().Replace(",", "."), NumberStyles.Any, new CultureInfo("en"), out unitPrice))
                if (!double.TryParse(model.UnitPriceText.Trim().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out unitPrice))
                    return new ErrorResult("Unit price must be a decimal number!");

                model.UnitPrice = unitPrice;
                model.ExpirationDate = null;
                if (!string.IsNullOrWhiteSpace(model.ExpirationDateText))
                    model.ExpirationDate = DateTime.Parse(model.ExpirationDateText, new CultureInfo("en"));
                var entity = _productRepository.GetEntityQuery(p => p.Id == model.Id).SingleOrDefault();

                entity.CategoryId = model.CategoryId;
                //entity.//Description = model.Description == null ? null : model.Description.Trim(),
                entity.Description = model.Description?.Trim();
                entity.ExpirationDate = model.ExpirationDate;
                entity.Name = model.Name.Trim();
                entity.StockAmount = model.StockAmount;
                entity.UnitPrice = model.UnitPrice;
                _productRepository.Update(entity);
                return new SuccessResult();
            }
            catch (Exception exc)
            {
                return new ExceptionResult(exc);
            }
        }
    }
}
