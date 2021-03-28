using LoanPaymentCalculator.Messages.Common;
using static LoanPaymentCalculator.Models.LoanModel;

namespace LoanPaymentCalculator.Manager.Interface
{
    public interface ILoanManager
    {
        Response<CalculateLoanResponse> CalculateLoan(CalculateLoanRequest request);
        Response<CalculateAllInterestData> CalculateAllInterestData(CalculateLoanRequest request);
        Response<SaveLoanDetailResponse> SaveLoanDetails(SaveLoanDetailsRequest request);
    }
}
