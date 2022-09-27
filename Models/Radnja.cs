using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Radnja
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Naziv { get; set; }

        [JsonIgnore]
        public List<Artikal> Artikli { get; set; }
    }
}