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
            var parameters = controllerAction.GetRouteParameters();
            return helper.Action(parameters.ActionName, parameters.ControllerName, parameters.RouteValues);
        }

        public static string AbsoluteAction<TController>(
            this UrlHelper helper,
            Expression<Func<TController, ActionResult>> controllerAction, string scheme)
            where TController : Controller
        {
            var parameters = controllerAction.GetRouteParameters();
            return helper.Action(parameters.ActionName, parameters.ControllerName, parameters.RouteValues, scheme, null);
        }
    }
}