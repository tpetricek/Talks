#I __SOURCE_DIRECTORY__
#I "../packages/MBrace.Azure/tools"
#I "../packages/Streams/lib/net45"
#r "../packages/Streams/lib/net45/Streams.dll"
#I "../packages/MBrace.Flow/lib/net45"
#r "../packages/MBrace.Flow/lib/net45/MBrace.Flow.dll"
#load "../packages/MBrace.Azure/MBrace.Azure.fsx"
#load "../packages/MBrace.Azure.Management/MBrace.Azure.Management.fsx"

namespace global

module FsiPrinters =
  let row tag items =
    items
    |> Seq.map (fun i -> sprintf "<%s>%s</%s>" tag i tag)
    |> String.concat ""
    |> sprintf "<tr>%s</tr>"

  #if HAS_FSI_ADDHTMLPRINTER
  fsi.AddHtmlPrinter(fun (cluster:MBrace.Runtime.MBraceClient) ->
    let (!) (n:System.Nullable<float>) = n.GetValueOrDefault(0.0).ToString("F2")
    let style = """
      <style type="text/css">
        .mbrace-cluster td { font-family:@font-family; }
        .mbrace-cluster td { margin-right:10px; }
        .mbrace-cluster tr { background-color: @background-color-highlighted; }
        .mbrace-cluster tbody tr:nth-child(odd) { background-color: @background-color-alternate; }
        .mbrace-cluster thead tr { background: @background-color-highlighted; }
        .mbrace-cluster table { border-spacing: 0; border-collapse: collapse; }
        .mbrace-cluster thead th { border-bottom:3px solid @border-color; }
        .mbrace-cluster td, .mbrace-cluster th {
          border-bottom:1px solid @border-color;
          padding:4px 10px 4px 10px;
        }
        .mbrace-cluster th {
          padding:4px 20px 4px 10px;
          text-align:left;
          font-weight:bold;
        }
      </style>
    """
    let table =
      [ yield "<table class='mbrace-cluster'><thead>"
        yield row "th" [ "Id"; "Memory usage"; "Total memory"; "CPU usage";
          "Active work items"; "Network up"; "Network down" ]
        yield "</thead><tbody>"
        for worker in cluster.Workers do
          yield row "td" [ string worker.Id; !worker.MemoryUsage;
            !worker.TotalMemory; !worker.CpuUsage; string worker.ActiveWorkItems;
            !worker.NetworkUsageUp; !worker.NetworkUsageDown ]
        yield "</tbody></table>" ]
      |> String.concat ""
    seq [ "style", FsLab.Formatters.Styles.replaceStyles style ], table)
  #endif

module Config =

    open System.IO
    open MBrace.Core
    open MBrace.Runtime
    open MBrace.Azure
    open MBrace.Azure.Management

    // This script is used to reconnect to your cluster.

    // You can download your publication settings file at
    //     https://manage.windowsazure.com/publishsettings
    let pubSettingsFile = Path.Combine(__SOURCE_DIRECTORY__, "azure.publishsettings")

    // If your publication settings defines more than one subscription,
    // you will need to specify which one you will be using here.
    let subscriptionId : string option = None

    // Your prefered Azure service name for the cluster.
    // NB: must be a valid DNS prefix unique across Azure.
    let clusterName = "ndc-demo-3"

    // Your prefered Azure region. Assign this to a data center close to your location.
    let region = Region.East_US
    // Your prefered VM size
    let vmSize = VMSize.Medium
    // Your prefered cluster count
    let vmCount = 8

    let GetSubscriptionManager() =
        SubscriptionManager.FromPublishSettingsFile(pubSettingsFile, region, ?subscriptionId = subscriptionId, logger = new ConsoleLogger())

    /// Gets the already existing deployment
    let GetDeployment() = GetSubscriptionManager().GetDeployment(clusterName)

    /// Provisions a new cluster to Azure with supplied parameters
    let ProvisionCluster() =
        GetSubscriptionManager().Provision(vmCount, serviceName = clusterName, vmSize = vmSize)

    /// Resizes the cluster using an updated VM count
    let ResizeCluster(newVmCount : int) =
        let deployment = GetDeployment()
        deployment.Resize(newVmCount)

    /// Deletes an existing cluster deployment
    let DeleteCluster() =
        let deployment = GetDeployment()
        deployment.Delete()

    /// Connect to the cluster
    let GetCluster() =
        let deployment = GetDeployment()
        AzureCluster.Connect(deployment, logger = ConsoleLogger(true), logLevel = LogLevel.Info)
