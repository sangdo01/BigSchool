using LabBigSchool_DoVanSang.Models;
using LabBigSchool_DoVanSang.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace LabBigSchool_DoVanSang.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dBContext;
        public CoursesController()
        {
            _dBContext = new ApplicationDbContext();
        }
        // GET: Courses
        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dBContext.Categories.ToList()
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dBContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dBContext.Courses.Add(course);
            _dBContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}