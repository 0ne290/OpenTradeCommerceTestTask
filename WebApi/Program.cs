using Serilog;
using Serilog.Events;
using WebApi.gRpcServices;
using WebApi.Middlewares;

namespace WebApi;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing", LogEventLevel.Warning)
            
            .Enrich.FromLogContext()
            
            .WriteTo.Async(c => c.Console())
            
            .CreateLogger();
        
        try
        {
            Log.Information("Starting host build");
            
            var builder = WebApplication.CreateBuilder(args);

            var yandexTranslateConfiguration = builder.Configuration.GetRequiredSection("YandexTranslate");
            var yandexTranslateApiKey = yandexTranslateConfiguration["ApiKey"] ??
                                        throw new Exception("Configuration item \"YandexTranslate.ApiKey\" not found.");
            var yandexTranslateFolderId = yandexTranslateConfiguration["FolderId"] ??
                                          throw new Exception("Configuration item \"YandexTranslate.FolderId\" not found.");
            var yandexTranslateApiUrl = yandexTranslateConfiguration["ApiUrl"] ??
                                        throw new Exception("Configuration item \"YandexTranslate.ApiUrl\" not found.");

            Infrastructure.Cache.Extensions.ServiceCollectionExtension.AddServices(builder.Services, 274_877_906_944);// Именно столько четырехбайтовых символов (максимальный размер символа UTF-8) содержится в одном гигабайте
            Infrastructure.Translator.Extensions.ServiceCollectionExtension.AddServices(builder.Services,
                yandexTranslateApiUrl, yandexTranslateApiKey, yandexTranslateFolderId);
            Core.Extensions.ServiceCollectionExtension.AddServices(builder.Services);
            builder.Services.AddSerilog();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddControllersWithViews().AddNewtonsoftJson();
            builder.Services.AddGrpc();
        
            var app = builder.Build();
            
            app.UseSerilogRequestLogging();
            app.UseMiddleware<ExceptionLoggingMiddleware>();
            
            app.UseHsts();
            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseRouting();
            //app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}");
            
            app.MapGrpcService<TranslatorService>();
            
            Log.Information("Success to build host. Starting web application");
            
            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Failed to build host");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}