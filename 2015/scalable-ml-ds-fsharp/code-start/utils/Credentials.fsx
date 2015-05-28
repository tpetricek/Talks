#load "MBrace.fsx"

let storageConnection = "<Insert your storage connection>"
let serviceBusConnection = "<Insert your service bus connection>"

let config =
    { MBrace.Azure.Configuration.Default with
        StorageConnectionString = storageConnection
        ServiceBusConnectionString = serviceBusConnection }
