#r @"packages\FParsec.1.0.2\lib\net40-client\FParsecCS.dll"
#r @"packages\FParsec.1.0.2\lib\net40-client\FParsec.dll"

open System
open System.IO
open FParsec
open FParsec.CharParsers

Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

let input = File.ReadAllLines @"input07.txt"

let arrow:Parser<unit,unit> = skipString "->"
let AND:Parser<string, string> = pstring "AND"
let OR:Parser<string, string> = pstring "OR"
let LSHIFT:Parser<string, string> = pstring "LSHIFT"
let RSHIFT:Parser<string, string> = pstring "RSHIFT"
let NOT:Parser<string, string> = pstring "NOT"
let identifier = many
pint16