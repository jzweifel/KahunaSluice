workflow "Build workflow" {
  on = "push"
  resolves = ["dotnet nuget push"]
}

workflow "Pull Request Status Checks" {
  resolves = "PR Status Giphy"
  on = "pull_request"
}

action "dotnet nuget push" {
  needs = ["dotnet master pack", "dotnet pr pack"]
  uses = "Azure/github-actions/dotnetcore-cli@master"
  args = ["nuget", "push", "src/KahunaSluice.Core/out/*", "-k", "$NUGET_KEY", "-s", "$NUGET_SOURCE"]
  secrets = ["NUGET_KEY"]
  env = {
    NUGET_SOURCE = "https://api.nuget.org/v3/index.json"
  }
}

action "dotnet master pack" {
  needs = ["master-branch-filter"]
  uses = "Azure/github-actions/dotnetcore-cli@master"
  args = ["pack", "src/KahunaSluice.Core/", "-o", "out"]
}

action "master-branch-filter" {
  needs = ["dotnet build"]
  uses = "actions/bin/filter@master"
  args = "branch master"
}

action "dotnet pr pack" {
  needs = ["pr-filter"]
  uses = "Azure/github-actions/dotnetcore-cli@master"
  args = ["pack", "src/KahunaSluice.Core/", "-o", "out", "--version-suffix", "dev-$GITHUB_SHA"]
}

action "pr-filter" {
  needs = ["dotnet build"]
  uses = "actions/bin/filter@master"
  args = "action 'opened|synchronize'"
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
