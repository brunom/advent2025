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
string[] lines2 = {
    "svr: aaa bbb",
    "aaa: fft",
    "fft: ccc",
    "bbb: tty",
    "tty: ccc",
    "ccc: ddd eee",
    "ddd: hub",
    "hub: fff",
    "eee: dac",
    "dac: fff",
    "fff: ggg hhh",
    "ggg: out",
    "hhh: out",
};
lines = File.ReadAllLines("input.txt");
//lines = lines2;
var graph = lines
    .Select(line => line.Split(new[] { ':', ' ', }, StringSplitOptions.RemoveEmptyEntries))
    .ToDictionary(parts => parts[0], parts => parts[1..]);
long Paths(string from, Dictionary<string, long> visited)
{
    long total = 0;
    if (visited.TryGetValue(from, out total))
        return total;
    if (graph.TryGetValue(from, out var neighbors))
    {
        foreach (var neighbor in neighbors)
        {
            total += Paths(neighbor, visited );
        }
    }
    visited.Add(from, total);
    return total;
}
Console.WriteLine(Paths("you", new() { ["out"] = 1 }));

Console.WriteLine(
    Paths("svr", new() { ["fft"] = 1, ["dac"] = 0, }) *
    Paths("fft", new() { ["dac"] = 1, }) *
    Paths("dac", new() { ["out"] = 1, ["fft"] = 0, })
    +
    Paths("svr", new() { ["dac"] = 1, ["fft"] = 0, }) *
    Paths("dac", new() { ["fft"] = 1, }) *
    Paths("fft", new() { ["out"] = 1, ["dac"] = 0, }));
