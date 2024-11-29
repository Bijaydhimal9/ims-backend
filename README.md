
## Inmate Management System

## Technologies

* [ASP.NET Core 8](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)


## Run In Development

```
dotnet build
```

```
dotnet ef database update  --project src/Infrastructure --startup-project src/Web
```

```bash
dotnet watch run --project=src/Web/Web.csproj
```

NOTE: if you are working on more frequent backend changes then, run the frontend separately, [learn more here](https://learn.microsoft.com/en-us/aspnet/core/client-side/spa/react?view=aspnetcore-7.0&tabs=netcore-cli#run-the-cra-server-independently)


## Database Migrations

To add a new migration from the root folder

```
dotnet ef migrations add "migration message" --project src/Infrastructure --startup-project src/Api -o Data/Migrations
```

To remove migrations 
```
dotnet ef migrations  remove --project src/Infrastructure --startup-project src/Web
```

To update database

```bash
dotnet ef database update  --project src/Infrastructure --startup-project src/Web
```


## Docker

Build docker image

```bash
docker build -t inmatemanagementsystem . 
```

Run docker container

```bash
docker run -d -p 8080:80 --name inmatesystemapi inmatemanagementsystem
```

## Admin Authentication

```
email : admin@gmail.com
password : BookingSystem@123
```