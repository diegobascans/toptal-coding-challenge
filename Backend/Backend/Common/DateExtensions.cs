namespace Backend.Common
{
    public static class DateExtensions
    {
        public static DateTime EndOfDay(this DateTime date)
        {
            return date.Date.AddDays(1).AddTicks(-1);
        }
    }
}
