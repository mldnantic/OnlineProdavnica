using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class RacunSpoj
    {
        [Key]
        public int ID { get; set; }

        [JsonIgnore]
        public Artikal Artikal { get; set; }

        public Racun Racun { get; set; }
    }
}