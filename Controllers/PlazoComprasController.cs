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
    public class PlazoComprasController : ApiController
    {
        private GolloVentasEntities db = new GolloVentasEntities();

        // GET: api/PlazoCompras
        public IQueryable<PlazoCompra> GetPlazoCompra()
        {
            return db.PlazoCompra;
        }

        // GET: api/PlazoCompras/5
        [ResponseType(typeof(PlazoCompra))]
        public IHttpActionResult GetPlazoCompra(int id)
        {
            PlazoCompra plazoCompra = db.PlazoCompra.Find(id);
            if (plazoCompra == null)
            {
                return NotFound();
            }

            return Ok(plazoCompra);
        }

        // PUT: api/PlazoCompras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlazoCompra(int id, PlazoCompra plazoCompra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != plazoCompra.plazoId)
            {
                return BadRequest();
            }

            db.Entry(plazoCompra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlazoCompraExists(id))
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

        // POST: api/PlazoCompras
        [ResponseType(typeof(PlazoCompra))]
        public IHttpActionResult PostPlazoCompra(PlazoCompra plazoCompra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlazoCompra.Add(plazoCompra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = plazoCompra.plazoId }, plazoCompra);
        }

        // DELETE: api/PlazoCompras/5
        [ResponseType(typeof(PlazoCompra))]
        public IHttpActionResult DeletePlazoCompra(int id)
        {
            PlazoCompra plazoCompra = db.PlazoCompra.Find(id);
            if (plazoCompra == null)
            {
                return NotFound();
            }

            db.PlazoCompra.Remove(plazoCompra);
            db.SaveChanges();

            return Ok(plazoCompra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlazoCompraExists(int id)
        {
            return db.PlazoCompra.Count(e => e.plazoId == id) > 0;
        }
    }
}