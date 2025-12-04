string[] input = {
    "987654321111111",
    "811111111111119",
    "234234234234278",
    "818181911112111",
};
input = File.ReadAllLines("input.txt");
int total = 0;
foreach (var line in input)
{
    int maxPos = line
        .SkipLast(1)
        .Select((ch, i) => (ch, i))
        .MaxBy(x => x.ch).i;
    char ch1 = line[maxPos];
    string rest = line.Substring(maxPos + 1);
    char ch2 = rest.Max();
    total += int.Parse(ch1.ToString()+ch2.ToString());
}
Console.WriteLine(total);
