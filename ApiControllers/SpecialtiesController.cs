using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using teleRDV.Models;

namespace teleRDV.Controllers
{
    [Route("api/[controller]")]
    public class SpecialtiesController : Controller
    {
        private readonly Context db;

        public SpecialtiesController(Context ctx)
        {
            db = ctx;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var list = await db.Specialties.Find(t => true).ToListAsync();
            return Ok(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var obj = await db.Specialties.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (obj == null)
            {
                return this.NotFound();
            }
            return this.Ok(obj);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Specialty value)
        {
            await db.Specialties.InsertOneAsync(value);
            return this.Ok(value);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody]Specialty value)
        {
            var obj = await db.Specialties.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (obj == null)
            {
                return this.NotFound();
            }

            var query = Builders<Specialty>.Filter.Eq(e => e.Id, id);
            await db.Specialties.ReplaceOneAsync(query, value);
            return this.Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            {
                var builder = Builders<Subscriber>.Filter;
                var filter = builder.Eq("SpecialtyId", id);
                var result = await db.Subscribers.Find(filter).CountAsync();
                if (result > 0)
                {
                    return this.BadRequest("Specialty Has Subscribers");
                }
            }

            await db.Specialties.FindOneAndDeleteAsync(t => t.Id == id);
            return this.Ok();
        }
    }
}