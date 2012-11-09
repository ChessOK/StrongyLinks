using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;

using ChessOk.StrongyLinks.Internals;

namespace ChessOk.StrongyLinks
{
    public static class AjaxHelperExtensions
    {
        public static MvcForm BeginForm<TController>(
            this AjaxHelper helper,
            Expression<Func<TController, ActionResult>> controllerAction,
            AjaxOptions options)
            where TController : Controller
        {
            RouteValueDictionary routeValues;
            string controllerName;
            string actionName;
            controllerAction.GetRouteParameters(out actionName, out controllerName, out routeValues);

            return helper.BeginForm(actionName, controllerName, routeValues, options);
        }

        public static MvcHtmlString ActionLink<TController>(
            this AjaxHelper helper,
            string linkText,
            Expression<Func<TController, ActionResult>> controllerAction,
            AjaxOptions options,
            object htmlAttributes = null)
            where TController : Controller
        {
            RouteValueDictionary routeValues;
            string controllerName;
            string actionName;
            controllerAction.GetRouteParameters(out actionName, out controllerName, out routeValues);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return helper.ActionLink(linkText, actionName, controllerName, routeValues, options, attributes);
        }

        public static MvcForm BeginForm<TController>(this AjaxHelper helper,
                                                     Expression<Func<TController, ActionResult>> controllerAction,
                                                     AjaxOptions ajaxOptions,
                                                     object htmlAttributes)
            where TController : Controller
        {
            RouteValueDictionary routeValues;
            string controllerName;
            string actionName;
            controllerAction.GetRouteParameters(out actionName, out controllerName, out routeValues);

            return helper.BeginForm(actionName, controllerName, null, ajaxOptions, htmlAttributes);
        }
    }
}
