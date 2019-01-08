let hex = 0xfcaf      // hexadecimal
let oct = 0o7771L     // octal (long)
let bin = 0b00101010y // binary (as bytes)
let tup = (hex, oct, bin)

// using type annotation
let addFloat (num1 : float) num2 =
  num1 + num2

// apostrophe (') used to specify a generice parameter <T> 
let ident (result : 'a) = result

let printGreeting shouldGreet greeting =
  if shouldGreet then
    printfn "%s" greeting

let isEven x =
  let result = 
    if x % 2 = 0 then
      true
    else
      false
  result

// nested if's use elif (like python)
let isDayOfWeek day =
  if day = "Monday" then true
  elif day = "Tuesday" then true
  elif day = "Wednesday" then true
  elif day = "Thursday" then true
  elif day = "Friday" then true
  else false

// define a tuple
let namesTuple = ("Al", "Bundy")
let firstName = fst namesTuple
let surname = snd namesTuple

// to extract more than 2 values, we need to extracted / captured into values
let snacks = ("Soda", "Cookies", "Candy")
let snack0, snack1, snack2 = snacks
let _, cookies, _ = snacks

// difference in parameters between normal func
// and func which takes a tuple

// x y are two discreet parameters
let add x y = x + y
// (x, y) is a single parameter of type tuple
let addTuple(x, y) = x + y

add 1 3
addTuple(1,3)

