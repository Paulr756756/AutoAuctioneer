using API_AutoAuctioneer.Services.BidService;
using API_AutoAuctioneer.Services.CarService;
using API_AutoAuctioneer.Services.ListingService;
using API_AutoAuctioneer.Services.PartService;
using API_AutoAuctioneer.Services.UserService;
using DataAccessLayer_AutoAuctioneer.Repositories.Implementations;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.CodeDom.Compiler;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services    
    .AddScoped<IBaseRepository, BaseRepository>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<ICarRepository, CarRepository>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<ICarService, CarService>()
    .AddScoped<IPartRepository, PartRepository>()
    .AddScoped<IPartService, PartService>()
    .AddScoped<IListingRepository, ListingRepository>()
    .AddScoped<IListingService, ListingService>()
    .AddScoped<IBidService, BidService>()
    .AddScoped<IBidRepository, BidRepository>()
    .AddScoped<IItemRepository, ItemRepository>()


    .AddHttpContextAccessor()
    .AddLogging(logging => {
        logging.ClearProviders();
        /*logging.AddConsole();*/
        logging.AddJsonConsole( options=> {
            options.IncludeScopes = true;
            options.TimestampFormat = "HH:mm:ss";
            options.JsonWriterOptions = new JsonWriterOptions {
                Indented= true,
            };
        });
    })

    .AddCors(c =>
    {
        c.AddPolicy("AllowOrigin", options =>
            options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    })
    
    .AddAuthentication().AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(
                builder.Configuration.GetSection(
                    "Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!)
            )
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

//Setting the CORS policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();