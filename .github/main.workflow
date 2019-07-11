workflow "Build workflow" {
    on = "push"
    resolves = ["dotnet build"]
}

workflow "Pull Request Status Checks" {
    resolves = "PR Status Giphy"
    on = "pull_request"
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

action "PR Status Giphy" {
    uses = "jzweifel/pr-status-giphy-action@master"
    secrets = ["GITHUB_TOKEN", "GIPHY_API_KEY"]
}
