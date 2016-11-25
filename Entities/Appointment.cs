using System;

namespace teleRDV.Models
{
    public class Appointment : BaseModel
    {
        public string SubscriberId { get; set; }
        public string PersonId { get; set; }
        public DateTime DateTime { get; set; }
        public string Remarks { get; set; }
        public AppointmentStatus Status { get; set; }
        public string UserId { get; set; }
    }
}