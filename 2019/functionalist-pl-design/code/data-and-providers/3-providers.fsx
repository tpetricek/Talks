#load "packages/FsLab/FsLab.fsx"
open FSharp.Data
open XPlot.GoogleCharts

let wb = WorldBankData.GetDataContext()

// TODO: CO2 emissions of United States and Germany
// TODO: Get weather forecast in Porto
// TODO: Write 'getTemp' function taking a city

// http://api.openweathermap.org/data/2.5/forecast/daily?q=Porto&mode=json&units=metric&cnt=10&APPID=cb63a1cf33894de710a1e3a64f036a27
