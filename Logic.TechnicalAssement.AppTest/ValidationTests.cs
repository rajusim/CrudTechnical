using Logic.TechnicalAssement.App.Services;

namespace Logic.TechnicalAssement.AppTest
{
        [TestFixture]
        public class ValidationTests : PageTest
        {
            [Test]
            public void Test_ValidateEmail_Valid()
            {
                // Arrange
                var email = "rajusim@yahoo.com";
                // Act
                IValidationRule validation = new ValidationRule();
                var result = validation.IsValidEmail(email);
                //assert
                Assert.IsTrue(result);
            }
            [Test]
            public void Test_InValidateEmail_Valid()
            {
                // Arrange
                var email = "rajusim@Y";

                // Act
                IValidationRule validation = new ValidationRule();
                var result = validation.IsValidEmail(email);
                //assert
                Assert.IsFalse(result);
            }
            [Test]
            public void Test_ValidateDate_Valid()
            {
                // Arrange
                var startdate = DateTime.Now;
                var enddate = DateTime.Now.AddDays(3);
                var halfDay = false;

                // Act
                IValidationRule validation = new ValidationRule();
                var result = validation.IsValidDates(startdate, enddate, halfDay);
                //assert
                Assert.IsTrue(result == "Valid");
            }

            [Test]
            public void Test_InValidateDate_Valid()
            {
                // Arrange
                var startdate = DateTime.Now;
                var enddate = DateTime.Now.AddDays(-3);
                var halfDay = false;

                // Act
                IValidationRule validation = new ValidationRule();
                var result = validation.IsValidDates(startdate, enddate, halfDay);
                //assert
                Assert.IsFalse(result == "Valid");
            }
            [Test]
            public void Test_ValidateHalfDay_Valid()
            {
                // Arrange
                var startdate = DateTime.Now;
                var enddate = DateTime.Now;
                var halfDay = true;

                // Act
                IValidationRule validation = new ValidationRule();
                var result = validation.IsValidDates(startdate, enddate, halfDay);
                //assert
                Assert.IsFalse(result == "Valid");
            }
            [Test]
            public void Test_InValidateHalfDay_Valid()
            {
                // Arrange
                var startdate = DateTime.Now;
                var enddate = DateTime.Now.AddDays(1);
                var halfDay = true;

                // Act
                IValidationRule validation = new ValidationRule();
                var result = validation.IsValidDates(startdate, enddate, halfDay);
                //assert
                Assert.IsFalse(result == "Valid");
            }


        }
}

