using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using SimHubAPI.Models;

namespace SimHubAPI.Controllers
{
    public class SimsController : ApiController
    {
        private SimHubAPIContext db = new SimHubAPIContext();

        // GET: api/Sims
        public IQueryable<Sim> GetSims()
        {
            // return db.Sims;

            //new code: include owner infor
            return db.Sims
                 .Include(b => b.Owner);
        }

        // GET: api/Sims/5
        [ResponseType(typeof(Sim))]
        public async Task<IHttpActionResult> GetSim(int id)
        {
            var sim = from s in db.Sims
                      where s.Id == id
                      select new SimDTO()
                      {
                          Number = s.Number,
                          Price = s.Price,
                          Made = s.Made,
                          OwnerId = s.OwnerId,
                          Type = s.Type
                      };

            if (sim == null)
            {
                return NotFound();
            }

            return Ok(sim);
        }

        //get sim theo number tan cung bang day
        // GET: api/Sims?end=678
        [ResponseType(typeof(Sim))]
        public async Task<IHttpActionResult> GetSimEnding(string end)
        {
            // Sim sim = await db.Sims.FindAsync(end);
            var sim = from s in db.Sims
                      where s.Number.EndsWith(end)
                      select new SimDTO()
                      {
                          Number = s.Number,
                          Price = s.Price,
                          Made = s.Made,
                          OwnerId = s.OwnerId,
                          Type = s.Type
                      };
            if (sim == null)
            {
                return NotFound();
            }

            return Ok(sim);
        }

        //get sim theo kieu number co chua day
        // GET: api/Sims?end=678
        [ResponseType(typeof(Sim))]
        public async Task<IHttpActionResult> GetSimContain(string segment)
        {
            // Sim sim = await db.Sims.FindAsync(end);
            var sim = from s in db.Sims
                      where s.Number.Contains(segment)
                      select new SimDTO()
                      {
                          Number = s.Number,
                          Price = s.Price,
                          Made = s.Made,
                          OwnerId = s.OwnerId,
                          Type = s.Type
                      };
            if (sim == null)
            {
                return NotFound();
            }
            return Ok(sim);
        }







        // PUT: api/Sims/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSim(int id, Sim sim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sim.Id)
            {
                return BadRequest();
            }

            db.Entry(sim).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sims
        [ResponseType(typeof(Sim))]
        public async Task<IHttpActionResult> PostSim(Sim sim)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sims.Add(sim);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sim.Id }, sim);
        }

        // DELETE: api/Sims/5
        [ResponseType(typeof(Sim))]
        public async Task<IHttpActionResult> DeleteSim(int id)
        {
            Sim sim = await db.Sims.FindAsync(id);
            if (sim == null)
            {
                return NotFound();
            }

            db.Sims.Remove(sim);
            await db.SaveChangesAsync();

            return Ok(sim);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SimExists(int id)
        {
            return db.Sims.Count(e => e.Id == id) > 0;
        }


    }
}