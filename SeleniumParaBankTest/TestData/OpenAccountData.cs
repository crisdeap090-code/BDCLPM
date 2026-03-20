using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumParaBankTest.TestData
{
    public class OpenAccountData
    {
        public string testcase { get; set; }

        public string action { get; set; }

        public string accountType { get; set; }

        public bool expected { get; set; }
    }
}
