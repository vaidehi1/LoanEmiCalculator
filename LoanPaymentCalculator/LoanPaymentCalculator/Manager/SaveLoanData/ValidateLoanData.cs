using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LoanPaymentCalculator.Common.Constants;
using LoanPaymentCalculator.Models;

namespace LoanPaymentCalculator.Manager.SaveLoanData
{
    public class ValidateLoanData : SaveLoanDataHandler
    {
        public override LoanModel.SaveLoanDetailResponse HandleRequest(LoanModel.SaveLoanDetailsRequest request)
        {
            var query = "select Id from Loan where Amount = @Amount and" +
                        " InterestRate = @InterestRate and " +
                        "Tenure = @Tenure";
            var isExists = false;
            var response = new LoanModel.SaveLoanDetailResponse();
            var con = new SqlConnection();
            try
            {
                using (con = new SqlConnection(Constants.ConnectionString))
                {
                    con.Open();
                    var command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@Amount", request.LoanAmount);
                    command.Parameters.AddWithValue("@InterestRate", request.InterestRate);
                    command.Parameters.AddWithValue("@Tenure", request.NumberOfYears);
                    
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isExists = reader["Id"] != null &&
                                       Convert.ToInt32(reader["Id"])  > 0;
                        }
                    }
                }

                if (isExists)
                {
                    response.IsAlreadyExists = true;
                    return response;
                }
                else
                {
                    return Successor?.HandleRequest(request);
                }
               
            }
            finally
            {
                con.Close();
            }
        }
    }
}