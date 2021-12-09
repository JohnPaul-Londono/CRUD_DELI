using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUD_DELI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_DELI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {

            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
            return View();
        }

        [HttpGet("goAdd")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("addDish")]
        public IActionResult addDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Add");
            }
        }

        [HttpGet("viewDish/{dId}")]
        public IActionResult oneDish(int dId)
        {
            Dish one = _context.Dishes.FirstOrDefault(d => d.DishId == dId);
            return View(one);
        }

        [HttpGet("delete/{dId}")]
        public IActionResult DeleteDish(int dId)
        {
            Dish toDelete = _context.Dishes.SingleOrDefault(d => d.DishId == dId);
            _context.Dishes.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet("editDish/{dId}")]
        public IActionResult Edit(int dId)
        {
            Dish oneDish = _context.Dishes.FirstOrDefault(d => d.DishId == dId);
            return View(oneDish);
        }

        [HttpPost("updateDish/{dId}")]
        public IActionResult Update(int dId, Dish edited)
        {
            edited.DishId =  dId;
            if (ModelState.IsValid)
            {
                
                Dish original = _context.Dishes.FirstOrDefault(d => d.DishId == dId);
                original.Name = edited.Name;
                original.Chef = edited.Chef;
                original.Tastiness = edited.Tastiness;
                original.Calories = edited.Calories;
                original.Description = edited.Description;
                original.UpdatedAt = DateTime.Now;
                _context.SaveChanges();
                return View("oneDish",original);
            } else{
                return View("Edit", edited);
            } 

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
