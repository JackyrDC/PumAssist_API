using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PumAssist_API.Controllers
{
    public class PermanentRollController1 : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();
        [HttpGet]
        [Route("api/PermanentRoll/Get")]
        public async Task<IEnumerable<Models.PermanentRoll>> Get()
        {
            return await db.PermanentRoll.ToList();
        }

        [HttpGet]
        [Route("api/PermanentRoll/Get/{id}")]
        public async Task<Models.PermanentRoll> Get(int id)
        {
            try
            {
                return await db.PermanentRoll.Find(id);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        [Route("api/PermanentRoll/Post")]
        public async Task<IHttpActionResult> Post([FromBody] Models.PermanentRoll roll)
        {
            try
            {
                db.PermanentRoll.Add(roll);
                await db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/PermanentRoll/Put")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] string value)
        {
            db.PermanentRoll.Update(id, value);
            try
            {
                await db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("/api/PermanentRoll/Delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var roll = db.PermanentRoll.Find(id);
            try
            {
                roll.IsDeleted = true;
                db.PermanentRoll.State = System.Data.Entity.State.Modified;
                await db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
            
        }
    }
}