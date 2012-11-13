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
            // Determine controller action name, it can be overriden.
            var actionNameAttribute =
                expression.Method.GetCustomAttributes(typeof(ActionNameAttribute), true).SingleOrDefault() as ActionNameAttribute;

            var actionName = actionNameAttribute != null ? actionNameAttribute.Name : expression.Method.Name;

            // Determine controller name. Hm... it can be buggy...
            var controllerName = typeof(TController).Name.Replace("Controller", String.Empty);

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

        private static string GetExpressionFingerprint<TController>(MethodCallExpression expression)
        {
            return typeof(TController).Name + "." + expression.Method.Name;
        }
    }
}
