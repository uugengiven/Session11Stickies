using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Session11Stickies.Data;
using Session11Stickies.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Session11Stickies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StickiesDbContext db;

        public HomeController(ILogger<HomeController> logger, StickiesDbContext globalDbCopy)
        {
            _logger = logger;
            db = globalDbCopy;
        }

        public IActionResult Stickies()
        {
            List<Category> cats = db.Categories.Include(c => c.Stickies).ToList();
            return View(cats);
        }

        public IActionResult Index()
        {
            List<Category> cats = db.Categories.Include(c => c.Stickies).ToList();
            return View(cats);
        }

        public Category AddCategory(CategoryViewModel category)
        {
            // add it to the db
            Category myCategory = new Category(); // create a new, empty category
            myCategory.Name = category.CategoryName; // fill in all of the info for that category
            db.Categories.Add(myCategory); // add it to the table (create the sql command)
            db.SaveChanges(); // save the table changes (execute the sql command)
            // go back to index
            return myCategory;
        }

        public string ReorderStickies(List<int> Ids)
        {
            // right here
            // Ids holds an list of ints
            // that are in the order we want our stickies
            // to be in
            for (int i = 0; i < Ids.Count; i++)
            {
                // grab the current sticky
                // set its sort order to the current
                // place in the array
                Sticky currentSticky = db.Stickies.Find(Ids[i]);
                currentSticky.SortOrder = i;
            }
            db.SaveChanges();
            return "hello";
        }

        public IActionResult AddSticky(int categoryId, string text)
        {
            // add the sticky
            Sticky sticky = new Sticky();
            sticky.CategoryId = categoryId;
            sticky.Text = text;
            sticky.Timestamp = DateTime.Now;
            sticky.SortOrder = 0;

            db.Stickies.Add(sticky);
            db.SaveChanges();

            // go back to index
            return RedirectToAction("Index");
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

    public class CategoryViewModel
    {
        public CategoryViewModel() { }
        public string CategoryName { get; set; }
    }
}
