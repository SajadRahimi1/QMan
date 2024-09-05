dotnet ef migrations add $1 --project QMan.Infrastructure --startup-project QMan.Api
dotnet ef database update --project QMan.Infrastructure --startup-project QMan.Api
