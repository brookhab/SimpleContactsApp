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

# Session Sharing

 ASP.NET pages and .NET Core backend use the same Redis cache for session storage. When a user logs in through UI, the application stores a unique identifier in the session, that identifier will be accessible in the shared Redis cache. When the user navigates to the .net core application, that unique identifier will be retrieved from the session and used to access user-specific data, like querying list of contacts and updating contacts. 
Here are some pictures of Session keys in Redis: 

![redis screenshot](https://github.com/brookhab/SimpleContactsApp/assets/11322420/82905027-5e25-45a6-a9d9-77b9330f0ae6)


# Testing

 The core components, including Web, Application, and Infrastructure, are thoroughly tested. Mocking is facilitated using Moq, while Xunit serves as the testing framework. The test suite encompasses both unit and integration tests.

# CI/CD Pipelines
The application has a dedicated CI/CD pipeline that runs unit tests and generates a test report after each run. There is also a dedicated release pipeline that is connected to Azure App Service that will deploy upon successful completion of the build pipeline. 

AzureDevops Build Pipeline 
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/d21dc90f-5c99-4a7f-86e5-1437a436bf7d)

![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/cb2facf8-8f05-460c-85cf-0a1e48b1a3ff)

Azure Devops Release Pipeline
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/ba803b77-2047-4f57-85c9-301ad79ac7b6)

# Deployment

The Simple Contact App is hosted on Azure App Service. The entire deployment process, from setting up Resource Groups to configuring resources is created manually. Azure Key Vault securely stores the database connection string, enabling seamless authentication and retrieval during startup. Azure SQL Database efficiently manages the application's data, and Application Insights monitors the app's performance, providing real-time insights. 

Below are a couple of screenshots showcasing the app's Azure integration:
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/f35efc03-8431-4f44-b78c-d2a96f26ffa5)
Azure Redis
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/fcb5ae3b-47e3-4686-8e5c-f2d9272508b7)

Azure App Service 
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/cbe5aa08-8ce7-438b-9056-27ae9c008370)

Azure AppInsights
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/8d658745-b37c-4583-9d91-d1e220ce673a)


# Future Development and Enhancements 

- [] Further refining the API to follow RESTful practices
- [] Implementing filtering and sorting on both the API and UI fronts
- [X] Introducing session management to enhance user experience
- [] Enabling users to upload and manage user profiles using Azure Blob Storage
- [X] Establishing CI/CD pipelines to automate deployment and run comprehensive unit tests using yamal scripts
- [] Adding Iac using terraform for automatic creation of resources and reduce manual intervention 


## Thank you for exploring my App! 
