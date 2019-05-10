using CoreDomain;
using IrisAttributes;

using System.Reflection;

namespace Iris10ReportUI.GridBuilder.Extensions
{
    //Use this helper for any table that is in batch mode, it replaces all null keys with an empty string
    public class KeyHelper<T>
    {
        public KeyHelper()
        {

        }

        public T FixKeys(T model)
        {
            CoreService service = new CoreService();
            PropertyInfo[] pi = model.GetType().GetProperties();

            foreach(var item in pi)
            {
                if(item.Name.EndsWith("Key") && item.GetValue(model,null) == null && item.PropertyType.Name == "String")
                {
                    item.SetValue(model, "");
                }
                //if(item.GetCustomAttribute<CalculatedFieldAttribute>() != null)
                //{
                //    CalculatedFields(item, model, pi);
                //}
            }

            return model;
        }

        public T KeysToNull(T model)
        {
            PropertyInfo[] pi = model.GetType().GetProperties();

            foreach (var item in pi)
            {
                if (item.Name.EndsWith("Key") && item.GetValue(model)?.ToString() == "" && item.PropertyType.Name == "String")
                {
                    item.SetValue(model, null);
                }
                //if (item.GetCustomAttribute<CalculatedFieldAttribute>() != null)
                //{
                //    CalculatedFields(item, model, pi);
                //}
            }

            return model;
        }

        //private void CalculatedFields(PropertyInfo item, T model, PropertyInfo[] pi)
        //{
        //    switch(item.GetCustomAttribute<CalculatedFieldAttribute>().RateType)
        //    {
        //        case "EquipmentRate":
        //            EquipmentRateCalculation rate = new EquipmentRateCalculation();
        //            item.SetValue(model, rate.GetRate(model, pi));
        //            break;
        //        case "InventoryLocationUOM":
        //            InventoryLocationUOMCalculation uom = new InventoryLocationUOMCalculation();
        //            item.SetValue(model, uom.GetUOM(model, pi));
        //            break;
        //        case "InventoryLocationDescription":
        //            if (item.GetValue(model) == null)
        //            {
        //                InventoryLocationDescription desc = new InventoryLocationDescription();
        //                item.SetValue(model, desc.GetDescription(model, pi));
        //            }
        //            break;
        //        case "InventoryLocationCost":
        //            if (item.GetValue(model) == null)
        //            {
        //                InventoryLocationCost cost = new InventoryLocationCost();
        //                item.SetValue(model, cost.GetCost(model, pi));
        //            }
        //            break;
        //        case "RoadNumber":
        //            if (item.GetValue(model) == null)
        //            {
        //                RoadNameCalculation roadName = new RoadNameCalculation();
        //                item.SetValue(model, roadName.GetRoad(model, pi));
        //            }
        //            break;
        //        default:
        //            break;
        //    }
        //}

    }
}