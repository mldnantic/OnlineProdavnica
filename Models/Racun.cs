using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Racun
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public DateTime Datum { get; set; }

        public int Iznos { get; set; }

        //[Required]
        public Radnja Radnja { get; set; }

        // [JsonIgnore]
        public List<RacunSpoj> RacunArtikli { get; set; }

    }
}