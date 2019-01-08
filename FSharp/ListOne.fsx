open System.IO

let directories path = 
  let dirs = System.IO.Directory.EnumerateDirectories(path, "*.*", System.IO.SearchOption.TopDirectoryOnly)

  let result = 
    dirs
    |> (fun dir -> System.IO.Directory.GetDirectories(dir, "*.*", System.IO.SearchOption.TopDirectoryOnly))
    |> (fun subDirs -> List.length)
    |> Seq.sum

  result

Seq.iter (printfn "%s") t
