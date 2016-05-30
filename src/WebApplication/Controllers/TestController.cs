using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        private static ConcurrentDictionary<Guid, ApiModel> list = new ConcurrentDictionary<Guid, ApiModel>();
        private static Random rnd = new Random();
        private static bool sw = false;
        private static object loc = new object();

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
            Thread.Sleep(rnd.Next(190, 950));
            model.Id = Guid.NewGuid();
            if (!list.TryAdd(model.Id, model))
            {
                throw new Exception("Failed in Add");
            }
            return Ok(model);
        }

        [HttpGet]
        [Route("api/Get/Test")]
        public IHttpActionResult Get()
        {
            /*lock (loc)
            {
                Thread.Sleep(sw ? 600 : 1200);
                sw = !sw;
            }*/
            var model = new ApiModel();
            var str = new StringBuilder();
            var val = rnd.Next(50000, 500000);
            for (int i = 0; i < val; i++)
            {
                var x = i/val*213 ^ 7;
                str.AppendLine(@"a");
            }
            model.Value = str.ToString();
            var s = rnd.Next(600, 1800);
            Thread.Sleep(s);

            model.Id = Guid.NewGuid();
            if (!list.TryAdd(model.Id, model))
            {
                throw new Exception("Failed in Add");
            }
            if (s > 1500)
                return BadRequest();
            return Ok(model);
        }
    }
}