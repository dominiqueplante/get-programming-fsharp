#load "Domain.fs"
#load "Operations.fs"

open Capstone3.Operations
open Capstone3.Domain
open System


let isValidCommand c =
    [ 'd'; 'w'; 'x'] |> Seq.contains c
    
let isStopCommand c =
   c = 'x'   

let getAmount c =
    if c = 'd' then ('d', 50M)
    elif c = 'w' then ('w', 25M)
    else ('x', 0M)
    
let processCommand (account:Account) (command:char, amount: decimal) =
    let newAccount = if command = 'd'  then {account with  Balance = account.Balance + amount}
                     elif command = 'w' then {account with Balance = Math.Max(account.Balance - amount, 0M)}
                     else account
    newAccount
    
    
    


let openingAccount =
    { Owner = { Name = "Isaac" }; Balance = 0M; AccountId = Guid.Empty }  
let account =
    let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w' ]                  

    commands
    |> Seq.filter isValidCommand
    |> Seq.takeWhile (not << isStopCommand)
    |> Seq.map getAmount
    |> Seq.fold processCommand openingAccount                            