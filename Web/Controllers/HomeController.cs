using Core.EntityBase;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<owner> _owner;
        private readonly IUnitOfWork<ProtiflioItem>_portflio;

        public HomeController(IUnitOfWork<owner> Owner,IUnitOfWork<ProtiflioItem> Portflio)
        {
           _owner = Owner;
            _portflio = Portflio;
        }
        public IActionResult Index()
        {
            var homeviewmodel = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                portfolio = _portflio.Entity.GetAll().ToList()
            };
            return View(homeviewmodel);
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
