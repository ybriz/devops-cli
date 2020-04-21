# DevOps Client

[![Build Status](https://dev.azure.com/elosegui/OSS/_apis/build/status/jmelosegui.DevOps-CLI?branchName=master)](https://dev.azure.com/elosegui/OSS/_build/latest?definitionId=2&branchName=master)

[![NuGet Badge](https://buildstats.info/nuget/Jmelosegui.DevOpsCLI)](https://www.nuget.org/packages/Jmelosegui.DevOpsCLI/)

[![NuGet Badge](https://buildstats.info/nuget/Jmelosegui.DevOps.Client)](https://www.nuget.org/packages/Jmelosegui.DevOps.Client/)

DevOps Client is library designed to interact with the TFS apis.

This project is split into 2 packages:
- **Jmelosegui.DevOps.Client** - A netstandard library that contain a Rest client for the different endpoints in the [Azure DevOps Api](https://docs.microsoft.com/en-us/rest/api/azure/devops/?view=azure-devops-rest-5.1) collection.
- **Jmelosegui.DevOpsCLI** - A CLI tool CLI that allows you to interact with [Azure DevOps](https://azure.microsoft.com/en-us/services/devops/) from the command line.


### Installing the devops .NET Standard Client package

If you need to use a CI build add the following nuget feed.

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <clear />
        <add key="Jmelosegui.OSS" value="https://f.feedz.io/jmelosegui/oss/nuget/index.json" />
        <add key="NuGet.org" value="https://api.nuget.org/v3/index.json" />
    </packageSources>
</configuration>
```

### Using the devops .NET Core CLI tool

Perform a one-time install of the `Jmelosegui.DevOpsCLI` tool using the following dotnet CLI command:

```
dotnet tool install -g Jmelosegui.DevOpsCLI
```

If you need to install a pre-release version please specify the `--version` argument

```
dotnet tool install -g Jmelosegui.DevOpsCLI --version 1.0.2-beta
```

If you need to use a CI build please specify the `--add-source` argument

```
dotnet tool install -g Jmelosegui.DevOpsCLI --add-source https://f.feedz.io/jmelosegui/oss/nuget/index.json
```

To know all the available commands run

```
devops --help
```
