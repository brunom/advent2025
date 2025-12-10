using System.Numerics;

string[] lines = {
    "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
    "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
    "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}",
};
lines = File.ReadAllLines("input.txt");
int total = 0;
foreach (var line in lines)
{
    var parts = line.Split(' ');
    var indicator =
        parts[0].Trim('[', ']')
        .Select((c, i) => (c == '#' ? 1u : 0u) << i)
        .Aggregate(0u, (acc, val) => acc | val);
    var schematics =
        parts[1..^1]
        .Select(part =>
            part
            .Trim('(', ')')
            .Split(',')
            .Select(int.Parse)
            .Aggregate(0u, (acc, val) => acc | (1u << val)))
        .ToArray();
    int minButtons = int.MaxValue;
    for (uint i = 0; i < (1 << schematics.Length); i++)
    {
        uint tempIndicator = 0;
        for (int j = 0; j < schematics.Length; j++)
        {
            if (((i >> j) & 1) == 1)
            {
                tempIndicator ^= schematics[j];
            }
        }
        int buttons = BitOperations.PopCount(i);
        if (tempIndicator == indicator && buttons < minButtons)
        {
            minButtons = buttons;
        }
    }
    total += minButtons;
}
Console.WriteLine(new { total });
