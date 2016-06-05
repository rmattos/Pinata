using System;
using System.Diagnostics;

namespace Piñata
{
    public static class DateTimeExtension
    {
        private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        private static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 23, 59, 59, 999);

        [DebuggerStepThrough]
        public static bool IsToday(this DateTime target)
        {
            var today = SystemTime.Today();
            return (target.Day == today.Day) && (target.Month == today.Month) && (target.Year == today.Year);
        }

        [DebuggerStepThrough]
        public static bool IsValid(this DateTime target)
        {
            return (target >= MinDate) && (target <= MaxDate);
        }

        [DebuggerStepThrough]
        public static string ToDateString(this DateTime target)
        {
            return target.ToDateString("/");
        }

        [DebuggerStepThrough]
        public static string ToDateString(this DateTime? target)
        {
            return target.ToDateString("/");
        }

        [DebuggerStepThrough]
        public static string ToDateString(this DateTime target, string separator)
        {
            return target.ToString("dd{0}MM{0}yyyy".FormatWith(separator));
        }

        [DebuggerStepThrough]
        public static string ToDateString(this DateTime? target, string separator)
        {
            return target.HasValue ? target.Value.ToDateString(separator) : string.Empty;
        }

        [DebuggerStepThrough]
        public static string ToTimeString(this DateTime target)
        {
            return target.ToString("HH:mm") + "h";
        }

        [DebuggerStepThrough]
        public static string ToTimeString(this DateTime? target)
        {
            return target.HasValue ? target.Value.ToTimeString() : string.Empty;
        }

        [DebuggerStepThrough]
        public static string ToRelativeTimeString(this DateTime target)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(SystemTime.Now().Ticks - target.Ticks);
            double delta = ts.TotalSeconds;

            if (delta < 1 * MINUTE)
            {
                return ts.Seconds <= 1 ? "um segundo atrás" : ts.Seconds + " segundos atrás";
            }
            if (delta < 2 * MINUTE)
            {
                return "um minuto atrás";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " minutos atrás";
            }
            if (delta < 90 * MINUTE)
            {
                return "uma hora atrás";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " horas atrás";
            }
            if (delta < 48 * HOUR)
            {
                return "ontem";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " dias atrás";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "um mês atrás" : months + " meses atrás";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "um ano atrás" : years + " anos atrás";
            }
        }
    }
}
