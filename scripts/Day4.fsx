let input = "ckczppom"
let zeros = "00000"

open System
open System.Text
open System.Security.Cryptography

let md5' :(byte[]->byte[]) =
    let engine  = MD5.Create()
    engine.ComputeHash

let md5  (s:string) =
    let a =  Encoding.ASCII.GetBytes s |> md5' |> BitConverter.ToString
    a.Replace("-", "")

Seq.initInfinite string |> Seq.map (fun i -> (i, md5 (input + string i))) |> Seq.filter (fun (_,s) -> (s |> Seq.take 5 |> String.Concat) = zeros ) |> Seq.head 