
using API.Common.Extensions;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddWebApiConfiguration(builder.Configuration);
            var app = builder.Build();
            app.AddCommonApplicationBuilder();
            app.Run();
        }
    }
}
