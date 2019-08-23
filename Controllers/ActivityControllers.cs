using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using belt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http; 



namespace belt.Controllers
{
    [Route("")]
    // localhost:5000/Activity
    public class ActivityController : Controller
    {
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        

        
        private MyContext dbContext;
        public ActivityController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {

            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var x = dbContext.Activities
                .Include(p => p.Creator)
                .Include(p => p.Participants)
                // ThenInclude() to get user name from user_id
                    .ThenInclude(v => v.ppUsers)
                .OrderBy (f=>f.Date).ToList() ;
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");

            User UC = dbContext.Users.FirstOrDefault(a => a.UserId == (int)HttpContext.Session.GetInt32("UserId"));
            ViewBag.UserInfo = UC;

            return View(x);

        }







        [HttpGet("new")]
        public IActionResult New()
        {

            if(HttpContext.Session.GetInt32("UserId") == null)
                {
                    return RedirectToAction("Index", "Home");
                }
            var users = dbContext.Users.ToList();
            ViewBag.Users = users;
            return View();
            }






        [HttpPost("create")]
        public IActionResult CreatePost(Act newAct)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            if(ModelState.IsValid)
            {
                newAct.UserId = (int)HttpContext.Session.GetInt32("UserId");
                dbContext.Activities.Add(newAct);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
            return View("New");
        }





        [HttpGet("show/{ActId}")]
        public IActionResult Show(int ActId)
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var x = dbContext.Activities
                .Include(p => p.Creator)
                .Include(p => p.Participants)
                // ThenInclude() to get user name from user_id
                    .ThenInclude(v => v.ppUsers)
                .FirstOrDefault(u => u.ActivityId == ActId);
            ViewBag.UserId = HttpContext.Session.GetInt32("UserId");


            return View(x);
            
            // ViewBag.UserId = HttpContext.Session.GetInt32("UserId");
         
            // return View();
        }




 


 






        [HttpGet("delete/{ActId}")]
        public IActionResult Delete(int ActId)
        {

            // query for thing
            Act toDel = dbContext.Activities.FirstOrDefault(v => v.ActivityId == ActId && v.UserId == (int)SessionUser);
            dbContext.Activities.Remove(toDel);
            dbContext.SaveChanges();
            return RedirectToAction("dashboard", "Activity");
        }





        public void removeExpired()
        {
            var allActivities = dbContext.Activities.ToList();
            foreach(var activity in allActivities)
            {
                var timeNdate = activity.Date;
                if(timeNdate <DateTime.Now)
                {
                    dbContext.Remove(activity);
                    dbContext.SaveChanges();
                }
            }
        }





    }
}