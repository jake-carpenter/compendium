# Generating C# clients from Swagger / OpenAPI with NSwag

## Configure server project

As demonstrated in `./Server`, once an API is configured to use Swashbuckle to generate an OpenAPI spec, that spec can be used by NSwag on a client to generate modals and services.

The Server web API can be run and it creates a Swagger UI at `http://localhost:5106/swagger`. The OpenAPI spec can be found at `http://localhost:5106/swagger/swagger.json`.

## Configure the consumer to generate a client

### Add the NSwag exe to project configuration

NOTE: See `./CSharpConsumer/CSharpConsumer.csproj` for the full configuration.

The following code must be added to the csproj file of the project.

```xml
<ItemGroup>
  <PackageReference Include="NSwag.MSBuild" Version="14.0.7">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  </PackageReference>
</ItemGroup>
```

Then the target must be defined for Nswag to determine parameters for client generation

```xml
<!-- This section is needed for Nswag pre-build generation -->
<Target Name="NSwag" AfterTargets="PreBuildEvent">
  <Exec WorkingDirectory="$(ProjectDir)" Command="$(NSwagExe_Net60) run"/>
  <!-- You can add another one like above right here if you have multiple to generate in a different directory -->
</Target>
```

This section makes reference to `$(ProjectDir)` as the working directory, which can be adjusted to point to the location of the NSwag configuration. In this case, that is `server.nswag`.

### Nswag configuration file

See `./CSharpConsumer/server.nswag` for the full configuration.

In this file there are numerous options for you to customize your model/client generation. In order to generate a client, there are a few options that need to be made specifically:

```json
{
  "documentGenerator": {
    "fromDocument": {
      // This is the path to the OpenAPI spec the Server generates. This example uses
      // a static file saved to the project. It is relative to the nswag config file.
      // Note that you can also provide a URL to the OpenAPI spec, and it will be pulled
      // each time the client is generated. You may prefer this option in some cases.
      "url": "./server.json",
      
      // ...
    }
    },
    "codeGenerators": {
      "openApiToCSharpClient": {
        // This is the namespace of the generated files. Customize to your liking.
        "namespace": "CSharpConsumer.ServerClient",

        // This is the filename of the generated client. Customize to your liking.
        "output": "Client.g.cs",

        // This is the class name of the generated client. Customize to your liking.
        "className": "Client",

        // ...
      }
    }
}
```