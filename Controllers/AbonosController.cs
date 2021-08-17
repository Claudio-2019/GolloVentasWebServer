using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GolloAPI.Models;

namespace GolloAPI.Controllers
{
    public class AbonosController : ApiController
    {
        private GolloVentasEntities db = new GolloVentasEntities();

        // GET: api/Abonos
        public IQueryable<Abono> GetAbono()
        {
            return db.Abono;
        }

        // GET: api/Abonos/5
        [ResponseType(typeof(Abono))]
        public IHttpActionResult GetAbono(int id)
        {
            Abono abono = db.Abono.Find(id);
            if (abono == null)
            {
                return NotFound();
            }

            return Ok(abono);
        }

        // PUT: api/Abonos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAbono(int id, Abono abono)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != abono.abonoId)
            {
                return BadRequest();
            }

            db.Entry(abono).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AbonoExists(id))
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

        // POST: api/Abonos
        [ResponseType(typeof(Abono))]
        public IHttpActionResult PostAbono(Abono abono)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Abono.Add(abono);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = abono.abonoId }, abono);
        }

        // DELETE: api/Abonos/5
        [ResponseType(typeof(Abono))]
        public IHttpActionResult DeleteAbono(int id)
        {
            Abono abono = db.Abono.Find(id);
            if (abono == null)
            {
                return NotFound();
            }

            db.Abono.Remove(abono);
            db.SaveChanges();

            return Ok(abono);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AbonoExists(int id)
        {
            return db.Abono.Count(e => e.abonoId == id) > 0;
        }
    }
}