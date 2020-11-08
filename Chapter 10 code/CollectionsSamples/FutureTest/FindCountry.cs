using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace FutureTest
{
    public class FindCountry
    {
        public FindCountry(string country) {
            _country = country;
        }
        private string _country;

        public bool FindCountryPredicate(Racer racer)
        {
            // Contract.Requires<ArgumentNullException>(racer != null);
            return racer.Country == _country;
        }

    }
}
