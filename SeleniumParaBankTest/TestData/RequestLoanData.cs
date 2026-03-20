using System;

namespace SeleniumParaBankTest.TestData
{
    public class RequestLoanData
    {
        public string loanAmount { get; set; }

        public string downPayment { get; set; }

        public bool expected { get; set; }
    }
}