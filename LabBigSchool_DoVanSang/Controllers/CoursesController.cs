using LabBigSchool_DoVanSang.Models;
using LabBigSchool_DoVanSang.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        [Authorize]
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
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dBContext.Attendances
                .Where(a => a.AttendeeID == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();
            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dBContext.Courses
                .Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now)
                .Include(l => l.Lecturer)
                .Include(c => c.Category)
                .ToList();
            return View(courses);
        }
        [Authorize]
        //public ActionResult Edit(int id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    var course = _dBContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);

        //    var viewModel = new CourseViewModel
        //    {
        //        Categories = _dBContext.Categories.ToList(),
        //        Date = course.DateTime.ToString("dd/M/yyyy"),
        //        Time = course.DateTime.ToString("HH:mm"),
        //        Category = course.CategoryId,
        //        Place = course.Place,
        //        Heading = "Edit Course",
        //        Id = course.Id
        //    };
        //    return View("CourseForm", viewModel);
        //}
    }
}