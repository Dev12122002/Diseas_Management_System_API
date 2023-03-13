using System.Text.Json.Serialization;

namespace Diseas_Management_System.Models
{
    public class DiseasBodyPart
    {
        public int diseasBodyPartId { get; set; }

        public int? diseasId { get; set; }
        [JsonIgnore]
        public Diseas? Diseas { get; set; }

        public int? bodypartId { get; set; }
        [JsonIgnore]
        public BodyPart? BodyPart { get; set; }
    }
}
