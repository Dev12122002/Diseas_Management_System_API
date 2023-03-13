using System.Text.Json.Serialization;

namespace Diseas_Management_System.Models
{
    public class Medicine
    {
        public int medicineId { get; set; }

        public string? medicineName { get; set; }

        [JsonIgnore]
        public IList<DiseasMedicine>? DiseasMedicines { get; set; }
    }
}
