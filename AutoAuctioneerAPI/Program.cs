using API_AutoAuctioneer.Services;
using API_AutoAuctioneer.Services.ListingService;
using API_AutoAuctioneer.Services.UserService;
using DataAccessLayer_AutoAuctioneer.Repositories;
using DataAccessLibrary_AutoAuctioneer;
using Microsoft.EntityFrameworkCore;
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
    .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IListingRepository, ListingRepository>()
    .AddScoped<IListingService, ListingService>()
    .AddScoped<IBidService, BidService>()
    .AddScoped<IBidRepository, BidRepository>()
    
    .AddHttpContextAccessor()
    
    .AddDbContext<DatabaseContext>(options => options
        .UseNpgsql(builder.Configuration.GetConnectionString("BidStampDb")))
    
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