namespace LoanPaymentCalculator.Messages.Common
{
    public class State
    {
        public bool HasSuccess { get; set; }
        public bool HasError { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public int StatusCode { get; set; }
        public string SecurityMessage { get; private set; }
        public bool HasSecurityError { get; private set; }

        public void SetSuccessMessage(string message, int successStatusCode = 0)
        {
            HasSuccess = true;
            SuccessMessage = message;
            StatusCode = successStatusCode;
        }
        public void SetErrorMessage(string message, int errorStatusCode = 0)
        {
            ErrorMessage = message;
            HasError = true;
            StatusCode = errorStatusCode;
        }
        public void SetSecurityMessage(string message, int securityStatusCode = 0)
        {
            SecurityMessage = message;
            HasSecurityError = true;
            StatusCode = securityStatusCode;
        }
    }
}