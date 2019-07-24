workflow "Build workflow on push" {
  resolves = ["dotnet nuget master push"]
  on = "push"
}

workflow "Build workflow on PR" {
  resolves = ["dotnet nuget pr push"]
  on = "pull_request"
}

workflow "Pull Request Status Checks" {
  resolves = "PR Status Giphy"
  on = "pull_request"
}

action "dotnet nuget master push" {
  needs = ["dotnet master pack"]
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

action "dotnet nuget pr push" {
  needs = ["dotnet pr pack"]
  uses = "Azure/github-actions/dotnetcore-cli@master"
  args = ["nuget", "push", "src/KahunaSluice.Core/out/*", "-k", "$NUGET_KEY", "-s", "$NUGET_SOURCE"]
  secrets = ["NUGET_KEY"]
  env = {
    NUGET_SOURCE = "https://api.nuget.org/v3/index.json"
  }
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
  needs = ["not deleted filter"]
  uses = "Azure/github-actions/dotnetcore-cli@master"
  args = ["restore"]
}

action "not deleted filter" {
  uses = "actions/bin/filter@master"
  args = "not deleted"
}

action "PR Status Giphy" {
  uses = "jzweifel/pr-status-giphy-action@master"
  secrets = ["GITHUB_TOKEN", "GIPHY_API_KEY"]
}
