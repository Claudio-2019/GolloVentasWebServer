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
    public class EstadoComprasController : ApiController
    {
        private GolloVentasEntities db = new GolloVentasEntities();

        // GET: api/EstadoCompras
        public IQueryable<EstadoCompra> GetEstadoCompra()
        {
            return db.EstadoCompra;
        }

        // GET: api/EstadoCompras/5
        [ResponseType(typeof(EstadoCompra))]
        public IHttpActionResult GetEstadoCompra(int id)
        {
            EstadoCompra estadoCompra = db.EstadoCompra.Find(id);
            if (estadoCompra == null)
            {
                return NotFound();
            }

            return Ok(estadoCompra);
        }

        // PUT: api/EstadoCompras/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstadoCompra(int id, EstadoCompra estadoCompra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estadoCompra.estadoId)
            {
                return BadRequest();
            }

            db.Entry(estadoCompra).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadoCompraExists(id))
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

        // POST: api/EstadoCompras
        [ResponseType(typeof(EstadoCompra))]
        public IHttpActionResult PostEstadoCompra(EstadoCompra estadoCompra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EstadoCompra.Add(estadoCompra);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = estadoCompra.estadoId }, estadoCompra);
        }

        // DELETE: api/EstadoCompras/5
        [ResponseType(typeof(EstadoCompra))]
        public IHttpActionResult DeleteEstadoCompra(int id)
        {
            EstadoCompra estadoCompra = db.EstadoCompra.Find(id);
            if (estadoCompra == null)
            {
                return NotFound();
            }

            db.EstadoCompra.Remove(estadoCompra);
            db.SaveChanges();

            return Ok(estadoCompra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstadoCompraExists(int id)
        {
            return db.EstadoCompra.Count(e => e.estadoId == id) > 0;
        }
    }
}