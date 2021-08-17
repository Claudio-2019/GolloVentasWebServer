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
    public class UsuariosController : ApiController
    {
        private GolloVentasEntities db = new GolloVentasEntities();

        // GET: api/Usuarios
        public IQueryable<Usuario> GetUsuario()
        {
            return db.Usuario;
        }

        // GET: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult GetUsuario(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuarios/5
        /* [ResponseType(typeof(void))]
         public IHttpActionResult PutUsuario(string id, Usuario usuario)
         {
             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             if (id != usuario.cedula)
             {
                 return BadRequest();
             }

             db.Entry(usuario).State = EntityState.Modified;

             try
             {
                 db.SaveChanges();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!UsuarioExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return StatusCode(HttpStatusCode.NoContent);
         }*/

        // POST: api/Usuarios
        /*[ResponseType(typeof(Usuario))]
        public IHttpActionResult PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Usuario.Add(usuario);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UsuarioExists(usuario.cedula))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = usuario.cedula }, usuario);
        }*/

        [HttpPost]
        public IHttpActionResult Ingresar(Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["GolloBD"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Usuario (cedula, nombre, apellidos, email, telefono,residencia,rolId,contrasena)
                                                            VALUES (@cedula, @nombre, @apellidos, @email, @telefono,@residencia,@rolId,@contrasena)", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@cedula", usuario.cedula);
                    sqlCommand.Parameters.AddWithValue("@nombre", usuario.nombre);
                    sqlCommand.Parameters.AddWithValue("@apellidos", usuario.apellidos);
                    sqlCommand.Parameters.AddWithValue("@email", usuario.email);
                    sqlCommand.Parameters.AddWithValue("@telefono", usuario.telefono);
                    sqlCommand.Parameters.AddWithValue("@residencia", usuario.residencia);
                    sqlCommand.Parameters.AddWithValue("@rolId", usuario.rolId);
                    sqlCommand.Parameters.AddWithValue("@contrasena", usuario.contrasena);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);

            }

            return Ok(usuario);
        }

        // DELETE: api/Usuarios/5
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult DeleteUsuario(string id)
        {
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            db.Usuario.Remove(usuario);
            db.SaveChanges();

            return Ok(usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UsuarioExists(string id)
        {
            return db.Usuario.Count(e => e.cedula == id) > 0;
        }
    }
}