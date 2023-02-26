# OnlineStore_AspNet

1)You will need to run migrations from WebApi: 
cd .../OnlineStore.WebApi

2)You will go to the project from where you will need to carry out migrations: 
dotnet ef migrations add <Name-Migrations or init> -p .../OnlineStore.Data

3)At the end write the update command: 
dotnet ef database update
