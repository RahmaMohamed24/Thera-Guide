using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using TheraGuide.Context;
using TheraGuide.EmailSender;
using TheraGuide.Entity;
using TheraGuide.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddSwaggerGen();
//AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

//[Authorize] using jwt token to check Authntication
builder.Services.AddAuthentication(option =>
{
    //check for token 
    //AuthenticateScheme ÈÊÔæÝ  token  valid or not
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    //DefaultChallengeScheme  áæ  token not valid  ÈÊæÏíå á  form login
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    // option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    //check for claims
}).AddJwtBearer(options =>
{
    //ÇÊÇßÏ Çä ÇáÏÇÊÇ ÇÊÚãáåÇ  save  
    options.SaveToken = true;
    //  áæ ÚÇæÒ ÇÔÊÛá https  ÈÓ ÇÍäÇ åäÇ ÔÛÇáíä  http
    options.RequireHttpsMetadata = false;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
        //ValidateIssuer = true,
        //ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        //ValidateAudience = true,
        //ValidAudience = builder.Configuration["JWT:ValidAudience"],
        //ValidateLifetime = true,
        //ValidateIssuerSigningKey = true,
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddCors(CorsOptions =>
{
    CorsOptions.AddPolicy("MyPolicy", CorsPolicyBuilder =>
    {
        CorsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
