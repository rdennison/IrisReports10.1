
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace IrisAttributes
{
    /// <summary>
    ///   Use this class instead of Authorize to redirect to a spcific view on unauthorized access.
    ///   If this attribute is used on a child action, it does the base result else, it redirect
    ///   to the specified view. The default view is UnauthorizedAccess, but can be overriden with 
    ///   the ActionOrViewName property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizeWithRedirectToViewAttribute : AuthorizeAttribute
    {
        #region Private Fields

        private const string DefaultActionOrViewName = "UnauthorizedAccess";
        private string _actionOrViewName;
        #endregion

        #region Properties

        /// <summary>
        ///   The name of the view to render on authorization failure. Default is 
        ///   "UnauthorizedAccess".
        /// </summary>
        public string ActionOrViewName
        {
            get
            {
                return string.IsNullOrWhiteSpace(_actionOrViewName)
                           ? DefaultActionOrViewName
                           : _actionOrViewName;
            }
            set { _actionOrViewName = value; }
        }

        public string Controller { get; set; }
        public string Area { get; set; }

        #endregion

        #region Overrides

        /// <summary>
        ///   Processes HTTP requests that fail authorization.
        /// </summary>
        /// <param name="filterContext"> Encapsulates the information for using 
        ///   <see cref="T:System.Web.Mvc.AuthorizeAttribute" /> . The 
        ///   <paramref name="filterContext" /> object contains the controller, HTTP context, 
        ///   request context, action result, and route data. </param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.IsChildAction)
                base.HandleUnauthorizedRequest(filterContext);
            else
            {
                var factory = new HttpUnauthorizedWithRedirectToResultFactory();
                filterContext.Result = factory.GetInstance(
                                                           Area,
                                                           Controller,
                                                           ActionOrViewName);
            }
        }

        #endregion
    }
}
