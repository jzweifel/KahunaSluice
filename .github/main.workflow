workflow "Build workflow" {
    on = "push"
    resolves = ["cake build"]
}

action "cake build" {
    uses = "actions/bin/sh@master"
    args = ["./build.sh"]
}
