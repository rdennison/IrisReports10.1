using Iris10ReportUI.GridBuilder.Interface;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Iris10ReportUI.GridBuilder.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IrisWidgetFactory Iris(this HtmlHelper helper)
        {
            return new IrisWidgetFactory(helper);
        }

        public static IrisWidgetFactory IrisToolBar(this HtmlHelper helper)
        {
            return new IrisWidgetFactory(helper);
        }
        public static IrisWidgetFactory<T> Iris<T>(this HtmlHelper helper) where T : class
        {
            return new IrisWidgetFactory<T>(helper);
        }

        public static ChildWidgetFactory<T> IrisChild<T>(this HtmlHelper helper) where T : class
        {
            return new ChildWidgetFactory<T>(helper);
        }

        public static IrisWidgetFactory<T> IrisTreeList<T>(this HtmlHelper helper) where T : class
        {
            return new IrisWidgetFactory<T>(helper);
        }

        public static IrisWidgetFactory IrisTreeView(this HtmlHelper helper)
        {
            return new IrisWidgetFactory(helper);
        }
    }
}
