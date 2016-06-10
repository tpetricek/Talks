#nowarn "211"
#load "packages/FsLab/Themes/AtomChester.fsx"
#load "packages/FsLab/FsLab.fsx"
#load "utils/mbrace.fsx"
open MBrace.Core
open MBrace.Azure
open MBrace.Flow
open MBrace.Azure.Management

// -------------------------------------------------------------------------------------------------
// Create cluster (needed for `houses-cloud.fsx` and `prices-cloud.fsx`)
// -------------------------------------------------------------------------------------------------

// Create cluster & check progress until it's done
let deployment = Config.ProvisionCluster()
deployment.ShowInfo()

// Get the cluster & do some basic testing
let cluster = Config.GetCluster()
cluster.AttachLogger(ConsoleLogger())

let t =
  cloud { return 1 + 1 }
  |> cluster.CreateProcess

t.Status
t.Result

// Display workers using Ionide formatter
cluster

// Delete the cluster for fun & profit (mostly profit)
// Config.DeleteCluster()

// -------------------------------------------------------------------------------------------------
// Install R  on the cluster (needed for `prices-cloud.fsx`)
// -------------------------------------------------------------------------------------------------

open System
open System.IO
open System.Diagnostics

/// Path to R installer mirror; change as appropriate
let R_Installer = "https://cran.cnr.berkeley.edu/bin/windows/base/R-3.3.0-win.exe"

/// checks whether R is installed in the local computer
let isRInstalled() = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\R-core") <> null

/// Performs R installation operation on an MBrace cluster
/// Assumes workers running with elevated privileges
let installR () = cloud {
    let installRToCurrentWorker() = local {
        if not <| isRInstalled() then
            do! Cloud.Logf "Installing R in local machine."
            use wc = new System.Net.WebClient()
            let tmp = Path.GetTempPath()
            let tmpExe = Path.Combine(tmp, Path.ChangeExtension(Path.GetRandomFileName(),".exe"))
            do! Cloud.Logf "Downloading R bits..."
            do wc.DownloadFile(Uri R_Installer, tmpExe)
            do! Cloud.Logf "Installing R..."
            let psi = new ProcessStartInfo(tmpExe, "/COMPONENTS=x64,main,translation /SILENT")
            psi.UseShellExecute <- false
            let proc = Process.Start(psi)
            proc.WaitForExit()
            if proc.ExitCode <> 0 then invalidOp "failed to install R in local context"
            do! Cloud.Logf "R installation complete."
    }

    // performs install operation for every worker in the current cluster
    let! _ = Cloud.ParallelEverywhere(installRToCurrentWorker())
    return ()
}

/// Parallel workflow that verifies whether R is successfully installed across the cluster
let isRInstalledCloud() = cloud {
    let! results = Cloud.ParallelEverywhere (cloud { return isRInstalled ()})
    return Array.forall id results
}

isRInstalledCloud() |> cluster.Run

let rp = installR() |> cluster.CreateProcess
rp.Status

// -------------------------------------------------------------------------------------------------
// Install quantmod on the cluster (needed for `prices-cloud.fsx`)
// -------------------------------------------------------------------------------------------------

open RDotNet
open RProvider
open RProvider.utils

// Local install
namedParams
  [ "pkgs", "quantmod"
    "repos", "http://cran.us.r-project.org" ]
|> R.install_packages

// Remote install
let installQuantmod () = cloud {
  namedParams
    [ "pkgs", "quantmod"
      "repos", "http://cran.us.r-project.org" ]
  |> R.install_packages
  |> ignore }

// Spawn remote install
let ip =
  Cloud.ParallelEverywhere (installQuantmod())
  |> cluster.CreateProcess

ip.Status
ip.Result

// -------------------------------------------------------------------------------------------------
// Test R and quantmod on the cluster
// -------------------------------------------------------------------------------------------------

open RDotNet
open RProvider
open RProvider.stats
open RProvider.quantmod

let testR() = cloud {
  let res = R.Delt [1.0; 2.0; 3.0]
  return res.AsNumeric() |> Array.ofSeq
}

let tp = testR() |> cluster.CreateProcess
tp.Status
tp.Result
