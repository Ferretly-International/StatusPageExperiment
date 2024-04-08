# StatusPage .NET

A .NET library for [Atlassian StatusPage](https://www.atlassian.com/software/statuspage).

## Getting Started

The sample executable uses Azure App Configuration to store the StatusPage API key and page ID. You should
create an Azure App Configuration instance and add an environment variable named `AppConfigConnectionString`
with the connection string to your Azure App Configuration instance.

The sample executable will read the StatusPage API key and page ID from the Azure App Configuration instance.

## Dependencies

To integrate the library into your project, ensure that you are injecting an `IConfiguration` instance into the
services collection. 

You can use the helper method `Helpers.AddStatusPageLibrary` to add this library to the services collection. This will throw
an `InvalidOperationException` if an `IConfiguration` instance is not injected into the services collection.

## Development

Before each push to `main`, be sure to update the NuGet package version in the `StatusPageLibrary.csproj` file. This will
ensure that the NuGet package is updated with the latest changes. The build will fail if the version is not updated.
