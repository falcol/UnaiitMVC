# UnaiitMVC

# Database && Migration  
```bash
dotnet ef database update 
dotnet ef database drop
dotnet ef migration add name
dotnet ef migration remove 
``` 

# Create Controllers
```bash
dotnet aspnet-codegenerator controller -name SchoolController -m UnaiitMVC.Models.School.SchoolTable -dc UnaiitMVC.Models.UnaiitDbContext -udl -outDir Areas/School/Controllers/
```  

# Create Identity
```bash
dotnet aspnet-codegenerator identity -dc Unaiit.Models.UnaiitDbContext -f
```  
# Create Razor Pages
```bash
dotnet aspnet-codegenerator razorpage -m DB -dc DBContext -udl -outDir DirectoryPath --referenceScriptLibraries
```
