open System
open System.IO
open System.Text
open System.Security.Cryptography



let input = File.ReadAllLines @"input05.txt"

let vowels = ['a';'e';'i';'o';'u']
let forbiddenStrings = ["ab";"cd";"pq";"xy"]

let contains x (str:string) = str.Contains x
let areSame (x,y) = x=y
let checkString1 str = 
    let forbiddenStringsCondition() =  forbiddenStrings |> Seq.forall (fun fs -> str |> contains fs |> not)
    let twiceInRowCondition() =  str |> Seq.pairwise |> Seq.exists areSame
    let threeVowelsCondition() =  (str |> Seq.filter (fun c -> vowels |> Seq.contains c) |> Seq.length) >= 3
    forbiddenStringsCondition() && twiceInRowCondition() && threeVowelsCondition()


let output1 = input |> Seq.map checkString1 |> Seq.filter id |> Seq.length

module Seq =
    let product xs ys = seq { for x in xs do for y in ys do yield x,y }

let triplewise source = source |> Seq.windowed 3 |> Seq.map (fun x ->(x.[0],x.[1],x.[2]))

let checkString2 (str:string) = 
    let notOverlapingPairsCondition() = 
        str 
        |> Seq.indexed //(1,a),(2,b)
        |> Seq.pairwise
        |> Seq.groupBy (fun ((_,x),(_,y)) -> (x,y))
        |> Seq.map snd
        |> Seq.map (Seq.map (fun ((i1,_),(i2,_)) -> (i1,i2)))
        |> Seq.exists (fun pairs -> Seq.product pairs pairs |> Seq.exists (fun ((x1, y1), (x2,y2)) -> x1 <> x2
                                                                                                      && y1 <> y2
                                                                                                      && x1 <> y2
                                                                                                      && x2 <> y1 ))
                         
    let exactlyOneBetweenCondition() =  str |> triplewise |> Seq.exists (fun (x,_,y)-> x=y)
    notOverlapingPairsCondition() && exactlyOneBetweenCondition()

let output2 = input |> Seq.map checkString2 |> Seq.filter id |> Seq.length