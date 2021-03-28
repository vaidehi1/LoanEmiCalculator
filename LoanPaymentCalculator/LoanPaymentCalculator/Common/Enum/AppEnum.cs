using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanPaymentCalculator.Common.Enum
{
    public class AppEnum
    {
        #region ResponseStatusCode
        public enum ResponseStatusCode
        {
            Success = 200,
            WrongOperation = 404,
            DataNotFound = 400,
            NoMoreData = 204,
            AlreadyExitData = 208,
            ValidationError = 409,
            Unauthorized = 401
        }
        #endregion
    }
}