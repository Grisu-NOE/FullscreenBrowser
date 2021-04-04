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
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitHub;
using Nuke.Common.Tools.GitReleaseManager;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities;
using Nuke.Common.Utilities.Collections;
using Nuke.GitHub;
using Octokit;
using System.IO.Compression;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using GitHubTasks = Nuke.Common.Tools.GitHub.GitHubTasks;

[CheckBuildProjectConfigurations]
[ShutdownDotNetAfterServerBuild]
partial class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion(Framework = "net5.0")] readonly GitVersion GitVersion;

    [Parameter] string GitHubAuthenticationToken;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    const string MainBranch = "main";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
            EnsureCleanDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .SetCopyright($"Copyright © Freiwillige Feuerwehr Krems/Donau {DateTime.Now.Year}")
                .SetOutputDirectory(OutputDirectory)
                .EnableNoRestore());
        });

    Target Pack => _ => _
        .DependsOn(Compile)
        .Produces(ArtifactsDirectory / "*.*")
        .Executes(() =>
        {
            CompressionTasks.CompressZip(OutputDirectory, ArtifactsDirectory / $"{GitVersion.MajorMinorPatch}.zip");
        });

    Target PublishGitHubRelease => _ => _
        .DependsOn(Pack)
        .Consumes(Pack)
        .Requires(() => GitHubAuthenticationToken)
        .OnlyWhenStatic(() => Configuration.Equals(Configuration.Release) && IsOnMainBranch())
        .WhenSkipped(DependencyBehavior.Skip)
        .Executes(async () =>
        {
            var releaseTag = $"v{GitVersion.MajorMinorPatch}";

            //var latestRelease = await GitRepository.GetLatestRelease();


            //var changeLogSectionEntries = ExtractChangelogSectionNotes(ChangeLogFile);
            //var latestChangeLog = changeLogSectionEntries
            //    .Aggregate((c, n) => c + Environment.NewLine + n);
            //var completeChangeLog = $"## {releaseTag}" + Environment.NewLine + latestChangeLog;

            var files = GlobFiles(ArtifactsDirectory, "*.*").NotEmpty().ToArray();

            var gitHubName = GitRepository.GetGitHubName();
            var gitHubOwner = GitRepository.GetGitHubOwner();
            await Nuke.GitHub.GitHubTasks.PublishRelease(settings => settings
                .SetArtifactPaths(files)
                .SetCommitSha(GitVersion.Sha)
                .SetRepositoryName(gitHubName)
                .SetRepositoryOwner(gitHubOwner)
                .SetTag(releaseTag)
                .SetToken(GitHubAuthenticationToken)
            );
        });

    bool IsOnMainBranch()
    {
        return GitVersion.BranchName.EqualsOrdinalIgnoreCase(MainBranch) || GitVersion.BranchName.EqualsOrdinalIgnoreCase($"origin/{MainBranch}");
    }
}
