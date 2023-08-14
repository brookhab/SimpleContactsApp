# SimpleContactsApp

Project URL: https://simplecontactapp.azurewebsites.net

# Introduction

The Simple Contact App is a straightforward demonstration of contact management, allowing users to store, retrieve, create, update, and delete contacts online. It empowers users with the ability to register and create accounts, granting them control over their contact lists. This application is built using SP.NET Core serving as the backend and ASP.NET Pages handling the frontend. The application is hosted on Azure, utilizing its cloud infrastructure for seamless deployment.

# Frontend

The frontend user interface is crafted using ASP.NET Pages and enhanced with the Bootstrap library to provide a clean and intuitive look.

# Backend

The backend is developed with .NET Core, and its architecture follows the principles of the Onion Architecture, promoting maintainability and readability. The design decisions made in this architecture make the codebase understandable and extensible for future developers.

# Database
Azure SQL Database serves as the application's data store, seamlessly connected through Entity Framework. This integration enables us to apply various CRUD (Create, Read, Update, Delete) operations efficiently.

# Testing

 The core components, including Web, Application, and Infrastructure, are thoroughly tested. Mocking is facilitated using Moq, while Xunit serves as the testing framework. The test suite encompasses both unit and integration tests.

# Deployment

The Simple Contact App is hosted on Azure App Service. The entire deployment process, from setting up Resource Groups to configuring resources is created manually. Azure Key Vault securely stores the database connection string, enabling seamless authentication and retrieval during startup. Azure SQL Database efficiently manages the application's data, and Application Insights monitors the app's performance, providing real-time insights.

Below are a couple of screenshots showcasing the app's Azure integration:

![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/b05485b9-f745-476d-a6ba-038e684550e8)
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/7ed69d99-8d45-4f4d-9fd9-299e9b2ffe55)
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/3a67b340-d113-4b11-9b62-9a090bd5c735)

  

# Future Development and Enhancements 

- Further refining the API to follow RESTful practices
- Implementing filtering and sorting on both the API and UI fronts
- Introducing session management to enhance user experience
- Enabling users to upload and manage user profiles using Azure Blob Storage
- Establishing CI/CD pipelines to automate deployment and run comprehensive unit tests using yamal scripts
- Adding Iac using terraform for automatic creation of resources and reduce manual intervention 


## Thank you for exploring my App! 
