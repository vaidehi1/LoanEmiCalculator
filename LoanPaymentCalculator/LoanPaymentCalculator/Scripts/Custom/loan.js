$(document).ready(function () {
    var loanData;
    $("#loanAmount, #loanInterest, #NumOfYear").blur(function() {
        calculateEmi();
    });

    $("#loanAmount, #loanInterest, #NumOfYear").keypress(function (event) {
        return isNumberKey(event, this);
    });

    // add comma in number
    function addCommas(nStr) {
        nStr += '';
        var x = nStr.split('.');
        var x1 = x[0];
        var x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1)) {
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        }
        return x1 + x2;
    }

    $("#saveLoanData").click(function () {
        if (loanData && loanData.length > 0) {
            var postObj = {
                "LoanAmount": Number($("#loanAmount").val()),
                "InterestRate": Number($("#loanInterest").val()),
                "NumberOfYears": Number($("#NumOfYear").val()),
                "InterestData": loanData
            };
            $.ajax({
                url: "Loan/saveLoanData",
                type: "POST",
                dataType: "json",
                data: postObj,
                success: function (result) {
                    if (result != null && result.Result != null) {
                        if (result.Result.IsAlreadyExists) {
                            $("#recordExists").removeClass("hidden");
                            setTimeout(function() {
                                    $("#recordExists").addClass("hidden");
                                },
                                3000);
                        } else {
                            $("#dataSaved").removeClass("hidden");
                            setTimeout(function () {
                                $("#dataSaved").addClass("hidden");
                                },
                                3000);
                        }
                    }
                }
            });
        }
        
    });

    $("#calculateAllInterest").click(function () {
        var postObj = {
            "LoanAmount": Number($("#loanAmount").val()),
            "InterestRate": Number($("#loanInterest").val()),
            "NumberOfYears":Number($("#NumOfYear").val())
        };
        $.ajax({
            url: "Loan/calculateAllInterestData",
            type: "POST",
            dataType: "json",
            data: postObj,
            success: function (result) {
                if (result != null && result.Result != null && result.Result.InterestData.length > 0) {
                    loanData = result.Result.InterestData;
                    $("#tblBody").html('');
                    var tblRow = '';
                    for (var ins = 0; ins < loanData.length; ins++) {
                        tblRow = tblRow +
                        '<tr>' +
                            '<td>' +loanData[ins].Installment+'</td>' +
                            '<td>' + addCommas(loanData[ins].Opening)+'</td>' +
                            '<td>' + addCommas(loanData[ins].Principal)+'</td>' +
                            '<td>' + addCommas(loanData[ins].Interest)+'</td>' +
                            '<td>' + addCommas(loanData[ins].Emi) +'</td>' +
                            '<td>' + addCommas(loanData[ins].Closing)+'</td>' +
                            '<td>' + addCommas(loanData[ins].CumulativeInterest)+'</td>' +
                            '</tr>';
                    }
                    $("#tblBody").append(tblRow);

                }
            }
        });
    });

    // validation for only number decimal digits
    function isNumberKey(evt, element) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (
            (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
            return false;
        return true;
    }

    var calculateEmi = function () {
        var emi = null;
        var isValid = validateInputs();
        if (isValid) {
            // calculate loan EMI
            var p = Number($("#loanAmount").val());
            var r = Number($("#loanInterest").val()) / (12 * 100); // one month interest
            var n = Number($("#NumOfYear").val()) * 12; // one month period
            emi = p * r *  Math.pow((1 + r), n)
                / (Math.pow((1 + r), n) - 1);
            if (emi != null) {
                console.log(emi);
                console.log(r);
                $("#Emi").html(emi.toFixed(2)) ;
                $("#MonthlyRate").html(r.toFixed(9));
            }
        }
        return emi;
    };

    var validateInputs = function() {
        var isLoanAmountValid = $("#loanAmount").val() != null &&
            $("#loanAmount").val() != '' && !isNaN($("#loanAmount").val());
        var isInterestRateValid = $("#loanInterest").val() != null &&
            $("#loanInterest").val() != '' && !isNaN($("#loanInterest").val());
        var isNumberOfYearValid = $("#NumOfYear").val() != null &&
            $("#NumOfYear").val() != '' && !isNaN($("#NumOfYear").val());
        return isLoanAmountValid && isInterestRateValid && isNumberOfYearValid;
    };
});