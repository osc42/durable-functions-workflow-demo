module Workflow

open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Extensions.DurableTask
open Microsoft.Extensions.Logging
open FSharp.Control.Tasks.V2.ContextInsensitive

let eventName = "Workflow"

[<FunctionName("Workflow")>]
let run
    ([<OrchestrationTrigger>] context : IDurableOrchestrationContext)
    (logger : ILogger) =
    task {
        let input = context.GetInput<string>()
        sprintf "Starting workflow for %s" input |> logger.LogInformation

        do! context.WaitForExternalEvent(eventName)

        sprintf "Workflow for %s is stopping" input |> logger.LogInformation
    }