using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace My.AppStore.Models
{
    public class CheckoutModel
    {
        //TODO: Set error messages for credit card info.
        [Required]
        public DateTime? CreditCardExpiration { get; set; }
        [CreditCard]
        [Required]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CreditCardName { get; set; }
        [Required]
        public string CreditCardVerificationValue { get; set; }

        [EmailAddress]
        public string EmailAddress { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please specify an address.")]
        public string ShippingAddress1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string ShippingAddress2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Please specify a city.")]
        public string ShippingCity { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Please specify a country.")]
        public string ShippingCountry { get; set; }

        [Display(Name = "State")]
        public string ShippingState { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "Please specify a zip code.")]
        [MinLength(5)]
        [MaxLength(12)]
        public string ZipCode { get; set; }
    }
}