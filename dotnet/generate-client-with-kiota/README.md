# Generating C# clients from Swagger / OpenAPI with Kiota

## Configure server project

As demonstrated in `./Server`, once an API is configured to use Swashbuckle to generate an OpenAPI spec, that spec can be used by NSwag on a client to generate modals and services.

The Server web API can be run and it creates a Swagger UI at `http://localhost:5106/swagger`. The OpenAPI spec can be found at `http://localhost:5106/swagger/swagger.json`.

## Configure the consumer to generate a client

1. Install NuGet packages

```shell
dotnet add package Microsoft.Kiota.Abstractions
dotnet add package Microsoft.Kiota.Http.HttpClientLibrary
dotnet add package Microsoft.Kiota.Serialization.Form
dotnet add package Microsoft.Kiota.Serialization.Json
dotnet add package Microsoft.Kiota.Serialization.Text
dotnet add package Microsoft.Kiota.Serialization.Multipart
```

1. Install `Microsoft.Openapi.Kiota` dotnet tool
```shell
dotnet new tool manifest
dotnet tool install Microsoft.Openapi.Kiota
```

1. Generate the client using the tool from the `server.json` OpenAPI spec
```shell
dotnet kiota generate \
  -d server.json \
  -l csharp \
  -o GeneratedClient \
  -c Client \
  -n CSharpConsumer \
  --exclude-backward-compatible
```
