using ChessOk.StrongyLinks.Internals;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StrongyLinks.Tests
{
    [TestClass]
    public class RoutesHelperTests
    {
        [TestMethod]
        public void ShouldIgnoreTheControllerSuffix_WhenDetermineItsName()
        {
            var p = RoutesHelper.GetRouteParameters<FakeController>(x => x.Index());
            Assert.AreEqual("Fake", p.ControllerName);
        }

        [TestMethod]
        public void ShouldUseExactTypeNameAsControllerName_IfSuffixIsAbsent()
        {
            var p = RoutesHelper.GetRouteParameters<AnotherFakeController>(x => x.Index());
            Assert.AreEqual("AnotherFake", p.ControllerName);
        }

        [TestMethod]
        public void ShouldUseMethodName_WhenThereAreNoAttributes()
        {
            var p = RoutesHelper.GetRouteParameters<AnotherFakeController>(x => x.Index());
            Assert.AreEqual("Index", p.ActionName);
        }

        [TestMethod]
        public void ShouldUseActionNameValue_IfAttributeHasBeenApplied()
        {
            var p = RoutesHelper.GetRouteParameters<AnotherFakeController>(x => x.YetAnotherIndex());
            Assert.AreEqual("Index", p.ActionName);
        }

        [TestMethod]
        public void ShouldOnlyAddEmptyArea_WhenActionHasNoParameters()
        {
            var p = RoutesHelper.GetRouteParameters<FakeController>(x => x.Index());
            Assert.AreEqual(1, p.RouteValues.Count);
            Assert.AreEqual("", p.RouteValues["area"]);
        }

        [TestMethod]
        public void ShouldUseAreaNameAttribute()
        {
            var p = RoutesHelper.GetRouteParameters<AnotherFakeController>(x => x.Index());
            Assert.AreEqual("FakeArea", p.RouteValues["area"]);
        }

        [TestMethod]
        public void ShouldEvaluateConstantParametersExpressions()
        {
            var p = RoutesHelper.GetRouteParameters<FakeController>(x => x.Details(3));
            Assert.AreEqual(3, p.RouteValues["id"]);
        }

        [TestMethod]
        public void ShouldEvaluateComplexExpressions()
        {
            var p = RoutesHelper.GetRouteParameters<FakeController>(x => x.Details(3 + 5 + int.Parse("-1")));
            Assert.AreEqual(7, p.RouteValues["id"]);
        }

        [TestMethod]
        public void ShouldSupportCollectionParametersMapping()
        {
            var p = RoutesHelper.GetRouteParameters<FakeController>(x => x.Delete(new[] { 5, 3 }));
            Assert.AreEqual(5, p.RouteValues["ids[0]"]);
            Assert.AreEqual(3, p.RouteValues["ids[1]"]);
        }

        [TestMethod]
        public void ShouldSupportAsyncActions()
        {
            var p = RoutesHelper.GetRouteParameters<FakeController>(x => x.ActionAsync());
            Assert.AreEqual("Action", p.ActionName);
        }
    }
}