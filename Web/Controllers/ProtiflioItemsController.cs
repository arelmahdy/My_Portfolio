using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.EntityBase;
using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;
using Web.ViewModels;
using System.IO;

namespace Web.Controllers
{
    public class ProtiflioItemsController : Controller
    {
        public IUnitOfWork<ProtiflioItem> _Portfolio { get; }
        public IWebHostEnvironment _Host { get; }

        public ProtiflioItemsController(IUnitOfWork<ProtiflioItem> portfolio, IWebHostEnvironment host)
        {
            _Portfolio = portfolio;
            _Host = host;
        }

        // GET: ProtiflioItems
        public IActionResult Index()
        {
            return View(_Portfolio.Entity.GetAll());
        }

        // GET: ProtiflioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protiflioItem = _Portfolio.Entity.GetByID(id);

            if (protiflioItem == null)
            {
                return NotFound();
            }

            return View(protiflioItem);
        }

        // GET: ProtiflioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProtiflioItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string upload = Path.Combine(_Host.WebRootPath, @"img\portfolio");
                    string fullpath = Path.Combine(upload, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                ProtiflioItem portflioitem = new ProtiflioItem
                {
                    ProjectName = model.ProjectName,
                    ImageUrl = model.ImageUrl,
                    Description = model.Description
                };
                _Portfolio.Entity.Insert(portflioitem);
                _Portfolio.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ProtiflioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protiflioItem = _Portfolio.Entity.GetByID(id);
            if (protiflioItem == null)
            {
                return NotFound();
            }
            PortfolioViewModel portfolioViewModel = new PortfolioViewModel
            {
                ID = protiflioItem.ID,
                Description = protiflioItem.Description,
                ImageUrl = protiflioItem.ImageUrl,
                ProjectName = protiflioItem.ProjectName
            };

            return View(portfolioViewModel);

        }

        // POST: ProtiflioItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfolioViewModel model)
        {
            if (id != model.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null || model.File.Length > 0)
                    {
                        string uploads = Path.Combine(_Host.WebRootPath, @"img\portfolio");
                        string fullPath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }

                    ProtiflioItem portfolioItem = new ProtiflioItem
                    {
                        ID  = model.ID,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.File.FileName
                    };

                    _Portfolio.Entity.Update(portfolioItem);
                    _Portfolio.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtiflioItemExists(model.ID))
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

        // GET: ProtiflioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioItem = _Portfolio.Entity.GetByID(id);
            if (portfolioItem == null)
            {
                return NotFound();
            }

            return View(portfolioItem);
        }

        // POST: ProtiflioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _Portfolio.Entity.Delete(id);
            _Portfolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ProtiflioItemExists(Guid id)
        {
            return _Portfolio.Entity.GetAll().Any(e => e.ID == id);
        }
    }
}
