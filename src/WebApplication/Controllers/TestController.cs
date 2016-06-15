using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http;
using WebApiPerformanceFilter;

namespace WebApplication.Controllers
{
    public class ApiTimeModel
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


    public class ApiFakeModel
    {
        public ApiFakeModel()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }



    public class TestController : ApiController
    {
        private static ConcurrentDictionary<Guid, ApiTimeModel> modelDictionary = new ConcurrentDictionary<Guid, ApiTimeModel>();
        private static Random random = new Random();
        private static bool shouldBeLong = false;
        private static object lok = new object();



        [HttpPost]
        [Route("api/Add/Test")]
        public IHttpActionResult Add(ApiFakeModel model)
        {
            return Ok(model);
        }

        [HttpGet]
        [WebActionPerformanceFilter]
        [Route("api/Get/Test")]
        public IHttpActionResult Get()
        {
            var startDate = DateTime.UtcNow;
            /*var stringBuilder = new StringBuilder();
            var loopCount = 0;
            int intervalTime;
            lock (lok)
            {
                intervalTime = shouldBeLong ? 1200 : 200;
                shouldBeLong = !shouldBeLong;
            }
            /*while (startDate.AddMilliseconds(intervalTime) > DateTime.UtcNow)
            {
                /*loopCount = loopCount + 1;
                stringBuilder.Append("a");
            }#1#
            Thread.Sleep(intervalTime);*/
            var data = new List<byte[]>();
            Random r = new Random();
            //for (int x = 0; x < 1024; x++)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 1024; j++)
                    {
                        var temp = new byte[1024*1024];
                        r.NextBytes(temp);
                        data.Add(temp);
                    }
                }
            }

            var result = new ApiTimeModel {Id = Guid.NewGuid(), Start = startDate, Finish = DateTime.UtcNow, /*Data = stringBuilder.ToString(),
                DataLoopCount = loopCount,*/};
            return Ok(result);
            //return Ok(string.Format("Start At : {0} - Done at {1}",startDate.ToString("O"), DateTime.UtcNow.ToString("O")));
        }
    }
    
}