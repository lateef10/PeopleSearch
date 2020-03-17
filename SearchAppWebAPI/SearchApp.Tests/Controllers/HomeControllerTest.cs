using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SearchApp;
using SearchApp.Controllers;
using SearchApp.Models;

namespace SearchApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        PeopleDBEntities db = new PeopleDBEntities();
        HomeController contrller = new HomeController();
        [TestMethod]
        public void GetAllPeople_ShouldReturnAllPeople()
        {

            //var actual = contrller.GetPeople() as JsonResult;
            //var result = JsonHelper.GetJsonObjectRepresentation<IDictionary<string, object>>(actual);

            var people = contrller.GetPeople();
            //var contentResult = people as OkNegotiatedContentResult<person>;

            var p = db.people.ToList();


            Assert.AreEqual(p.Count, 3);
            Assert.IsNotNull(people, "There should be some data for the Json Result");
        }

        [TestMethod]
        public void SearchPerson_ShouldReturnSomePeople()
        {
            var person = contrller.SearchPeople("Lateef");
            
            Assert.IsNotNull(person, "There should be some data for the Json Result");
        }
        
        [TestMethod]
        public void DeletePerson_ShouldReturnOK()
        {
            int id = 1;
            var del = contrller.Delete(id);
            
            Assert.IsNotNull(del, "There should be a response message");
        }

    }
}
