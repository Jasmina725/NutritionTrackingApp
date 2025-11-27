# Nutrition Tracking Application

This is a **Nutrition Tracking Application** that allows users to track their daily nutritional intake, set goals, and monitor whether they are meeting those goals. It integrates with the **Spoonacular API** to fetch detailed nutritional data for food items and stores this information in a database. The application consists of three main tabs:

- **Today**: Add and view food entries for the current day.
- **History**: View past food entries and total calories for specific dates.
- **Goals**: Set and track daily nutritional goals (e.g., calories, protein, carbs, fat, sugar).

## Technology Stack

### Frontend:
- HTML, CSS (Bootstrap 5)
- JavaScript (jQuery)

### Backend:
- .NET 8 (Web-API)
- Entity Framework Core 8.0.0
- PostgreSQL

### API Documentation:
- Swagger

### Authentication:
- JWT (JSON Web Token) for secure user authentication

## Running the Application

### Prerequisites

Before running the application, ensure the following:

- **Spoonacular API Key**: You need an API key from Spoonacular. You can get it by signing up at [Spoonacular](https://spoonacular.com/food-api).
- **PostgreSQL**: The application uses PostgreSQL Server for storing user and food data.

### Steps to Run the Application

1. **Set up the Database**
   - Open `appsettings.json` (in the Backend project) and ensure the connection string to the database is correct. By default, it should look like:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=calorie_tracker;Username=postgres;Password=postgres"
     }
     ```
2. **Update the Database**
   - Open the **Package Manager Console** in Visual Studio (or use a terminal with the .NET CLI).
   - Run the following command to apply any pending migrations and set up the database:
     ```
     Update-Database
     ```

3. **Running the Application**
   - Once the database is set up, build and run the application by pressing **F5** or selecting **Run** in Visual Studio.
   - The application will start, and the API will be ready to use.
   - 
