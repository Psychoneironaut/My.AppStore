using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace My.AppStore.Models
{
    public class CheckoutModel
    {
        //[EmailAddress]
        //[RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$")]
        //public string EmailAddress { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string ShippingAddress1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string ShippingAddress2 { get; set; }

        [Required(ErrorMessage = "You need to specify a city")]
        [Display(Name = "City")]
        public string ShippingCity { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string ShippingCountry { get; set; }

        [Display(Name = "State")]
        public string ShippingState { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(12)]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
    }
}