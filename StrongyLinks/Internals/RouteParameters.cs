using System.Web.Routing;

namespace ChessOk.StrongyLinks.Internals
{
    public struct RouteParameters
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public RouteValueDictionary RouteValues { get; set; }
    }
}
