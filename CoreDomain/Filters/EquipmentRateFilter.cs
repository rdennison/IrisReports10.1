
using SqlComponents;
using System;

namespace CoreDomain.Filters
{
   public sealed class EquipmentRateFilter : IDataFilter
    {
        public string EquipmentKey { get; set; }
        public DateTime? TaskDate { get; set; }

        public void Apply(SqlGenerator sqlgen)
        {
            if (string.IsNullOrEmpty(EquipmentKey) == false) { sqlgen.AddWhereParameter(null, "EquipmentRate", "Equipment_Key", EquipmentKey, SqlWhereComparison.SqlComparer.Equal, null); }
            if (TaskDate.HasValue)
            {
                sqlgen.AddTable("FISCAL", SqlGenerator.SqlJoins.Inner, "Fiscal_Key");
                sqlgen.AddWhereParameter(null, "FISCAL", "Beg_Fiscal", TaskDate.Value.ToShortDateString(), SqlWhereComparison.SqlComparer.LessThan | SqlWhereComparison.SqlComparer.Equal, null);
                sqlgen.AddWhereParameter(null, "FISCAL", "End_Fiscal", TaskDate.Value.ToShortDateString(), SqlWhereComparison.SqlComparer.GreaterThan | SqlWhereComparison.SqlComparer.Equal, null);
            }
        }
    }
}
