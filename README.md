# Inventory System

A simple inventory management system built using ASP.NET Core Web API, providing CRUD operations for managing product inventory.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Running Tests](#running-tests)
- [Contributing](#contributing)
- [License](#license)
- [Contact Information](#contact-information)
- [Feedback](#feedback)

## Features

- Create, read, update, and delete products in inventory
- RESTful API using ASP.NET Core Web API
- Support for in-memory or relational databases
- Test suite with integration tests

## Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- (Optional, for relational databases) MySQL or another relational database system

## Installation

1. Clone the repository: 		
   git clone https://github.com/zajigalich/InventorySystem.git

2. (Optional) If you want to use a relational database, set up your database and update the connection string in the `appsettings.json` file in the `InventorySystem.API` directory:
	
	"DefaultConnection": "Server=localhost;Database=inventory;User=user;Password=password"

3. Install the required dependencies:
	dotnet restore
	
4. Build the project:
	dotnet build

5. (Optional) If you want to run the test suite, navigate to the `InventorySystem.Tests` directory, then run:
	dotnet test


## Usage

1. Start the application:
	dotnet run --project InventorySystem

2. The API server will be running at `http://localhost:5000`. You can interact with it using an HTTP client like [Postman](https://www.postman.com) or [curl](https://curl.se/), or you can build a front-end application to consume the API.

Sample API endpoints:

- Get all products: `GET /api/products`
- Get a single product: `GET /api/products/{id}`
- Create a new product: `POST /api/products` with JSON body
- Update an existing product: `PUT /api/products/{id}` with JSON body
- Delete a product: `DELETE /api/products/{id}`

## Running Tests

To run the test suite, navigate to the `InventorySystem.Tests` directory and run the following command:

dotnet test


## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

## Contact Information
- [GitHub](https://github.com/zajigalich)
- [LinkedIn](https://www.linkedin.com/in/%D1%82%D0%B8%D1%82%D0%B0%D1%80%D0%B5%D0%BD%D0%BA%D0%BE-%D0%BA%D0%BE%D0%BD%D1%81%D1%82%D0%B0%D0%BD%D1%82%D0%B8%D0%BD-6a5ba3269/)


## Feedback 
- Was it easy to complete the task using AI? 
  
	Yes, it was easy
  
- How long did task take you to complete? (Please be honest, we need it to gather anonymized statistics) 
 
	About 3-5 hours
	
- Was the code ready to run after generation? What did you have to change to make it usable?
 
	No, it wasn't. I had to change test configuration file `CustomWebApplicationFactory`

- Which challenges did you face during completion of the task?
 
	Some explanations of mistakes in generated code lead to GPT repeating simular mistakes
	
- Which specific prompts you learned as a good practice to complete the task?

	Starting conversation with description of desired projects and providing acceptence criteria, it's usefull to add: 

	Create a list of tasks with examples of prompts I can ask you for each task to get relevant examples. 

	Thus, it's easy to nagigate beetween all stages of development using provided promts.