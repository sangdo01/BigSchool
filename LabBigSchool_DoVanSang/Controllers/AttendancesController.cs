using LabBigSchool_DoVanSang.DTOs;
using LabBigSchool_DoVanSang.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LabBigSchool_DoVanSang.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend (AttendanceDTO attendanceDTO)
        {
            var userID = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeID == userID && a.CouresID == attendanceDTO.CourseID)) ;
            return BadRequest("The Attendance already exists!");
            var attendance = new Attendance
            {
                CouresID = attendanceDTO.CourseID,
                AttendeeID = userID
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
