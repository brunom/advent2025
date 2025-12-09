string[] lines = {
    "7,1",
    "11,1",
    "11,7",
    "9,7",
    "9,5",
    "2,5",
    "2,3",
    "7,3",
};
lines = File.ReadAllLines("input.txt");
var pointsArray = lines.Select(line =>
{
    var parts = line.Split(',');
    return (X: long.Parse(parts[0]), Y: long.Parse(parts[1]));
}).ToArray();
//var pointsSet = pointsArray.ToHashSet();
List<long> areas = new();
for (int i = 0; i < pointsArray.Length; i++)
{
    var (x1, y1) = pointsArray[i];
    for (int j = i + 1; j < pointsArray.Length; j++)
    {
        var (x2, y2) = pointsArray[j];
        var area = (1 + Math.Abs(x2 - x1)) * (1 + Math.Abs(y2 - y1));
        areas.Add(area);
    }
}
Console.WriteLine($"Max area: {areas.Max()}");
