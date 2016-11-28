using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using teleRDV.Models;

namespace teleRDV.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class SubscribersController : Controller
    {
        private readonly Context db;

        public SubscribersController(Context ctx)
        {
            this.db = ctx;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Subscriber>> Get()
        {
            return await db.Subscribers.Find(t => true).ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var obj = new Subscriber();
            if (id != "new")
            {
                obj = await db.Subscribers.Find(t => t.Id == id).FirstOrDefaultAsync();
                if (obj == null)
                {
                    return this.NotFound();
                }
            }
            return this.Ok(obj);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Subscriber value)
        {
            await db.Subscribers.InsertOneAsync(value);
            return this.Ok(value);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody]Subscriber value)
        {
            var obj = await db.Subscribers.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (obj == null)
            {
                return this.NotFound();
            }

            var query = Builders<Subscriber>.Filter.Eq(e => e.Id, id);
            await db.Subscribers.ReplaceOneAsync(query, value);
            return this.Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            await db.Subscribers.FindOneAndDeleteAsync(t => t.Id == id);
            return this.Ok();
        }
    }
}