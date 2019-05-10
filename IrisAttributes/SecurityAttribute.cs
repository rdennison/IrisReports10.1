using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;

namespace IrisAttributes
{
    public sealed class SecurityAttribute : Attribute
    {
        private static Dictionary<string, Guid> _controllerGuids = null;
        private static Dictionary<string, Guid> _modelGuids = null;

        public string FkDescriptor { get; set; }
        public bool NotReportable { get; set; }

        public string Module { get; set; }
        public string Display { get; set; }
        public string ListValue { get; set; }

        public string Name { get; set; }

        public sealed class ModuleListData
        {
            public string Key { get; set; }
            public string Description { get; set; }

            public object BaseData { get; set; }
        }


        public enum Modules
        {
            AccountsPayable = 0,
            AccountsReceivable = 1,
            CostAccounting = 2,
            EquipmentManagement = 3,
            RoadInventory = 4,
            ServiceRequest = 5,
            SERVICES = 6,
            StreetWise = 7,
            VegetationManagement = 8,
            Utilities = 9
        }

        readonly static Dictionary<Modules, string> modulesLookup = new Dictionary<Modules, string>()
        {
            { Modules.AccountsPayable, "Accounts Payable" },
            { Modules.AccountsReceivable, "Accounts Receivable" },
            { Modules.CostAccounting, "Cost Accounting" },
            { Modules.EquipmentManagement, "Equipment Management" },
            { Modules.RoadInventory, "Road Inventory" },
            { Modules.ServiceRequest, "Service Request" },
            { Modules.SERVICES, "SERVICES" },
            { Modules.StreetWise, "Street Wise" },
            { Modules.VegetationManagement, "Vegetation Management" },
            { Modules.Utilities, "Utilities" }
        };

        public void GenerateUIGridList()
        {
            Dictionary<Modules, string> modules = modulesLookup;
            Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            IEnumerable<ModuleListData> moduleLista = new List<ModuleListData>();
            IList<ModuleListData> moduleListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> aPLista = new List<ModuleListData>();
            IList<ModuleListData> aPListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> aRLista = new List<ModuleListData>();
            IList<ModuleListData> aRListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> cALista = new List<ModuleListData>();
            IList<ModuleListData> cAListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> eMLista = new List<ModuleListData>();
            IList<ModuleListData> eMListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> rILista = new List<ModuleListData>();
            IList<ModuleListData> rIListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> sRLista = new List<ModuleListData>();
            IList<ModuleListData> sRListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> sLista = new List<ModuleListData>();
            IList<ModuleListData> sListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> sWLista = new List<ModuleListData>();
            IList<ModuleListData> sWListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> vMLista = new List<ModuleListData>();
            IList<ModuleListData> vMListb = new List<ModuleListData>();
            IEnumerable<ModuleListData> uLista = new List<ModuleListData>();
            IList<ModuleListData> uListb = new List<ModuleListData>();
            IList<ModuleListData> allScreensList = new List<ModuleListData>();

            foreach (var m in modules)
            {
                ModuleListData mods = new ModuleListData();
                mods.Key = m.Key.ToString();
                mods.Description = m.Value;
                moduleListb.Add(mods);
            }

            moduleLista = moduleListb.OrderBy(o => o.Description);
            SelectList moduleList = new SelectList(moduleLista, "Key", "Description");
            HttpRuntime.Cache.Add("ModuleList", moduleList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);

            foreach (TypeInfo ti in a.DefinedTypes)
            {
                if (ti.GetCustomAttribute<SecurityAttribute>() != null)
                {
                    switch (ti.GetCustomAttribute<SecurityAttribute>().Module)
                    {
                        case "Accounts Payable":
                            ModuleListData modsap = new ModuleListData();
                            modsap.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modsap.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            aPListb.Add(modsap);
                            allScreensList.Add(modsap);
                            break;
                        case "Accounts Receivable":
                            ModuleListData modsar = new ModuleListData();
                            modsar.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modsar.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            aRListb.Add(modsar);
                            allScreensList.Add(modsar);
                            break;
                        case "Cost Accounting":
                            ModuleListData modsca = new ModuleListData();
                            modsca.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modsca.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            cAListb.Add(modsca);
                            allScreensList.Add(modsca);
                            break;
                        case "Equipment Management":
                            ModuleListData modsem = new ModuleListData();
                            modsem.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modsem.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            eMListb.Add(modsem);
                            allScreensList.Add(modsem);
                            break;
                        case "Road Inventory":
                            ModuleListData modsri = new ModuleListData();
                            modsri.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modsri.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            rIListb.Add(modsri);
                            allScreensList.Add(modsri);
                            break;
                        case "Service Request":
                            ModuleListData modssr = new ModuleListData();
                            modssr.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modssr.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            sRListb.Add(modssr);
                            allScreensList.Add(modssr);
                            break;
                        case "SERVICES":
                            ModuleListData modss = new ModuleListData();
                            modss.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modss.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            sListb.Add(modss);
                            allScreensList.Add(modss);
                            break;
                        case "Street Wise":
                            ModuleListData modssw = new ModuleListData();
                            modssw.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modssw.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            sWListb.Add(modssw);
                            allScreensList.Add(modssw);
                            break;
                        case "Vegetation Management":
                            ModuleListData modsvm = new ModuleListData();
                            modsvm.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modsvm.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            vMListb.Add(modsvm);
                            allScreensList.Add(modsvm);
                            break;
                        case "Utilities":
                            ModuleListData modsu = new ModuleListData();
                            modsu.Key = ti.GetCustomAttribute<SecurityAttribute>().ListValue;
                            modsu.Description = ti.GetCustomAttribute<SecurityAttribute>().Display;
                            uListb.Add(modsu);
                            allScreensList.Add(modsu);
                            break;
                    }
                }
            }
            aPLista = aPListb.OrderBy(o => o.Description);
            aRLista = aRListb.OrderBy(o => o.Description);
            cALista = cAListb.OrderBy(o => o.Description);
            eMLista = eMListb.OrderBy(o => o.Description);
            rILista = rIListb.OrderBy(o => o.Description);
            sRLista = sRListb.OrderBy(o => o.Description);
            sLista = sListb.OrderBy(o => o.Description);
            sWLista = sWListb.OrderBy(o => o.Description);
            vMLista = vMListb.OrderBy(o => o.Description);
            uLista = uListb.OrderBy(o => o.Description);
            SelectList aRList = new SelectList(aRLista, "Key", "Description");
            SelectList aPList = new SelectList(aPLista, "Key", "Description");
            SelectList cAList = new SelectList(cALista, "Key", "Description");
            SelectList eMList = new SelectList(eMLista, "Key", "Description");
            SelectList rIList = new SelectList(rILista, "Key", "Description");
            SelectList sRList = new SelectList(sRLista, "Key", "Description");
            SelectList sList = new SelectList(sLista, "Key", "Description");
            SelectList sWList = new SelectList(sWLista, "Key", "Description");
            SelectList vMList = new SelectList(vMLista, "Key", "Description");
            SelectList uList = new SelectList(uLista, "Key", "Description");
            HttpRuntime.Cache.Add("AllScreenList", new SelectList(allScreensList.OrderBy(o => o.Description), "Key", "Description"), null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityAccountsPayableList", aPList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityAccountsReceivableList", aRList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityCostAccountingList", cAList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityEquipmentManagementList", eMList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityRoadInventoryList", rIList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityServiceRequestList", sRList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecuritySERVICESList", sList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityStreetWiseList", sWList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityVegetationManagementList", vMList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
            HttpRuntime.Cache.Add("SecurityUtilitiesList", uList, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
        }

        public SecurityAttribute()
        {
            
        }

        public void GenerateGuidList()
        {
            if (_controllerGuids == null)
            {
                _controllerGuids = new Dictionary<string, Guid>(StringComparer.InvariantCultureIgnoreCase);
                Assembly a = Assembly.Load("Iris10ReportUI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                foreach (TypeInfo ti in a.DefinedTypes)
                {
                    if (ti.FullName.StartsWith("Iris10ReportUI.Controllers") && !ti.Name.StartsWith("<"))
                    {
                        if(ti.GetCustomAttribute<SecurityAttribute>()?.Name == null)
                            _controllerGuids.Add(ti.Name.Substring(0, ti.Name.Length - 10),ti.GUID);
                        else if(ti.GetCustomAttribute<SecurityAttribute>()?.Name != null)
                            _controllerGuids.Add(ti.GetCustomAttribute<SecurityAttribute>()?.Name, ti.GUID);
                    }
                        
                }
            }
            HttpRuntime.Cache.Add("ControllerGuids", _controllerGuids,null,DateTime.Now.AddDays(14),Cache.NoSlidingExpiration,CacheItemPriority.NotRemovable,null);
        }

        public void GenerateModelGuidList()
        {
            if (_modelGuids == null)
            {
                _modelGuids = new Dictionary<string, Guid>(StringComparer.InvariantCultureIgnoreCase);
                Assembly a = Assembly.Load("IrisModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                foreach (TypeInfo ti in a.DefinedTypes)
                {
                    if (ti.FullName.StartsWith("IrisModels.Models") && !ti.Name.StartsWith("<"))
                    {
                        _modelGuids.Add(ti.Name.Substring(0, ti.Name.Length - 5), ti.GUID);
                    }
                }
            }
            HttpRuntime.Cache.Add("ModelGuids", _modelGuids, null, DateTime.Now.AddDays(14), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, null);
        }
    }
}
