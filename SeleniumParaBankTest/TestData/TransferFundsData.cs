using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumParaBankTest.TestData
{
    public class TransferFundsData
    {
        public string testcase { get; set; }

        public string action { get; set; }

        public string amount { get; set; }

        public string fromAccount { get; set; }

        public string toAccount { get; set; }

        public bool expected { get; set; }
    }
}