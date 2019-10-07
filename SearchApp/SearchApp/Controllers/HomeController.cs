using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SearchApp.Models;

namespace SearchApp.Controllers
{
    [AllowAnonymous]
    public class HomeController : ApiController
    {
        PeopleDBEntities db = new PeopleDBEntities();

        [HttpGet]
        public IHttpActionResult SearchPeople(string search)
        {
            System.Threading.Thread.Sleep(2000);
            var model = db.people.Where(x => x.FirstName.ToLower().Contains(search.ToLower()) || x.LastName.ToLower().Contains(search.ToLower())).ToList();
            return Json(model);
        }
        
        [HttpGet]
        public IHttpActionResult GetPeople()
        {
            try
            {
                var data = db.people.ToList();
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(new { StatusCode = ex.Message });
            }
        }
        
        [HttpPost]
        public IHttpActionResult Post()
        {
            var httpRequest = HttpContext.Current.Request;
            string imageName = null;
            string FirstNameeeee = httpRequest["FirstName"];

            //upload image
            var postedFile = httpRequest.Files["Picture"];
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //create custom filename
                imageName = new string(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath = HttpContext.Current.Server.MapPath("~/Upload/" + imageName);
                postedFile.SaveAs(filePath);
            }
            else
            {
                return Json(new { StatusCode = "Error occured!" });
            }

            try
            {
                var pu = new person
                {
                    FirstName = httpRequest["FirstName"],
                    LastName = httpRequest["LastName"],
                    Address = httpRequest["Address"],
                    Age = Convert.ToInt16(httpRequest["Age"]),
                    Interests = httpRequest["Interests"],
                    Picture = imageName
                };
                db.people.Add(pu);
                db.SaveChanges();
                return Json(new { StatusCode = "Added Successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
        
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]person value)
        {
            if (value == null || value.ID != id)
            {
                return BadRequest();
            }
            try
            {
                var data = db.people.SingleOrDefault(x => x.ID == id);
                if (data != null)
                {
                    data.FirstName = value.FirstName;
                    data.LastName = value.LastName;
                    data.Address = value.Address;
                    data.Age = value.Age;
                    data.Interests = value.Interests;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
            return Json(new { StatusCode = "Update Successful" });
        }
        
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var d = db.people.FirstOrDefault(t => t.ID == id);
            if (d == null)
            {
                return NotFound();
            }
            db.people.Remove(d);
            db.SaveChanges();
            return Json(new { StatusCode = "User deleted Successfully!" });
        }
    }
}
