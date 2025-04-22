using PumAssist_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
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
            return await db.PermanentRolls.ToListAsync();
        }

        [HttpGet]
        [Route("api/PermanentRoll/Get/{id}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            try
            {
                var roll = await db.PermanentRolls.FindAsync(id);
                if (roll == null)
                {
                    return NotFound();
                }
                return Ok(roll);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("api/PermanentRoll/Post")]
        public async Task<IHttpActionResult> Post([FromBody] Models.PermanentRoll roll)
        {
            try
            {
                db.PermanentRolls.Add(roll);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/PermanentRoll/Put")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] Models.PermanentRoll roll)
        {
            var existingRoll = await db.PermanentRolls.FindAsync(id);
            if (existingRoll == null)
            {
                return NotFound();
            }

            existingRoll.idDailyRoll = roll.idDailyRoll;
            existingRoll.idStudent = roll.idStudent;
            existingRoll.rollState = roll.rollState;
            existingRoll.IsDeleted = roll.IsDeleted;

            try
            {
                await db.SaveChangesAsync();
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
            var roll = await db.PermanentRolls.FindAsync(id);
            try
            {
                if (roll == null)
                {
                    return NotFound();
                }
                roll.IsDeleted = true;
                db.Entry(roll).State = EntityState.Modified;
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