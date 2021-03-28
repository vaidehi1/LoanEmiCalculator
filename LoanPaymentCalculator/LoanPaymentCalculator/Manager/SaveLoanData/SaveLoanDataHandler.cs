using LoanPaymentCalculator.Models;

namespace LoanPaymentCalculator.Manager.SaveLoanData
{
    public abstract class SaveLoanDataHandler
    {
        #region Declaration
        protected SaveLoanDataHandler Successor;
        #endregion

        public void SetSuccessor(SaveLoanDataHandler successor)
        {
            Successor = successor;
        }

        #region Public Method
        public abstract LoanModel.SaveLoanDetailResponse HandleRequest(LoanModel.SaveLoanDetailsRequest request);
        #endregion

    }
}