using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Web.Mvc;

namespace ChessOk.StrongyLinks.Internals
{
    public static class RoutesHelper
    {
        public static RouteParameters GetRouteParameters<TController>(
            this Expression<Func<TController, ActionResult>> controllerAction)
            where TController : Controller
        {
            var descriptor = ActionDescriptorCache.GetActionDescriptor(controllerAction);

            var routeValues = new RouteValueDictionary();
            routeValues.Add("area", descriptor.AreaName);

            FillRouteValues(controllerAction, routeValues);

            return new RouteParameters
            {
                ActionName = descriptor.ActionName,
                ControllerName = descriptor.ControllerName,
                RouteValues = routeValues
            };
        }

        private static void FillRouteValues<TController>(
            Expression<Func<TController, ActionResult>> controllerAction, RouteValueDictionary routeValues)
            where TController : Controller
        {
            var member = controllerAction.Body as MethodCallExpression;
            if (member == null)
            {
                throw new InvalidOperationException("controllerAction должен указывать на метод");
            }

            // Готовим route values
            var argumentNames = member.Method.GetParameters();

            for (int i = 0; i < argumentNames.Length; i++)
            {
                var value = GetArgumentValue(member.Arguments[i]);

                // RouteValueDictionary can't work with collections, but we can teach it.
                // It looks a bit messy in the URL, but it works.
                var enumerable = value as IEnumerable;
                if (enumerable != null)
                {
                    var index = 0;
                    foreach (var o in enumerable)
                    {
                        routeValues.Add(string.Format("{0}[{1}]", argumentNames[i].Name, index++), o);
                    }
                }
                else
                {
                    routeValues.Add(argumentNames[i].Name, value);
                }
            }
        }

        private static object GetArgumentValue(Expression expression)
        {
            var constantExpression = expression as ConstantExpression;

            if (constantExpression != null)
            {
                return constantExpression.Value;
            }

            try
            {
                return CachedExpressionCompiler.Evaluate(expression);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}