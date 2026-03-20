using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace SeleniumParaBankTest.TestData
{
    public class BillPayData
    {
        public string payeeName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string phone { get; set; }
        public string account { get; set; }
        public string verifyAccount { get; set; }
        public string amount { get; set; }

        public bool expected { get; set; }
    }
}
