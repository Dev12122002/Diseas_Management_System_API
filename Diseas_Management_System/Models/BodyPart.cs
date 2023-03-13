using System.Text.Json.Serialization;

namespace Diseas_Management_System.Models
{
    public class BodyPart
    {
        public int bodypartId { get; set; }

        public string? bodypartName { get; set; }

        [JsonIgnore]
        public IList<DiseasBodyPart>? DiseasBodyParts { get; set; }
    }
}
