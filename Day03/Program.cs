static long Joltage(string line, int digits)
{
    long result = 0;
    while (digits > 0)
    {
        int maxIndex = line
            .SkipLast(digits - 1)
            .Select((ch, i) => (ch, i))
            .MaxBy(x => x.ch).i;
        result = result * 10 + (line[maxIndex] - '0');
        line = line.Substring(maxIndex + 1);
        digits -= 1;
    }
    return result;
}

string[] input = {
    "987654321111111",
    "811111111111119",
    "234234234234278",
    "818181911112111",
};
input = File.ReadAllLines("input.txt");
long total2 = 0;
long total12 = 0;
foreach (var line in input)
{
    total2 += Joltage(line, 2);
    total12 += Joltage(line, 12);
}
Console.WriteLine(total2);
Console.WriteLine(total12);
