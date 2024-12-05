# This application was developed for Web Application module, as coursework portfolio project @ WIUT by student ID: 00015589

### Project Calculation
15589 % 20 = 9 which is <b>Movie App</b>

## Movie App [Demo](https://www.youtube.com/watch?v=UpZJdla4TG0)
This movie application is build with Angular for the client and ASP.NET for the server. 

## Features
- Simple user registeration and login
- Add favorite movies and TV shows with personal comments or reviews
- View and explore other users' movie recommendations and reviews

## Technologies used
### Frontend
- Angular
- TypeScript
- Tailwind CSS for the UI
### Backend
- ASP.NET Core
- Microsoft SQL Server Management
- Entity Framework Core
- JWT Bearer
- Swashbuckle for Swagger

## Prerequisites
- Angular CLI
```bash
npm install -g @angular/cli
```
- [Node.js](https://nodejs.org/en/download/package-manager)
- [.Net 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio](https://visualstudio.microsoft.com/) (for Windows): Ensure you have the web development workload installed.
- [Visual Studio Code](https://code.visualstudio.com/download) 

## How to run
```bash
git clone https://github.com/hc-b666/movie-app.git
```
### Backend
1. Open .sln solution file inside 00015589-WebApplicationDevelopment-CW in Visual Studio | ```cd ./00015589-WebApplicationDevelopment-CW``` in Visual Studio Code
2. Migrate the models to your Database
```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
``` 
3. Click on F5 or Green Run button in Visual Studio |  ```dotnet run``` in Visual Studio Code
4. In Chrome, go to http://localhost:5029/swagger, to see the API documentation

### Frontend
1. Navigate to frontend folder ```cd ./MovieAppClient```
2. Install all the dependencies ```npm install```
3. Run the project ```ng serve```
4. In Chrome, go to http://localhost:4200, to see the frontend
