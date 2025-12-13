using System.Numerics;

string[] lines = {
    "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
    "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
    "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}",
};
lines = File.ReadAllLines("input.txt");
int total1 = 0;
int total2 = 0;
foreach (var line in lines)
{
    var parts = line.Split(' ');
    var light_goal =
        parts[0].Trim('[', ']')
        .Select((c, i) => (c == '#' ? 1u : 0u) << i)
        .Aggregate(0u, (acc, val) => acc | val);
    var button_effect =
        parts[1..^1]
        .Select(part =>
            part
            .Trim('(', ')')
            .Split(',')
            .Select(int.Parse)
            .Aggregate(0u, (acc, val) => acc | (1u << val)))
        .OrderByDescending(BitOperations.PopCount)
        .ToArray();
    var light_joltage_goal =
        parts[^1]
        .Trim('{', '}')
        .Split(',')
        .Select(int.Parse)
        .ToArray();

    int minButtons1 = int.MaxValue;
    for (uint i = 0; i < (1 << button_effect.Length); i++)
    {
        uint tempIndicator = 0;
        for (int j = 0; j < button_effect.Length; j++)
        {
            if (((i >> j) & 1) == 1)
            {
                tempIndicator ^= button_effect[j];
            }
        }
        int buttons = BitOperations.PopCount(i);
        if (tempIndicator == light_goal && buttons < minButtons1)
        {
            minButtons1 = buttons;
        }
    }
    total1 += minButtons1;

    int minButtons2 = int.MaxValue;
    int[] curr_presses = new int[button_effect.Length];
    int[] curr_joltage = new int[light_joltage_goal.Length];
    bool add_joltage(int button, int presses) // returns overflow
    {
        bool overflow = false;
        for (int light = 0; light < curr_joltage.Length; light++)
        {
            if (((button_effect[button] >> light) & 1) == 1)
            {
                curr_joltage[light] += presses;
                if (curr_joltage[light] > light_joltage_goal[light])
                {
                    overflow = true;
                }
            }
        }
        return overflow;
    }
    bool inc_presses() // returns carry
    {
        for (int button = 0; button < button_effect.Length; button++)
        {
            curr_presses[button]++;
            if (!add_joltage(button, 1))
                return false;
            add_joltage(button, -curr_presses[button]);
            curr_presses[button] = 0;
        }
        return true;
    }
    while (true)
    {
        if (inc_presses())
            break;
        if (Enumerable.SequenceEqual(curr_joltage, light_joltage_goal))
        {
            int buttons = curr_presses.Sum();
            if (buttons < minButtons2)
            {
                minButtons2 = buttons;
                break;
            }
        }
    }
    total2 += minButtons2;

}
Console.WriteLine(new { total1, total2 });
