using System;

namespace teleRDV.Models
{
    public class CallEntry : BaseModel
    {
        public string ParentId { get; set; }
        public CallType CallType { get; set; }
        public DateTime Started { get; set; }
        public string Line { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Remarks { get; set; }
        public string UserId { get; set; }
        public CallStatus Status { get; set; }
        public CallReason Reason { get; set; }
        public DateTime? Ended { get; set; }
        public int Duration { get; set; }
    }
}