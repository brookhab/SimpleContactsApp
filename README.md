# SimpleContactsApp

Project URL: https://simplecontactswebapp-asp.azurewebsites.net/index.asp

# Introduction

The Simple Contact App is a straightforward demonstration of contact management, allowing users to store, retrieve, create, update, and delete contacts online. It empowers users with the ability to register and create accounts, granting them control over their contact lists. This application is built using SP.NET Core serving as the backend and ASP.NET Pages handling the frontend. The application is hosted on Azure, utilizing its cloud infrastructure for seamless deployment.

# Frontend

The frontend user interface is crafted using Classic ASP pages and enhanced with the Bootstrap library to provide a clean and intuitive look.

# Backend

The backend is developed with .NET Core, and its architecture follows the principles of the Onion Architecture, promoting maintainability and readability. The design decisions made in this architecture make the codebase understandable and extensible for future developers.

# Database

Azure SQL Database serves as the application's data store, seamlessly connected through Entity Framework. This integration enables us to apply various CRUD (Create, Read, Update, Delete) operations efficiently.

# Session Sharing Between Classic ASP and .NETCore

When a user logs in through UI, the api will send a request witht he "userId" as a custom Session value via a request to Classic ASP Bridge. The Bridge is a small Classic ASP page that captures the userId that was sent as query parameter and stores it in the current session which makes it avaliable and accessiable for all other page that are under the same session. Then the UI uses the session value to make the basic CRUD operations for the given user.


# Testing

 The core components, including Web, Application, and Infrastructure, are thoroughly tested. Mocking is facilitated using Moq, while Xunit serves as the testing framework. The test suite encompasses both unit and integration tests.

# UI Contenerization
The UI is contenerized using docker and deployed to its own hosting environment using Azure App Service via the CI/CD pipeline. Azure container Registery is also used to store the docker images as showen below :
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/34c44ef6-1727-41b9-97da-ca69634abe6b)

![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/a2648eaf-3338-4159-8abd-a7937e2957f4)


# CI/CD Pipelines
The application has a dedicated CI/CD pipeline that runs unit tests and generates a test report after each run. There is also a dedicated release pipeline that is connected to Azure App Service that will deploy upon successful completion of the build pipeline. 

AzureDevops Build Pipeline 
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/a6ea706a-7b09-4261-869b-787e92675384)

![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/0cb3026a-aa10-4140-a0f3-e1c09f1a2570)


Azure Devops Release Pipeline
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/8973d38e-a14c-41cc-ac80-2cc055755a5a)

![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/91939b3d-c2fb-4c21-958a-3d22ba999424)

# Deployment

The Simple Contact App is hosted on Azure App Service. The entire deployment process, from setting up Resource Groups to configuring resources is created manually. Azure Key Vault securely stores the database connection string, enabling seamless authentication and retrieval during startup. Azure SQL Database efficiently manages the application's data, and Application Insights monitors the app's performance, providing real-time insights. 

Below are a couple of screenshots showcasing the app's Azure integration:
Azure Resources
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/8e7b1fa7-3f61-4e86-bae3-2d87ab5769ec)

Azure Redis
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/a2305ad9-515b-4170-9e2c-b74ac109ae41)


Azure App Service 
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/620ac1af-7ff5-4e00-870e-c4e7e25cfb79)


Azure AppInsights
![image](https://github.com/brookhab/SimpleContactsApp/assets/11322420/3e123d93-bcf4-49a3-958c-0df9c8363cf3)

# Future Development and Enhancements 

 - [ ] Further refining the API to follow RESTful practices
 - [ ] Implementing filtering and sorting on both the API and UI fronts
 - [X] Introducing session management to enhance user experience
 - [ ] Enabling users to upload and manage user profiles using Azure Blob Storage
 - [X] Establishing CI/CD pipelines to automate deployment and run comprehensive unit tests using yamal scripts
 - [ ] Adding Iac using terraform for automatic creation of resources and reduce manual intervention 


## Thank you for exploring my App! 
