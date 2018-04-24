using System;

namespace RestaurantBot.Services
{
    [Serializable]
    public class DateTimeRangeWithPrecision
    {
        public bool IsSingleDate { get; set; }
        public DateTime StartDate { get; set; }
        public DatePrecision StartDatePrecision { get; set; }
        public DateTime EndDate { get; set; }
        public DatePrecision EndDatePrecision { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is DateTimeRangeWithPrecision range)
            {
                return IsSingleDate == range.IsSingleDate
                       && StartDate == range.StartDate
                       && StartDatePrecision == range.StartDatePrecision
                       && EndDate == range.EndDate
                       && EndDatePrecision == range.EndDatePrecision;
            }

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 2017379781;
            hashCode = hashCode * -1521134295 + IsSingleDate.GetHashCode();
            hashCode = hashCode * -1521134295 + StartDate.GetHashCode();
            hashCode = hashCode * -1521134295 + StartDatePrecision.GetHashCode();
            hashCode = hashCode * -1521134295 + EndDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EndDatePrecision.GetHashCode();
            return hashCode;
        }
    }

    public enum DatePrecision
    {
        Morning = 0,
        Afternoon = 1
    }
}