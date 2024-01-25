using CodeChallanger.UI.Configurations;
using CodeChallanger.UI.Interfaces;
using CodeChallanger.UI.Services;
using CodeChallenge.Domain.Interfaces;
using CodeChallenge.Repository;
using CodeChallenge.Repository.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var databaseName = "codeChallanger"; 
var queueSectionName = "RabbitMQOptions";

builder.Services.AddDbContext<CodeChallengerContext>(options =>
    options.UseInMemoryDatabase(databaseName), ServiceLifetime.Scoped);

InitializeDatabase(builder.Services.BuildServiceProvider());

builder.Services.AddTransient<IChallengeRepository, ChallengeRepository>();
builder.Services.AddTransient<ICacheService, MemoryCacheService>();

builder.Services.Configure<RabbitMQOptions>(builder.Configuration.GetSection(queueSectionName));
builder.Services.AddSingleton<IConnectionFactory>(_ =>
{
    var rabbitMQConfig = builder.Configuration.GetSection(queueSectionName).Get<RabbitMQOptions>();
    return new ConnectionFactory()
    {
        HostName = rabbitMQConfig.HostName,
         Password=  rabbitMQConfig.Password,
         UserName = rabbitMQConfig.UserName
    };
});

builder.Services.AddSingleton<RabbitMQPublisher>();

builder.Services.AddTransient<ChallengeServices>();
builder.Services.AddHostedService<RabbitMQConsumerService>();


builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void InitializeDatabase(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<CodeChallengerContext>();

        context.Challenges.AddRange(StaticInitalDatabaseData.StaticChallanges);
        context.SaveChanges();
    }
}