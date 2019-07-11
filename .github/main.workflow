workflow "Build workflow" {
    on = "push"
    resolves = ["dotnet build"]
}

action "dotnet build" {
    needs = ["dotnet restore", "dotnet test"]
    uses = "Azure/github-actions/dotnetcore-cli@master"
    args = ["build"]
}

action "dotnet test" {
    needs = ["dotnet restore"]
    uses = "Azure/github-actions/dotnetcore-cli@master"
    args = ["test"]
}

action "dotnet restore" {
    uses = "Azure/github-actions/dotnetcore-cli@master"
    args = ["restore"]
}
