// versus .NET objects
// type inference can infer a record type
// fields are immutable by default
// records cannot be inherited
// records can be used with pattern matching
// structure comparison and equality semantics

type Person = {First : string; Last : string; mutable Age : int }
let personInst = {First = "Al"; Last = "Bundy"; Age = 40}
personInst.Age <- 10
printfn "%s %s is %d years old." personInst.First personInst.Last personInst.Age

// cloning records
type Car = { Make : string; Model : string; Year : int}
let thisYear = {Make = "Car make"; Model = "Coup"; Year = 2013}
let nextYear = {thisYear with Model = "Convertable"; Year = 2014}

// pattern matching
let allNewCars = [thisYear; nextYear]
let allCoups =
  allNewCars
  |> List.filter
    (function
      | {Model = "Coup"} -> true
      | _ -> false)

// type inference
type Point = { X : float; Y : float }
let distance pt1 pt2 =
  let square x = x * x
  sqrt <| square (pt1.X - pt2.X) + square (pt1.Y - pt2.Y)

distance { X = 0.0; Y = 0.0 } { X = 10.0; Y = 10.0 }

// ambiguous
type Point_ = { X : float; Y : float }
type Vector_ = { X : float; Y : float; Z : float }

let distance_ (pt1 : Point) (pt2 : Point) =
  let square x = x * x
  sqrt <| square (pt1.X - pt2.X) + square (pt1.Y - pt2.Y)

let origin = {Point.X = 0.0; Point.Y = 0.0}

// methods and properties
type Vector = 
  { X : float; Y : float; Z : float}
  
  member this.Length =
    sqrt <| this.X ** 2.0 + this.Y ** 2.0 + this.Z ** 2.0

let v = { Vector.X = 10.0; Vector.Y = 20.0; Vector.Z = 30.0 }
v.Length