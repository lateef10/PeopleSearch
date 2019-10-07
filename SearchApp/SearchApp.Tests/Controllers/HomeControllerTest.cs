using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SearchApp;
using SearchApp.Controllers;

namespace SearchApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void GetAllPeople_ShouldReturnAllPeople()
        {
            HomeController contrller = new HomeController();

            JsonResult actual = contrller.GetPeople() as JsonResult;
            var result = JsonHelper.GetJsonObjectRepresentation<IDictionary<string, object>>(actual);

            Assert.AreEqual("test", result);
        }


        
    }
}
