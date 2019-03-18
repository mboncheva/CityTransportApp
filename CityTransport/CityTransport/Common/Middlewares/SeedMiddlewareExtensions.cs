namespace CityTransport.Web.Common.Middlewares
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedMiddlewareExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedMiddleware>();
        }
    }
}
