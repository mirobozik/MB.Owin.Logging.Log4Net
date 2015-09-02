using System.Collections.Generic;
using System.Web.Http;

namespace MB.Owin.Logging.Log4Net.Demo
{
    [RoutePrefix("api/values")]
    public class ValuesController : ApiController
    {             

        // GET api/<controller>
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [Route("{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}