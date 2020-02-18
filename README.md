# DevOpsCLI .NET Core tool

[![Build Status](https://dev.azure.com/elosegui/OSS/_apis/build/status/jmelosegui.DevOps-CLI?branchName=master)](https://dev.azure.com/elosegui/OSS/_build/latest?definitionId=2&branchName=master)

devops is a command line tool designed to interact with the TFS apis


### Using the devops .NET Core tool

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
