using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class KorpaSpoj
    {
        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        public Radnja Radnja { get; set; }
        [JsonIgnore]
        public Artikal Artikal { get; set; }
    }
}