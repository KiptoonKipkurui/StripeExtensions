# StripeExtensions
Collection of methods to help in interracting with stripe api via c#

Stripe is a popular payment processing platform. 
This is a libray to help you interract with their Api to manage your transactions as a merchant.
Usage
```C#
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
```
