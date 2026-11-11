using DalilAcademy.API.Middleware;
using DalilAcademy.Core.Repository;
using DalilAcademy.Core.Security;
using DalilAcademy.Core.Service;
using DalilAcademy.Core.Storage;
using DalilAcademy.Core.Validators;
using DalilAcademy.Infra.Data;
using DalilAcademy.Infra.Repository;
using DalilAcademy.Infra.Security;
using DalilAcademy.Infra.Service;
using DalilAcademy.Infra.Storage;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. ربط ال AppDbContext مع قاعدة بيانات PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. تسجيل ال Repositories (Dependency Injection)
builder.Services.AddScoped<ICourseRepository,CourseRepository>();
builder.Services.AddScoped<IStudentRepository,StudentRepository>();
builder.Services.AddScoped<IInstructorRepository,InstructorRepository>();
builder.Services.AddScoped<IEnrollmentRepository,EnrollmentRepository>();
// 2. تسجيل ال Repositories (Dependency Injection) Table User
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();


// 3.تسجيل ال Services (Dependency Injection)
builder.Services.AddScoped<ICourseService,CourseService>();
builder.Services.AddScoped<IStudentService,StudentService>();
builder.Services.AddScoped<IInstructorService,InstructorService>();
builder.Services.AddScoped<IEnrollmentService,EnrollmentService>();

//تسجيل  exchanerateApi service
builder.Services.AddHttpClient<ICurrencyService, CurrencyService>(client =>
{
    client.BaseAddress = new Uri(
        $"{builder.Configuration["ExchangeRate:BaseUrl"]}{builder.Configuration["ExchangeRate:ApiKey"]}/");
});
// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    }); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "Dalil Academy LMS API",
        Version = "v1",
        Description = "Learning Management System API built with ASP.NET Core 10"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Paste the token from /api/Auth/login"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

// 4. تسجيل ال Validators (Dependency Injection)
builder.Services.AddValidatorsFromAssemblyContaining<CreateStudentDTOValidator>();  
builder.Services.AddValidatorsFromAssemblyContaining<CreateEnrollmentDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateInstructorDTOValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateCourseDTOValidator>();

builder.Services.AddFluentValidationAutoValidation();
//Handle exceptions globally
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
//___________________________
//Configure JWT authentication كيف يقراء السيكريت  اللي اعملناها  وكيف يفحص التوكن
var jwt = builder.Configuration.GetSection("jwt").Get<JwtSetting>();//هاذ اللاين  عشان  نستخدمها هون  في سي س  الsetting  اللي عملناهافي 
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("jwt"));// نقوم بتسجيل وتوصيل قسم "Jwt" من ملف appsettings.json مع كلاس الـ JwtSetting في نظام الـ DI (Dependency Injection)
// لكي نتمكن من حقن وحفظ هذه الإعدادات (مثل الـ Issuer، Audience، والمفتاح السري) واستخدامها بسهولة داخل الـ Services عبر IOptions<JwtSetting>
//_________________________________
//تففعيل ال authentication  
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>

    options.TokenValidationParameters = new TokenValidationParameters()

    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)),
        ClockSkew = TimeSpan.FromMinutes(1)
    });
    builder.Services.AddAuthorization();
//تخزين  ال wwwroot Images  في ال API
builder.Services.AddSingleton<IImageService, LocalImageService>();




var app = builder.Build();//-------------------------Line
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Dalil Academy API v1");
        options.RoutePrefix = string.Empty; // Swagger at root
    });
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
