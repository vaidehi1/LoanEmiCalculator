using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoanPaymentCalculator.Common.Constants;
using LoanPaymentCalculator.Models;

namespace LoanPaymentCalculator.Manager.SaveLoanData
{
    public class SaveEmiTransaction : SaveLoanDataHandler
    {
        public override LoanModel.SaveLoanDetailResponse HandleRequest(LoanModel.SaveLoanDetailsRequest request)
        {
            var response = new LoanModel.SaveLoanDetailResponse();
            var query = "Insert Into LoanEMITransaction " +
                        "(LoanId, InstallmentNumber, OpeningAmount" +
                        ",PrincipalAmount, InterestAmount" +
                        ",EMI, ClosingAmount" +
                        ",CumulativeInterestAmount) " +
                        "Values " +
                        "(@LoanId, @InstallmentNumber, @OpeningAmount" +
                        ",@PrincipalAmount, @InterestAmount" +
                        ",@EMI, @ClosingAmount" +
                        ",@CumulativeInterestAmount);";
            var con = new SqlConnection();
            try
            {
                foreach (var emi in request.InterestData)
                {
                    using (con = new SqlConnection(Constants.ConnectionString))
                    {
                        con.Open();
                        var cmd = new SqlCommand(query,con);
                        cmd.Parameters.AddWithValue("@LoanId", request.LoanId);
                        cmd.Parameters.AddWithValue("@InstallmentNumber", emi.Installment);
                        cmd.Parameters.AddWithValue("@OpeningAmount", emi.Opening);
                        cmd.Parameters.AddWithValue("@PrincipalAmount", emi.Principal);
                        cmd.Parameters.AddWithValue("@InterestAmount", emi.Interest);
                        cmd.Parameters.AddWithValue("@EMI", emi.Emi);
                        cmd.Parameters.AddWithValue("@ClosingAmount", emi.Closing);
                        cmd.Parameters.AddWithValue("@CumulativeInterestAmount", emi.CumulativeInterest);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                response.IsAlreadyExists = false;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }
            }

            return response;
        }
    }
}