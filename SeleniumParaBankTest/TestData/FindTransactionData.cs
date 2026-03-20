using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumParaBankTest.TestData
{
    public class FindTransactionData
    {
        public string testcase { get; set; }

        public string action { get; set; }

        public string account { get; set; }

        public string transactionId { get; set; }

        public string date { get; set; }

        public string fromDate { get; set; }

        public string toDate { get; set; }

        public string amount { get; set; }

        public bool expected { get; set; }
    }
}