string[] lines = {
    "3-5",
    "10-14",
    "16-20",
    "12-18",
    "",
    "1",
    "5",
    "8",
    "11",
    "17",
    "32",
};
lines = File.ReadAllLines("input.txt");
int emptyIndex = Array.IndexOf(lines, "");
var fresh = lines[0..emptyIndex].Select(s =>
{
    var parts = s.Split('-').Select(long.Parse).ToArray();
    return (parts[0], parts[1] + 1); // open-closed intervals simplify part 2

}).ToArray();
var available = lines[(emptyIndex + 1)..].Select(long.Parse).ToArray();
long count1 = 0;
foreach (var a in available)
{
    if (fresh.Any(f => f.Item1 <= a && a < f.Item2))
    {
        count1++;
    }
}
Console.WriteLine(count1);

var edges = fresh
    .SelectMany(f => new[] { (ID: f.Item1, d: +1), (ID: f.Item2, d: -1), })
    .OrderBy(t => t.ID)
    .ToArray();
long level = 0;
long prev = 0;
long count2 = 0;
foreach (var (ID, d) in edges)
{
    if (level > 0)
    {
        count2 += ID - prev;
    }
    level += d;
    prev = ID;
}
Console.WriteLine(count2);
