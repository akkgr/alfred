using Microsoft.AspNetCore.Identity.MongoDB;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using teleRDV.Models;
using Microsoft.AspNetCore.Authorization;

namespace teleRDV.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class RolesController : Controller
    {
        private readonly Context db;

        public RolesController(Context ctx)
        {
            this.db = ctx;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<IdentityRole>> Get()
        {
            return await db.Roles.Find(t => true).ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var obj = await db.Roles.Find(t => t.Name == id).FirstOrDefaultAsync();
            if (obj == null)
            {
                return this.NotFound();
            }
            return this.Ok(obj);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Role value)
        {
            await db.Roles.InsertOneAsync(value);
            return this.Ok(value);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody]Role value)
        {
            var obj = await db.Roles.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (obj == null)
            {
                return this.NotFound();
            }

            var query = Builders<Role>.Filter.Eq(e => e.Id, id);
            await db.Roles.ReplaceOneAsync(query, value);
            return this.Ok(value);
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            var obj = await db.Roles.Find(t => t.Id == id).FirstOrDefaultAsync();
            if (obj == null)
            {
                return this.NotFound();
            }

            var builder = Builders<User>.Filter;
            var filter = builder.Eq("User.Roles", obj.Name);
            var result = await db.Users.Find(filter).CountAsync();
            if (result > 0)
            {
                return this.BadRequest();
            }

            if (obj.Name == "Administrator")
            {
                return this.BadRequest();
            }

            await db.Roles.FindOneAndDeleteAsync(t => t.Id == id);
            return this.Ok();
        }
    }
}