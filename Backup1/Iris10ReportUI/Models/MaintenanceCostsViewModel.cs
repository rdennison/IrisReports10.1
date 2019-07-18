using IrisModels.Models;
using Iris10ReportUI.GridBuilder.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Iris10ReportUI.Models
{
    [ModelDataBindings(KeyFieldName = "Project_Key", TableName = "Project")]

    public class MaintenanceCostsViewModel
    {
        public string Project_Key { get; set; }

        [Display(Name = "Equipment")]
        public byte EquipmentProject { get; set; }

        [Display(Name = "Labor")]
        public decimal? EstLaborCost { get; set; }

        [Display(Name = "Material")]
        public decimal? EstMaterialCost { get; set; }

        [Display(Name = "Services")]
        public decimal? EstOutsideServicesCost { get; set; }

        [Display(Name = "Other")]
        public decimal? BillResourceRateLabor { get; set; }

        [Display(Name = "Total Project Cost")]
        public decimal? TotalProjectCost { get; set; }

    }
}