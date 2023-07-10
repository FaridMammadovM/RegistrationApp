using AddressBook.Api;
using Serilog;

namespace NotificationAddressBookApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                 .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((hostingContext, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
           );
    }
}
