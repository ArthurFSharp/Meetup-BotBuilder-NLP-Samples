using Microsoft.Bot.Framework.Builder.Witai.Models;
using Microsoft.Bot.Framework.Builder.Witai.Parsers;
using RestaurantBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestaurantBot.Helpers
{
    public static class DateTimeRangeWithPrecisionHelper
    {
        public static DateTimeRangeWithPrecision Convert(DateTimeRange date)
        {
            return ExtractPrecision(date);
        }

        private static DateTimeRangeWithPrecision ExtractPrecision(DateTimeRange date)
        {
            if (date.IsSingleDate)
            {
                return new DateTimeRangeWithPrecision
                {
                    IsSingleDate = true,
                    StartDate = date.StartDate.DateTime,
                    StartDatePrecision = DatePrecision.Morning,
                    EndDate = date.EndDate.Date,
                    EndDatePrecision = DatePrecision.Afternoon
                };
            }
            else
            {
                var endDate = date.EndDate.Date.AddSeconds(-1);

                if (date.StartDate.Hour == 4 && date.EndDate.Hour == 13)
                {
                    return new DateTimeRangeWithPrecision
                    {
                        IsSingleDate = true,
                        StartDate = date.StartDate.DateTime,
                        StartDatePrecision = DatePrecision.Morning,
                        EndDate = endDate,
                        EndDatePrecision = DatePrecision.Morning
                    };
                }
                else if (date.StartDate.Hour == 12 && date.EndDate.Hour == 20)
                {
                    return new DateTimeRangeWithPrecision
                    {
                        IsSingleDate = true,
                        StartDate = date.StartDate.DateTime,
                        StartDatePrecision = DatePrecision.Afternoon,
                        EndDate = endDate,
                        EndDatePrecision = DatePrecision.Afternoon
                    };
                }
                else
                {
                    return new DateTimeRangeWithPrecision
                    {
                        IsSingleDate = true,
                        StartDate = date.StartDate.DateTime,
                        StartDatePrecision = DatePrecision.Morning,
                        EndDate = endDate,
                        EndDatePrecision = DatePrecision.Afternoon
                    };
                }
            }
        }

        public static IEnumerable<DateTimeRangeWithPrecision> GetDatesFromWitEntities(IEnumerable<WitEntity> witEntities)
        {
            return witEntities.Select(d => DateTimeParser.TryParse(d, out var range) ? range : null)
                              .Where(d => d != null)
                              .OrderBy(d => d.StartDate)
                              .ThenBy(d => d.EndDate)
                              .Select(Convert)
                              .ToList();
        }
    }
}