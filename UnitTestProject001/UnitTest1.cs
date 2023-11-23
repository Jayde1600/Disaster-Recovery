using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DisastersRecovery.Controller; // Import the namespace where your controller resides
using DisastersRecovery.Models; // Import other necessary namespaces if needed

namespace UnitTestProject001
{
    [TestClass]
    public class DisasterChecksControllerTests
    {
        [TestMethod]
        public void TestCreateAction_ValidModelState()
        {
            // Arrange
            var controller = new DisasterChecksController(); // Create an instance of the controller

            var disasterCheck = new DisasterCheck
            {
                // Provide sample valid data here for testing
                // Ensure it meets the required constraints set in the model
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                // Other properties initialization
            };

            // Act
            var result = controller.Create(disasterCheck) as RedirectToActionResult; // Assuming the method returns a RedirectToActionResult on success

            // Assert
            Assert.IsNotNull(result); // Check if the action result is not null, indicating success
            Assert.AreEqual("Index", result.ActionName); // Verify that the action redirects to the correct action
        }

        // Add more test methods to cover various scenarios for different actions in the controller
    }
}
