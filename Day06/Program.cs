using System.ComponentModel.Design;

string[] lines =
{
    "123 328  51 64 ",
    " 45 64  387 23 ",
    "  6 98  215 314",
    "*   +   *   +  ",
};
lines = File.ReadAllLines("input.txt");

if (true)
{
    var words = lines
        .Select(line => line.Split(' ', StringSplitOptions.RemoveEmptyEntries))
        .ToArray();
    int nproblems = words[0].Length;
    long grandtotal = 0;
    for (int p = 0; p < nproblems; p++)
    {
        string op = words.Last()[p];
        long total;
        if (op == "+")
        {
            total = 0;
        }
        else if (op == "*")
        {
            total = 1;
        }
        else
        {
            throw new Exception();
        }
        for (int n = 0; n != words.Length - 1; n++)
        {
            long num = long.Parse(words[n][p]);
            if (op == "+")
            {
                total += num;
            }
            else if (op == "*")
            {
                total *= num;
            }
            else
            {
                throw new Exception();
            }

        }
        grandtotal += total;
    }
    Console.WriteLine(new { grandtotal });
}

if (true)
{
    char op = '+';
    List<long> nums = [];
    long grandtotal = 0;
    for (int col = 0; col < lines[0].Length; col++)
    {
        {
            char maybe_op = lines.Last()[col];
            if (maybe_op != ' ')
            {
                op = maybe_op;
            }
        }
        int num = 0;
        for (int row = 0; row < lines.Length - 1; row++)
        {
            char c = lines[row][col];
            if (c != ' ')
            {
                num = num * 10 + (c - '0');
            }
        }
        if (num != 0)
        {
            nums.Add(num);
        }
        if (num == 0 || col == lines[0].Length - 1)
        {
            long total;
            if (op == '+')
            {
                total = 0;
                foreach (var n in nums)
                    total += n;
            }
            else if (op == '*')
            {
                total = 1;
                foreach (var n in nums)
                    total *= n;
            }
            else
            {
                throw new Exception();
            }
            nums.Clear();
            grandtotal += total;
        }
    }
    Console.WriteLine(new { grandtotal });
}