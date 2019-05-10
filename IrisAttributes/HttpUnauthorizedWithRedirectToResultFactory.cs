using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IrisAttributes
{
    public class HttpUnauthorizedWithRedirectToResultFactory
    {
        public HttpUnauthorizedWithRedirectToResultBase GetInstance(string area,
               string controller, string actionOrViewName)
        {
            if (string.IsNullOrWhiteSpace(actionOrViewName))
                throw new ArgumentException("You must set an actionOrViewName");

            if (string.IsNullOrWhiteSpace(controller))
                return new HttpUnauthorizedWithRedirectToViewResult(actionOrViewName, area);
            return new HttpUnauthorizedWithRedirectToRouteResult(actionOrViewName, controller, area);
        }
    }
}