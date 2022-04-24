# Books API 
[![Build Status](https://dev.azure.com/owen-personal/Books%20Demo/_apis/build/status/owen-roberts.books-demo?branchName=main)](https://dev.azure.com/owen-personal/Books%20Demo/_build/latest?definitionId=2&branchName=main)

## Architecture 
The architecture of this service follows [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) principles and is based on a similar structure to these .NET sample repositories: 
- [Clean Architecture Worker Service](https://github.com/ardalis/CleanArchitecture.WorkerService)
- [Clean Architecture](https://github.com/ardalis/CleanArchitecture#table-of-contents) 
- [eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)

## Running locally
**Docker Compose**

A `docker-compose.yaml` file has been provided at the project root which will build and run the API project along with a MongoDB database and Mongo Express UI. 
This will bring up the API service on `http://localhost:8080`

A `docker-compose-db-only.yaml` has also been provided which will create a separate test database on port `27018` which is used to run the integration tests. 

## Libraries
The following third party libraries were used: 

**General**
- [AutoMapper](https://automapper.org/)
- [MediatR](https://github.com/jbogard/MediatR)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

**Testing**
- [AutoFixture](https://autofixture.github.io/)
- [FluentAssertions](https://fluentassertions.com/)



