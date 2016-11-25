using Microsoft.AspNetCore.Identity.MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace teleRDV.Models
{
    public class User : IdentityUser
    {
        [BsonIgnore]
        public string Password { get; set; }

        [BsonIgnore]
        public string OldPassword { get; set; }

        [BsonIgnore]
        public string NewPassword { get; set; }

        [BsonIgnore]
        public string ConfirmPassword { get; set; }
    }
}