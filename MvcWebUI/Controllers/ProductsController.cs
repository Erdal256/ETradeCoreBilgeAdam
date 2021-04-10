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
        private readonly ETradeContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        //public ProductsController(ETradeContext context)
        //{
        //    _context = context;
        //}
        public ProductsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var eTradeContext = _context.Products.Include(p => p.Category);
        //    return View(await eTradeContext.ToListAsync());
        //}
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
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products
        //        .Include(p => p.Category)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}
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
        //public IActionResult Create()
        //{
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
        //    return View();
        //}
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Name,Description,UnitPrice,StockAmount,ExpirationDate,CategoryId,Id,Guid")] Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        //    return View(product);
        //}
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
        // GET: Products/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
        //    return View(product);
        //}
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return View("NotFound");
            var productResult = _productService.GetQuery();
            if(productResult.Status == ResultStatus.Exception)
                throw new Exception(productResult.Message);
            var product = productResult.Data.SingleOrDefault(p => p.Id == id.Value);
            if (product == null)
                return View("NoFound");
            var categoryResult = _categoryService.GetQuery();
            if(categoryResult.Status == ResultStatus.Exception)
            {
                throw new Exception(productResult.Message);
            }
            ViewBag.Categories = new SelectList(categoryResult.Data.ToList(), "Id", "Name", product.CategoryId);
            return View(product);
        }



        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,UnitPrice,StockAmount,ExpirationDate,CategoryId,Id,Guid")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
