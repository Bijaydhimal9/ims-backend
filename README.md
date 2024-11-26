## Database Migrations

To add a new migration from the root folder

```
dotnet ef migrations add "migration message" --project src/Infrastructure --startup-project src/Web -o Data/Migrations
```

To remove migrations 
```
dotnet ef migrations  remove --project src/Infrastructure --startup-project src/Web
```

To update database

```bash
dotnet ef database update  --project src/Infrastructure --startup-project src/Web
```