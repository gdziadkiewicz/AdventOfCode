let input = System.IO.File.ReadAllText "Input1.txt"

let part1Answer = input |> Seq.map (fun x -> if x = '(' then 1 else -1) |> Seq.sum

printfn "Part1: %d" part1Answer

System.Console.ReadKey() |> ignore 