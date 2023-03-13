using System.Text.Json.Serialization;

namespace Diseas_Management_System.Models
{
    public class DiseasReport
    {
        public int diseasReportId { get; set; }

        public int? diseasId { get; set; }
        [JsonIgnore]
        public Diseas? Diseas { get; set; }

        public int? reportId { get; set; }
        [JsonIgnore]
        public Report? Report { get; set; }
    }
}
