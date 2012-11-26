using System;
using System.Web.Mvc;

namespace StrongyLinks.Tests
{
    public class FakeController : Controller
    {
        public ActionResult Index()
        {
            throw new NotSupportedException();
        }

        public ActionResult Details(int id)
        {
            throw new NotSupportedException();
        }

        public ActionResult Delete(int[] ids)
        {
            throw new NotSupportedException();
        }

        public ActionResult ActionAsync()
        {
            throw new NotSupportedException();
        }
    }
}
