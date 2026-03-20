using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumParaBankTest.TestData
{
    public class RegisterData
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
        public string phone { get; set; }
        public string ssn { get; set; }

        public string username { get; set; }
        public string password { get; set; }
        public string confirmPassword { get; set; }

        public bool expected { get; set; }
    }
}