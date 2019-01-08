// declaration of discriminated union
type Suit =
  | Heart
  | Diamond
  | Spade
  | Club

// data can also be associated with each union case
type PlayingCard =
  | Ace of Suit
  | King of Suit
  | Queen of Suit
  | Jack of Suit
  | ValueCard of int * Suit // tuple type signature

let deckOfCards =
  [
    for suit in [Spade; Club; Heart; Diamond] do
      yield Ace(suit)
      yield King(suit)
      yield Queen(suit)
      yield Jack(suit)
      for value in [2 .. 10] do
        yield ValueCard(value, suit)
  ]

// recursive DU's (joined using the and keyword)
type Statement =
  | Print of string
  | Sequence of Statement * Statement
  | IfStmt of Expression * Statement * Statement
and Expression =
  | Integer of int
  | LessThan of Expression * Expression
  | GreaterThan of Expression * Expression

let program =
  IfStmt(
    GreaterThan(Integer(3), Integer(1)),
      Print("3 is greater then 1."),
      Sequence(Print("3 is not"), Print("greater than 1"))
      )

program

// creating tree structures
type BinaryTree =
  | Node of int * BinaryTree * BinaryTree
  | Empty

let rec printInOrder tree =
  match tree with
  | Node (data, left, right)
    ->  printInOrder left
        printfn "Node %d"data
        printInOrder right
  | Empty
    -> ()

let binTree =
  Node (2, Node(1, Empty, Empty),
    Node(4, 
      Node(3, Empty, Empty),
      Node(5, Empty, Empty)))

printInOrder binTree

// pattern matching
// describe a pair of cards
let describeHoleCards cards =
  match cards with
  | []
  | [_]
    -> failwith "Too few cards." // raise an exception
  | cards when List.length cards > 2
    -> failwith "Too many cards."
  | [Ace(_); Ace(_)] -> "Pocket Rockets"
  | [King(_); King(_)] -> "Cowboys"
  | [ValueCard(2, _); ValueCard(2, _)]
    -> "Ducks"
  | [Queen(_); Queen(_)]
  | [Jack(_); Jack(_)]
    -> "Pair of face cards."
  | [ValueCard(x, _); ValueCard(y, _)] when x = y
    -> "A pair"
  | [first; second]
    -> sprintf "Two cards: %A and %A" first second

let cards = [Ace(Heart); Ace(Club)]
describeHoleCards cards
describeHoleCards []

// pattern matching with recursive DU's
type Employee = 
  | Manager of string * Employee list
  | Worker of string

let rec printOrg worker =
  match worker with
  | Worker(name) -> printfn "Employee %s" name
  | Manager(name, [Worker(employeeName)])
    -> printfn "Manager %s with Worker %s" name employeeName
  | Manager(name, [Worker(employee1); Worker(employee2)])
    -> printfn "Manager %s with 2 Workers %s and %s" name employee1 employee2
  | Manager(name, workers)
    ->  printfn "Manager %s with workers..." name
        workers |> List.iter printOrg

let company1 = Manager("Tom", [Worker("Dick"); Worker("Harry")])
let company2 = Manager("Tom", [Worker("Dick"); Worker("Harry"); Worker("John")])
printOrg company1
printOrg company2

// methods and properties
type PlayingCard_ =
  | Ace of Suit
  | King of Suit
  | Queen of Suit
  | Jack of Suit
  | ValueCard of int * Suit

  member this.Value = 
    match this with
    | Ace(_) -> 11
    | King(_) | Queen(_) | Jack(_) -> 10
    | ValueCard(x, _) when x <= 10 && x >= 2
      -> x
    | ValueCard(_) -> failwith "Card has invalid value."

let highCard = Ace(Spade)
let highCardValue = highCard.Value