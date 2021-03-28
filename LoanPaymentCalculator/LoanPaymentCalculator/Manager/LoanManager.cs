using System;
using System.Collections.Generic;
using LoanPaymentCalculator.Common.Constants;
using LoanPaymentCalculator.Common.Enum;
using LoanPaymentCalculator.Manager.Interface;
using LoanPaymentCalculator.Manager.SaveLoanData;
using LoanPaymentCalculator.Messages.Common;
using static LoanPaymentCalculator.Models.LoanModel;

namespace LoanPaymentCalculator.Manager
{
    public class LoanManager : ILoanManager
    {
        public Response<CalculateLoanResponse> CalculateLoan(CalculateLoanRequest request)
        {
            var response = new Response<CalculateLoanResponse>
            {
                Result = new CalculateLoanResponse()
            };
            try
            {
                response.Result.Emi = CalculateEmi(request);
                response.StateModel.SetSuccessMessage(AppMessageConstants.ResultSuccess,
                    (int)AppEnum.ResponseStatusCode.Success);
            }
            catch (Exception ex)
            {
                // handle exception
                response.StateModel.SetErrorMessage(AppMessageConstants.SystemError,
                    (int)AppEnum.ResponseStatusCode.WrongOperation);
            }
            return response;
        }

        public Response<CalculateAllInterestData> CalculateAllInterestData(CalculateLoanRequest request)
        {
            var response = new Response<CalculateAllInterestData>
            {
                Result = new CalculateAllInterestData
                {
                    InterestData = new List<InterestData>()
                }
            };
            try
            {
                var monthlyRateOfInterest = 
                    request.InterestRate / (12 * 100); // one month interest
                var closing = request.LoanAmount;
                var opening = request.LoanAmount;
                decimal cumulativeInterest = 0; 
                var installment = 1;
                while (Math.Round(closing) > 0)
                {
                    var interest = opening * monthlyRateOfInterest;
                    var emi = CalculateEmi(request);
                    var principal = emi - interest;
                    closing = opening - principal;
                    cumulativeInterest = cumulativeInterest + interest;
                    var obj = new InterestData
                    {
                        Opening = Math.Round(opening, 2),
                        Closing = closing < 0 ? 0 : Math.Round(closing, 2),
                        Emi = Math.Round(emi, 2),
                        CumulativeInterest = Math.Round(cumulativeInterest, 2),
                        Installment = installment,
                        Interest = Math.Round(interest, 2),
                        Principal = Math.Round(principal, 2)
                    };
                    response.Result.InterestData.Add(obj);
                    opening = closing;
                    installment += 1;
                } 
            }
            catch (Exception ex)
            {
                // handle exception
                response.StateModel.SetErrorMessage(AppMessageConstants.SystemError,
                    (int)AppEnum.ResponseStatusCode.WrongOperation);
            }
            return response;
        }

        public Response<SaveLoanDetailResponse> SaveLoanDetails(SaveLoanDetailsRequest request)
        {
            var response = new Response<SaveLoanDetailResponse>
            {
                Result = new SaveLoanDetailResponse()
            };
            try
            {
                // create instances of chained classes
                var validateLoanData = new SaveLoanData.ValidateLoanData();
                var saveLoanData = new SaveLoanData.SaveLoanData();
                var saveEmiTransaction = new SaveEmiTransaction();

                // create a chain
                validateLoanData.SetSuccessor(saveLoanData);
                saveLoanData.SetSuccessor(saveEmiTransaction);

                // execute chain
                response.Result = validateLoanData.HandleRequest(request);
            }
            catch (Exception ex)
            {
                // handle exception
                response.StateModel.SetErrorMessage(AppMessageConstants.SystemError,
                    (int)AppEnum.ResponseStatusCode.WrongOperation);
            }
            return response;
        }

        private decimal CalculateEmi(CalculateLoanRequest request)
        {
            var p = request.LoanAmount;
            var r = request.InterestRate / (12 * 100); // one month interest
            var n = request.NumberOfYears * 12; // one month period
            var emi = p * r * (decimal)Math.Pow((double)(1 + r), n)
                      / (decimal)(Math.Pow((double)(1 + r), n) - 1);
            return emi;
        }
    }
}