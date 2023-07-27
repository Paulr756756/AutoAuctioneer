using API_BidStamp.Services;
using API_BidStamp.Services.ListingService;
using API_BidStamp.Services.StampService;
using API_BidStamp.Services.UserService;
using DataAccessLayer_BidStamp;
using DataAccessLayer_BidStamp.Models;
using DataAccessLayer_BidStamp.Repositories;
using DataAccessLibrary_BidStamp;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services
    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IStampService, StampService>()
    .AddScoped<IStampRepository, StampRepository>()
    .AddScoped<IListingRepository, ListingRepository>()
    .AddScoped<IListingService, ListingService>()
    .AddScoped<IBidService, BidService>()
    .AddScoped<IBidRepository, BidRepository>();


builder.Services.AddHttpContextAccessor();

/*4
 builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String("+7C70wuX/udADESFi7Wgww=="))
    };
})*/


builder.Services.AddAuthentication().AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(
            builder.Configuration.GetSection(
                "Authentication:Schemes:Bearer:SigningKeys:0:Value").Value!)
        )
    };
});
/*IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        builder.Configuration.GetSection("AppSettings:Token").Value!))#1#
};
});*/
/*
builder.Services.AddAuthentication().AddJwtBearer();*/
builder.Services.AddCors(c => {
    c.AddPolicy("AllowOrigin", options =>
        options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<DatabaseContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("BidStampDb")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
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