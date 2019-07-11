workflow "Build workflow" {
    on = "push"
    resolves = ["dotnet build"]
}

action "dotnet build" {
    uses = "Azure/github-actions/dotnetcore-cli@master"
    args = ["build"]
}
