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
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingsController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(FollowingDTO followingDTO)
        {
            var userID = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FollowerID == userID && f.FolloweeID == followingDTO.FolloweeID)) ;
            return BadRequest("The Attendance already exists!");
            var following = new Following
            {
                FollowerID = userID,
                FolloweeID = followingDTO.FolloweeID
            };
            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
