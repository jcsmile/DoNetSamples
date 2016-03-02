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
using AsyncEntityFramework.Areas.Api.Models;
using AsyncEntityFramework.Models;

namespace AsyncEntityFramework.Areas.Api.Controllers
{
    [RoutePrefix("api/v1/labels")]
    public class LabelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Labels
        public IQueryable<Label> GetLabels()
        {
            return db.Labels;
        }

        // GET: api/Labels/5
        [ResponseType(typeof(Label))]
        public async Task<IHttpActionResult> GetLabel(string id)
        {
            Label label = await db.Labels.FindAsync(id);
            if (label == null)
            {
                return NotFound();
            }

            return Ok(label);
        }

        // PUT: api/Labels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLabel(string id, Label label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != label.Id)
            {
                return BadRequest();
            }

            db.Entry(label).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LabelExists(id))
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

        // POST: api/Labels
        [ResponseType(typeof(Label))]
        public async Task<IHttpActionResult> PostLabel(Label label)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Labels.Add(label);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LabelExists(label.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = label.Id }, label);
        }

        // DELETE: api/Labels/5
        [ResponseType(typeof(Label))]
        public async Task<IHttpActionResult> DeleteLabel(string id)
        {
            Label label = await db.Labels.FindAsync(id);
            if (label == null)
            {
                return NotFound();
            }

            db.Labels.Remove(label);
            await db.SaveChangesAsync();

            return Ok(label);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LabelExists(string id)
        {
            return db.Labels.Count(e => e.Id == id) > 0;
        }
    }
}