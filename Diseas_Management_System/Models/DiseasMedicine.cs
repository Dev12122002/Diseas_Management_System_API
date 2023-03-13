using System.Text.Json.Serialization;

namespace Diseas_Management_System.Models
{
    public class DiseasMedicine
    {
        public int diseasMedicineId { get; set; }

        public int? diseasId { get; set; }
        [JsonIgnore]
        public Diseas? Diseas { get; set; }

        public int? medicineId { get; set; }
        [JsonIgnore]
        public Medicine? Medicine { get; set; }
    }
}
