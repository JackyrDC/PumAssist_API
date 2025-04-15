using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PumAssist_API.Controllers
{
    public class CampusController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();
        [HttpGet]
        public async Task<IEnumerable<Models.Campus>> Get()
        {
            return await db.Campus.ToList();
        }
        [HttpGet]
        public async Task<Models.Campus> Get(int id)
        {
            return await db.Campus.Find(id);
        }
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Models.Campus campus)
        {
            db.Campus.Add(campus);
            await db.SaveChanges();
            return Ok();
        }
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody]Models.Campus campus)
        {
            db.Entry(campus).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChanges();
            return Ok();
        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                Models.Campus campus = db.Campus.Find(id);
                campus.IsDeleted = true;
                db.Entry(campus).State = System.Data.Entity.EntityState.Modified;
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
