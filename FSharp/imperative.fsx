// default values
Unchecked.defaultof<int>
Unchecked.defaultof<System.IO.FileInfo>

// null keyword
// can be used to test uninitialized reference types
// however null cannot be assigned to reference types
// use Some / None
let isNull = function
  null -> true
  | _ -> false

let isNull_ x =
  match x with
  | null -> true
  | _ -> false

isNull ""
isNull (null : string)

type Thing =
  | Plant
  | Animal
  | Mineral

// can be used to test uninitialized reference types
// however null cannot be assigned to reference types
// use Some / None
let testThing thing =
  match thing with
  | Plant -> "Plant"
  | Animal -> "Animal"
  | Mineral -> "Mineral"
  //| null -> "(null)" // invalid value

// reference type aliasing
let x = [|0|]
let y = x
x.[0] <- 1
y.[0]

// mutable values
let mutable message = "World"
printfn "Hello, %s" message
message <- "Universe"
printfn "Hello, %s" message

// mutable values - limitations:
// cannot be returned from functions instead a copy is made
// cannot be captured by inner functions
let returnMutable () =
  let mutable result = 0
  result

let c = returnMutable ()
// c <- 1 // invalid

(*
let invalidUse () =
  let mutable x = 0

  let incrementX () = x <- x + 1
  incrementX ()
  x
*)

// reference cells
// used to by pass the limitations of mutable values types. Used to store
// mutable data on the heap
// ! - retrieve the value
// :=  - set the value
let planets =
  ref [
    "Mercury"; "Venus"; "Earth";
    "Mars"; "Jupiter"; "Saturn";
    "Uranus"; "Neptune"; "Pluto"
  ]

// reassign the planets reference cell
planets := !planets |> List.filter (fun p -> p <> "Pluto")
!planets

// build-in helper functions for reference cells
let number = ref 0
incr number
decr number

// mutable records
type MutableCar = { Make : string; Model : string; mutable Miles : int }

let driveForASeason car =
  let rng = new System.Random()
  car.Miles <- car.Miles + rng.Next() % 10000

let kitt = { Make = "Pontiac"; Model = "Trans Am"; Miles = 0 }
driveForASeason kitt
driveForASeason kitt
driveForASeason kitt
driveForASeason kitt

// arrays
// Array comprehension
let perfectSquares = [| for i in 1 .. 7 -> i * i |] // similiar to list / sequence comprehension
let manualSquares = [| 1; 4; 9; 16; 25; 36; 49; 64; 81 |] // manual array creation

// indexing
printfn
  "The first 3 perfect squares are %d, %d, %d"
  perfectSquares.[0]
  perfectSquares.[1]
  perfectSquares.[2]

let rot13Encrypt (letter : char) =
  if System.Char.IsLetter(letter) then
    let newLetter = 
      (int letter)
      |> (fun letterIdx -> letterIdx - (int 'A'))
      |> (fun letterIdx -> (letterIdx + 13) % 26)
      |> (fun letterIdx -> letterIdx + (int 'A'))
      |> char
    newLetter
  else
    letter

let encryptText (text : char[]) =
  for idx = 0 to text.Length - 1 do
    let letter = text.[idx]
    text.[idx] <- rot13Encrypt letter

let text = Array.ofSeq "THE QUICK BROWN FOX JUMPED OVER THE LAZY DOG"

printfn "Original = %s" <| new System.String(text)
encryptText text
printfn "Encrypted = %s" <| new System.String(text)
encryptText text
printfn "Decrypted = %s" <| new System.String(text)