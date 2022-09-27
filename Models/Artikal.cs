using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Artikal
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int Cena { get; set; }

        [Required]
        [MaxLength(20)]
        public string Ime { get; set; }

        [JsonIgnore]
        [Required]
        public Radnja Radnja { get; set; }

        [JsonIgnore]
        public List<RacunSpoj> ArtikliRacun { get; set; }

        [JsonIgnore]
        public List<KorpaSpoj> ArtikliKorpa { get; set; }
    }
}