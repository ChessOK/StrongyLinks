using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

using ChessOk.StrongyLinks.Internals;

namespace ChessOk.StrongyLinks
{
    public static class UrlHelperExtensions
    {
        public static string Action<TController>(
            this UrlHelper helper,
            Expression<Func<TController, ActionResult>> controllerAction)
            where TController : Controller
        {
            RouteValueDictionary routeValues;
            string controllerName;
            string actionName;
            controllerAction.GetRouteParameters(out actionName, out controllerName, out routeValues);

            return helper.Action(actionName, controllerName, routeValues);
        }

        public static string AbsoluteAction<TController>(
            this UrlHelper helper,
            Expression<Func<TController, ActionResult>> controllerAction, string scheme)
            where TController : Controller
        {
            RouteValueDictionary routeValues;
            string controllerName;
            string actionName;
            controllerAction.GetRouteParameters(out actionName, out controllerName, out routeValues);

            return helper.Action(actionName, controllerName, routeValues, scheme, null);
        }
    }
}
