using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models
{
    public class Rent
    {
        [Key]
        public string Factuurnummer { get; set; }

        [Key]
        public string Kenteken { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime StartDatum { get; set; }

        public DateTime EindDatum { get; set; }

        public string Dagprijs { get; set; }
    }
}
