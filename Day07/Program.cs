using System.Collections.Concurrent;

string[] input =
{
    ".......S.......",
    "...............",
    ".......^.......",
    "...............",
    "......^.^......",
    "...............",
    ".....^.^.^.....",
    "...............",
    "....^.^...^....",
    "...............",
    "...^.^...^.^...",
    "...............",
    "..^...^.....^..",
    "...............",
    ".^.^.^.^.^...^.",
    "...............",
};
input = File.ReadAllLines("input.txt");
static void Sum(Dictionary<int, long> beams, int beam, long count)
{
    beams.TryGetValue(beam, out var prev);
    beams[beam] = prev + count;
}
Dictionary<int, long> beams = new();
Sum(beams, input[0].IndexOf('S'), 1);
int limit = 0;
for (int i = 2; i < input.Length; i += 2)
{
    Dictionary<int, long> newBeams = new();
    foreach (var (beam, count) in beams)
    {
        if (input[i][beam] == '.')
        {
            Sum(newBeams, beam, count);
        }
        else if (input[i][beam] == '^')
        {
            Sum(newBeams, beam - 1, count);
            Sum(newBeams, beam + 1, count);
            limit++;
        }
        else throw new Exception();
    }
    beams = newBeams;
}
Console.WriteLine(new
{
    limit,
    timelines = beams.Values.Sum(),
});
