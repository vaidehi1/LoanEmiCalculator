using System.Data;
using System.Data.SqlClient;
using LoanPaymentCalculator.Common.Constants;
using LoanPaymentCalculator.Models;

namespace LoanPaymentCalculator.Manager.SaveLoanData
{
    public class SaveLoanData : SaveLoanDataHandler
    {
        public override LoanModel.SaveLoanDetailResponse HandleRequest(LoanModel.SaveLoanDetailsRequest request)
        {
            var query = "Insert Into Loan " +
                           "(Amount, InterestRate, Tenure) " +
                           "Values (@Amount, @InterestRate, @Tenure);" +
                           "SELECT CAST(scope_identity() AS int)";
            var con = new SqlConnection();
            try
            {
                using (con = new SqlConnection(Constants.ConnectionString))
                {
                    con.Open();
                    var cmd = new SqlCommand(query, con);
                    cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = request.LoanAmount;
                    cmd.Parameters.Add("@InterestRate", SqlDbType.Decimal).Value = request.InterestRate;
                    cmd.Parameters.Add("@Tenure", SqlDbType.Float).Value = request.NumberOfYears;
                    cmd.CommandType = CommandType.Text;
                    request.LoanId = (int)cmd.ExecuteScalar();
                    con.Close();
                }

                return Successor?.HandleRequest(request);
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }
        }
    }
}