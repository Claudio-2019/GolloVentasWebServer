using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GolloAPI.Models;

namespace GolloAPI.Controllers
{
    public class FacturasController : ApiController
    {
        private GolloVentasEntities db = new GolloVentasEntities();

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Factura> facturas = new List<Factura>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GolloBD"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT facturaId,estadoId ,plazoId ,garantiaMeses,pagoPorMes ,Saldo,cedula,fecha FROM Factura", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Factura factura = new Factura();
                        factura.facturaId = sqlDataReader.GetInt32(0);
                        factura.estadoId = sqlDataReader.GetInt32(1);
                        factura.plazoId = sqlDataReader.GetInt32(2);
                        factura.garantiaMeses = sqlDataReader.GetInt32(3);
                        factura.pagoPorMes = sqlDataReader.GetDecimal(4);
                        factura.saldo = sqlDataReader.GetDecimal(5);
                        factura.cedula = sqlDataReader.GetString(6);
                        factura.fecha = sqlDataReader.GetDateTime(7);



                        facturas.Add(factura);
                    }

                    sqlConnection.Close();

                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

            return Ok(facturas);
        }

        // GET: api/Facturas/5
        [ResponseType(typeof(Factura))]
        public IHttpActionResult GetFactura(int id)
        {
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            return Ok(factura);
        }

        // PUT: api/Facturas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFactura(int id, Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != factura.facturaId)
            {
                return BadRequest();
            }

            db.Entry(factura).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST: api/Facturas
        [ResponseType(typeof(Factura))]
        public IHttpActionResult PostFactura(Factura factura)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Factura.Add(factura);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = factura.facturaId }, factura);
        }

        // DELETE: api/Facturas/5
        [ResponseType(typeof(Factura))]
        public IHttpActionResult DeleteFactura(int id)
        {
            Factura factura = db.Factura.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            db.Factura.Remove(factura);
            db.SaveChanges();

            return Ok(factura);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FacturaExists(int id)
        {
            return db.Factura.Count(e => e.facturaId == id) > 0;
        }
    }
}