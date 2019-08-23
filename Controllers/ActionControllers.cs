using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using belt.Models;
using Microsoft.AspNetCore.Http;

namespace wedding.Controllers
{
    [Route("action")]
    public class ActionController : Controller
    {
        private int? SessionUser
        {
            get { return HttpContext.Session.GetInt32("UserId"); }
            set { HttpContext.Session.SetInt32("UserId", (int)value); }
        }
        MyContext dbContext;
        public ActionController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("{ActId}/Action/{z}")]
        public IActionResult Action(int ActId, bool z)
        {
            
            Join newGuy = new Join()
            {
                ActivityId = ActId,
                join = z,
                UserId = (int)SessionUser
            };

            dbContext.Participants.Add(newGuy);
            dbContext.SaveChanges();
            return RedirectToAction("dashboard", "Activity");
        }

        [HttpGet("delete/{ActId}")]
        public IActionResult Remove(int ActId)
        {
            // query for thing
            Join toDel = dbContext.Participants.FirstOrDefault(v => v.ActivityId == ActId && v.UserId == (int)SessionUser);
            dbContext.Participants.Remove(toDel);
            dbContext.SaveChanges();
            return RedirectToAction("dashboard", "Activity");
        }
        






    }
}