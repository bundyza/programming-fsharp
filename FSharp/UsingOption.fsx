// Option can be thought of as similiar to System.Nullable<>
// use Option.get to extract the value - throws error if is None
// use Option.isSome or Option.isNone to test if the value exists

open System

let isInteger str = 
  let successful, result = Int32.TryParse(str) // F# automatically converts out parameters to a tuple
  if successful then
    Some(result)
  else
    None

let isLessThanZero x = x < 0
let containsNegativeNumbers intList =
  let filteredList = List.filter isLessThanZero intList
  if List.length filteredList > 0 then 
    Some(filteredList) 
  else 
    None

let containsNegativeNumber = containsNegativeNumbers [6; 20; -8; 45; -5]
if Option.isSome containsNegativeNumber 
then 
  Option.get containsNegativeNumber 
  |> printfn "%A"
else
  printfn "No numbers are negative"


