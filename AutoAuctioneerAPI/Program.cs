using API_AutoAuctioneer.Services.UserService;
using DataAccessLayer_AutoAuctioneer.Repositories.Implementations;
using DataAccessLayer_AutoAuctioneer.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

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
    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserRepository, UserRepository>()
/*    .AddScoped<IListingRepository, ListingRepository>()
    .AddScoped<IListingService, ListingService>()
    .AddScoped<IBidService, BidService>()
    .AddScoped<IBidRepository, BidRepository>()
    .AddScoped<ICarRepository, CarRepository>()
    .AddScoped<ICarService, CarService>()
    .AddScoped<ICarPartService, CarPartService>()
    .AddScoped<IPartRepository, PartRepository>()*/
    .AddHttpContextAccessor()
    .AddLogging(logging => {
        logging.ClearProviders();
        logging.AddConsole();
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