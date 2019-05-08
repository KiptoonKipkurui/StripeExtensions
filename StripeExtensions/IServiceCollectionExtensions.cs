using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe.Stripe
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Iservice  collection extensions for stripe
        /// </summary>
        /// <param name="services">IService Collection extensions</param>
        /// <param name="Configuration">IConfiguration</param>
        /// <returns></returns>
        public static IServiceCollection AddStripe(this IServiceCollection services,IConfiguration Configuration)
        {
            //add stripe service
            services.AddScoped<IStripeService, StripeService>();

            services.Configure<StripeOptions>(Configuration.GetSection("StripeOptions"))
                    .PostConfigure<StripeOptions>(PostConfigureOptions);

            return services;
        }

        /// <summary>
        /// post configure options
        /// </summary>
        /// <typeparam name="TOptions">Options class to be configured</typeparam>
        /// <param name="options"></param>
        private static void PostConfigureOptions<TOptions>(TOptions options)where TOptions : StripeOptions, new()
        {
            //ensure options has been supplied
            if (options==null)
            {
                throw new ArgumentNullException("Stripe Options cannot be null");
            }

            //ensure api key has been supplied
            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                throw new ArgumentNullException("Please supply Stripe Api Key cannot be null");
            }

            //ensure Currency has been supplied
            if (string.IsNullOrWhiteSpace(options.Currency))
            {
                throw new ArgumentNullException("Please supply Currency to transact in");
            }
        }
    }
}
