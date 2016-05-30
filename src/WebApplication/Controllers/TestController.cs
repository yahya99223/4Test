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
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        /*public int DataLoopCount { get; set; }
        public string Data { get; set; }*/

        public double ProcessTime
        {
            get { return (Finish - Start).TotalMilliseconds; }
        }
    }



    public class TestController : ApiController
    {
        private static ConcurrentDictionary<Guid, ApiModel> modelDictionary = new ConcurrentDictionary<Guid, ApiModel>();
        private static Random random = new Random();
        private static bool shouldBeLong = false;
        private static object lok = new object();


        [HttpGet]
        [Route("api/Get/Test")]
        public IHttpActionResult Get()
        {
            var startDate = DateTime.UtcNow;
            var stringBuilder = new StringBuilder();
            var loopCount = 0;
            int intervalTime;
            lock (lok)
            {
                intervalTime = shouldBeLong ? 1200 : 200;
                shouldBeLong = !shouldBeLong;
            }
            while (startDate.AddMilliseconds(intervalTime) > DateTime.UtcNow)
            {
                /*loopCount = loopCount + 1;
                stringBuilder.Append("a");*/
            }
            var result = new ApiModel
            {
                Id = Guid.NewGuid(),
                Start = startDate,
                Finish = DateTime.UtcNow,
                /*Data = stringBuilder.ToString(),
                DataLoopCount = loopCount,*/
            };
            return Ok(result);
            //return Ok(string.Format("Start At : {0} - Done at {1}",startDate.ToString("O"), DateTime.UtcNow.ToString("O")));
        }
    }
}