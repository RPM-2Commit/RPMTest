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
using RPMTest.Models;

namespace RPMTest.Controllers
{
    public class ThingsController : ApiController
    {
        private RPMTestDB db = new RPMTestDB();

        // GET: api/Things
        public IQueryable<Thing> GetThing()
        {
            return db.Thing;
        }

        // GET: api/Things/5
        [ResponseType(typeof(Thing))]
        public async Task<IHttpActionResult> GetThing(int id)
        {
            Thing thing = await db.Thing.FindAsync(id);
            if (thing == null)
            {
                return NotFound();
            }

            return Ok(thing);
        }

        // PUT: api/Things/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutThing(int id, Thing thing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != thing.Id)
            {
                return BadRequest();
            }

            db.Entry(thing).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThingExists(id))
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

        // POST: api/Things
        [ResponseType(typeof(Thing))]
        public async Task<IHttpActionResult> PostThing(Thing thing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Thing.Add(thing);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = thing.Id }, thing);
        }

        // DELETE: api/Things/5
        [ResponseType(typeof(Thing))]
        public async Task<IHttpActionResult> DeleteThing(int id)
        {
            Thing thing = await db.Thing.FindAsync(id);
            if (thing == null)
            {
                return NotFound();
            }

            db.Thing.Remove(thing);
            await db.SaveChangesAsync();

            return Ok(thing);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ThingExists(int id)
        {
            return db.Thing.Count(e => e.Id == id) > 0;
        }
    }
}