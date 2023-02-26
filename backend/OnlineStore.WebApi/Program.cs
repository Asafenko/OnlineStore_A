using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineStore.Data;
using OnlineStore.Data.Repositories.Account_Repo;
using OnlineStore.Data.Repositories.Cart_Repo;
using OnlineStore.Data.Repositories.Product_Repo;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Domain.RepositoriesInterfaces;
using OnlineStore.Domain.Services;
using OnlineStore.WebApi.Configurations;
using OnlineStore.WebApi.FilterExceptions;
using OnlineStore.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

//Глобальное применение фильтра Exceptions
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CentralizedExceptionHandlingFilter>(order: 0);
    options.Filters.Add<ApiKeyFilter>();
    
});


// Add services to the container.
// Метод AddControllers добавляет в ваше приложение необходимые сервисы для контроллеров API.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Repository REGIST
// DbContext does not support Thread-safe
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICartRepository,CartRepository>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CartService>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();
// Unit Of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWorkEf>();
// Adding CORS
builder.Services.AddCors();
//позволяет настроить хеширование
builder.Services.Configure<PasswordHasherOptions>(
    opt => opt.IterationCount = 10_000);
builder.Services.AddSingleton<IPasswordHasherService, Pbkdf2PasswordHasher>();
// STEP 5
const string dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));
// Паттерн Generic Repository: Регистрация
//builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));


// Token JWT
// Шаг 6: Считывание параметров токена
var jwtConfig = builder.Configuration
    .GetSection("JwtConfig")
    .Get<JwtConfig>()!;

// Шаг 2: Добавление аутентификации и авторизации
builder.Services.AddSingleton(jwtConfig);
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
          
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudiences = new[] { jwtConfig.Audience },
            ValidIssuer = jwtConfig.Issuer
        };
    });
builder.Services.AddAuthorization();

// Поддержка авторизации в Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer'" +
                      " [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciO_iJTQwZ8\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});






// Логирование всех запросов и ответов
builder.Services.AddHttpLogging(options => //настройка
{
    options.LoggingFields = HttpLoggingFields.RequestHeaders
                            | HttpLoggingFields.ResponseHeaders
                            | HttpLoggingFields.RequestBody
                            | HttpLoggingFields.ResponseBody;
});


var app = builder.Build();


//Простой Middleware
app.Use(async (context, next) =>
{
    //логика перед выполнением конвейера
    var headers = context.Request.Headers.UserAgent.ToString();
    // Should to Substitute right browser 
    if (headers.Contains("Edg"))
    {
        await context.Response.WriteAsJsonAsync(new { message = "Only Google is Supported" });
    }
    else
    {
        await next();

    }
    // await next(); //выполнение следующего метода в конвейере

    //логика, происходящая после выполнения конвейера
});


// STEP 8
//GET PRODUCTS
//  app.MapGet("/products", async ([FromServices]IAppDbContextRepository dbContext,CancellationToken cts ) =>
// {
//     var products = await dbContext.GetProducts(cts);
//     return products;
//  });
//GET PRODUCT BY ID
// app.MapGet("/product/{id:guid}", async ([FromServices]IAppDbContextRepository dbContext,Guid id,CancellationToken cts) =>
// {
//     //[FromQuery]: /product?ID=0000
//     var productId = await dbContext.GetProduct(id,cts);
//     return Results.Ok(productId);
// });
// ADD PRODUCT
// app.MapPost("/add", async (
// [FromServices]IAppDbContextRepository dbContext,[FromBody] Product product,HttpResponse response,CancellationToken cts) =>
// {
//     product.Id = new Guid();
//     await dbContext.AddProduct(product,cts);
//     response.StatusCode = StatusCodes.Status201Created;
//     return response.StatusCode;
//     //return Results.Created($"http://localhost/add/{product.Id}",null);
// });
// UPDATE PRODUCT BY ID
// app.MapPut("/product/{id:guid}", async (
// [FromServices]IAppDbContextRepository dbContext,[FromBody]Product product,[FromRoute] Guid id,CancellationToken cts) =>
// {
//     var productUP =  await dbContext.UpdateProduct(product,cts);
//     return productUP;
// });
// DELETE PRODUCT BY ID
// app.MapDelete("/product/{id:guid}",async (
// [FromServices]IAppDbContextRepository dbContext,[FromRoute] Guid id,CancellationToken cts) =>
// {
//     var product = await dbContext.DeleteProduct(id,cts);
//     return product;
// });
// DELETE
// app.MapDelete("/delete", async (
// [FromServices]IAppDbContextRepository dbContext,CancellationToken cts) =>
// {
//    await dbContext.Delete(cts);
// });


// 1 Configure the HTTPS request pipeline.
app.UseHttpsRedirection();

app.UseStaticFiles();
 

// 2 Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//добавление HttpLoggingMiddleware в конвейер
app.UseHttpLogging();


// CORS
app.UseCors(policy =>
{
    policy
        .AllowAnyMethod()
        .AllowAnyHeader() // White list domain
        .WithOrigins("https://localhost:7079", "https://localhost:5001");
});

// Шаг 3: Добавление аутентификации и авторизации
app.UseAuthentication();
app.UseAuthorization();

// Метод MapControllers настраивает действия контроллера API в вашем приложении как конечные точки.
app.MapControllers();

//app.UseMiddleware<RequestLoggingMiddleware>();

app.Run();


