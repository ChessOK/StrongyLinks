using System;
using System.Web.Mvc;

using ChessOk.StrongyLinks;

namespace StrongyLinks.Tests
{
    [AreaName("FakeArea")]
    public class AnotherFakeController : Controller
    {
        public ActionResult Index()
        {
            throw new NotSupportedException();
        }

        [ActionName("Index")]
        public ActionResult YetAnotherIndex()
        {
            throw new NotSupportedException();
        }
    }
}
