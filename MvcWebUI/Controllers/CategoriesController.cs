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
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public IActionResult Index()
        {
            var result = _categoryService.GetQuery();
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            var model = result.Data.ToList();
            return View(model);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            var model = new CategoryModel();
            return View(model);
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Add(category);
                if (result.Status == ResultStatus.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(category);
                }
                throw new Exception(result.Message);
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View("NotFound");
            }

            var result = _categoryService.GetQuery();
            if (result.Status == ResultStatus.Exception)
                throw new Exception(result.Message);
            var category = result.Data.SingleOrDefault(c => c.Id == id.Value);

            if (category == null)
            {
                return View("NotFound");
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.Update(category);
                if (result.Status == ResultStatus.Success)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (result.Status == ResultStatus.Error)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(category);
                }

                throw new Exception(result.Message);
            }
            return View(category);
        }

        // POST: Categories/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var deleteResult = _categoryService.Delete(id);
            if (deleteResult.Status == ResultStatus.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            if (deleteResult.Status == ResultStatus.Error)
            {
                ModelState.AddModelError("", deleteResult.Message);
                var getResult = _categoryService.GetQuery();
                if (getResult.Status == ResultStatus.Success)
                {
                    var category = getResult.Data.SingleOrDefault(c => c.Id == id);
                    return View("Edit", category);
                }
                throw new Exception(getResult.Message);
            }
            throw new Exception(deleteResult.Message);
        }
    }
}