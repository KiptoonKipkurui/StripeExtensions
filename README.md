# StripeExtensions
Collection of methods to help in interracting with stripe api via c#

Stripe is a popular payment processing platform. 
This is a libray to help you interract with their Api to manage your transactions as a merchant.
Usage
```C#
//configuration of startup 
 public class Startup
 {
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //register stipe service in dependency injection container
        services.AddStripe(Configuration);
        ...
        //service 
        services.AddMvc();
    }       
 }
 
 
 //example usage on a controller
  [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StripeController : ControllerBase
    {
        private readonly IStripeService stripeService;
        public StripeController(IStripeService stripeService)
        {
            this.stripeService = stripeService ?? throw new ArgumentNullException(nameof(stripeService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(StripeResponse), 200)]
        [ProducesResponseType(typeof(ErrorModel), 400)]
        public async Task<IActionResult> PayWithStripeAsync([FromBody]StripeRequest request)
        {
            //validate model
            //ensure email to supply receipt from stipe is present
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("email is missing");
            }

            //ensure token from front end client is present
            if (string.IsNullOrWhiteSpace(request.Token))
            {
                return BadRequest("Stripe one time token is missing");
            }

            //ensure valid amount is supplied
            if (request.Amount<=0)
            {
                return BadRequest("Invalid Charge Amount");
            }

            //perform a charge operation to stripe
            var chargeResponse = await stripeService.ProcessStripePaymentAsync(request);

            //handle errors if any
            if (!chargeResponse.success)
            {
                var error = chargeResponse.ErrorModel;
                //handle error from appropriate error code

            }

            //handle successful payment

            return Ok();
        }
    }
```
