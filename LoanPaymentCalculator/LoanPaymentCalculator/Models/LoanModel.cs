using System;
using System.Collections.Generic;

namespace LoanPaymentCalculator.Models
{
    public class LoanModel
    {
        public class CalculateLoanRequest
        {
            public decimal LoanAmount { get; set; }
            public decimal InterestRate { get; set; }
            public float NumberOfYears { get; set; }

        }

        public class CalculateLoanResponse
        {
            public decimal Emi { get; set; }
        }

        public class CalculateAllInterestData
        {
            public List<InterestData> InterestData { get; set; }
        }

        public class InterestData
        {
            public int Installment { get; set; }
            public decimal Opening { get; set; }
            public decimal Principal { get; set; }
            public decimal Interest { get; set; }
            public decimal Emi { get; set; }
            public decimal Closing { get; set; }
            public decimal CumulativeInterest { get; set; }
        }

        public class SaveLoanDetailsRequest : CalculateAllInterestData
        {
            public decimal LoanAmount { get; set; }
            public decimal InterestRate { get; set; }
            public float NumberOfYears { get; set; }
            public int? LoanId { get; set; }
        }

        public class SaveLoanDetailResponse
        {
            public bool IsAlreadyExists { get; set; }
        }
    }
}