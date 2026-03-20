using System;

namespace SeleniumParaBankTest.TestData
{
    public class UpdateProfileData
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string address { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zipCode { get; set; }

        public string phone { get; set; }

        public bool expected { get; set; }
    }
}