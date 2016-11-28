using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using teleRDV.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.MongoDB;

namespace teleRDV.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        private readonly Context db;
        private readonly UserManager<User> userManager;

        public UsersController(Context ctx, UserManager<User> um)
        {
            this.db = ctx;
            this.userManager = um;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await db.Users.Find(t => true).ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.NotFound();
            }
            return this.Ok(user);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]User value)
        {
            var result = await userManager.CreateAsync(value, value.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                return this.BadRequest(errors);
            }

            return this.Ok(value);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody]User value)
        {
            var user = await userManager.FindByIdAsync(id);

            user.UserName = value.UserName;
            user.Email = value.Email;
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                return this.BadRequest(errors);
            }

            result = await userManager.AddToRolesAsync(user, value.Roles.Except(user.Roles).ToList<string>());
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                return this.BadRequest(errors);
            }

            result = await userManager.RemoveFromRolesAsync(user, user.Roles.Except(value.Roles).ToList<string>());
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                return this.BadRequest(errors);
            }

            return this.Ok(user);
        }

        // DELETE api/values/5
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors);
                return this.BadRequest(errors);
            }

            return this.Ok();
        }
    }
}