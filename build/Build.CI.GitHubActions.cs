using Nuke.Common.CI.GitHubActions;

[GitHubActions(
    "create-release",
    GitHubActionsImage.WindowsLatest,
    AutoGenerate = true,
    OnPushBranches = new []{ MainBranch },
    PublishArtifacts = false,
    InvokedTargets = new[] { nameof(PublishGitHubRelease) },
    EnableGitHubToken = true)]
partial class Build
{
}