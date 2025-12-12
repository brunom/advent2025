string[] lines = {
    "aaa: you hhh",
    "you: bbb ccc",
    "bbb: ddd eee",
    "ccc: ddd eee fff",
    "ddd: ggg",
    "eee: out",
    "fff: out",
    "ggg: out",
    "hhh: ccc fff iii",
    "iii: out",
};
lines = File.ReadAllLines("input.txt");
var graph = lines
    .Select(line => line.Split(new[] { ':', ' ', }, StringSplitOptions.RemoveEmptyEntries))
    .ToDictionary(parts => parts[0], parts => parts[1..]);
long Paths(string from)
{
    if (from == "out")
        return 1;
    long total = 0;
    foreach (var neighbor in graph[from])
        total += Paths(neighbor);
    return total;
}
Console.WriteLine(Paths("you"));

//HashSet<string> reachable = new();
//void Traverse(string node)
//{
//    if (!reachable.Add(node))
//        return;
//    if (node == "out")
//        return;
//    foreach (var neighbor in graph[node])
//        Traverse(neighbor);
//}
//Traverse("you");
//graph = graph
//    .Where(kv => reachable.Contains(kv.Key))
//    .ToDictionary(kv => kv.Key, kv => kv.Value.Where(v => reachable.Contains(v)).ToArray());
