using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChessOk.StrongyLinks.Internals
{
    public static class RoutesHelper
    {
        public static void GetControllerAndActionNames<TController>(this Expression<Func<TController, ActionResult>> controllerAction,
            out string controllerName, out string actionName)
        {
            var member = controllerAction.Body as MethodCallExpression;
            if (member == null)
            {
                throw new InvalidOperationException("controllerAction должен указывать на метод");
            }

            // Определяем имя экшена
            var actionNameAttribute =
                member.Method.GetCustomAttributes(typeof(ActionNameAttribute), true).SingleOrDefault() as ActionNameAttribute;

            actionName = actionNameAttribute != null ? actionNameAttribute.Name : member.Method.Name;

            // Определяем имя контроллера
            controllerName = typeof(TController).Name.Replace("Controller", String.Empty);
        }

        public static void GetRouteParameters<TController>(
            this Expression<Func<TController, ActionResult>> controllerAction,
            out string actionName, out string controllerName, out RouteValueDictionary routeValues)
            where TController : Controller
        {
            controllerAction.GetControllerAndActionNames(out controllerName, out actionName);

            routeValues = new RouteValueDictionary();

            FillRouteValues(controllerAction, routeValues);
        }

        public static void FillRouteValues<TController>(Expression<Func<TController, ActionResult>> controllerAction, RouteValueDictionary routeValues)
            where TController : Controller
        {
            var member = controllerAction.Body as MethodCallExpression;
            if (member == null)
            {
                throw new InvalidOperationException("controllerAction должен указывать на метод");
            }

            var areaNameAttributes = typeof(TController).GetCustomAttributes(typeof(AreaNameAttribute), true);
            var areaName = areaNameAttributes.Length > 0 ? ((AreaNameAttribute)areaNameAttributes[0]).AreaName : String.Empty;

            routeValues.Add("area", areaName);

            // Готовим route values
            var argumentNames = member.Method.GetParameters().Select(x => x.Name).ToArray();

            for (int i = 0; i < argumentNames.Length; i++)
            {
                var lambda = Expression.Lambda(member.Arguments[i], controllerAction.Parameters);
                var getter = lambda.Compile();
                var argumentValue = getter.DynamicInvoke(new object[1]);

                var enumerable = argumentValue as IEnumerable;
                if (enumerable != null)
                {
                    // RouteValueDictionary не умеет правильно маппить массивы и коллекции,
                    // возвращая всякую хрень (имя типа, а не сам массив) в качестве значения.
                    var index = 0;
                    foreach (var o in enumerable)
                    {
                        routeValues.Add(string.Format("{0}[{1}]", argumentNames[i], index++), o);
                    }
                }
                else
                {
                    routeValues.Add(argumentNames[i], argumentValue);
                }
            }
        }
    }
}
