using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RentACar.Models.ViewModels
{
    public class CarViewModel
    {
        [DisplayName("Kenteken")]
        [Required]
        public string Kenteken { get; set; }

        [DisplayName("Type")]
        [Required]
        public string Type { get; set; }

        [DisplayName("Merk")]
        [Required]
        public string Merk { get; set; }

        [DisplayName("Dagprijs")]
        [Required]
        public string Dagprijs { get; set; }

    }
}
