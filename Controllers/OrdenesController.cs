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
    public class OrdenesController : ApiController
    {
        private GolloVentasEntities db = new GolloVentasEntities();

        // GET: api/Ordenes
        public IQueryable<Orden> GetOrden()
        {
            return db.Orden;
        }

        // GET: api/Ordenes/5
        [ResponseType(typeof(Orden))]
        public IHttpActionResult GetOrden(int id)
        {
            Orden orden = db.Orden.Find(id);
            if (orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        // PUT: api/Ordenes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrden(int id, Orden orden)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orden.ordenId)
            {
                return BadRequest();
            }

            db.Entry(orden).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrdenExists(id))
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

        // POST: api/Ordenes
        [ResponseType(typeof(Orden))]
        public IHttpActionResult PostOrden(Orden orden)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orden.Add(orden);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = orden.ordenId }, orden);
        }

        // DELETE: api/Ordenes/5
        [ResponseType(typeof(Orden))]
        public IHttpActionResult DeleteOrden(int id)
        {
            Orden orden = db.Orden.Find(id);
            if (orden == null)
            {
                return NotFound();
            }

            db.Orden.Remove(orden);
            db.SaveChanges();

            return Ok(orden);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrdenExists(int id)
        {
            return db.Orden.Count(e => e.ordenId == id) > 0;
        }
    }
}