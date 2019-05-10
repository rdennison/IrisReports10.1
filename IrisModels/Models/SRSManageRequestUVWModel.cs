using IrisAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrisModels.Models
{
    public sealed class SRSManageRequestUVWModel
    {
        [Key]
        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(10)]
        [ScaffoldColumn(false)]
        public string SRSManageRequestUVW_Key { get; set; }


        [MaxLength(10)]
        [ScaffoldColumn(false)]
        [ForeignKey(typeof(SRSRequestModel))]
        public string SRSRequest_Key { get; set; }

        [Display(Name = "SRS Request Category")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSRequestCategoryModel), ForeignKeyDisplayField = "Name")]
        public string SRSRequestCategory_Key { get; set; }

        [Display(Name = "Road Name")]
        [MaxLength(10)]
        [ForeignKey(typeof(RoadNameModel))]
        public string RoadName_Key { get; set; }

        //[ReadOnly(true)]
        [MaxLength(8)]
        [Display(Name = "Milepost")]
        public decimal Milepost { get; set; }

        [Display(Name = "SRS User")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSUserModel))]
        public string SRSUser_Key { get; set; }

        [Display(Name = "SRS Service Area")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSServiceAreaModel))]
        public string SRSServiceArea_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "SRS Requester")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSRequesterModel))]
        public string SRSRequester_Key { get; set; }

        //TODO: merged into lookup tables
        [Display(Name = "SRS Contact Method")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSContactMethodModel))]
        public string SRSContactMethod_Key { get; set; }

        [Display(Name = "SRS Request Type")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSRequestTypeModel))]
        public string SRSRequestType_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "SRS Section")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSSectionModel))]
        public string SRSSection_Key { get; set; }

        //TODO: merged into lookup tables
        [Display(Name = "SRS Response Method")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSResponseMethodModel))]
        public string SRSResponseMethod_Key { get; set; }

        //TODO: SRS User 2
        [Display(Name = "SRS User 2")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSUserModel))]
        public string SRSUser2_Key { get; set; }

        [Display(Name = "Project")]
        [MaxLength(10)]
        [ForeignKey(typeof(ProjectModel))]
        public string Project_Key { get; set; }

        [Required]
        [MaxLength(10)]
        [Display(Name = "Request Number")]
        public string RequestNumber { get; set; }

        //TODO: change pivot logic at some point probably
        //[ReadOnly(true)]
        [MaxLength(10)]
        [Required(ErrorMessage = "Your {0} is required.")]
        [IsExcludeSql]
        public string NameDesc { get; set; }

        [MaxLength(50)]
        [Display(Name = "X Street")]
        public string XStreet { get; set; }

        [MaxLength(50)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [MaxLength(100)]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Receive Date")]
        [MaxLength(10)]
        //[ReadOnly(true)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? ReceiveDate { get; set; }

        [Required]
        [MaxLength(2048)]
        [Display(Name = "Request")]
        public string Request { get; set; }

        [MaxLength(2048)]
        [Display(Name = "Previous Section Comment")]
        public string PreviousSectionComment { get; set; }

        [Required]
        [Display(Name = "File Archive")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool FileArchive { get; set; }

        //TODO: merged into lookup tables
        [Display(Name = "SRS Priority")]
        [MaxLength(10)]
        [ForeignKey(typeof(SRSPriorityModel), ForeignKeyDisplayField = "Name")]
        public string SRSPriority_Key { get; set; }

        [Required]
        [Display(Name = "Utility Request")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool UtilityRequest { get; set; }

        [MaxLength(20)]
        [Display(Name = "Pole Number")]
        public string PoleNumber { get; set; }

        [MaxLength(20)]
        [Display(Name = "Utility Map Number")]
        public string UtilityMapNumber { get; set; }

        [MaxLength(20)]
        [Display(Name = "Tax Lot Number")]
        public string TaxLotNumber { get; set; }

        [Required]
        [Display(Name = "Complete")]
        [DataType("Boolean")]
        [MaxLength(1)]
        public bool Complete { get; set; }

        [Required]
        [MaxLength(8)]
        [Display(Name = "View Status")]
        public string ViewStatus { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Complete Date")]
        //[ReadOnly(true)]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? CompleteDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Section Assigned")]
        //[ReadOnly(true)]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateSectionAssigned { get; set; }

        [MaxLength(1)]
        [Display(Name = "Lat Dir")]
        public string LatDirection { get; set; }

        [MaxLength(2)]
        [Display(Name = "Lat Degrees")]
        public string LatDegrees { get; set; }

        [MaxLength(2)]
        [Display(Name = "Lat Minutes")]
        public string LatMinutes { get; set; }

        [MaxLength(3)]
        [Display(Name = "Lat Seconds")]
        public string LatSeconds { get; set; }

        [MaxLength(1)]
        [Display(Name = "Long Dir")]
        public string LongDirection { get; set; }

        [MaxLength(3)]
        [Display(Name = "Long Degrees")]
        public string LongDegrees { get; set; }

        [MaxLength(2)]
        [Display(Name = "Long Minutes")]
        public string LongMinutes { get; set; }

        [MaxLength(3)]
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

        [MaxLength(50)]
        [Display(Name = "Entry Screen")]
        public string EntryScreen { get; set; }

        //TODO: Probably needs a range
        [Required]
        [DataType("Integer")]
        [MaxLength(4)]
        [Display(Name = "Email Alert Count")]
        public int EmailAlertCount { get; set; }

    }
}
