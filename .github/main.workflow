workflow "Build workflow" {
    on = "push"
    resolves = ["cake build"]
}

action "cake build" {
    uses = "./build.sh"
}
