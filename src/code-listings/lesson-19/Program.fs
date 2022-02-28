module Capstone3.Program

open System
open Capstone3.Domain
open Capstone3.Operations

let isValidCommand c =
    [ 'd'; 'w'; 'x'] |> Seq.contains c
    
let isStopCommand c =
   c = 'x'   

let getAmount c =
    if c = 'd' then ('d', 50M)
    elif c = 'w' then ('w', 25M)
    else ('x', 0M)
    
let getAmountConsole c =
    Console.WriteLine()
    Console.Write "Enter Amount: "
    let amt = Console.ReadLine() |> Decimal.Parse
    if c = 'd' then ('d', amt)
    elif c = 'w' then ('w', amt)
    else ('x', 0M)    

let consoleCommands = seq { 
    while true do
        Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
        yield Console.ReadKey().KeyChar
}
    
let processCommand (account:Account) (command:char, amount: decimal) =
    let newAccount = if command = 'd'  then {account with  Balance = account.Balance + amount}
                     elif command = 'w' then 
                         if amount > account.Balance then account
                         else { account with Balance =account.Balance - amount }
                     
                     else account
    let s = sprintf "Account " + newAccount.Owner.Name + ": " + command.ToString()  + " with amount " + amount.ToString() + ", new balance is " + newAccount.Balance.ToString()
    Console.WriteLine s
    newAccount
    

[<EntryPoint>]
let main _ =
    let name =
        Console.Write "Please enter your name: "
        Console.ReadLine()

    let withdrawWithAudit = auditAs "withdraw" Auditing.composedLogger withdraw
    let depositWithAudit = auditAs "deposit" Auditing.composedLogger deposit

    let openingAccount = { Owner = { Name = name }; Balance = 0M; AccountId = Guid.Empty } 

    let closingAccount =
        // Fill in the main loop here...
        let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w' ]
        
        let consoleCommands = seq { 
        while true do
        Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
        yield Console.ReadKey().KeyChar }

        consoleCommands
        |> Seq.filter isValidCommand
        |> Seq.takeWhile (not << isStopCommand)
        |> Seq.map getAmountConsole
        |> Seq.fold processCommand openingAccount   

    Console.Clear()
    printfn "Closing Balance:\r\n %A" closingAccount
    Console.ReadKey() |> ignore

    0