using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Web.ViewModels;
using Core.Interfaces;

namespace Web.Controllers
{
    public class PortofolioItemsController : Controller
    {
        private readonly DataContext _context;
        private readonly IUnitOfWork<PortofolioItem> _portfolio;
        private readonly IHostingEnvironment _hosting;

        public PortofolioItemsController(DataContext context , IHostingEnvironment hosting , IUnitOfWork<PortofolioItem> portfolio)
        {
            _context = context;
            _hosting = hosting;
            _portfolio = portfolio;
        }

        // GET: PortofolioItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortofolioItems.ToListAsync());
        }

        // GET: PortofolioItems/Details/5
      

        // GET: PortofolioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortofolioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProjectName,Description,ImageURL,ID,File")] PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullPath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                PortofolioItem portfolioItem = new PortofolioItem
                {
                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageURL = model.File.FileName
                };
                _portfolio.Entity.Insert(portfolioItem);
                _portfolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortofolioItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetByID(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                ID = portfolioItem.ID,
                Description = portfolioItem.Description,
                ImageURL = portfolioItem.ImageURL,
                ProjectName = portfolioItem.ProjectName
            };

            return View(portfolioViewModel);
        }

        // POST: PortofolioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProjectName,Description,ImageURL,ID,File")] PortfolioViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    PortofolioItem portfolioItem = new PortofolioItem
                    {
                        ID = model.ID,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageURL = model.File.FileName
                    };

                    _portfolio.Entity.Update(portfolioItem);
                    _portfolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioItemExists(model.ID))
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
            return View(model);
        }
        private bool PortfolioItemExists(Guid id)
        {
            return _portfolio.Entity.GetAll().Any(e => e.ID == id);
        }
        // GET: PortofolioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _portfolio.Entity.GetByID(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: PortfolioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfolio.Entity.Delete(id);
            _portfolio.Save();
            return RedirectToAction(nameof(Index));
        }


    }
}
