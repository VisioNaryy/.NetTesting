using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using TestNinja.Mocking.Helpers;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private Mock<IEmployeeStorage> _storage;

        [SetUp]
        public void SetUp()
        {
            _storage = new Mock<IEmployeeStorage>();
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDb()
        {
            var controller = new EmployeeController(_storage.Object);

            var result = controller.DeleteEmployee(1);

            _storage.Verify(x => x.DeleteEmployee(1));
        }

        [Test]
        public void DeleteEmployee_WhenCalled_ReturnRedirectToAction()
        {
            var controller = new EmployeeController(_storage.Object);

            var result = controller.DeleteEmployee(1);

            Assert.That(result, Is.InstanceOf<ActionResult>());
        }
    }
}