using Ledger.Core.PatientBalance;

namespace Ledger.Core.AddPayment
{
    public class AddPaymentInteractor
    {
        private readonly IPatientBalanceService patientBalanceService;
        private readonly IAddPaymentPresenter addPaymentPresenter;

        public AddPaymentInteractor(IPatientBalanceService patientBalanceService, IAddPaymentPresenter addPaymentPresenter)
        {
            this.addPaymentPresenter = addPaymentPresenter;
            this.patientBalanceService = patientBalanceService;
        }

        public void Execute()
        {
            var addPaymentResponse = new AddPaymentResponse
            {
                Amount = patientBalanceService.GetPatientBalance()
            };
            addPaymentPresenter.Present(addPaymentResponse);
        }
    }
}
