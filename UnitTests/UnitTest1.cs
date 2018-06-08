using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MarketCore.Classes;
using MarketCore.EmunsAndConst;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineBoutique.Models;
using OnlineBoutique.Models.EmunsAndConst;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            double expected = 8.0;
            Assert.AreEqual(expected, SizeResponseController.CalculateRating(69, 70, 75));
        }

        [TestMethod]
        public void Test2()
        {
            double expected = 5.0;
            Assert.AreEqual(expected, SizeResponseController.CalculateRating(80, 70, 90));
        }

        [TestMethod]
        public void Test3()
        {

            var sizeVariation = new SizeVariation()
            {
                ListParams = new List<Size>()
                {
                    new Size() {TypeSize = SizesEnum.Breast, Value = 90, MaxValue = 100},
                    new Size() {TypeSize = SizesEnum.Waist, Value = 65, MaxValue = 70},
                    new Size() {TypeSize = SizesEnum.Hips, Value = 95, MaxValue = 100},
                    new Size() {TypeSize = SizesEnum.WidthOfTheShoulders, Value = 70, MaxValue = 75}
                }
            };
            var pv = new ProductVariation()
            {
                SizeVariation = new List<SizeVariation>()
                {
                    sizeVariation
                }
            };
            var customerSize = new UserSizes()
            {
                Breast = 92,
                Waist = 66,
                Thigh = 97,
                ShouldersWidth = 69,
            };
            double expected = 7.28;
            pv = SizeResponseController.CalculateRating(pv, customerSize);
            var actual=pv.SizeVariation.FirstOrDefault().Rating;
            Assert.IsTrue(Math.Abs(expected-actual)<0.1);
        }
    }
}
