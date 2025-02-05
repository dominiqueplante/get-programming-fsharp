﻿module Capstone3.FileRepository

open Capstone3.Domain
open System.IO
open System

let private accountsPath =
    let path = @"accounts"
    Directory.CreateDirectory path |> ignore
    path
let private findAccountFolder owner =    
    let folders = Directory.EnumerateDirectories(accountsPath, sprintf "%s_*" owner)
    if Seq.isEmpty folders then ""
    else
        let folder = Seq.head folders
        DirectoryInfo(folder).Name
let private buildPath(owner, accountId:Guid) = sprintf @"%s\%s_%O" accountsPath owner accountId

/// Logs to the file system
let writeTransaction accountId owner transaction  =
    let path = buildPath(owner, accountId)    
    path |> Directory.CreateDirectory |> ignore
    let filePath = sprintf "%s/%d.txt" path (transaction.Timestamp.ToFileTimeUtc())
    let line = sprintf "%O***%s***%M***%b" transaction.Timestamp transaction.Operation transaction.Amount transaction.Accepted
    File.WriteAllText(filePath, line)