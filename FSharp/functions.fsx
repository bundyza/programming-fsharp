// functions samples

// higher order functions - functions which take or return a function
let negate x = -x
List.map negate [1..10]

// Anonymous / lambda's
(fun x -> x + 3) 5
List.map (fun x -> -x) [1..10]

// currying
open System.IO
let appendFile (filename : string) (text : string) =
  use file = new StreamWriter(filename, true)
  file.WriteLine(text)
  file.Close()

appendFile @"e:\fsharp.txt" "testing"

let curriedAppendFile = appendFile @"e:\fsharp.txt"
curriedAppendFile "testing - curried"

// not curried
List.iter (fun i -> printfn "%d" i) [1..3]
// curried version of printfn
List.iter (printfn "%d") [1..3]

// functions which return functions
// use `` if the identifier is a reserved keyword
let generatePowerOfFunc ``base`` = (fun exponent -> ``base`` ** exponent)
let powerOfTwo = generatePowerOfFunc 2.0
let powerOfThree = generatePowerOfFunc 3.0

powerOfTwo 8.0
powerOfThree 2.0

// can be called in line as well
generatePowerOfFunc 2.0 8.0
(generatePowerOfFunc 2.0) 8.0

// recursive functions (use the rec keyword)
let rec factorial x =
  if x <= 1 then
    1
  else
    x * factorial (x - 1)

factorial 5

// mutual recursion (and keyword is used to join the functions)
let rec isOdd n = (n = 1) || isEven(n - 1)
and isEven n = (n = 0) || isOdd(n - 1)

isOdd 14

// symbolic operators
// define a factorial operator
let rec (!) x =
  if x > 1 then
    x * !(x - 1)
  else
    1

!5

// define a regex operator (===)
open System.Text.RegularExpressions
let (===) str (regex : string) =
  Regex.Match(str, regex).Success

"The quick brown fox" === "The (.*) fox"

// prefix notation operator (can prefix with either ~, ? or !)
let ( !+++ ) x y z = x + y + z
!+++ 1 2 3

// symbolic operators can be passed to higher order functions
List.fold (+) 0 [1..10]
List.fold (*) 1 [1..10]

let minus = (-)
List.fold minus 10 [3; 3; 3]

// function composition
// pipe forward operator |> [let (|>) x f = f x]
[1..3] |> List.iter (printfn "%d")

let sizeOfFolder folder =
  
  let getFiles folder = 
    // using a .NET methods requires that the call be made using a tuple
    // i.e. System.IO.Directory.GetFiles folder "*.*" System.IO.SearchOption.AllDirectories does not work
    System.IO.Directory.GetFiles(folder, "*.*", System.IO.SearchOption.AllDirectories) 

  let totalSize =
    folder
    |> getFiles
    |> Array.map (fun file -> new System.IO.FileInfo(file))
    |> Array.map (fun info -> info.Length)
    |> Array.sum

  totalSize

// forward composition operator
let sizeOfFolderComp =
  let getFiles folder =
    System.IO.Directory.GetFiles(folder, "*.*", System.IO.SearchOption.AllDirectories)

  // the result of this expression is a function which takes one parameter
  // which will be passed to getFiles and piped through the remaining functions
  getFiles
  >> Array.map (fun file -> new System.IO.FileInfo(file))
  >> Array.map (fun info -> info.Length)
  >> Array.sum

sizeOfFolder @"e:\Sandbox"
sizeOfFolderComp @"e:\Sandbox"

let square x = x * x
let toString (x : int) = x.ToString()
let strLen (x : string) = x.Length
let lengthOfSquare = square >> toString >> strLen

square 128
lengthOfSquare 128

// pipe backward operator (<|)
// the following expressions are the same
List.iter (printfn "%d") [1..3]
List.iter (printfn "%d") <| [1..3]

// can be used to manage the precedence of the operators
printfn "The result of sprintf is %s" (sprintf "(%d, %d)" 1 2) // either use parentheses
printfn "The result of sprintf is %s" <| sprintf "(%d, %d)" 1 2 // or use pipe backward operator

// backward composition operator (<<)
let square__ x = x * x;
let negate__ x = -x;
(square__ >> negate__) 10
(square__ << negate__) 10

let data = 
  [[1]; []; [4;5;6]; [3;4]; []; []; []; [9]]
  |> List.filter (not << List.isEmpty)

data