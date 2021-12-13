using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACar.Models
{
    public class Car
    {
        [Key]
        public string Kenteken { get; set; }

        public string Type { get; set; }

        public string Merk { get; set; }

        public string Dagprijs { get; set; }

    }
}
