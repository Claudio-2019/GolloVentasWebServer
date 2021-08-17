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
    public class ArticulosController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Articulo> articulos = new List<Articulo>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GolloBD"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT articuloId,nombre ,descripcion ,precio,categoriaId ,imagen FROM Articulo", sqlConnection);

                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        Articulo articulo = new Articulo();
                        articulo.articuloId = sqlDataReader.GetInt32(0);
                        articulo.nombre = sqlDataReader.GetString(1);
                        articulo.descripcion = sqlDataReader.GetString(2);
                        articulo.precio = sqlDataReader.GetDecimal(3);
                        articulo.categoriaId = sqlDataReader.GetInt32(4);
                        articulo.imagen = sqlDataReader.GetString(5);


                        articulos.Add(articulo);
                    }

                    sqlConnection.Close();

                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

            return Ok(articulos);
        }
        /*
        private GolloVentasEntities db = new GolloVentasEntities();

        // GET: api/Articulos
        public IQueryable<Articulo> GetArticulo()
        {
            return db.Articulo;
        }

        // GET: api/Articulos/5
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult GetArticulo(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(articulo);
        }

        // PUT: api/Articulos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticulo(int id, Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articulo.articuloId)
            {
                return BadRequest();
            }

            db.Entry(articulo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloExists(id))
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

        // POST: api/Articulos
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult PostArticulo(Articulo articulo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articulo.Add(articulo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = articulo.articuloId }, articulo);
        }

        // DELETE: api/Articulos/5
        [ResponseType(typeof(Articulo))]
        public IHttpActionResult DeleteArticulo(int id)
        {
            Articulo articulo = db.Articulo.Find(id);
            if (articulo == null)
            {
                return NotFound();
            }

            db.Articulo.Remove(articulo);
            db.SaveChanges();

            return Ok(articulo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticuloExists(int id)
        {
            return db.Articulo.Count(e => e.articuloId == id) > 0;
        }*/
    }
}