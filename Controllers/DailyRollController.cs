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
        public IEnumerable<Models.DailyRoll> Get()
        {

            return db.DailyRolls.ToList();
        }

        [HttpGet]
        [Route("/daily/getdaily/{id}")]
        public Models.DailyRoll Get(int id)
        {
            return db.DailyRolls.Find(id);
        }

        [HttpPost]
        [Route("/daily/postdaily")]
        public IHttpActionResult Create()
        {
            try
            {
                Models.DailyRoll dailyRoll = new Models.DailyRoll();
                dailyRoll.creationDate = DateTime.Now;
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/daily/postmanydaily/")]
        public IHttpActionResult CreateMany(IEnumerable<Models.DailyRoll> collection)
        {
            try
            {
                collection.ToList().ForEach(dailyRoll =>
                {
                    dailyRoll.creationDate = DateTime.Now;
                    db.DailyRolls.Add(dailyRoll);
                });
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("/daily/putdaily/{id}")]
        public IHttpActionResult Edit(int id)
        {
            try
            {
                Models.DailyRoll dailyRoll = db.DailyRolls.Find(id);
                dailyRoll.creationDate = DateTime.Now;
                db.Entry(dailyRoll).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/daily/deletedaily/{id}")]
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                db.DailyRolls.Remove(db.DailyRolls.Find(id));
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
