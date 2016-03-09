let input = System.IO.File.ReadAllText @"C:\Users\zwei1\Source\Repos\AdventOfCode\scripts\input03.txt"

let f (x, y) c =
     match c with
        |'>' -> (x+1, y)
        |'<' -> (x-1, y)
        |'^' -> (x, y+1)
        |'v' -> (x, y-1)
        | x -> x |> sprintf "Unexpected symbol '%c'" |> failwith

let output1 = input |> Seq.scan f (0,0) |> Seq.distinct |> Seq.length

printfn "output1: %i" output1

let isEven i = i%2=0
let santa = input |> Seq.indexed |> Seq.where (fst>>isEven>>not) |> Seq.map snd
let roboSanta = input |> Seq.indexed |> Seq.where (fst>>isEven)  |> Seq.map snd

let output2 = [santa; roboSanta] |> Seq.map (Seq.scan f (0,0)) |> Seq.concat |> Seq.distinct |> Seq.length

printfn "output2: %i" output2