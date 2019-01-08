open System
open System.IO

let writeDirectoryFile baseDir outputFile =
  let directories = Directory.EnumerateDirectories(baseDir, "*", SearchOption.AllDirectories)
  let output = File.CreateText(outputFile)
  
  try
    directories 
    |> Seq.iter (fun line -> output.WriteLine(Path.GetFullPath(line)))
  
  finally
    output.Close()

Console.Write("Base directory to scan: ")
let baseDir = Console.ReadLine()
Console.Write("Output file name: ")
let outFile = Console.ReadLine()

writeDirectoryFile baseDir outFile
