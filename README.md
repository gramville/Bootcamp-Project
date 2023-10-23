## Product Brand Category Management System
This repository contains a web application built using ASP.NET 6 MVC that provides a comprehensive solution for managing products, brands, and categories. The application utilizes MSSQL as its database backend and offers functionalities for creating, updating, and deleting products, brands, and categories. Additionally, it includes a feature to view and restore deleted products, brands, and categories.

## Features
Product Management: Add, update, and delete products with ease. Each product can be associated with a brand and category.

Brand Management: Manage brands by creating new ones, updating existing ones, and deleting unnecessary brands.

Category Management: Organize products into categories, create new categories, update existing ones, and remove categories when necessary.

Deleted Items: View a list of deleted products, brands, and categories. Deleted items can be restored if needed.

## Technologies Used
ASP.NET 6 MVC: The web application framework used for building the project.

MSSQL Database: The database management system used to store and retrieve data.

## Getting Started
To run the application locally, follow these steps:

Make sure you have ASP.NET 6 SDK and MSSQL installed on your machine.

Clone this repository to your local machine using the following command:

Copy
git clone https://github.com/gramville/repository-name.git


Open the solution in your preferred development environment (e.g., Visual Studio, Visual Studio Code).

Update the connection string in the appsettings.json file with your database credentials.

Open the Package Manager Console in Visual Studio by going to "Tools" -> "NuGet Package Manager" -> "Package Manager Console".

In the Package Manager Console, run the following command to add a new migration:


Add-Migration MigrationName


Replace "MigrationName" with a descriptive name for your migration.

After the migration is created, run the following command in the Package Manager Console to apply the migration and update the database:


Update-Database


This command will create the necessary tables in the database based on the migration.

Start the application and access it through your preferred web browser.

## Usage
Once the application is running, you can access the various management functionalities through the user interface. Here are the main sections of the application:

Products: Add, update, and delete products. Associate each product with a brand and category.

Brands: Manage brands by creating new ones, updating existing ones, and deleting unnecessary brands.

Categories: Organize products into categories. Create new categories, update existing ones, and remove categories when necessary.

Deleted Items: View a list of deleted products, brands, and categories. You can choose to restore any deleted item as needed.

