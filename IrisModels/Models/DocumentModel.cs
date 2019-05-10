//using IrisWeb.Code.Data.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/Document/Read", "~/API/Document/Create", "~/API/Document/Update", "~/API/Document/Destroy")]
    public sealed class DocumentModel
    {
        [Required(ErrorMessage = "Your {0} is required.")]
        [Key]
        [ScaffoldColumn(false)]
        //[IrisGridColumn(DefaultValue = "1600000000")]
        [MaxLength(10)]
        public string Document_Key { get; set; }

        [Display(Name = "Security Object")]
        [MaxLength(10)]
        //[ForeignKey(typeof(SecurityObjectModel))]
        public string SecurityObject_Key { get; set; }

        [Display(Name = "Document Type")]
        [MaxLength(10)]
        //[ForeignKey(typeof(DocumentTypeModel))]
        public string Document_Type_key { get; set; }

        [MaxLength(512)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        //TODO: Foreign Key model???
        [Display(Name = "Document Type")]
        [MaxLength(10)]
        //[ForeignKey(typeof(ForeignModel))]
        public string Foreign_Key { get; set; }

        [MaxLength(256)]
        public string Comments { get; set; }

        [MaxLength(256)]
        public string FileLocation { get; set; }

        [MaxLength(10)]
        public string FileType { get; set; }

        [MaxLength(256)]
        public string BatchDescription { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 1")]
        public string User1 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 2")]
        public string User2 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 3")]
        public string User3 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 4")]
        public string User4 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 5")]
        public string User5 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 6")]
        public string User6 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 7")]
        public string User7 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 8")]
        public string User8 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 9")]
        public string User9 { get; set; }

        [MaxLength(30)]
        [Display(Name = "User 10")]
        public string User10 { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Last Modified")]
        [MaxLength(10)]
        //[IrisGridColumn(DefaultValue = DefaultValueSlug.DateTimeNow)]
        public DateTime? DateStamp { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [Display(Name = "IRIS User")]
        [MaxLength(10)]
        public string SecurityUser_Key { get; set; }
    }
}