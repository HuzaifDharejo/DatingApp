using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DatingApp.Helpers;

namespace DatingApp.Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void WhenBirthDateIsPassedThenIncludeAllYears()
        {
            var birthdate = new DateTime(2015, 10, 10);
            var expectedAge = 4;

            var result = birthdate.CalculateAge();

            Assert.AreEqual(expectedAge, result);

        }

        [TestMethod]
        public void WhenBirthDateIsInFutreThenSubtractOne()
        {
            var today = new DateTime(2019, 2, 15);
            var birthdate = new DateTime(2015, 10, 22);
            var expectedAge = 3;

            var result = birthdate.CalculateAge(today);

            Assert.AreEqual(expectedAge, result);

        }
    }


}
