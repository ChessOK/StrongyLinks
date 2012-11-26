using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace ChessOk.StrongyLinks.Internals
{
    public static class ActionDescriptorCache
    {
        private static readonly ConcurrentDictionary<string, ActionDescriptor> Cache =
            new ConcurrentDictionary<string, ActionDescriptor>();

        public static ActionDescriptor GetActionDescriptor<TController>(Expression<Func<TController, ActionResult>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var call = expression.Body as MethodCallExpression;
            if (call == null)
            {
                throw new ArgumentException("Expression must be an instance of MethodCallExpression type.");
            }

            var fingerprint = GetExpressionFingerprint<TController>(call);
            return Cache.GetOrAdd(fingerprint, x => CreateDescriptor<TController>(call));
        }

        private static ActionDescriptor CreateDescriptor<TController>(MethodCallExpression expression)
        {
            var controllerName = GetControllerName<TController>();
            var actionName = GetActionName(expression);

            // Determine area name.
            var areaNameAttributes = typeof(TController).GetCustomAttributes(typeof(AreaNameAttribute), true);
            var areaName = areaNameAttributes.Length > 0 ? ((AreaNameAttribute)areaNameAttributes[0]).AreaName : String.Empty;

            return new ActionDescriptor
            {
                ActionName = actionName,
                AreaName = areaName,
                ControllerName = controllerName
            };
        }

        private static string GetControllerName<TController>()
        {
            var controllerTypeName = typeof(TController).Name;

            if (!controllerTypeName.EndsWith("Controller"))
            {
                throw new InvalidOperationException();
            }

            return controllerTypeName.Substring(0, controllerTypeName.Length - "Controller".Length);
        }

        private static string GetActionName(MethodCallExpression expression)
        {
            // Determine controller action name, it can be overriden.
            var actionNameAttribute =
                expression.Method.GetCustomAttributes(typeof(ActionNameAttribute), true).SingleOrDefault() as
                ActionNameAttribute;

            string actionName;
            if (actionNameAttribute == null)
            {
                actionName = expression.Method.Name;
                if (actionName.EndsWith("Async", StringComparison.OrdinalIgnoreCase))
                {
                    actionName = actionName.Substring(0, actionName.Length - "Async".Length);
                }
            }
            else
            {
                actionName = actionNameAttribute.Name;
            }
            return actionName;
        }

        private static string GetExpressionFingerprint<TController>(MethodCallExpression expression)
        {
            return typeof(TController).Name + "." + expression.Method.Name;
        }
    }
}
