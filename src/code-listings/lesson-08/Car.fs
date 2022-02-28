module Car

open System

//TODO: Create helper functions to provide the building blocks to implement driveTo.

let getDistance (destination) =
    if destination = "Gas" then 10
    elif destination = "Home" then 25
    elif destination = "Office" then 50
    elif destination = "Stadium" then 25
    // fill in the blanks!
    else failwith "Unknown destination!"
    
let calculateRemainingPetrol(currentPetrol:int, distance:int) : int = 
    if currentPetrol >= distance then currentPetrol - distance
    else failwith "Oops! You’ve run out of petrol!"    

/// Drives to a given destination given a starting amount of petrol
let driveTo (petrol:int, destination:string) : int =
    let distanceToGas = getDistance(destination)
    let after = calculateRemainingPetrol(petrol, distanceToGas)
    if destination = "Gas" then after + 50
    else after