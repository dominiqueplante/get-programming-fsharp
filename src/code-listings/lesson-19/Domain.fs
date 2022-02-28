namespace Capstone3.Domain

open System

type Customer = { Name : string }
type Account = { AccountId : Guid; Owner : Customer; Balance : decimal }

type Transaction = { Timestamp : DateTime; Operation : string; Amount : decimal; Accepted : bool }

module Transaction = 
    
    /// Serializes a transaction
    let serialize transaction =
        
        sprintf "%O***%s***%M***%b" transaction.Timestamp transaction.Operation transaction.Amount transaction.Accepted