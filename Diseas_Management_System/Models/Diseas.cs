using System.Text.Json.Serialization;

namespace Diseas_Management_System.Models
{
    public class Diseas
    {
        public int diseasId { get; set; }

        public string? imageURL { get; set; }

        public string? diseasName { get; set; }

        public string? symptoms { get; set; }

        [JsonIgnore]
        public IList<DiseasMedicine>? DiseasMedicines { get; set; }

        public char gender { get; set; }

        public int ageRangeStart { get; set; }

        public int ageRangeEnd { get; set; }

        public DateTime discoveryDate { get; set; }

        public float infectionRate { get; set; }

        public float deathRate { get; set; }

        [JsonIgnore]
        public IList<DiseasBodyPart>? DiseasBodyParts { get; set; }

        public string? spreadingWays { get; set; }

        public string? typeOfInfection { get; set; }

        public string? typeOfDiseas { get; set; }

        public bool isSelfCurable { get; set; }

        public string? vaccineName { get; set; }

        public int recoveryTime { get; set; }

        public string? diseasSource { get; set; }

        [JsonIgnore]
        public IList<DiseasReport>? DiseasReports { get; set; }

        public string? precautions { get; set; }
    }
}
