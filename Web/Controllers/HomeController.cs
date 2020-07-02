using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _Owner;
        private readonly IUnitOfWork<PortofolioItem> _Portofolio;

        public HomeController(IUnitOfWork<Owner> Owner ,IUnitOfWork<PortofolioItem> Portofolio)
        {
            _Owner = Owner;
            _Portofolio = Portofolio;
        }
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel()
            {
                Owner = _Owner.Entity.GetAll().First(),
                PortofolioItems = _Portofolio.Entity.GetAll().ToList()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
