using System.Text.Json.Serialization;

namespace Diseas_Management_System.Models
{
    public class Report
    {
        public int reportId { get; set; }

        public string? reportName { get; set; }

        [JsonIgnore]
        public IList<DiseasReport>? ReportMedicines { get; set; }
    }
}
