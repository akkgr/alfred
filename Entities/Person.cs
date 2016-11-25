using System;
using System.Collections.Generic;

namespace teleRDV.Models
{
    public class Person : BaseModel
    {
        public Person()
        {
            Addresses = new List<Address>();
            Phones = new List<Phone>();
            Infos = new List<Info>();
            Relatives = new List<Person>();
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public DateTime BirthDate { get; set; }
        public IList<Address> Addresses { get; set; }
        public IList<Phone> Phones { get; set; }
        public IList<Info> Infos { get; set; }
        public IList<Person> Relatives { get; set; }
        public string UserId { get; set; }
    }
}