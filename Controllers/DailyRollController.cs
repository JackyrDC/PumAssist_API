using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PumAssist_API.Models;



namespace PumAssist_API.Controllers
{
    public class DailyRollController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();
        [Route("api/daily/getdaily")]
        [HttpGet]
        public async Task<IEnumerable<Models.DailyRoll>> Get()
        {
            return await db.DailyRolls.ToListAsync();
        }

        [HttpGet]
        [Route("api/daily/getdaily/{id}")]
        public async Task<Models.DailyRoll> Get(int id)
        {
            return await db.DailyRolls.FindAsync(id);
        }

        [HttpPost]
        [Route("api/daily/postdaily")]
        public async Task<IHttpActionResult> Create()
        {
            try
            {
                Models.DailyRoll dailyRoll = new Models.DailyRoll();
                dailyRoll.creationDate = DateTime.Now;
                db.DailyRolls.Add(dailyRoll);
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/daily/postmanydaily/")]
        public async Task<IHttpActionResult> CreateMany(IEnumerable<Models.DailyRoll> collection)
        {
            try
            {
                foreach (var dailyRoll in collection)
                {
                    dailyRoll.creationDate = DateTime.Now;
                    db.DailyRolls.Add(dailyRoll);
                }
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("api/daily/putdaily/{id}")]
        public async Task<IHttpActionResult> Edit(int id)
        {
            try
            {
                Models.DailyRoll dailyRoll = await db.DailyRolls.FindAsync(id);
                dailyRoll.creationDate = DateTime.Now;
                db.Entry(dailyRoll).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("api/daily/deletedaily/{id}")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                Models.DailyRoll dailyRoll = await db.DailyRolls.FindAsync(id);
                dailyRoll.IsDeleted = true;
                db.Entry(dailyRoll).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
