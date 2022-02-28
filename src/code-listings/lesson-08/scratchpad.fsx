open System

/// Gets the distance to a given destination 
let getDistance (destination) =
    if destination = "Gas" then 10
    elif destination = "Home" then 25
    elif destination = "Office" then 50
    elif destination = "Stadium" then 25
    // fill in the blanks!
    else failwith "Unknown destination!"

// Couple of quick tests
getDistance("Home") = 25
getDistance("Stadium") = 25
getDistance("Gas") = 10
getDistance("Office") = 50
getDistance("Bar") = 1

let calculateRemainingPetrol(currentPetrol:int, distance:int) : int = 
    if currentPetrol >= distance then currentPetrol - distance
    else failwith "Oops! You’ve run out of petrol!"

calculateRemainingPetrol(30, 10) = 20

calculateRemainingPetrol(30, 40) = 20

let distanceToGas = getDistance("Gas")
calculateRemainingPetrol(25, distanceToGas)
calculateRemainingPetrol(5, distanceToGas)

let driveTo (petrol:int, destination:string) : int =
    let distanceToGas = getDistance(destination)
    let after = calculateRemainingPetrol(petrol, distanceToGas)
    if destination = "Gas" then after + 50
    else after




driveTo(50, "Office") = 0
driveTo(50, "Home") = 25
driveTo(50, "Gas") = 90

let a = driveTo(100, "Office")          
let b = driveTo(a, "Stadium")
let c = driveTo(b, "Gas")
let answer = driveTo(c, "Home")    
answer = 40     