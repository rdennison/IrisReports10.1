using IrisAttributes;
using Iris10ReportUI.GridBuilder.Attributes;
using SqlComponents;
using System;
//using IrisWeb.Code.Data.Attributes;
//using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

namespace IrisModels.Models
{
    //[IrisGrid("~/API/User/Read", "~/API/User/Create", "~/API/User/Update", "~/API/User/Destroy")]
    [EditorType(InCell = true)]
    [ModelDataBindings(DatabaseName = "AmazonDatabase", TableName = "Users", KeyFieldName = "Users_Key")]
    [AddSpecialColumn(ColumnName = "Record Time", ButtonName = "recordTime")]
    public sealed class UsersModel : ModelBase
    {
        [Key]
        [ScaffoldColumn(false)]
        public string Users_Key { get; set; }

        [Required(ErrorMessage = "Your {0} is required.")]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        [FilterType(Text = true)]
        public string UserName { get; set; }

        [MaxLength(50)]
        [Display(Name = "Password")]
        //[IrisGridColumn(Hidden = true)]
        public string UserPassword { get; set; }

        [Display(Name = "Domain")]
        [CopyDown(true)]
        public string Domain { get; set; }

        [Display(Name = "First Name")]
        [MaxLength(100)]
        [FilterType(Text = true)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [MaxLength(100)]
        [FilterType(Text = true)]
        public string LastName { get; set; }

        //[IrisGridColumn(Hidden = true)]
        [FilterType(Dropdown = true)]
        [Display(Name = "County Key")]
        [CopyDown(true)]
        [ForeignKey(typeof(CountyModel))]
        public string County_Key { get; set; }

        [Display(Name = "County")]
        [FilterType(Text = true)]
        [CopyDown(true)]
        public string County { get; set; }

        [Display(Name = "Database Name")]
        [FilterType(Text = true)]
        [CopyDown(true)]
        public string DatabaseName { get; set; }

        [Display(Name = "Server Name")]
        [FilterType(Text = true)]
        [CopyDown(true)]
        public string ServerName { get; set; }

        [Display(Name = "Primary IP")]
        [CopyDown(true)]
        public string PrimaryIP { get; set; }

        [Display(Name = "Backup IP")]
        [CopyDown(true)]
        public string BackupIP { get; set; }

        [Display(Name = "Client Version")]
        [CopyDown(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public double ClientVersion { get; set; }

        [Display(Name = "Last Login Version")]
        [CopyDown(true)]
        public double LastLoginVersion { get; set; }

        [Display(Name = "IRIS Path")]
        [CopyDown(true)]
        [IrisGridColumn(Hidden = true)]
        public string IRISPath { get; set; }

        [Display(Name = "System User Name")]
        [CopyDown(true)]
        [IrisGridColumn(Hidden = true)]
        public string SystemUserName { get; set; }

        [Display(Name = "Encrypted Password")]
        [IrisGridColumn(Hidden = true)]
        public string EncryptedPassword { get; set; }

        [Display(Name = "Last Login Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [FilterType(Date = true)]
        [IrisGridColumn(Hidden = true)]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "Login Total")]
        [Required(ErrorMessage = "Your {0} is required.")]
        [IrisGridColumn(Hidden = true)]
        public int LoginTotal { get; set; }

        [Display(Name = "Allow Login")]
        [CopyDown(true)]
        [Required(ErrorMessage = "Your {0} is required.")]
        public bool AllowLogin { get; set; }

        [Display(Name = "Display Message")]
        [CopyDown(true)]
        [IrisGridColumn(Hidden = true)]
        public string DisplayMessage { get; set; }

        [Display(Name = "ScrewDrivers Setup EXE")]
        [CopyDown(true)]
        [IrisGridColumn(Hidden = true)]
        public string ScrewDriversSetupEXE { get; set; }

        [Display(Name = "Show Printer Warning")]
        [CopyDown(true)]
        [IrisGridColumn(Hidden = true)]
        public bool ShowPrinterWarning { get; set; }

        [Display(Name = "Remote Server")]
        public string RemoteServer { get; set; }

        [Display(Name = "Remote Database")]
        public string RemoteDatabase { get; set; }

        [Display(Name = "Authorization GUID")]
        [IrisGridColumn(Hidden = true)]
        [CopyDown(true)]
        public string AuthGUID { get; set; }
        
        [Display(Name = "Authentication Date")]
        [IrisGridColumn(Hidden = true)]
        [CopyDown(true)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? AuthDate { get; set; }
        

       

    }
}