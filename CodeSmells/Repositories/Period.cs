using System;
namespace CodeSmells.Repositories
{
    public class Period
    {
        public DateTime Start { get; private set; }
        public TimeSpan Duration { get; private set; }

        public Period(DateTime start, TimeSpan duration)
        {
            this.Start = start;
            this.Duration = duration;
        }

        public static Period CreateForMonth(int year, int month)
        {
            var date1 = new DateTime(year, month, 1, 0, 0, 0);
            var timeSpan = date1.AddMonths(1).AddDays(-1).Subtract(date1);
            return new Period(date1, timeSpan);
        }
    }
}
