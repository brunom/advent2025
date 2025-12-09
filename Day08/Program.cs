string[] lines = {
    "162,817,812",
    "57,618,57",
    "906,360,560",
    "592,479,940",
    "352,342,300",
    "466,668,158",
    "542,29,236",
    "431,825,988",
    "739,650,466",
    "52,470,668",
    "216,146,977",
    "819,987,18",
    "117,168,530",
    "805,96,715",
    "346,949,466",
    "970,615,88",
    "941,993,340",
    "862,61,35",
    "984,92,344",
    "425,690,689",
};
lines = File.ReadAllLines("input.txt");
var points = lines.Select(line =>
{
    var parts = line.Split(',').Select(long.Parse).ToArray();
    return new Point(parts[0], parts[1], parts[2]);
}).ToArray();
static long Sqr(long x) => x * x;
var adjs =
    Enumerable.Range(0, points.Length)
    .SelectMany(i => Enumerable.Range(i + 1, points.Length - (i + 1))
        .Select(j => (i, j, SqrDistance:
            Sqr(points[i].X - points[j].X) +
            Sqr(points[i].Y - points[j].Y) +
            Sqr(points[i].Z - points[j].Z))))
    .OrderBy(t => t.SqrDistance)
    .Take(points.Length)
    .SelectMany(p => new[] { (p.i, p.j), (p.j, p.i) })
    .ToLookup(x => x.Item1, x => x.Item2);

HashSet<int> visited = new();
int Size(int node)
{
    int size = 1;
    foreach (var neighbor in adjs[node])
    {
        if (visited.Add(neighbor))
        {
            size += Size(neighbor);
        }
    }
    return size;
}
List<int> circuitSizes = new();
foreach (var adj in adjs)
{
    int node = adj.Key;
    if (visited.Add(node))
    {
        circuitSizes.Add(Size(node));
    }
}
Console.WriteLine(circuitSizes.OrderDescending().Take(3).Aggregate(1, (x, y) => x * y));
// 54600 too low
record struct Point(long X, long Y, long Z);
