using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using System.Web;
using CoreDomain;
using IrisAttributes;
using Iris10ReportUI.GridBuilder.Extensions;

namespace Iris10ReportUI.GridBuilder.Interface.Widgets
{
    public sealed class ReportListGridBuilder<T> : IHtmlString where T : class
    {
        readonly HtmlHelper _helper;
        readonly GridBuilder<T> _builder;
        readonly Type _modelType;
        readonly EditorTypeAttribute _editor;
        readonly UserConfigHelper _configHelper = new UserConfigHelper();
        readonly UserFilterHelper _filterHelper = new UserFilterHelper();

        public ReportListGridBuilder(HtmlHelper htmlHelper)
        {
            _helper = htmlHelper;
            _modelType = typeof(T);
            _editor = _modelType.GetCustomAttribute<EditorTypeAttribute>();
            var factory = new WidgetFactory(htmlHelper);
            _builder = factory.Grid<T>();

            // Render our actual grid
            CreateGrid();
        }

        private void CreateGrid()
        {
            var modelName = _modelType.Name.Replace("Model","");
            GridSetupAttribute gridSetupAttribute = _modelType.GetCustomAttribute<GridSetupAttribute>();
            _builder.Name("ReportListGrid");

            AjaxDataSourceBuilder<T> dataSourceBuilder = null;
            _builder.DataSource(ds =>
            {
                dataSourceBuilder = ds.Ajax();
                dataSourceBuilder.Model(model => { model.Id(o => o.GetDatabaseBindings().KeyFieldName); });
                dataSourceBuilder.ServerOperation(true);
                dataSourceBuilder.Read(r => r.Action("Read", modelName).Type(HttpVerbs.Get));
                dataSourceBuilder.Create(c => c.Action("Create", modelName).Type(HttpVerbs.Post));
                dataSourceBuilder.Update(u => u.Action("Update", modelName).Type(HttpVerbs.Post));
                dataSourceBuilder.Destroy(d => d.Action("Destroy", modelName).Type(HttpVerbs.Post));
            });
            
            _builder.Columns(c =>
            {
                dataSourceBuilder.Aggregates(a =>
                {
                    dataSourceBuilder.Model(m =>
                    {
                        PropertyInfo[] modelProperties = _modelType.GetProperties(); //Incase we want to go with model attributes
                        foreach (var p in modelProperties)
                        {
                            var columnAttributes = p.GetCustomAttribute<IrisGridColumnAttribute>() ?? IrisGridColumnAttribute.Default;
                            
                            if (p.Name.EndsWith("User_Key", StringComparison.InvariantCultureIgnoreCase)) //Give it a fake ID
                                m.Id(p.Name);
                            else
                            {
                                DataSourceModelFieldDescriptorBuilder<object> fieldBuilder = m.Field(p.Name, p.PropertyType);

                                if (p.Name.EndsWith("User_Key", StringComparison.InvariantCultureIgnoreCase))
                                    fieldBuilder.DefaultValue(columnAttributes.GetDefaultValue()); //changed for now
                                else if (columnAttributes.DefaultValue != null)
                                    fieldBuilder.DefaultValue(columnAttributes.GetDefaultValue());

                                if (columnAttributes.ReadOnly)
                                    fieldBuilder.Editable(false);
                            }
                            
                            GridBoundColumnBuilder<T> column = null;

                            if(p.Name == "Favorite")
                            {
                                column = c.Bound(p.Name).ClientTemplate("<input type='checkbox' #=" + p.Name + " ? checked='checked' : '' # class='fav'></input>");
                            }
                            else if (p.Name == "CustomReport")
                            {
                                column = c.Bound(p.Name).ClientTemplate("#=" + p.Name + " ? 'CR' : '' #").Editable("false");
                            }
                            else
                            {
                                column = c.Bound(p.Name).Editable("false");
                            }
                            
                            column.Width(columnAttributes.Width);
                            column.Hidden(columnAttributes.Hidden);

                            if (p.GetCustomAttribute<DisplayFormatAttribute>() != null)
                                column.Format(p.GetCustomAttribute<DisplayFormatAttribute>().DataFormatString);
                        }
                    });
                });

            });

            _builder.Selectable(s => { s.Type(GridSelectionType.Row); });
            _builder.Mobile();
            _builder.Navigatable();
            _builder.HtmlAttributes(new { style = "width: 350px" });
            _builder.Scrollable(a => a.Height("100%"));
            _builder.Events(x => { x.DataBound("ReportListDataBound"); });
            _builder.Editable(e => e.Mode(GridEditMode.InLine).Enabled(true));
        }

        public string ToHtmlString()
        {
            return _builder.ToHtmlString();
        }

    }

}