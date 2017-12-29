using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace soby.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "AddressRequired")]
        [Display(Name = "FirstAddress", ResourceType = typeof(Resources.Resource))]
        public string Line1 { get; set; }
        [Display(Name = "SecondAddress", ResourceType = typeof(Resources.Resource))]
        public string Line2 { get; set; }
        [Display(Name = "ThirdAddress", ResourceType = typeof(Resources.Resource))]
        public string Line3 { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "CityRequired")]
        [Display(Name = "City", ResourceType = typeof(Resources.Resource))]
        public string City { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "CountryRequired")]
        [Display(Name = "Country", ResourceType = typeof(Resources.Resource))]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
