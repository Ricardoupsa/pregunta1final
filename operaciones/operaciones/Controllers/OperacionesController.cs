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
using operaciones.Models;

namespace operaciones.Controllers
{
    public class OperacionesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Operaciones
        public IQueryable<Operaciones> GetOperaciones()
        {
            return db.Operaciones;
        }

        // GET: api/Operaciones/5
        [ResponseType(typeof(Operaciones))]
        public IHttpActionResult GetOperaciones(int id)
        {
            Operaciones operaciones = db.Operaciones.Find(id);
            if (operaciones == null)
            {
                return NotFound();
            }

            return Ok(operaciones);
        }

        // PUT: api/Operaciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOperaciones(int id, Operaciones operaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != operaciones.numero)
            {
                return BadRequest();
            }

            db.Entry(operaciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperacionesExists(id))
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

        // POST: api/Operaciones
        [ResponseType(typeof(Operaciones))]
        public IHttpActionResult PostOperaciones(Operaciones operaciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Operaciones.Add(operaciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = operaciones.numero }, operaciones);
        }

        [HttpGet]
        [Route("{id:int}")]
        public string Operacion(int id)
        {
            if (id < 0)
                return "Error";
            if (id == 0)
                return "Realizado por Ricardo";
            return "https://image.freepik.com/vector-gratis/numeros-cartel-imagen_1639-6453.jpg";
        }

        // DELETE: api/Operaciones/5
        [ResponseType(typeof(Operaciones))]
        public IHttpActionResult DeleteOperaciones(int id)
        {
            Operaciones operaciones = db.Operaciones.Find(id);
            if (operaciones == null)
            {
                return NotFound();
            }

            db.Operaciones.Remove(operaciones);
            db.SaveChanges();

            return Ok(operaciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OperacionesExists(int id)
        {
            return db.Operaciones.Count(e => e.numero == id) > 0;
        }
    }
}