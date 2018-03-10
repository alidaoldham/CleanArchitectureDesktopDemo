using Ledger.Core.AddPayment;
using Ledger.Core.PatientBalance;
using Moq;
using NUnit.Framework;

namespace Ledger.Core.UnitTests.AddPayment
{
    [TestFixture]
    public class AddPaymentInteractorTests
    {
        private Mock<IAddPaymentPresenter> mockAddPaymentPresenter;
        private Mock<IPatientBalanceService> mockPatientBalanceService;
        private AddPaymentResponse caputredAddPaymentResponse;

        private AddPaymentInteractor addPaymentInteractor;

        [SetUp]
        public void Setup()
        {
            mockPatientBalanceService = new Mock<IPatientBalanceService>();
            SetupMockAddPaymentPresenter();
            addPaymentInteractor = new AddPaymentInteractor(mockPatientBalanceService.Object, mockAddPaymentPresenter.Object);
        }

        private void SetupMockAddPaymentPresenter()
        {
            mockAddPaymentPresenter = new Mock<IAddPaymentPresenter>();
            mockAddPaymentPresenter.Setup(m => m.Present(It.IsAny<AddPaymentResponse>()))
                .Callback<AddPaymentResponse>(r => { caputredAddPaymentResponse = r; });
        }

        [Test]
        public void AddPaymentPresentsAZeroDollarPayment()
        {
            addPaymentInteractor.Execute();

            mockAddPaymentPresenter.Verify(m => m.Present(It.IsAny<AddPaymentResponse>()), Times.Once);
            Assert.IsNotNull(caputredAddPaymentResponse);
            Assert.AreEqual(0, caputredAddPaymentResponse.Amount);
        }

        [Test]
        public void AddPaymentPresentsPatientBalanceForPayment()
        {
            mockPatientBalanceService.Setup(m => m.GetPatientBalance()).Returns(120);
            addPaymentInteractor.Execute();
            Assert.IsNotNull(caputredAddPaymentResponse);
            Assert.AreEqual(120, caputredAddPaymentResponse.Amount);
        }
    }
}
