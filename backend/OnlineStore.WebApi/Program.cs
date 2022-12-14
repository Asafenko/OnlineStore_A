using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data;
using OnlineStore.Data.Repositories.Account_Repo;
using OnlineStore.Data.Repositories.Product_Repo;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoriesInterfaces;
using OnlineStore.Domain.Services;
using OnlineStore.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddScoped<AccountService>();
builder.Services.AddSingleton<IPasswordHasherService, Pbkdf2PasswordHasher>();
// STEP 5
const string dbPath = "myapp.db";
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));


// Паттерн Generic Repository: Регистрация
//builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));



// Adding CORS
builder.Services.AddCors();


//
var app = builder.Build();



// CORS
app.UseCors(policy =>
{
    policy
        .AllowAnyMethod()
        .AllowAnyHeader()   // White list domain
        . WithOrigins("https://localhost:7079","https://localhost:5001");
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
// app.MapPost("/add", async ([FromServices]IAppDbContextRepository dbContext,[FromBody] Product product,HttpResponse response,CancellationToken cts) =>
// {
//     product.Id = new Guid();
//     await dbContext.AddProduct(product,cts);
//     response.StatusCode = StatusCodes.Status201Created;
//     return response.StatusCode;
//     //return Results.Created($"http://localhost/add/{product.Id}",null);
// });


// UPDATE PRODUCT BY ID
// app.MapPut("/product/{id:guid}", async ([FromServices]IAppDbContextRepository dbContext,[FromBody]Product product,[FromRoute] Guid id,CancellationToken cts) =>
// {
//     var productUP =  await dbContext.UpdateProduct(product,cts);
//     return productUP;
// });


// DELETE PRODUCT BY ID
// app.MapDelete("/product/{id:guid}",async ([FromServices]IAppDbContextRepository dbContext,[FromRoute] Guid id,CancellationToken cts) =>
// {
//     var product = await dbContext.DeleteProduct(id,cts);
//     return product;
// });

// DELETE
// app.MapDelete("/delete", async ([FromServices]IAppDbContextRepository dbContext,CancellationToken cts) =>
// {
//    await dbContext.Delete(cts);
// });







// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
// Метод MapControllers настраивает действия контроллера API в вашем приложении как конечные точки.
app.MapControllers();

app.Run();


// ШАГ 1: Установка dotnet-ef
//Устанавливаем в систему инструмент для проектирования БД во время разработки.
//Он позволит создавать БД и миграции: dotnet tool install --global dotnet-ef

// ШАГ 2: Добавляем ef-tool к проекту
// тобы получить возможность использовать ef-tool из проекта, нужно добавить к проекту пакет: Microsoft.EntityFrameworkCore.Design

// Шаг 3: Добавляем провайдер
// Добавляем NuGet пакет с провайдером для необходимой БД. Например, Microsoft.EntityFrameworkCore.Sqlite

// ШАГ 4: Добавляем модель
// public class AppDbContext : DbContext
// {
//     //Список таблиц:
//     public DbSet<Order> Orders => Set<Order>();
//
//     public AppDbContext(
//         DbContextOptions<AppDbContext> options) 
//         : base(options)
//     {
//     }

// ШАГ 5: Регистрируем зависимость
// var dbPath = "myapp.db";
// builder.Services.AddDbContext<AppDbContext>(
// options => options.UseSqlite($"Data Source={dbPath}"));


// ШАГ 6: Создаем БД
// dotnet ef migrations add InitialCreate
// Выполните эту команду именно из папки с проектом, а не из папки с решением

//Миграция Пример : dotnet ef migrations add <MigrationName>

// ШАГ 7: Применение миграций
// Не забудьте вызвать применение миграций путем вызова команды:
//dotnet ef database update

// ШАГ 8: Внедряем зависимость
// app.MapGet("/orders", async (AppDbContext context)
// => await context.Orders.ToListAsync());