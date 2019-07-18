using CoreDomain;
using System;
using System.Collections.Generic;
using System.Reflection;
using CoreDomain.Filters;
using IrisAttributes;

namespace Iris10ReportUI.Services
{
    public class DatabaseServiceBase<T> : IDisposable
    {
        protected DatabaseModelBindings modelDataBindings;
        private CoreService service = new CoreService();

        public DatabaseServiceBase()
        {
            modelDataBindings = typeof(T).GetDatabaseBindings();
        }
        ~DatabaseServiceBase()
        {
            Dispose(false);
        }

        public IEnumerable<T> Read() { return ReadForFilters(null); }
        public IEnumerable<T> Read(string key) { return ReadForFilters(new ModelKeyFilter(modelDataBindings.TableName, modelDataBindings.KeyFieldName, key)); }
        public IEnumerable<T> ReadForFilters(IDataFilter filters)
        {
            
            //SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Select, modelDataBindings.TableName);
            //sqlgen.SelectFromModel<T>();
            //sqlgen.DoNotFullyQualifyFields = true;

            //if (filters != null)
            //    filters.Apply(sqlgen);

            return service.LoadModel<T>();
        }

        public T Create(T model)
        {
            //SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Insert, modelDataBindings.TableName);

            //Type t = typeof(T);
            //PropertyInfo keyProp = t.GetProperty(modelDataBindings.KeyFieldName, BindingFlags.Public | BindingFlags.Instance);

            // Set a unique key for this model
            //string newKey = SQLHelper.GetUniqueKey();
            //keyProp.SetValue(model, newKey);

            //sqlgen.InsertFromModel<T>(model);
            //sqlgen.DoNotFullyQualifyFields = true;

            service.SprocInsert(model);

            return model;
        }

        public void Update(T model)
        {
            //SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Update, modelDataBindings.TableName);
            //sqlgen.DoNotFullyQualifyFields = true;
            //sqlgen.UpdateFromModel<T>(model);

            service.SprocUpdate(model);
        }

        public void Destroy(T model)
        {
            //SqlGenerator sqlgen = new SqlGenerator(SqlGenerator.SqlTypes.Delete, modelDataBindings.TableName);
            //sqlgen.DoNotFullyQualifyFields = true;
            //sqlgen.DeleteFromModel<T>(model);

            service.SprocDelete(model);
        }

        public virtual IEnumerable<ForeignKeyReference> GetForeignKeyList(ForeignKeyAttribute fkAttribute)
        {
            Type mtype = typeof(T);

            DatabaseModelBindings bindings = mtype.GetDatabaseBindings();
            string keyName = bindings.KeyFieldName;

            PropertyInfo keyProp = null;
            PropertyInfo descProp = null;

            

            //if (mtype.Name.Contains("InventoryLocation"))
            //    Console.WriteLine("Stop Here");

            int currentDescScore = -1;
            foreach (PropertyInfo pi in mtype.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (pi.PropertyType == typeof(string) || pi.PropertyType == typeof(int) || pi.PropertyType == typeof(Int64) || pi.PropertyType == typeof(Int64?)) //TE: Allows the grabbing of int keys that have lists
                {
                    if (keyProp == null)
                    {
                        if (pi.Name == keyName)
                            keyProp = pi;

                        
                    }

                    if (mtype.Name.Contains("Rbf"))
                    {
                        Console.Write("Break Point");
                    }
                    //TODO: JRB - 2/7/17 - We need to set the ForeignKeyDisplayField property for 
                    //      all model foreign keys so that we can get rid of this weighting system.  
                    //      This probably was good for what it was used for at the time but now
                    //      we need more specificity.  So that nothing is broken if a match to 
                    //      the attribute property is found a score of 1000 will be applied.
                    int propertyScore = 0;

                    propertyScore = pi.Name == fkAttribute.ForeignKeyDisplayField ? 1000 : 0;

                    if (keyProp != pi) propertyScore += 1;
                    if (pi.Name.Contains("DisplayName")) propertyScore += 20;
                    else if (pi.Name.Contains("Description")) propertyScore += 10;
                    else if (pi.Name.Contains("Name")) propertyScore += 5;

                    if (propertyScore > currentDescScore)
                    {
                        descProp = pi;
                        currentDescScore = propertyScore;
                    }
                }
            }

            List<ForeignKeyReference> references = new List<ForeignKeyReference>();

            if (descProp != null && keyProp != null)
            {
                IEnumerable<T> models = Read();
                foreach (T m in models)
                {
                    ForeignKeyReference fr = new ForeignKeyReference();
                    fr.Key = keyProp.GetValue(m).ToString(); //can be an int, this is how it wants to parse
                    fr.Description = descProp.GetValue(m).ToString();
                    fr.BaseData = m;

                    references.Add(fr);
                }
            }

            return references;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) { }
    }
}
