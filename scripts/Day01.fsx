let input = System.IO.File.ReadAllText "Input01.txt"

let mappedInput = input |> Seq.map (fun x -> if x = '(' then 1 else -1) 

let part1Answer = mappedInput |> Seq.sum
let part2Answer = mappedInput |> Seq.scan (+) 0 |> Seq.findIndex (fun x -> x = -1)

printfn "Part1: %d" part1Answer
printfn "Part2: %d" part2Answer

System.Console.ReadKey() |> ignore 