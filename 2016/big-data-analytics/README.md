Analysing Big Time-Series Data in the Cloud
===========================================

The talk shows how to analyze Big Data in the cloud using
[BigDeedle](https://github.com/BlueMountainCapital/Deedle.BigDemo),
[MBrace](http://mbrace.io/) and how to use [Ionide](http://ionide.io)
Atom plugin.

If you want to run all the code, you need to set up a couple of things.
The repository contains the following files:

 1. `0-setup-cluster.fsx` - Run this if you want to run the MBrace
    demos (4 and 5). The script starts MBrace cluster and installs
    R with `quantmod` package (for R provider demo in step 5).
    You first need to download your Azure `.publishsettings`
    file and save it in `utils/azure.publishsettings`.     

 2. `1-houses-local.fsx` - Uses Deedle to analyze house prices
    from `data/pp-monthly-april-2016.csv` locally. No additional
    setup is needed for this demo.

 3. `2-houses-big.fsx` - Uses BigDeedle to access data remotely.
    For this you need to download and compile
    [Deedle.BigDemo project]([BigDeedle](https://github.com/BlueMountainCapital/Deedle.BigDemo)
    (see instructions below).

 4. `3-houses-cloud.fsx` - Uses BigDeedle to access data and MBrace
    to run computations over the data in the cloud. Reset F# Interactive
    before doing this, otherwise MBrace will upload all local data loaded
    in FSI into the cluster. Requires Deedle.BigDemo and running MBrace
    cluster.

 5. `4-prices-cloud.fsx` - Uses BigDeedle to access financial data,
    MBrace to run computations over it and also R provider to call financial
    functions from `quantmod` package. You need quite a bit of setup for
    this. See [Deedle.BigDemo](https://github.com/BlueMountainCapital/Deedle.BigDemo),
    which explains how to load financial data into Azure Table storage and
    follow the `0-setup-cluster.fsx` script to install R into MBrace cluster.

Getting the Deedle.BigDemo to compile
-------------------------------------

This implements the provider for BigDeedle that accesses financial data from
Azure table storage and House prices via a REST API. See more information
on [the project page](https://github.com/BlueMountainCapital/Deedle.BigDemo).
To compile it locally, get the code:

      git clone https://github.com/BlueMountainCapital/Deedle.BigDemo.git

If you just want to run the House prices demo (the easier one), exclude the
`trades.fs` file (so that you do not need Azure storage key) from the project
and build it. Copy the dll into `bigdeedle/Deedle.BigSources.dll` inside
the `code-done` folder.

You can also download the prices into your own Azure storage and start the REST
server that hosts the data. The source for the REST server is [in the BigDemo
repository](https://github.com/BlueMountainCapital/Deedle.BigDemo/tree/master/src/HousePrices.Server) and the script to save the data in Azure [is there as
well](https://github.com/BlueMountainCapital/Deedle.BigDemo/blob/master/src/Scripts.Setup/setup-houseprices.fsx).
