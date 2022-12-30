using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Rhino.Mocks;
using ServiceLayer;

namespace TestsServiceLayer
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MockRepository mocks = new MockRepository();
            IProductServices productServices = mocks.StrictMock<IProductServices>();

            using (mocks.Record())
            {

              //  Expect.Call(productServices.AddProduct()).Return(void);
            }

        }
    }
}
