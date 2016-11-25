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
    public class PaymentMethodsController : Controller
    {
        private readonly Context db;

        public PaymentMethodsController(Context ctx)
        {
            this.db = ctx;
        }

        // GET: api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var list = await db.PaymentMethods.Find(t => true).ToListAsync();
            return Ok(list);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var obj = await db.PaymentMethods.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (obj == null)
            {
                return this.NotFound();
            }
            return this.Ok(obj);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]PaymentMethod value)
        {
            await db.PaymentMethods.InsertOneAsync(value);
            return this.Ok(value);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody]PaymentMethod value)
        {
            var obj = await db.PaymentMethods.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (obj == null)
            {
                return this.NotFound();
            }

            var query = Builders<PaymentMethod>.Filter.Eq(e => e.Id, id);
            await db.PaymentMethods.ReplaceOneAsync(query, value);
            return this.Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            {
                var builder = Builders<Subscriber>.Filter;
                var filter = builder.Eq("PaymentMethod.Id", id);
                var result = await db.Subscribers.Find(filter).CountAsync();
                if (result > 0)
                {
                    return this.BadRequest("Payment Method Has Subscribers");
                }
            }

            await db.PaymentMethods.FindOneAndDeleteAsync(t => t.Id == id);
            return this.Ok();
        }
    }
}