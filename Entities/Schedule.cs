using System;
using System.ComponentModel.DataAnnotations;

namespace teleRDV.Models
{
    public class Schedule
    {
        public Schedule()
        {
            DayEntries = new DayEntry[7];
            DayEntries[1] = new DayEntry() { WeekDay = DayOfWeek.Monday };
            DayEntries[2] = new DayEntry() { WeekDay = DayOfWeek.Tuesday };
            DayEntries[3] = new DayEntry() { WeekDay = DayOfWeek.Wednesday };
            DayEntries[4] = new DayEntry() { WeekDay = DayOfWeek.Thursday };
            DayEntries[5] = new DayEntry() { WeekDay = DayOfWeek.Friday };
            DayEntries[6] = new DayEntry() { WeekDay = DayOfWeek.Saturday };
            DayEntries[0] = new DayEntry() { WeekDay = DayOfWeek.Sunday };
        }

        public DayEntry[] DayEntries { get; set; }
    }

    public class DayEntry
    {
        public DayEntry()
        {
            TimeEntries = new TimeEntry[2];
            TimeEntries[0] = new TimeEntry();
            TimeEntries[1] = new TimeEntry();
        }

        public DayOfWeek WeekDay { get; set; }
        public bool Active { get; set; }
        public TimeEntry[] TimeEntries { get; set; }
    }

    public class TimeEntry
    {
        [Range(0, 23)]
        public int StartHour { get; set; }

        [Range(0, 59)]
        public int StartMinute { get; set; }

        [Range(0, 23)]
        public int EndHour { get; set; }

        [Range(0, 59)]
        public int EndMinute { get; set; }
    }
}