using alfred;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace teleRDV.Models
{
    public class Context
    {
        private IOptions<DataOptions> dbOptions;
        private IMongoClient client { get; set; }
        private IMongoDatabase database { get; set; }

        public Context(IOptions<DataOptions> options)
        {
            dbOptions = options;
            client = new MongoClient(dbOptions.Value.Server);
            database = client.GetDatabase(dbOptions.Value.Database);

            Users = database.GetCollection<User>("Users");
            Roles = database.GetCollection<Role>("Roles");
            Subscribers = database.GetCollection<Subscriber>("Subscribers");
            Specialties = database.GetCollection<Specialty>("Specialties");
            PaymentMethods = database.GetCollection<PaymentMethod>("PaymentMethods");
            SocialSecurityFunds = database.GetCollection<SocialSecurityFund>("SocialSecurityFunds");
            CallReasons = database.GetCollection<CallReason>("CallReasons");
            CallQueue = database.GetCollection<CallEntry>("CallQueue");
            CallHistory = database.GetCollection<CallEntry>("CallHistory");
            People = database.GetCollection<Person>("People");
            Appointments = database.GetCollection<Appointment>("Appointments");
            EnsureIndexes();
        }

        public IMongoCollection<Role> Roles { get; set; }
        public IMongoCollection<User> Users { get; set; }
        public IMongoCollection<Subscriber> Subscribers { get; set; }
        public IMongoCollection<Specialty> Specialties { get; set; }
        public IMongoCollection<PaymentMethod> PaymentMethods { get; set; }
        public IMongoCollection<SocialSecurityFund> SocialSecurityFunds { get; set; }
        public IMongoCollection<CallReason> CallReasons { get; set; }
        public IMongoCollection<CallEntry> CallQueue { get; set; }
        public IMongoCollection<CallEntry> CallHistory { get; set; }
        public IMongoCollection<Person> People { get; set; }
        public IMongoCollection<Appointment> Appointments { get; set; }

        private void EnsureIndexes()
        {
            var options = new CreateIndexOptions();
            options.Unique = true;

            Users.Indexes.CreateOneAsync(Builders<User>.IndexKeys.Ascending(d => d.UserName), options);
            Users.Indexes.CreateOneAsync(Builders<User>.IndexKeys.Ascending(d => d.Email), options);
            Roles.Indexes.CreateOneAsync(Builders<Role>.IndexKeys.Ascending(d => d.Name), options);
        }

        public static void Init()
        {
            BsonClassMap.RegisterClassMap<BaseModel>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.Id)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetIdGenerator(StringObjectIdGenerator.Instance));
            });
        }
    }
}