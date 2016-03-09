#r @"packages\FParsec.1.0.2\lib\net40-client\FParsecCS.dll"
#r @"packages\FParsec.1.0.2\lib\net40-client\FParsec.dll"
open FParsec
open System
open System.IO

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

let input = File.ReadAllLines @"input06.txt"
type Command = |On|Off|Toggle
type Line = {
    command:Command
    start: int*int
    stop: int*int
    }
let parse = function
    | "turn on" -> On
    | "turn off" -> Off
    | "toggle" -> Toggle
    | _ -> failwith "wrong value"

let createLine a b c = {command=a|>parse; start=b; stop=c}

let test p str =
    match run p str with
    | Success(result, _, _)   -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

let str s = pstring s
let onOffToggleParser =  (str "turn on" <|> str "turn off" <|> str "toggle")
let pairParser = pint32 .>> str "," .>>. pint32
let lineParser = pipe3 (onOffToggleParser.>>spaces) (pairParser.>>spaces.>>str "through" .>>spaces) pairParser createLine
test lineParser "turn on 887,9 through 959,629"

let lines = input |> Seq.map (run lineParser) |> Seq.map (function | Success(result, _, _)   -> result  | Failure(errorMsg, _, _) ->failwith "parsing error")

let startingPlate = Array2D.create 1000 1000 false

let f command (x1,y1) (x2,y2) (plate:bool[,]) =
    let result = plate |> Array2D.copy
    for x in x1 .. x2 do
        for y in  y1 .. y2 do
            result.[x,y] <- match command with
                                | On     -> true
                                | Off    -> false
                                | Toggle -> not result.[x,y]
    result
let f' plate x = f x.command x.start x.stop plate
let endPlate = lines |> Seq.fold f' startingPlate 
let output = endPlate |> Seq.cast<bool> |> Seq.filter id |> Seq.length

let startingPlate2 = Array2D.zeroCreate<int> 1000 1000
let f2 command (x1,y1) (x2,y2) (plate:int[,]) =
    let result = plate |> Array2D.copy
    for x in x1 .. x2 do
        for y in  y1 .. y2 do
            let change =  match command with
                                | On     -> 1
                                | Off    -> -1
                                | Toggle -> 2
            result.[x,y] <- result.[x,y] + change
            if result.[x,y] < 0 then  result.[x,y] <- 0
    result

let f2' plate x = f2 x.command x.start x.stop plate
let endPlate2 = lines |> Seq.fold f2' startingPlate2 
let output2 = endPlate2 |> Seq.cast<int> |> Seq.sum