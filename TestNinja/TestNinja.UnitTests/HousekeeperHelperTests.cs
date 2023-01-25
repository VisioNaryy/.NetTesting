using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;
using TestNinja.Mocking.Helpers;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class HousekeeperHelperTests
    {
        private Mock<ISaveStatementHelper> _saveStatementHelper;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private string _fileName;
        private HouseKeeperService _service;
        private Housekeeper _housekeeper;
        private DateTime _date;

        [SetUp]
        public void SetUp()
        {
            _housekeeper = new Housekeeper
            {
                Email = "a",
                FullName = "b",
                Oid = 1,
                StatementEmailBody = "c"
            };

            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                 _housekeeper
            }.AsQueryable());

            _fileName = "filename";

            _saveStatementHelper = new Mock<ISaveStatementHelper>();
            _saveStatementHelper
                .Setup(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date))
                .Returns(() => _fileName); // Lazy evaluation

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HouseKeeperService(unitOfWork.Object,
                _saveStatementHelper.Object,
                _emailSender.Object,
                _messageBox.Object);

            _date = new DateTime(2022, 4, 25);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {
            _service.SendStatementEmails(_date);

            _saveStatementHelper.Verify(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date));
        }

        [Test]
        public void SendStatementEmails_EmailIsNull_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = null;

            _service.SendStatementEmails(_date);

            _saveStatementHelper.Verify(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_EmailIsWhiteSpace_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = " ";

            _service.SendStatementEmails(_date);

            _saveStatementHelper.Verify(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_EmailIsEmptyString_ShouldNotGenerateStatements()
        {
            _housekeeper.Email = "";

            _service.SendStatementEmails(_date);

            _saveStatementHelper.Verify(x => x.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _date),
                Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_date);

            VerifyEmailSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileIsNull_ShouldNotEmailTheStatement()
        {
            _fileName = null;

            _service.SendStatementEmails(_date);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileIsEmpty_ShouldNotEmailTheStatement()
        {
            _fileName = "";

            _service.SendStatementEmails(_date);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_StatementFileIsWhiteSpace_ShouldNotEmailTheStatement()
        {
            _fileName = " ";

            _service.SendStatementEmails(_date);

            VerifyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_DisplayMessageBox()
        {
            _emailSender.Setup(x => x.EmailFile(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string>()))
                .Throws<Exception>();


            _service.SendStatementEmails(_date);

            _messageBox.Verify(x => x.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK));
        }

        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(x => x.EmailFile(
                 It.IsAny<string>(),
                 It.IsAny<string>(),
                 It.IsAny<string>(),
                 It.IsAny<string>()),
                 Times.Never);
        }

        private void VerifyEmailSent()
        {
            _emailSender.Verify(x => x.EmailFile(
                _housekeeper.Email,
                _housekeeper.StatementEmailBody,
                _fileName,
                It.IsAny<string>()));
        }
    }
}