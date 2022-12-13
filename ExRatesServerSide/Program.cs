
using ExRatesServerSide.Services;
using ExRatesServerSide.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();

builder.Services.AddLogging(loggingBuilder => {
    loggingBuilder.AddFile("logs/log_{0:yyyy}-{0:MM}-{0:dd}.log", fileLoggerOpts => {
        fileLoggerOpts.FormatLogFileName = fName => {
            return String.Format(fName, DateTime.UtcNow);
        };
    });
});

var config = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
          .AddEnvironmentVariables()
          .Build();

var nbrbApiUrl = config.GetValue<string>("nbrbApiUrl");
var coincapApiUrl = config.GetValue<string>("coincapApiUrl");

builder.Services.AddHttpClient("nbrbClient", config =>
{
    config.BaseAddress = new Uri(nbrbApiUrl);
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});

builder.Services.AddHttpClient("coincapClient", config =>
{
    config.BaseAddress = new Uri(coincapApiUrl);
    config.Timeout = new TimeSpan(0, 0, 30);
    config.DefaultRequestHeaders.Clear();
});

builder.Services.AddScoped<IExRatesCacheService, ExRatesCacheService>();
builder.Services.AddScoped<ISerializeExRatesService, SerializeExRatesService>();
builder.Services.AddScoped<IExRatesService, ExRatesService>();
builder.Services.AddScoped<ICoincapService, CoincapService>();
builder.Services.AddScoped<InbrbService, nbrbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
