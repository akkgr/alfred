namespace teleRDV.Models
{
    public enum AppointmentStatus
    {
        Open,
        RescheduledBySubscriber,
        RescheduledByPerson,
        CanceledBySubscriber,
        CanceledByPerson,
        Done
    }
}