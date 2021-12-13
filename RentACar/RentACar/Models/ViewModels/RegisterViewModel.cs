﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.Models.ViewModels
{
    public class RegisterViewModel
    {
        [DisplayName("Voorletters")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Voorletters { get; set; }

        [DisplayName("Tussenvoegsels")]
        public string Tussenvoegsels { get; set; }

        [DisplayName("Achternaam")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Achternaam { get; set; }

        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Adres { get; set; }

        [DisplayName("Postcode")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [RegularExpression(@"[1-9]\d{3}[A-Za-z]{2}", ErrorMessage = "Postcode is niet geldig! Gebruik bijvoorveeld: 1234AB")]
        public string Postcode { get; set; }

        [DisplayName("Woonplaats")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Woonplaats { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Email { get; set; }

        [DisplayName("Gebruikersnaam")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string Gebruikersnaam { get; set; }

        [DisplayName("Wachtwoord")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Het {0} moet minstens {2} tekens bevatten", MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("Bevestig wachtwoord")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wachtwoorden komen niet overeen")]
        public string PasswordConfirm { get; set; }

        [DisplayName("Geboortedatum")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:dd/MM/yyy}")]
        public DateTime Geboortedatum { get; set; }

        [DisplayName("Rol")]
        [Required(ErrorMessage = "{0} is een verplicht veld")]
        public string RoleName { get; set; }
    }
}
