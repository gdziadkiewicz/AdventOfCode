let input = System.IO.File.ReadAllLines "Input02.txt"

type dims = 
    {
        l:int
        w:int
        h:int
    }

let split separator (s:string) = s.Split [|separator|]
let dims (a:int array) = {l=a.[0]; w=a.[1]; h=a.[2]}

let parseLine l = l |> split 'x' |> Array.map int |> dims 
let mappedInput = input |> Seq.map parseLine
let smallestSide p = Seq.min [p.l*p.w; p.l*p.h; p.h*p.w]
let part1Answer = mappedInput |> Seq.map (fun x -> 2*x.l*x.w + 2*x.w*x.h + 2*x.h*x.l + (smallestSide x)) |>Seq.sum
let part2Answer = mappedInput |>  Seq.map (fun x -> Seq.min [2*(x.l+x.w); 2*(x.w+x.h); 2*(x.h+x.l)] + x.w*x.l*x.h) |>Seq.sum

printfn "Part1: %d" part1Answer
printfn "Part2: %d" part2Answer

System.Console.ReadKey() |> ignore 