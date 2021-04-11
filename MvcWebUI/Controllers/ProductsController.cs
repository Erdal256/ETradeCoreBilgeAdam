using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.EntityFramework.Contexts;
using Entities.Entities;
using Business.Services.Bases;
using AppCore.Business.Models.Results;
using Business.Models;

namespace MvcWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Products
        public IActionResult Index()
        {

            var result = _productService.GetQuery();
            if (result.Status == ResultStatus.Exception)
            {
                throw new Exception(result.Message);
            }
            var model = result.Data.ToList();
            return View(model);
        }
        //Index aksiyonunun hata aldığı davranışını görebilmek için:
        //throw new Exception("Test Exception!");

        // GET: Products/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                //return NotFound();
                return View("NotFound");
            }

            var result = _productService.GetQuery();
            if (result.Status == ResultStatus.Exception)
            {
                throw new Exception(result.Message);
            }

            var model = result.Data.SingleOrDefault(p => p.Id == id.Value);

            if (result.Data == null)
            {
                return View("NotFound");
            }
            return View(model);

        }

        // GET: Products/Create
        public ActionResult Create()
        {
            var result = _categoryService.GetQuery();
            if (result.Status == ResultStatus.Exception)
            {
                throw new Exception(result.Message);
            }
            ViewBag.Categories = new SelectList(result.Data.ToList(), "Id", "Name");
            return View();
        }

        // POST: Products/Create
        public ActionResult Create(ProductModel product)
        {
            Result productResult;
            Result<IQueryable<CategoryModel>> categoryResult;

            if (ModelState.IsValid)
            {
                productResult = _productService.Add(product);
                if (productResult.Status == ResultStatus.Exception)
                {
                    throw new Exception(productResult.Message);
                }
                if (productResult.Status == ResultStatus.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                categoryResult = _categoryService.GetQuery();
                if (categoryResult.Status == ResultStatus.Exception)
                    throw new Exception(categoryResult.Message);
                ViewBag.Categories = new SelectList(categoryResult.Data.ToList(), "Id", "Name", product.CategoryId);
                return View(product);
            }
            // ViewBag.Message = productResult.Message;
            categoryResult = _categoryService.GetQuery();
            if (categoryResult.Status == ResultStatus.Exception)
                throw new Exception(categoryResult.Message);
            ViewBag.Categories = new SelectList(categoryResult.Data.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }
        ////GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return View("NotFound");
            var productResult = _productService.GetQuery();
            if (productResult.Status == ResultStatus.Exception)
                throw new Exception(productResult.Message);
            var product = productResult.Data.SingleOrDefault(p => p.Id == id.Value);
            if (product == null)
                return View("NoFound");
            var categoryResult = _categoryService.GetQuery();
            if (categoryResult.Status == ResultStatus.Exception)
            {
                throw new Exception(productResult.Message);
            }
            ViewBag.Categories = new SelectList(categoryResult.Data.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }
        public ActionResult Edit(ProductModel product)
        {
            Result productResult;
            Result<IQueryable<CategoryModel>> categoryResult;

            if (ModelState.IsValid)
            {
                productResult = _productService.Update(product);
                if (productResult.Status == ResultStatus.Exception)
                {
                    throw new Exception(productResult.Message);
                }
                if (productResult.Status == ResultStatus.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                categoryResult = _categoryService.GetQuery();
                if (categoryResult.Status == ResultStatus.Exception)
                    throw new Exception(categoryResult.Message);
                ViewBag.Categories = new SelectList(categoryResult.Data.ToList(), "Id", "Name", product.CategoryId);
                return View(product);
            }
            // ViewBag.Message = productResult.Message;
            categoryResult = _categoryService.GetQuery();
            if (categoryResult.Status == ResultStatus.Exception)
                throw new Exception(categoryResult.Message);
            ViewBag.Categories = new SelectList(categoryResult.Data.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (!id.HasValue)
                return View("NotFound");
            var result = _productService.Delete(id.Value);
            if (result.Status == ResultStatus.Success)
                return RedirectToAction(nameof(Index));
            throw new Exception(result.Message);
        }
    }
}
