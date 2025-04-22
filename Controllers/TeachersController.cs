using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using PumAssist_API.Models;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PumAssist_API.Controllers
{
    public class TeachersController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();
        [HttpGet]
        [Route("api/teachers/")]
        public async Task<IEnumerable<Models.Teachers>> Get()
        {
            return await db.Teachers.ToListAsync();
        }

        [HttpGet]
        [Route("api/teachers/{id}")]
        public Models.Teachers Get(int id)
        {
            return db.Teachers.Find(id);
        }

        [HttpPost]
        [Route("api/teachers/post")]
        public async Task<IHttpActionResult> Post([FromBody] Models.Teachers teacher)
        {
            try
            {
                db.Teachers.Add(teacher);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/teachers/createmultiple")]
        public async Task<IHttpActionResult> CreateMultiple([FromBody] IEnumerable<Models.Teachers> teachers)
        {
            try
            {
                db.Teachers.AddRange(teachers);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPut]
        [Route("api/teachers/put/{id}")]
        public async Task<IHttpActionResult> Put(int id)
        {
            var teacher = db.Teachers.Find(id);
            db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("api/teachers/put")]
        public async Task<IHttpActionResult> Edit(int id, [FromBody] Models.Teachers teacher)
        {
            try
            {
                db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/teachers/Delete/{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                Models.Teachers teacher = db.Teachers.Find(id);
                teacher.IsDeleted = true;
                db.Entry(teacher).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
