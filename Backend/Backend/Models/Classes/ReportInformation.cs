namespace Backend.Models.Classes
{
    public class ReportInformation
    {
        public DateTime LastWeekStartDate { get; set; }
        public DateTime LastWeekEndDate { get; set; }
        public int LastWeekEntries { get; set; }
        public DateTime PastWeekStartDate { get; set; }
        public DateTime PastWeekEndDate { get; set; }
        public int PastWeekEntries { get; set; }
    }
}
