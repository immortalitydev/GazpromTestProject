using Web;

var builder = WebApplication.CreateBuilder(args);
AppStartup.ConfigureServices(builder);

var app = builder.Build();

AppStartup.ConfigurePipeline(app);

app.Run();
