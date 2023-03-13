using Bogus;
using Moq;
using OnlineStore.Data;
using OnlineStore.Data.UnitOfWork;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoriesInterfaces;
using OnlineStore.Domain.Services;


namespace SuperShop.Domain.Test;

public class AccountServiceTestsMoq
{
    // Bogus
    private readonly Faker _faker=new ();

    [Fact]
    private async void Register()
    {
        // Arrenge: Create Mocks // Moq
        var cartRepositoryMock = new Mock<ICartRepository>();
        var accountRepositoryMock = new Mock<IAccountRepository>();
        var uowMock = new Mock<IUnitOfWork>();
        var passwordHasherMock = new Mock<IPasswordHasherService>();
        var tokenServiceStub = new Mock<ITokenService>();
        
        // Arrange Setup
        uowMock.Setup(uow => uow.AccountRepository).Returns(accountRepositoryMock.Object);
        uowMock.Setup(uow => uow.CartRepository).Returns(cartRepositoryMock.Object);
        passwordHasherMock.Setup(x => x.HashPassword(It.IsAny<string>())).
            Returns<string>(x => x);
        
        // ACT
        var accountService = new AccountService(
            uowMock.Object,passwordHasherMock.Object,tokenServiceStub.Object);
        
        var (account,_) = await accountService.RegisterAccount(
            _faker.Person.FullName,_faker.Person.Email,_faker.Internet.Password());
        
        // Asert
        accountRepositoryMock.Verify(x =>x.Add(It.Is<Account>(
            a=> a == account),default),Times.Once);
        
        cartRepositoryMock.Verify(x=>x.Add(It.Is<Cart>(
            c=>c.AccountId==account.Id),default),Times.Once);
        
        uowMock.Verify(x=>x.CommitAsync(default), Times.AtLeastOnce);
    }
}