# Using global.json to define supported SDK versions

[Additional Info](https://learn.microsoft.com/en-us/dotnet/core/tools/global-json)

Both of the projects defined here:

- Were created using net7.0
- Reference net7.0 as the SDK in the csproj file
- Reference SDK version 7.0.100 in the `global.json` file

If you do not have the net7.0 SDK installed, you will receive an error trying to run `dotnet build` inside `./OldSdkNoRollForward`

Example:

```shell
dotnet build
The command could not be loaded, possibly because:
  * You intended to execute a .NET application:
      The application 'build' does not exist.
  * You intended to execute a .NET SDK command:
      A compatible .NET SDK was not found.

Requested SDK version: 7.0.100
global.json file: <path to>/global.json

Installed SDKs:
6.0.422 [...]
8.0.300 [...]

Install the [7.0.100] .NET SDK or update [<path to>/global.json] to match an installed SDK.

Learn about SDK resolution:
https://aka.ms/dotnet/sdk-not-found
```

However, you can run `dotnet build` inside `./OldSdkRollForward` without a net7.0 SDK but instead with net8.0 installed. This is because the `global.json` in this directory contains a roll forward policy:

```json
// global.json
{
  "sdk": {
    "version": "7.0.100",
    "rollForward": "latestMajor" // Allows dotnet to use net8.0 SDK
  }
}
```

Example:

```shell
dotnet build
  Determining projects to restore...
  All projects are up-to-date for restore.
  OldSdkWithRollForward -> <path to>/OldSdkWithRollForward/bin/Debug/net7.0/OldSdkWithRollForward.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.62
```

This is useful to allow us to specify a target SDK version for local development without necessarily having to keep up with the latest patches on a server.
