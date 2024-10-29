# Innovation Lab Backend Template

## Description

This is a template project aimed to create backends for Innovation Labs. It's using C# and ASP.NET Core targetting .NET 8 with the following packages included:

- [FastEndpoints](https://fast-endpoints.com/): an opinionated framework to create endpoints using the REPR (Request-Endpoint-Response) Design Pattern. Please follow the documentation to configure endpoints. All endpoints in the template are in the folder Features (following the Vertical-Slice-Architecture).
- Entity Framework Core 8 with SQL Server support. All models are added in the Models folder and the context file is located in the Data folder.
- Microsoft Identity Web (to access Microsoft's Entra ID-generated JWTs).

## Installation

To install the template globally in your machine, open any terminal (powershell, cmd) and follow these steps:

- Clone this repo and change the directory to the newly cloned folder.
- Execute `dotnet new install .` to install the template.

After that, the new template will be available to be used in any IDE.

![image](https://github.com/user-attachments/assets/6f9dbb85-45c9-4fa8-9467-e394d109b0a4)

## Settings

We recommend using User Secrets as they're a secure way to have secrets in the local machine and not in the repository you'll upload it. Remember to create an app registration to generate access to Entra ID (the settings required for Microsoft Identity).

```json
{
    "ConnectionStrings:DefaultConnection": "<database connection string>",
    "AzureAd:ClientId": "<entra id requirement>",
    "AzureAd:TenantId": "<entra id requirement>",
    "AzureAd:Instance": "<entra id requirement>",
    "AzureAd:Scope": "<entra id requirement>"
}
```

## Don't forget

- Models in this template are using the Entity-First approach. You need to install the [dotnet ef tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) to generate migrations and apply them.
- Any database provider that supports EF Core can be used if SQL Server is not needed.
- As any other dotnet project, this can be extended in any way possible. 
