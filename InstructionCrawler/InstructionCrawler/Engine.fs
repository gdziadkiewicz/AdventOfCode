#if INTERACTIVE
#r @"../packages/FSharp.Data.2.2.5/lib/net40/FSharp.Data.dll"
#else
module InstructionCrawler.Engine
#endif

open System
open System.IO
open FSharp.Data

let getQuestions htmlDocument =
    HtmlDocument.body htmlDocument |> HtmlNode.descendants true (HtmlNode.hasClass "day-desc") |> Seq.map string |> String.concat ""

let mapSnd f (x, y) = (x, f y)
let questions = seq {1 .. 25}  |> Seq.map (fun i ->(i, @"http://adventofcode.com/day/" + string i)) |> Seq.map (mapSnd HtmlDocument.Load) |> Seq.map (mapSnd getQuestions)

questions |> Seq.iter (fun (i, text) ->(sprintf "Day%02i.html" i ,text) |> File.WriteAllText )