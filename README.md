# Books API 
[![Build Status](https://dev.azure.com/oroberts221/Books/_apis/build/status/owen-roberts.books-api?branchName=main)](https://dev.azure.com/oroberts221/Books/_build/latest?definitionId=1&branchName=main)

## Architecture 
The architecture of this service follows [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) principles and is based on the structure of these .net sample repositories: 
- [Clean Architecture](https://github.com/ardalis/CleanArchitecture#table-of-contents) 
- [eShopOnWeb](https://github.com/dotnet-architecture/eShopOnWeb)

The intention is to remove any dependencies between the presentation (in this case a Web API) and the data implementation. 

## Running locally
**Docker Compose**

A `docker-compose.yaml` file has been provided at the project root which will build and run the API project along with a MongoDB database and Mongo Express UI. 

A `docker-compose-db-only.yaml` has also been provided which will create only the database. This can be used alongside the VSCode launch configuration to run the project outside of a container. 

## Libraries
**General**
- [AutoMapper](https://automapper.org/)
- [MediatR](https://github.com/jbogard/MediatR)
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

**Testing**
- [AutoFixture](https://autofixture.github.io/)
- [FluentAssertions](https://fluentassertions.com/)



