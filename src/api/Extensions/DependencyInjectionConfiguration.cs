using api.Model.ServiceModel;
using Microsoft.Extensions.DependencyInjection;

namespace api.Extensions
{
    public static class DependencyInjectionConfiguration
    {
        public static void IServiceCollection(this IServiceCollection services)
        {

            //Services
            services.AddScoped<PaymentProcessing>();
            services.AddScoped<AnticipationProcess>();
        }
    }
}
