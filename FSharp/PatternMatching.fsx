
let isOdd x = x % 2 = 1
let describeNumber x =
  match isOdd x with
  | true -> printfn "x is odd"
  | false -> printfn "x is even"

describeNumber 12
describeNumber 13

let testAnd x y =
  match x, y with
  | true, true -> true
  | true, false -> false
  | false, true -> false
  | false, false -> false

testAnd true true
testAnd true false

// _ acts as a wildcard
let testAnd__ x y =
  match x, y with
  | true, true -> true
  | _, _ -> false

testAnd__ true true
testAnd__ true false

// named patterns [x captures the value and allows for use in the match body]
let greet name =
  match name with
  | "Robert" -> printfn "Hello, Bob"
  | "William" -> printfn "Hello, Bill"
  | x -> printfn "Hello, %s" x

greet "Robert"
greet "Bundy"

// matching literal
(*
In pattern matching expressions, identifiers that begin with lowercase characters are always treated as variables to be bound,
rather than as literals, so you should generally use initial capitals when you define literals.
 *)

[<Literal>]
let Bill = "Bill Gates" // value identifier needs to begin with upper case character
let greetLiteral name =
  match name with
  | Bill -> "Hello, Bob" // Bill is a literal and not a named pattern
  | x -> sprintf "Hello, %s" x

// alternate syntax
let getPrice = function
  | "banana" -> 0.79
  | "watermelon" -> 3.49
  | "tofu" -> 1.09
  | _ -> nan (* nan is a special value meaning "not a number" *)

getPrice "tofu"
getPrice "banana"
getPrice "apple"

// when guards
open System

let highLowGame () =
  let rng = new Random()
  let secretNumber = rng.Next() % 100

  let rec highLowGameStep () =
    printfn "Guess the secret number:"
    let guessStr = Console.ReadLine()
    let guess = Int32.Parse(guessStr)
    
    match guess with
    | _ when guess > secretNumber 
      ->  printfn "The secret number is lower."
          highLowGameStep ()

    | _ when guess = secretNumber
      ->  printfn "You've guessed correctly!"
          ()

    | _ when guess < secretNumber 
      -> printfn "The secret number is higher."
         highLowGameStep ()

    | _ -> () // clean up the "Incomplete pattern matches on this expression" warning

  highLowGameStep ()

// grouping patterns
let vowelTest c =
  match c with
  | 'a' | 'e' | 'i' | 'o' | 'u' // for or patterns
    -> true
  | _ -> false

vowelTest 'a'
vowelTest 'b'

let describeNumbers x y =
  match x, y with
  | 1, _
  | _, 1
    -> "One of the numbers is one."
  | (2, _) & (_, 2) // for and patterns
    -> "Both of the numbers are two."
  | _ -> "Other"

describeNumbers 1 0
describeNumbers 0 1
describeNumbers 2 2
describeNumbers 3 2

// matching data structures

// tuples
// x and y are captured into value tuple
let testXor x y =
  match x, y with
  | tuple when fst tuple <> snd tuple 
    -> true
  | true, true -> false
  | false, false -> false
  | _ -> false

testXor true false
testXor false true
testXor true true
testXor false false

// lists
let rec listLength l =
  match l with
  | [] -> 0
  | [_] -> 1
  | [_; _] -> 2
  | [_; _; _] -> 3
  | hd :: tail -> 1 + listLength tail

// options
let describeOption o =
  match o with
    | Some(42) -> "The answer is 42, but what was the question?"
    | Some(x) -> sprintf "The answer was %d" x
    | None -> "No answer was found."

let ident x = Some(x)
let ident42 = ident 42
let ident12 = ident 12

describeOption None
describeOption ident42
describeOption ident12

// pattern matching ouside of match expressions 
// i.e. can be used without 'match with'

// let bindings - provide sample
// anonymous functions - provide sample

// wildcard patterns
// unnamed parameter
List.iter (fun _ -> printfn "Step...") [1..3]
// extracting values from a tuple
let _, y, _ = (1, 2, 3)

// alternate lambda syntax
(*
function keyword works similar to fun except there is an implicit single parameter
which must be used with in the match statement
*) 
let rec listLength__ theList = // normal
  match theList with
  | [] -> 0
  | [_] -> 1
  | [_; _] -> 2
  | [_; _; _] -> 3
  | hd :: tail -> 1 + listLength tail

// alternate
let rec listLenAlt = function
  | [] -> 0
  | [_] -> 1
  | [_; _] -> 2
  | [_; _; _] -> 3
  | hd :: tail -> 1 + listLength tail