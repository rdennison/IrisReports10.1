using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace IrisAttributes
{
    public abstract class HttpUnauthorizedWithRedirectToResultBase : HttpUnauthorizedResult
    {
        protected ActionResult _result;

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (context.HttpContext.Request.IsAuthenticated)
            {
                context.HttpContext.Response.StatusCode = 200;
                InitializeResult(context);
                _result.ExecuteResult(context);
            }
            else
                base.ExecuteResult(context);
        }

        protected abstract void InitializeResult(ControllerContext context);
    }

    public class HttpUnauthorizedWithRedirectToViewResult
             : HttpUnauthorizedWithRedirectToResultBase
    {
        #region Ctors

        public HttpUnauthorizedWithRedirectToViewResult(string viewName, string area)
        {
            _viewName = string.IsNullOrWhiteSpace(viewName) ? viewName : viewName.Trim();
            _area = string.IsNullOrWhiteSpace(area) ? area : area.Trim();
        }

        #endregion

        #region Private Fields

        private readonly string _area;
        private readonly string _viewName;

        #endregion

        #region Overrides of HttpUnauthorizedWithRedirectToResultBase

        protected override void InitializeResult(ControllerContext context)
        {
            SetAreaRouteData(context);
            _result = new ViewResult
            {
                ViewName = _viewName,
            };
        }

        #endregion

        #region Methods

        private void SetAreaRouteData(ControllerContext context)
        {
            if (context.RequestContext.RouteData.DataTokens.ContainsKey("area"))
            {
                if (!string.IsNullOrWhiteSpace(_area))
                    context.RequestContext.RouteData.DataTokens["area"] = _area;
            }
            else
                context.RequestContext.RouteData.DataTokens.Add("area", _area);
        }

        #endregion
    }

    public class HttpUnauthorizedWithRedirectToRouteResult
                 : HttpUnauthorizedWithRedirectToResultBase
    {
        #region Ctors

        public HttpUnauthorizedWithRedirectToRouteResult(string action, string controller, string area)
        {
            _action = string.IsNullOrWhiteSpace(action) ? action : action.Trim();
            _controller = string.IsNullOrWhiteSpace(controller) ? controller : controller.Trim();
            _area = string.IsNullOrWhiteSpace(area) ? area : area.Trim();
        }

        #endregion

        #region Private Fields

        private readonly string _action;
        private readonly string _area;
        private readonly string _controller;

        #endregion

        #region Overrides of HttpUnauthorizedWithRedirectToResultBase

        protected override void InitializeResult(ControllerContext context)
        {
            _result = new RedirectToRouteResult(new RouteValueDictionary
                                                {
                                                    {"area", _area},
                                                    {"controller", _controller},
                                                    {"action", _action}
                                                });
        }

        #endregion
    }

   
}