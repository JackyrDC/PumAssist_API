using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PumAssist_API.Models;



namespace PumAssist_API.Controllers
{
    public class DailyRollController : ApiController
    {
        private MyAppDbContext db = new MyAppDbContext();
        [Route("/daily/getdaily")]
        [HttpGet]
        public async Task<IEnumerable<Models.DailyRoll>> Get()
        {
            return await db.DailyRolls.ToList();
        }

        [HttpGet]
        [Route("/daily/getdaily/{id}")]
        public async Task<Models.DailyRoll> Get(int id)
        {
            return await db.DailyRolls.Find(id);
        }

        [HttpPost]
        [Route("/daily/postdaily")]
        public async Task<IHttpActionResult> Create()
        {
            try
            {
                Models.DailyRoll dailyRoll = new Models.DailyRoll();
                await dailyRoll.creationDate = DateTime.Now;
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/daily/postmanydaily/")]
        public async Task<IHttpActionResult> CreateMany(IEnumerable<Models.DailyRoll> collection)
        {
                await collection.ToList().ForEach(dailyRoll =>
                {
                    try
                    {
                        dailyRoll.creationDate = DateTime.Now;
                        db.DailyRolls.Add(dailyRoll);
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        return badRequest(ex.Message);
                    }
                });            
        }

        [HttpPut]
        [Route("/daily/putdaily/{id}")]
        public async Task<IHttpActionResult> Edit(int id)
        {
            try
            {
                Models.DailyRoll dailyRoll = db.DailyRolls.Find(id);
                dailyRoll.creationDate = DateTime.Now;
                db.Entry(dailyRoll).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/daily/deletedaily/{id}")]
        [HttpPost]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                Models.DailyRoll dailyRoll = db.DailyRolls.Find(id);
                dailyRoll.IsDeleted = true;
                db.Entry(dailyRoll).State = System.Data.Entity.EntityState.Modified;
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
