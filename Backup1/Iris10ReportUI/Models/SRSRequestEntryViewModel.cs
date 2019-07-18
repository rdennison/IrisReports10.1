using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    public class SRSRequestEntryViewModel
    {
        [Key]
        public string SRSRequest_Key { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Display(Name = "FristName")]
        public string FirstName { get; set; }


        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Primary Phone")]
        public string PrimaryPhone { get; set; }

        [Display(Name = "Alternate Phone")]
        public string AlternatePhone { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Display(Name = "Search Field")]
        public string SearchField { get; set; }

        [Display(Name = "Request")]
        public string Request { get; set; }

        public string SRSRequester_Key { get; set; }

        [Display(Name = "Request Category")]
        public string SRSRequestCategory_Key { get; set; }

        [Display(Name = "Road Name")]
        public string RoadName_Key { get; set; }

        [Display(Name = "Milepost")]
       public decimal Milepost { get; set; }


        [Display(Name = "Service Area")]
        public string SRSServiceArea_Key { get; set; }

 


        [Display(Name = "Contact Method")]
        public string SRSContactMethod_Key { get; set; }

        [Display(Name = "Request Type")]
        public string SRSRequestType_Key { get; set; }

        [Display(Name = "SRS Section")]
        public string SRSSection_Key { get; set; }

        //TODO: merged into lookup tables
        [Display(Name = "Response Method")]
        public string SRSResponseMethod_Key { get; set; }


        [Display(Name = "Project")]
        public string Project_Key { get; set; }


        //TODO: change pivot logic at some point probably
        //[ReadOnly(true)]
   [Display(Name = "Description")]
        public string NameDesc { get; set; }

        [Display(Name = "Cross Street")]
        public string XStreet { get; set; }

        [MaxLength(50)]
        [Display(Name = "Address")]
        public string StreetAddress { get; set; }

        [Display(Name = "Priority Level")]
        public string SRSPriority_Key { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }


        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receive Date")]
        public DateTime? ReceiveDate { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Time Received")]
        public DateTime? OUNCTimeReceived { get; set; }

        [Display(Name = "SRS User")]
        public string SRSUser_Key { get; set; }


        [Display(Name = "Complete")]
        public bool Complete { get; set; }




        [Display(Name = "Lat Dir")]
        public string LatDirection { get; set; }


        [Display(Name = "Lat Degrees")]
        public string LatDegrees { get; set; }


        [Display(Name = "Lat Minutes")]
        public string LatMinutes { get; set; }

        [Display(Name = "Lat Seconds")]
        public string LatSeconds { get; set; }


        [Display(Name = "Long Dir")]
        public string LongDirection { get; set; }

        [Display(Name = "Long Degrees")]
        public string LongDegrees { get; set; }

        [Display(Name = "Long Minutes")]
        public string LongMinutes { get; set; }

        [Display(Name = "Long Seconds")]
        public string LongSeconds { get; set; }

        //[ReadOnly(true)]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Latitude { get; set; }

        //[ReadOnly(true)]
        [MaxLength(11)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public string Longitude { get; set; }



    }
}