using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

using ChessOk.StrongyLinks.Internals;

namespace ChessOk.StrongyLinks
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ActionLink<TController>(
            this HtmlHelper helper,
            string linkText,
            Expression<Func<TController, ActionResult>> controllerAction,
            IDictionary<string, object> htmlAttributes = null)
            where TController : Controller
        {
            RouteValueDictionary routeValues;
            string controllerName;
            string actionName;
            controllerAction.GetRouteParameters(out actionName, out controllerName, out routeValues);

            return helper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes);
        }

        public static MvcHtmlString Action<TController>(this HtmlHelper helper,
                                                        Expression<Func<TController, ActionResult>> controllerAction)
            where TController : Controller
        {
            RouteValueDictionary routeValues;
            string controllerName;
            string actionName;
            controllerAction.GetRouteParameters(out actionName, out controllerName, out routeValues);

            return helper.Action(actionName, controllerName, routeValues);
        }

        public static MvcForm BeginForm<TController>(this HtmlHelper helper,
                                                     Expression<Func<TController, ActionResult>> controllerAction,
                                                     FormMethod method = FormMethod.Post,
                                                     object htmlAttributes = null)
            where TController : Controller
        {
            RouteValueDictionary routeValues;
            string controllerName;
            string actionName;
            controllerAction.GetRouteParameters(out actionName, out controllerName, out routeValues);

            return helper.BeginForm(actionName, controllerName, method, htmlAttributes);
        }
    }
}
