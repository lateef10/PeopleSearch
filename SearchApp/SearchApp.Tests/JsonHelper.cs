using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Rhino.Mocks;
using System.Web.Http.Results;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SearchApp.Tests
{
    public class JsonHelper
    {
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }
        public static T GetJsonObjectRepresentation<T>(JsonResult jsonResult)
        {
            var controllerContextMock = MockRepository.GenerateStub<ControllerContext>();
            var httpContextMock = MockRepository.GenerateStub<HttpContextBase>();
            var httpResponseMock = MockRepository.GenerateStub<HttpResponseBase>();

            httpContextMock.Stub(x => x.Response).Return(httpResponseMock);
            controllerContextMock.HttpContext = httpContextMock;

            jsonResult.ExecuteResult(controllerContextMock);

            var args = httpResponseMock.GetArgumentsForCallsMadeOn(x => x.Write(null));

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(args[0][0] as string);
        }
    }
}
