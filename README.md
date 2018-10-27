# ELearnerApi

The Elearner application is for a project at Athabasca University which required the creation of an online learning management solution.

## To run the application

1. Update database credentials in ~\ElearnerApi.Data\DataContext\ApplicationDbContext.cs

2. Navigate to ~\ElearnerApi\ in the solution and update the Entity Framework Migrations
` dotnet ef database update `

3. Run the application
` dotnet run ElearnerApi.csproj `

4. Launch the web application in a browser. The application should be served at http://localhost:19001
