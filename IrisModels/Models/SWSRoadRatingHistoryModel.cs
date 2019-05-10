using IrisAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace IrisModels.Models
{
    [Report(Module = "Street Wise", Display = "SWS Road Rating History", ListValue = "SWSRoadRatingHistory")]
    [Security(Module = "Street Wise", Display = "Road Rating History", ListValue = "SWSRoadRatingHistory")]
    public sealed class SWSRoadRatingHistoryModel : ModelBase
    {
		[Key]
		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[IrisGridColumn(Width = 150)]
		public Int64 SWSRoadRatingHistory_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(SWSManagementSectionModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "SWS Management Section Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 SWSManagementSection_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Date)]
		[UIHint("IRISDate")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
		[FilterType(Date = true)]
		[Display(Name = "Road Rating Date")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public DateTime? RoadRatingDate { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[Required(ErrorMessage = "Your {0} is required.")]
		[ForeignKey(typeof(SWSLookupRoadRatingModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "SWS Lookup Road Rating Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64 SWSLookupRoadRating_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.BigInt)]
		[ForeignKey(typeof(SWSLookupRatingTypeModel), ForeignKeyDisplayField="NameDesc")]
		[FilterType(Dropdown = true)]
		[Display(Name = "SWS Lookup Rating Type Key")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public Int64? SWSLookupRatingType_Key { get; set; }

		[DbProperties(DatabaseType = SqlDbType.Int)]
		[DataType("Integer")]
		[FilterType(Number = true)]
		[Display(Name = "Inspection Rating")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public int? InspectionRating { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[DataType("Byte")]
		[Display(Name = "Road Rating Calculated From Inspection Rating")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte? RoadRatingCalculatedFromInspectionRating { get; set; }

		[DbProperties(DatabaseType = SqlDbType.TinyInt)]
		[DataType("Byte")]
		[Display(Name = "Inspection Rating Calculated From Road Rating")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = true, AllowCount = true, AllowMax = true, AllowMin = true, AllowSum = true)]
		public byte? InspectionRatingCalculatedFromRoadRating { get; set; }

		[DbProperties(DatabaseType = SqlDbType.VarChar, Size = 100)]
		[FilterType(Text = true)]
		[Display(Name = "Comment")]
		[IrisGridColumn(Width = 150)]
		[Aggregate(AllowAvg = false, AllowCount = true, AllowMax = false, AllowMin = false, AllowSum = false)]
		public string Comment { get; set; }

    }
}
