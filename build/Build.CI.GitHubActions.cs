using Microsoft.Build.Utilities;
using NuGet.Frameworks;
using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Nuke.GitHub;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[GitHubActions(
    "create-release",
    GitHubActionsImage.UbuntuLatest,
    OnPushBranches = new []{ MainBranch },
    PublishArtifacts = false,
    InvokedTargets = new[] { nameof(PublishGitHubRelease) },
    ImportGitHubTokenAs = nameof(GitHubAuthenticationToken))]
partial class Build
{
}
