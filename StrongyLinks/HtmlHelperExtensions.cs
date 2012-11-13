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
            var parameters = controllerAction.GetRouteParameters();
            return helper.ActionLink(linkText, parameters.ActionName,
                parameters.ControllerName, parameters.RouteValues, htmlAttributes);
        }

        public static MvcHtmlString Action<TController>(this HtmlHelper helper,
                                                        Expression<Func<TController, ActionResult>> controllerAction)
            where TController : Controller
        {
            var parameters = controllerAction.GetRouteParameters();
            return helper.Action(parameters.ActionName, parameters.ControllerName, parameters.RouteValues);
        }

        public static MvcForm BeginForm<TController>(this HtmlHelper helper,
                                                     Expression<Func<TController, ActionResult>> controllerAction,
                                                     FormMethod method = FormMethod.Post,
                                                     object htmlAttributes = null)
            where TController : Controller
        {
            var parameters = controllerAction.GetRouteParameters();
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            return helper.BeginForm(parameters.ActionName, parameters.ControllerName,
                parameters.RouteValues, method, attributes);
        }
    }
}