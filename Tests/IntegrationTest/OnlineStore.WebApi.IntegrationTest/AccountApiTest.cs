using Bogus;
using FluentAssertions;
using GreatShop.WebApi.Test;
using OnlineStore.Domain.Exceptions;
using OnlineStore.HttpApiClient;
using OnlineStore.HttpModels.Requests;

namespace OnlineStore.WebApi.IntegrationTest;

public class AccountApiTest : IClassFixture<CustomWebApplicationFactory<Program>> 
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    // Bogus
    private readonly Faker _faker= new();
    
    public AccountApiTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async void Registration_user_successfully()
    {
        // Arrenge
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var registerRequest = new RegisterRequest()
        {
            Email = _faker.Person.Email,
            Name = _faker.Person.FullName,
            Password = _faker.Internet.Password()
        };

        // ACT
        var registerResponse = await client.Registration(registerRequest);

        // Assert
        registerResponse.Should().NotBeNull();
        registerResponse.Email.Should().Be(registerRequest.Email);
        registerResponse.AccountId.Should().NotBeEmpty();

    }

    [Fact]
    public async void Registration_user_already_exist_is_impossible()
    {
        // Arrenge
        var httpClient =  _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var registerRequest = new RegisterRequest()
        {
            Email = _faker.Person.Email,
            Name = _faker.Person.FullName,
            Password = _faker.Internet.Password()
        };
        // ACT
        await client.Registration(registerRequest);
        
        // Assert
        await FluentActions.Invoking((() => client.Registration(registerRequest)))
            .Should()
            .ThrowAsync<HttpBadRequestException>()
            .WithMessage("*This Email already exists*");
    }

    [Fact]
    public async void Login_user_successfully()
    {
        // Arrange
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient :httpClient);
        var registerRequest = new RegisterRequest()
        {
            Name = _faker.Person.FullName,
            Email = _faker.Person.Email,
            Password = _faker.Internet.Password()
        };
        await client.Registration(registerRequest);
        
        // ACT
        var loginRequest = new LogInRequest()
        {
            Email = registerRequest.Email,
            Password = registerRequest.Password
        };
        var loginResponse = await client.Login(loginRequest);
        
        // Assert
        loginResponse.Should().NotBeNull();
        loginResponse.Email.Should().Be(registerRequest.Email);
        loginResponse.AccountId.Should().NotBeEmpty();
    }

    [Fact]
    public async void Login_user_with_incorrect_password_is_impossible()
    {
        // Arrange
        var httpClient = _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        var registerRequest = new RegisterRequest()
        {
            Name = _faker.Person.FullName,
            Email = _faker.Person.Email,
            Password = _faker.Internet.Password()
        };
        await client.Registration(registerRequest);
        
        // ACT
        var loginRequest = new LogInRequest()
        {
            Email = registerRequest.Email,
            Password = _faker.Internet.Password()
        };
        
        // Assert
        await FluentActions.Invoking((() => client.Login(loginRequest)))
            .Should()
            .ThrowAsync<HttpBadRequestException>()
            .WithMessage("*Invalid Password*");
    }
    
    [Fact]
    public async void Login_user_with_incorrect_email_is_impossible()
    {
        // Arrange
        var httpClient =  _factory.CreateClient();
        var client = new ShopClient(httpClient: httpClient);
        
        // ACT
        var loginRequest = new LogInRequest()
        {
            Email = _faker.Person.Email,
            Password = _faker.Internet.Password()
        };
        
        // Assert
        await FluentActions.Invoking((() => client.Login(loginRequest)))
            .Should()
            .ThrowAsync<HttpBadRequestException>()
            .WithMessage("*This Email was not found*");
    }
    
}