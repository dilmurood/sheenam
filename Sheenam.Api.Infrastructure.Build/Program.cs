using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

internal class Program
{
    private static void Main(string[] args)
    {
        var githubPipeline = new
        {
            Name = "Sheenam Build Pipeline",
            OnEvents = new Events
            {
                PullRequest = new PullRequestEvent
                {
                    Branches = new string[] { "main" }
                },

                Push = new PushEvent
                {
                    Branches = new string[] { "main" }
                }
            },


            Job = new Job
            {
                RunsOn = BuildMachines.Windows2022,

                Steps = new List<GithubTask>
                {
                    new CheckoutTaskV2
                    {
                        Name = "Checking out code"
                    },

                    new SetupDotNetTaskV1
                    {
                        Name = "Setting Up .NET",
                        TargetDotNetVersion = new TargetDotNetVersion
                        {
                            DotNetVersion = "6.0.300"
                        }
                    },

                    new RestoreTask
                    {
                        Name = "Restoring Nuget Packages"
                    },

                    new DotNetBuildTask
                    {
                        Name = "Building Project"
                    },

                    new TestTask
                    {
                        Name = "Running Tests"
                    }
                }
            }

        };

        var client = new ADotNetClient();

        client.SerializeAndWriteToFile(
            adoPipeline: githubPipeline,
            path: @"C:\\Users\\Asus\\Desktop\\sheenam\\.github\\workflows\\dotnet.yml");
    }
}