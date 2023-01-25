using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using TestNinja.Mocking.Helpers;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
        }


        [Test]
        [TestCase("customerName", "installerName")]
        public void DownloadInstaller_WithoutError_ReturnTrue(string customerName, string installerName)
        {
            var installerHelper = new InstallerHelper(_fileDownloader.Object);

            var result = installerHelper.DownloadInstaller(customerName, installerName);

            Assert.That(result, Is.True);
        }
        
        [Test]
        [TestCase("customerName", "installerName")]
        public void DownloadInstaller_WithError_ReturnFalse(string customerName, string installerName)
        {
            _fileDownloader.Setup(x => x.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var installerHelper = new InstallerHelper(_fileDownloader.Object);

            var result = installerHelper.DownloadInstaller(customerName, installerName);

            Assert.That(result, Is.False);
        }

    }
}