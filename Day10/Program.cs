using System.Numerics;

string[] lines = {
    "[.##.] (3) (1,3) (2) (2,3) (0,2) (0,1) {3,5,4,7}",
    "[...#.] (0,2,3,4) (2,3) (0,4) (0,1,2) (1,2,3,4) {7,5,12,7,2}",
    "[.###.#] (0,1,2,3,4) (0,3,4) (0,1,2,4,5) (1,2) {10,11,11,5,10,5}",
};
lines = File.ReadAllLines("input.txt");
int total1_linq = (
    from line in lines
    let parts = line.Split(' ')
    let light_bits =
        parts[0]
        .Trim('[', ']')
        .Reverse()
        .Aggregate(0u, (acc, c) => acc << 1 | (c == '#' ? 1u : 0u))
    //.Select((c, i) => (c == '#' ? 1u : 0u) << i)
    //.Aggregate(0u, (acc, val) => acc | val)
    let button_effect_bits =
        parts[1..^1]
        .Select(part =>
            part
            .Trim('(', ')')
            .Split(',')
            .Aggregate(0u, (acc, s) => acc | (1u << int.Parse(s))))
        .ToArray()
    select (
        from button_pressed_bits in Enumerable.Range(0, 1 << button_effect_bits.Length)
        let curr_light_bits =
            button_effect_bits
            .Select((effect, j) => ((button_pressed_bits >> j) & 1) == 1 ? effect : 0u)
            .Aggregate(0u, (acc, val) => acc ^ val)
        where curr_light_bits == light_bits
        select BitOperations.PopCount((uint)button_pressed_bits))
        .Min())
    .Sum();
var stats =
    from line in lines
    let parts = line.Split(' ')
    let light_joltage_goal =
        parts[^1]
        .Trim('{', '}')
        .Split(',')
        .Select(int.Parse)
        .ToArray()
    //let x = light_joltage_goal.Length
    from x in light_joltage_goal
    select x;
//Console.WriteLine(stats.Max());

int total1 = 0;
int total2 = 0;
long work2 = 0;
foreach (var line in lines)
{
    var parts = line.Split(' ');
    uint light_bits =
        parts[0].Trim('[', ']')
        .Select((c, i) => (c == '#' ? 1u : 0u) << i)
        .Aggregate(0u, (acc, val) => acc | val);
    uint[] button_effect_bits =
        parts[1..^1]
        .Select(part =>
            part
            .Trim('(', ')')
            .Split(',')
            .Select(int.Parse)
            .Aggregate(0u, (acc, val) => acc | (1u << val)))
        .ToArray();
    int[] light_joltage_goal =
        parts[^1]
        .Trim('{', '}')
        .Split(',')
        .Select(int.Parse)
        .ToArray();

    int minButtons1 = int.MaxValue;
    for (uint button_pressed_bits = 0; button_pressed_bits < (1 << button_effect_bits.Length); button_pressed_bits++)
    {
        uint curr_light_bits = 0;
        for (int button = 0; button < button_effect_bits.Length; button++)
        {
            if (((button_pressed_bits >> button) & 1) == 1)
            {
                curr_light_bits ^= button_effect_bits[button];
            }
        }
        int buttons = BitOperations.PopCount(button_pressed_bits);
        if (curr_light_bits == light_bits && buttons < minButtons1)
        {
            minButtons1 = buttons;
        }
    }
    total1 += minButtons1;

    Queue<(int numButtons, int[] joltage)> queue = new();
    HashSet<int[]> seen = new(IntArrayEqualityComparer.Instance);
    var joltage = light_joltage_goal;
    int numButtons = 0;
    while (true)
    {
        foreach (uint b in button_effect_bits)
        {
            work2++;
            int[] new_joltage = new int[joltage.Length];
            bool allzero = true;
            bool allnonnegative = true;
            for (int i = 0; i < joltage.Length; i++)
            {
                int j = joltage[i];                
                if (((b >> i) & 1) == 1)
                    j--;
                if (j != 0)
                    allzero = false;
                if (j < 0)
                    allnonnegative = false;
                new_joltage[i] = j;
            }
            if (allzero)
            {
                total2 += numButtons + 1;
                goto done2;
            }
            if (allnonnegative)
            {
                if (seen.Add(new_joltage))
                {
                    queue.Enqueue((numButtons: numButtons + 1, joltage: new_joltage));
                }
            }
        }
        if (queue.Count == 0)
            break;
        (numButtons, joltage) = queue.Dequeue();
    }
done2: { }


}
Console.WriteLine(new { total1_linq, total1, total2, work2, });

class IntArrayEqualityComparer : IEqualityComparer<int[]>
{
    public static IntArrayEqualityComparer Instance { get; } = new IntArrayEqualityComparer();
    public bool Equals(int[]? x, int[]? y)
    {
        if (x == null || y == null)
            return x == y;
        if (x.Length != y.Length)
            return false;
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i] != y[i])
                return false;
        }
        return true;
    }
    public int GetHashCode(int[] obj)
    {
        var hash = new HashCode();
        if (obj != null)
        {
            foreach (var item in obj)
            {
                hash.Add(item);
            }
        }
        return hash.ToHashCode();
    }
}
