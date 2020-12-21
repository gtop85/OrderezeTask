# OrderezeApp
3 tier architecture Web Application for image management consisting of:

 - Data layer is SQL Server with EFCore & Azure Blob Storage
 - Backend is .NET Core 3.1 Web API (fully testable with Swagger)
 - Frontend is React on .NET Core

The aim of the project is to create a simple environment to manage the images. User may be able to:

1) Show a list of the uploaded images

2) Upload a new image. For this, a new entry to the db and to the azure blob storage is created.

3) Delete image

