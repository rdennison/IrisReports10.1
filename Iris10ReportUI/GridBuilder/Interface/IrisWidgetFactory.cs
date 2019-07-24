using System.Web.Mvc;
using Iris10ReportUI.GridBuilder.Interface.Widgets;

namespace Iris10ReportUI.GridBuilder.Interface
{
    public sealed class IrisWidgetFactory
    {
        private HtmlHelper htmlHelper;
        public IrisWidgetFactory(HtmlHelper helper)
        {
            htmlHelper = helper;
        }

        //public ReportViewerBuilder ReportViewer()
        //{
        //    return new ReportViewerBuilder(htmlHelper);
        //}
    }
    public sealed class IrisWidgetFactory<T> where T : class
    {
        private HtmlHelper htmlHelper;

        public IrisWidgetFactory(HtmlHelper helper)
        {
            htmlHelper = helper;
        }

       

        public ReportListGridBuilder<T> ReportListGrid()
        {
            return new ReportListGridBuilder<T>(htmlHelper);
        }

        public IrisGridBuilder<T> Grid(string name = "null", bool partial = false)
        {
            return new IrisGridBuilder<T>(htmlHelper, name, partial);
        }

    }
    
    public sealed class ChildWidgetFactory<T> where T : class
    {
        private HtmlHelper htmlHelper;

        public ChildWidgetFactory(HtmlHelper helper)
        {
            htmlHelper = helper;
        }

   
    }

}
