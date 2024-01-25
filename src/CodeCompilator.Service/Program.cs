// See https://aka.ms/new-console-template for more information
using CodeCompilator.Service;
using CodeCompilator.Service.Configurations;
using CodeCompilator.Service.Interfaces;
using CodeCompilator.Service.Services;
using CodeExecutionContracts.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

Console.WriteLine("Hello, World!");


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
var host = new HostBuilder()
   .ConfigureServices((hostContext, services) =>
   {

       var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

       var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
           .Build();
       services.AddSingleton<IConfiguration>(configuration);

       services.Configure<RabbitMQOptions>(configuration.GetSection("RabbitMQOptions"));
       services.Configure<ChannelsConfiguration>(configuration.GetSection("ChannelsConfiguration"));

       var rabbitMQConfig = configuration.GetSection("RabbitMQOptions").Get<RabbitMQOptions>();
       
       services.AddTransient<ICodeTestingService, CodeTestingService>();
       services.AddSingleton<IConnectionFactory>(_ =>
       {
           return new ConnectionFactory()
           {
               HostName = rabbitMQConfig.HostName,
               Password = rabbitMQConfig.Password,
               UserName = rabbitMQConfig.UserName
           };
       });

       services.AddHostedService<CodeRunnerBackgroundService>();
   })
   .Build();

await host.RunAsync();




var asd = new CodeRunnerV1();

public class ExecutionCodeResult
{
    public Guid TaskId { get; set; }
    public string ErrorMessage { get; set; }
    public TimeSpan ExecutionTime { get; set; }
    public long MemoryUsageInBytes { get; set; }
}