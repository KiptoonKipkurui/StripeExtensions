using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Stripe
{
    public class StripeOptions
    {
        /// <summary>
        /// api key 
        /// </summary>
        public string ApiKey { get; set; }


        /// <summary>
        /// the currency to transact in
        /// </summary>
        public string Currency { get; set; }
    }
}
