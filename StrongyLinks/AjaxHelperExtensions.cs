using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;

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
            var parameters = controllerAction.GetRouteParameters();
            return helper.BeginForm(parameters.ActionName, parameters.ControllerName, parameters.RouteValues, options);
        }

        public static MvcHtmlString ActionLink<TController>(
            this AjaxHelper helper,
            string linkText,
            Expression<Func<TController, ActionResult>> controllerAction,
            AjaxOptions options,
            object htmlAttributes = null)
            where TController : Controller
        {
            var parameters = controllerAction.GetRouteParameters();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            return helper.ActionLink(linkText, parameters.ActionName, parameters.ControllerName,
                parameters.RouteValues, options, attributes);
        }

        public static MvcForm BeginForm<TController>(this AjaxHelper helper,
                                                     Expression<Func<TController, ActionResult>> controllerAction,
                                                     AjaxOptions ajaxOptions,
                                                     object htmlAttributes)
            where TController : Controller
        {
            var parameters = controllerAction.GetRouteParameters();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            return helper.BeginForm(parameters.ActionName, parameters.ControllerName, parameters.RouteValues, ajaxOptions, attributes);
        }
    }
}