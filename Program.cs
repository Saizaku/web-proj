using web_proj.Presistance.Contexts;
using web_proj.Services;
using web_proj.Services.Impl;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
      // To preserve the default behavior, capture the original delegate to call later.
        var builtInFactory = options.InvalidModelStateResponseFactory;

        options.InvalidModelStateResponseFactory = context =>
        {
            var logger = context.HttpContext.RequestServices
                                .GetRequiredService<ILogger<Program>>();

            // Perform logging here.
            // ...

            // Invoke the default behavior, which produces a ValidationProblemDetails
            // response.
            // To produce a custom response, return a different implementation of 
            // IActionResult instead.
            return builtInFactory(context);
        };
    });
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<IMovieService, MovieServiceImpl>();
builder.Services.AddScoped<IWatchListService, WatchListServiceImpl>();
builder.Services.AddScoped<IAuthService, AuthServiceImpl>();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapControllers();

app.UseCors("corsapp");

app.Run();
