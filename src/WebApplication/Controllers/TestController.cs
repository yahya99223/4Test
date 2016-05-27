using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace WebApplication.Controllers
{
    public class ApiModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }


    public class TestController : ApiController
    {
        private static ConcurrentDictionary<Guid,ApiModel> list = new ConcurrentDictionary<Guid, ApiModel>();
        private static Random rnd = new Random();
        /*[HttpGet]
        [Route("api/Test/{id}")]
        public IHttpActionResult GetById(Guid id)
        {
            var model = list.FirstOrDefault(x => x.Id == id);
            return Ok(model);
        }*/

        [HttpPost]
        [Route("api/Test")]
        public IHttpActionResult Add(ApiModel model)
        {
            var val = rnd.Next(2000000, 200000000);
            for (int i = 0; i < val; i++)
            {
                var x = i/val; //* 213 ^ 12312;
            }
            Thread.Sleep(rnd.Next(90, 250));
            model.Id = Guid.NewGuid();
            if (!list.TryAdd(model.Id, model))
            {
                throw new Exception("Failed in Add");
            }
            return Ok(model);
        }
    }
}
