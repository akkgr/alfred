using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace teleRDV.Models
{
    public class Subscriber : BaseModel
    {
        public Subscriber()
        {
            Addresses = new List<Address>();
            Phones = new List<Phone>();
            Infos = new List<Info>();
            DivertPhones = new List<Phone>();
            PaymentMethods = new List<PaymentMethod>();
            SocialSecurityFunds = new List<SocialSecurityFund>();
            WorkSchedule = new Schedule();
            Faqs = new List<Faq>();
        }

        [Required]
        public string CallNumber { get; set; }

        public string UserId { get; set; }

        [Required]
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Company { get; set; }

        public string CallAnswer { get; set; }

        public Specialty Specialty { get; set; }

        //[EnsureMinimumElements(1, ErrorMessage = "At least an address is required")]
        public IList<Address> Addresses { get; set; }

        //[EnsureMinimumElements(1, ErrorMessage = "At least a phone is required")]
        public IList<Phone> Phones { get; set; }

        public IList<Info> Infos { get; set; }

        public string FullName { get { return string.Format("{0} {1}", LastName, FirstName); } }

        public string AllPhones { get { return string.Join(", ", this.Phones.Select(t => t.Value)); } }

        public Schedule WorkSchedule { get; set; }

        public Schedule DivertSchedule { get; set; }

        public string SimpleAnswer { get; set; }

        public string EmergencyAnswer { get; set; }

        public string EmergencyWhen { get; set; }

        public Schedule RdvSchedule { get; set; }

        public int RdvDuration { get; set; }

        public int MessagesPerMonth { get; set; }

        public string GeneralInfo { get; set; }

        public string AgentInfo { get; set; }

        public string MessagesInfo { get; set; }

        public Schedule MessagesSchedule { get; set; }

        //[EnsureMinimumElements(1, ErrorMessage = "At least a phone is required")]
        public IList<Phone> DivertPhones { get; set; }

        public string DivertLine { get; set; }

        public IList<PaymentMethod> PaymentMethods { get; set; }

        public IList<SocialSecurityFund> SocialSecurityFunds { get; set; }

        public IList<Faq> Faqs { get; set; }
    }
}