using System.Web.Mvc;
using LoanPaymentCalculator.Common.Constants;
using LoanPaymentCalculator.Common.Enum;
using LoanPaymentCalculator.Manager;
using LoanPaymentCalculator.Manager.Interface;
using LoanPaymentCalculator.Messages.Common;
using LoanPaymentCalculator.Models;

namespace LoanPaymentCalculator.Controllers
{
    public class LoanController : Controller
    {
        // GET: Loan
        private readonly ILoanManager _loanManager;

         public LoanController()
        {
            _loanManager = new LoanManager();
        }

        public ActionResult Loan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CalculateLoan(LoanModel.CalculateLoanRequest request)
        {
            Response<LoanModel.CalculateLoanResponse> response;
            if (ModelState.IsValid)
            {
                // execute the logic
                response = _loanManager.CalculateLoan(request);
                response.ResponseMessage(HttpContext.Response.StatusCode, response.StateModel);
                return Json(response);
            }
            response = new Response<LoanModel.CalculateLoanResponse>();
            response.StateModel.SetSecurityMessage(AppMessageConstants.SystemError, (int)AppEnum.ResponseStatusCode.WrongOperation);
            response.ResponseMessage(HttpContext.Response.StatusCode, response.StateModel);
            return Json(response);
        }

        /// <summary>Calculates all interest data.</summary>
        /// <param name="request">Calculate interest data for given loan details.</param>
        /// <returns>returns installment details</returns>
        [HttpPost]
        public ActionResult CalculateAllInterestData(LoanModel.CalculateLoanRequest request)
        {
            Response<LoanModel.CalculateAllInterestData> response;
            if (ModelState.IsValid)
            {
                // execute the logic
                response = _loanManager.CalculateAllInterestData(request);
                response.ResponseMessage(HttpContext.Response.StatusCode, response.StateModel);
                return Json(response);
            }
            response = new Response<LoanModel.CalculateAllInterestData>();
            response.StateModel.SetSecurityMessage(AppMessageConstants.SystemError, (int)AppEnum.ResponseStatusCode.WrongOperation);
            response.ResponseMessage(HttpContext.Response.StatusCode, response.StateModel);
            return Json(response);
        }

        // save loan data to database
        [HttpPost]
        public ActionResult SaveLoanData(LoanModel.SaveLoanDetailsRequest request)
        {
            Response<LoanModel.SaveLoanDetailResponse> response;
            if (ModelState.IsValid)
            {
                // execute the logic
                response = _loanManager.SaveLoanDetails(request);
                response.ResponseMessage(HttpContext.Response.StatusCode, response.StateModel);
                return Json(response);
            }
            response = new Response<LoanModel.SaveLoanDetailResponse>();
            response.StateModel.SetSecurityMessage(AppMessageConstants.SystemError, (int)AppEnum.ResponseStatusCode.WrongOperation);
            response.ResponseMessage(HttpContext.Response.StatusCode, response.StateModel);
            return Json(response);
        }
    }
}