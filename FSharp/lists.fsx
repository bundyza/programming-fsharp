// samples of using lists

let emptyList = []
let vowels = ['a'; 'e'; 'i'; 'o'; 'u']

// cons :: adds an item to the front of the list - 
// creates ['y'; 'a'; 'e'; 'i'; 'o'; 'u']
let vowelsWithY = 'y' :: vowels

// generate lists using range and step
let odds = [1..2..10]
let evens = [0..2..10]

// append the lists
let all = odds @ evens

// list comprehensions
// yields runs real time and is not the same as IEnumerable yield in C#
let numbersNear x =
  [
    yield x - 1
    yield x
    yield x + 1
  ]

// more complex list comprehension
// comprehensions allow for func definitions
let x =
  [ let negate x = -x // define negate function
    for i in 1 .. 10 do
      if  i % 2 = 0 then yield negate i
      else yield i
  ]

// when using for loops, do yield can be simplified by using ->
let multiplesOf x = [ for i in 1 .. 10 do yield x * i ]
let multiplesOf2 x = [ for i in 1 .. 10 -> x * i ]

// using list module functions
// paritition (returns a tuple of 2 items)
// first list where predicate == true
// second list where predicate == false
let isMultipleOf5 x = x % 5 = 0
let multOf5, nonMultOf5 =
  List.partition isMultipleOf5 [1 .. 15]

// map
let square x = x * x
List.map square [1..10]

// fold (reduce) - accumulator function, list
let insertCommas (acc : string) item = acc + ", " + item
List.reduce insertCommas ["Jack"; "Jill"; "Jim"; "Joe"; "Jane"]

// fold accumulator function, initial accumulator value, list
let countVowels (str : string) =
  let charList = List.ofSeq str

  // create an accumulator function for the fold operation
  let accFunc (As, Es, Is, Os, Us) letter =
    if letter = 'a' then (As + 1, Es, Is, Os, Us)
    elif letter = 'e' then (As, Es + 1, Is, Os, Us)
    elif letter = 'i' then (As, Es, Is + 1, Os, Us)
    elif letter = 'o' then (As, Es, Is, Os + 1, Us)
    elif letter = 'u' then (As, Es, Is, Os, Us + 1)
    else (As, Es, Is, Os, Us)
    
  List.fold accFunc (0, 0, 0, 0, 0) charList

countVowels "The quick brown fox jumps over the lazy dog"

// iter - calls a function for each item in the list
let printNumber x = printfn "Printing %d" x
List.iter printNumber [1 .. 5]