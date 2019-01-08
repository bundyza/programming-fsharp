// lazy types (use either Lazy<'a>.Create() or the lazy keyword)
let x = Lazy<int>.Create(fun () -> printfn "Evaluating x..."; 10) // ; line separator
let y = lazy (printfn "Evaluating y"; x.Value + x.Value)
y.Value

// sequences (alias for IEnumerable)
let seqNum = {1..10}
seqNum |> Seq.iter (printfn "%d")
Seq.iter (printfn "%d") <| seqNum

let allIntS = seq { for i = 0 to System.Int32.MaxValue do yield i }
let allIntS_ = seq { for i in 0 .. System.Int32.MaxValue -> i }
allIntS

// throws OutOfMemory exception
let allIntL = [ for i in 0 .. System.Int32.MaxValue -> i ]

// sequence expressions
let alphabet = seq { for c in 'A' .. 'Z' -> c }
Seq.take 4 alphabet

let noisyAlphabet = 
  seq {
    for c in 'A' .. 'Z' do
      printfn "Yielding %c..." c
      yield c
  }

let fifthLetter = Seq.nth 4 noisyAlphabet

// recursion (and yield! - used to flatten a collection and merge into the sequence (similar to SelectMany))
let rec allFilesUnder basePath =
  seq {
    // yield all files in the base folder
    yield! System.IO.Directory.GetFiles(basePath)

    for subdir in System.IO.Directory.GetDirectories(basePath) do
      yield! allFilesUnder subdir
  }
  
allFilesUnder "E:\\Sandbox"

// Sequence module functions
// take
let randomSequence = 
  seq {
    let rng = new System.Random()
    while true do
      yield rng.Next()
  }

randomSequence |> Seq.take 3

// unfold & toList
let nextFibUnder100 (a, b) =
  if a + b > 100 then
    None
  else
    let nextValue = a + b
    Some (nextValue, (nextValue, a))

let fibsUnder = Seq.unfold nextFibUnder100 (0, 1)
Seq.toList fibsUnder

// Aggregate operators
// iter
let oddsUnderN n = seq { for i in 1 .. 2 .. n -> i }
Seq.iter (printfn "%d") (oddsUnderN 10)
(oddsUnderN 10) |> Seq.iter (printfn "%d")
Seq.iter (fun number -> printfn "%d" number) <| oddsUnderN 10
Seq.iter (printfn "%d") <| oddsUnderN 10

// map
// arrays are compatible with sequences
let words = "The quick brown fox jumped over the lazy dog.".Split([|' '|])
words |> Seq.map (fun words -> words, words.Length)

// fold
Seq.fold (+) 0 <| seq { 1 .. 100 }
seq { 1 .. 100 } |> Seq.fold (+) 0 