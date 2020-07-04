using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorErp.Core.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime BeginingOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
        }

        public static DateTime EndOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
        }

        public static DateTime? BeginingOfDay(this DateTime? value)
        {
            return value.HasValue ? new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, 0, 0, 0) : value;
        }

        public static DateTime? EndOfDay(this DateTime? value)
        {

            return value.HasValue ? new DateTime(value.Value.Year, value.Value.Month, value.Value.Day, 23, 59, 59) : value;
        }
    }
}
