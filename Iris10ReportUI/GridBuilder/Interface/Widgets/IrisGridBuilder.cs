using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using System.Web;
using CoreDomain;
using IrisAttributes;
using System.Collections.Generic;
using Iris10ReportUI.GridBuilder.Extensions;
using Iris10ReportUI.Models;

namespace Iris10ReportUI.GridBuilder.Interface.Widgets
{
    public sealed class IrisGridBuilder<T> : IHtmlString where T : class
    {
        readonly HtmlHelper _helper;
        readonly GridBuilder<T> _builder;
        readonly Type _modelType;
        readonly EditorTypeAttribute _editor;
        readonly UserConfigHelper _configHelper = new UserConfigHelper();
        readonly UserFilterHelper _filterHelper = new UserFilterHelper();

        public IrisGridBuilder(HtmlHelper htmlHelper, string name, bool partial)
        {
            _helper = htmlHelper;
            _modelType = typeof(T);
            _editor = _modelType.GetCustomAttribute<EditorTypeAttribute>();
            var factory = new WidgetFactory(htmlHelper);
            _builder = factory.Grid<T>();

            // Render our actual grid
            CreateGrid(name, partial);
        }

        private void CreateGrid(string name, bool partial)
        {
            Dictionary<string, Guid> modelGuids = (Dictionary<string, Guid>) HttpRuntime.Cache["ModelGuids"];
            var modelName = _modelType.Name.Contains("ViewModel") ? _modelType.Name.Replace("ViewModel","") : _modelType.Name.Replace("Model","");
        
            GridSetupAttribute gridSetupAttribute = _modelType.GetCustomAttribute<GridSetupAttribute>();
            NoAutoSyncAttribute noSync = _modelType.GetCustomAttribute<NoAutoSyncAttribute>();
            AddSpecialColumnAttribute specialColumn = _modelType.GetCustomAttribute<AddSpecialColumnAttribute>();
            IrisGridColumnAttribute specialName = _modelType.GetCustomAttribute<IrisGridColumnAttribute>();
            if (_modelType.GetCustomAttribute<BatchModeAttribute>() == null)
            {
                _configHelper.SetUserConfig(modelName);
                _filterHelper.SetUserFilter(modelName);
            }

            if (name == "null")
                _builder.Name("Grid_" + modelName);
            else
                _builder.Name(name);

            AjaxDataSourceBuilder<T> dataSourceBuilder = null;
            _builder.DataSource(ds =>
            {
                dataSourceBuilder = ds.Ajax();
                //TODO: Revisit this, autosync kicks you out of edit mode in batch mode
                //if(noSync == null)
                //    dataSourceBuilder.AutoSync(true);
                dataSourceBuilder.Model(model => { model.Id(o => o.GetDatabaseBindings().KeyFieldName); });
                dataSourceBuilder.ServerOperation(true);
                dataSourceBuilder.Read(r => r.Action("Read", (specialName?.ScreenName != null) ? specialName?.ScreenName : modelName).Type(HttpVerbs.Get));
                dataSourceBuilder.Create(c => c.Action("Create", (specialName?.ScreenName != null) ? specialName?.ScreenName : modelName).Type(HttpVerbs.Post));
                dataSourceBuilder.Update(u => u.Action("Update", (specialName?.ScreenName != null) ? specialName?.ScreenName : modelName).Type(HttpVerbs.Post));
                dataSourceBuilder.Destroy(d => d.Action("Destroy", (specialName?.ScreenName != null) ? specialName?.ScreenName : modelName).Type(HttpVerbs.Post));
                dataSourceBuilder.PageSize(200);
                if ( (_editor != null && _editor.InCell) || _modelType.GetCustomAttribute<BatchModeAttribute>() != null)
                    dataSourceBuilder.Batch(true);
            });

            //if(gridSetupAttribute == null)
            //{
            //    _builder.ToolBar(t =>
            //    {
            //        //t.Create();
            //        t.Template("<text><input class='k-textbox' value=\"Search...\" onfocus=\"if (this.value=='Search strings...') this.value='';\" onblur=\"this.value = this.value==''?'Search...':this.value;\" id='searchbox' onkeyup=\"Search()\" /></text>");
            //    });
            //}
            
            _builder.Columns(c =>
            {
                c.Select().Width(50).MinResizableWidth(50).HeaderHtmlAttributes(new { @id = "headerChkBox" });//.Locked(true);
                //if (gridSetupAttribute != null)
                //{
                //c.Command(command =>
                //    {
                //        command.Custom("recordSelecter").Text(">");
                //    }).Width(100).Title("Select Row").HeaderHtmlAttributes(new { @class = "cantedit" });
                //}

                if (specialColumn != null)
                {
                    c.Command(col =>
                    {
                        col.Custom(specialColumn.ButtonName).Text(specialColumn.ColumnName);
                    }).Width(120).Title(specialColumn.ColumnName).HeaderHtmlAttributes(new { @class = "cantedit" });
                }

                dataSourceBuilder.Aggregates(a =>
                {
                    dataSourceBuilder.Model(m =>
                    {
                        PropertyInfo[] modelProperties = _modelType.GetProperties(); //Incase we want to go with model attributes
                        foreach (var p in modelProperties)
                        {
                            var columnAttributes = p.GetCustomAttribute<IrisGridColumnAttribute>() ?? IrisGridColumnAttribute.Default;
                            
                            if (p.Name == _modelType.GetDatabaseBindings().KeyFieldName)
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
                            ForeignKeyAttribute fkAttribute = p.GetCustomAttribute<ForeignKeyAttribute>();
                            UserDefinedAttribute userFieldAttribute = p.GetCustomAttribute<UserDefinedAttribute>();
                            AggregateAttribute aggregateOptions = p.GetCustomAttribute<AggregateAttribute>();
                            if (fkAttribute != null)
                            {
                                column = c.ForeignKey(p.Name, fkAttribute.GetForeignKeyList());
                                //column.EditorTemplateName("IRISGridForeignKey");
                            }else if(userFieldAttribute != null)
                            {
                               
                                    column = c.Bound(p.Name);
                                
                                
                            }
                            else //If it isnt a foreign key check binding types for checkboxes
                            {
                                if (p.PropertyType == typeof(byte))
                                    column = c.Bound(p.Name).ClientTemplate("<input type='checkbox' #=" + p.Name + " ? checked='checked' : '' # class='chkbx' disabled='disabled'></input>");
                                else if (p.PropertyType == typeof(bool))
                                    column = c.Bound(p.Name).EditorTemplateName("CheckBoxBool").ClientTemplate("<input type='checkbox' #=" + p.Name + " ? checked='checked' : '' # class='chkbx' disabled='disabled'></input>");
                                else
                                {
                                    column = c.Bound(p.Name);
                                }
                                    
                            }

                            //Setup our formatting for this column
                            if (string.IsNullOrEmpty(columnAttributes.Format) == false)
                                column.Format(columnAttributes.Format);

                            //Setup our custom editor templates if they are needed
                            if (p.PropertyType == typeof(DateTime?))
                                column.EditorTemplateName("IRISDate");

                            if (p.GetCustomAttribute<FilterDropdownAttribute>() != null)
                            {
                                if (p.GetCustomAttribute<FilterDropdownAttribute>().Description == true)
                                {
                                    column.EditorTemplateName("DescriptionDropdown");
                                }
                                if (p.GetCustomAttribute<FilterDropdownAttribute>().Operator == true)
                                {
                                    column.EditorTemplateName("OperatorDropdown");
                                }
                                if (p.GetCustomAttribute<FilterDropdownAttribute>().Operator2 == true)
                                {
                                    column.EditorTemplateName("AndOrDropdown");
                                }
                                if (p.GetCustomAttribute<FilterDropdownAttribute>().Value1 == true)
                                {
                                    column.EditorTemplateName("Value1");
                                }
                                if (p.GetCustomAttribute<FilterDropdownAttribute>().Value2 == true)
                                {
                                    column.EditorTemplateName("Value2");
                                }

                                if (p.GetCustomAttribute<FilterDropdownAttribute>().Group1 == true)
                                {
                                    column.EditorTemplateName("Group1");
                                }
                                if (p.GetCustomAttribute<FilterDropdownAttribute>().Group2 == true)
                                {
                                    column.EditorTemplateName("Group2");
                                }

                            }

                            if (p.PropertyType == typeof(decimal?) && p.GetCustomAttribute<DataTypeAttribute>()?.CustomDataType == "money")
                                column.EditorTemplateName("Currency");
                            else if(p.PropertyType == typeof(decimal?))
                                column.EditorTemplateName("Number");

                            //Setup custom attributes
                            if (p.GetCustomAttribute<RequiredAttribute>() != null && p.GetCustomAttribute<CantEditAttribute>() != null)
                                column.HeaderHtmlAttributes(new { @Class = "required-field cantedit" });

                            else if (p.GetCustomAttribute<RequiredAttribute>() != null)
                                column.HeaderHtmlAttributes(new { @Class = "required-field" });

                            else if (p.GetCustomAttribute<CantEditAttribute>() != null)
                                column.HeaderHtmlAttributes(new { @Class = "cantedit" });

                            else if (p.GetCustomAttribute<CalculatedFieldAttribute>() != null && p.GetCustomAttribute<CalculatedFieldAttribute>().Init)
                                column.HeaderHtmlAttributes(new { @Class = "calculatebase" });

                            column.Lockable(false).Locked(false);

                            var aggregateTemplate = "";
                            if(aggregateOptions != null)
                            {
                                if (aggregateOptions.AllowSum)
                                    aggregateTemplate += "<div id='" + p.Name + "Sum'></div>";
                                if (aggregateOptions.AllowCount)
                                    aggregateTemplate += "<div id='" + p.Name + "Count'></div>";
                                if (aggregateOptions.AllowMin)
                                    aggregateTemplate += "<div id='" + p.Name + "Minimum'></div>";
                                if (aggregateOptions.AllowMax)
                                    aggregateTemplate += "<div id='" + p.Name + "Maximum'></div>";
                                if (aggregateOptions.AllowAvg)
                                    aggregateTemplate += "<div id='" + p.Name + "Average'></div>";
                            }
                            
                            column.ClientFooterTemplate(aggregateTemplate);
                            column.Width(columnAttributes.Width);
                            column.Hidden(columnAttributes.Hidden);
                            column.IncludeInMenu(columnAttributes.IncludeInMenu);

                            if (p.GetCustomAttribute<DisplayAttribute>() != null && userFieldAttribute == null)
                                column.Title(p.GetCustomAttribute<DisplayAttribute>().Name);

                            if (p.GetCustomAttribute<DisplayFormatAttribute>() != null && userFieldAttribute == null)
                                column.Format(p.GetCustomAttribute<DisplayFormatAttribute>().DataFormatString);
                        }
                    });
                });

            });

            _builder.Selectable(s => { s.Type(GridSelectionType.Cell); });
            _builder.Mobile();
            _builder.Navigatable();
            _builder.Resizable(resize => resize.Columns(true));
            if (!partial)
            {
                if (gridSetupAttribute == null)
                {
                    var pageSizes = new List<int> { 5, 10, 50, 150, 200 };
                    if (specialName?.Grouping != false)
                        _builder.Groupable();
                    _builder.Pageable(p => p.Refresh(true).PageSizes(pageSizes).ButtonCount(5));
                    _builder.HtmlAttributes(new { style = "height: 500px" });
                    _builder.Scrollable(a => a.Height("100%"));
                    _builder.Reorderable(reorder => reorder.Columns(true));
                    _builder.Sortable(x => x.SortMode(GridSortMode.MultipleColumn));
                    _builder.Events(x => { x.DataBound(name + "DataBound"); });
                    if (specialName?.ColumnFilter != false)
                    {
                        _builder.Filterable();
                        _builder.ColumnMenu(m => m.Filterable(true).Sortable(false).Columns(false));//.Events(e => e.ColumnMenuInit("ColumnMenuInit"));

                    }
                }
                else
                {
                    _builder.Scrollable(a => a.Height(gridSetupAttribute.GridHeight));
                }
                _builder.Pdf(p => p.AllPages().FileName(HttpContext.Current.Session["CurrentUserName"].ToString().Substring(0, HttpContext.Current.Session["CurrentUserName"].ToString().IndexOf("@")) + modelName+ ".pdf"));
                _builder.Excel(e => e.AllPages(true).FileName(HttpContext.Current.Session["CurrentUserName"].ToString().Substring(0, HttpContext.Current.Session["CurrentUserName"].ToString().IndexOf("@")) + modelName + ".xlsx"));
                _builder.Editable(e =>
                {
                    if (_editor == null || _editor.PopOut)
                    {
                        e.Mode(GridEditMode.PopUp);
                        e.TemplateName("IrisEditor");
                    }
                    else if (_editor.InLine)
                    {
                        e.Mode(GridEditMode.InLine);
                    }
                    else if (_editor.InCell)
                    {
                        e.Mode(GridEditMode.InCell);
                        if (_modelType.GetCustomAttribute<BatchModeAttribute>() == null)
                        {
                            _builder.Events(x => x.Edit(name + "Edit"));
                        }
                    }
                    e.CreateAt(GridInsertRowPosition.Top);
                    e.DisplayDeleteConfirmation(false);
                });
            }

            _builder.Editable(e =>
            {
                if (_editor == null || _editor.PopOut)
                {
                    e.Mode(GridEditMode.PopUp);
                    e.TemplateName("IrisEditor");
                }
                else if (_editor.InLine)
                {
                    e.Mode(GridEditMode.InLine);
                }
                else if (_editor.InCell)
                {
                    e.Mode(GridEditMode.InCell);
                    if (_modelType.GetCustomAttribute<BatchModeAttribute>() == null)
                    {
                        _builder.Events(x => x.Edit(name + "Edit"));
                    }
                }
                e.CreateAt(GridInsertRowPosition.Top);
                e.DisplayDeleteConfirmation(false);
            });

            if (_modelType.GetCustomAttribute<HasChildGridAttribute>() != null)
            {
                _builder.ClientDetailTemplateId("detailTemplate");
            }
        }

        private GridBoundColumnBuilder<T> CreateUserField(GridColumnFactory<T> c,string name, string type, string title)
        {

            switch (type)
            {
                case "String":
                    return c.Bound(name).Title(title).Width(120);
                case "Date":
                    return c.Bound(name).Title(title).EditorTemplateName("IRISDate").Width(150).ClientTemplate("#=(" + name + " == null) ? ' ' : kendo.toString(kendo.parseDate(" + name + "), 'MM/dd/yyyy')#");
                case "Numeric":
                    return c.Bound(name).Title(title).EditorTemplateName("Number").HtmlAttributes(new { style = "text-align: left" }).Width(120);
                case "Checkbox":
                    return c.Bound(name).Title(title).HtmlAttributes(new { style = "text-align: center" }).ClientTemplate("<input type='checkbox' #=" + name + " ? checked='checked' : '' # class='chkbx' disabled='disabled'></input>").Width(120);
                case "Lookup": //TODO: need to design this
                               //c.ForeignKey(p.Name, (System.Collections.IEnumerable) ViewData["LookupUser" + (i + 1).ToString()], "Field1", "Field2").Width(350).EditorTemplateName("IRISGridForeignKey");
                    break;
            }
            return null;
        }

        public string ToHtmlString()
        {
            return _builder.ToHtmlString();
        }

    }

}