using System;

namespace Bot.Builder.Luisai.Parsers
{
    [Serializable]
    public class DateTimeRange
    {
        public DateTimeRange(DateTimeOffset date)
        {
            IsSingleDate = true;
            StartDate = date;
            EndDate = date;
        }

        public DateTimeRange(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            IsSingleDate = false;
            StartDate = startDate;
            EndDate = endDate;
        }

        public bool IsSingleDate { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}
