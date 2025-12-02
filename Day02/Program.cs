bool valid1(long n)
{
    var s = n.ToString();
    if (s.Length % 2 != 0)
    {
        return true;
    }
    string s1 = s.Substring(0, s.Length / 2);
    string s2 = s.Substring(s.Length / 2, s.Length / 2);
    return s1 != s2;
}
bool valid2(long n)
{
    var s = n.ToString();
    for (int len = 1; len < s.Length; ++len)
    {
        string s1 = s.Substring(0, len);
        string rest = s.Substring(len, s.Length - len);
        while (true)
        {
            if (rest.Length == 0)
            {
                return false;
            }
            else if (rest.Length < len)
            {
                break;
            }
            string sn = rest.Substring(0, len);
            rest = rest.Substring(len, rest.Length - len);
            if (s1 != sn)
            {
                break;
            }
        }
    }
    return true;
}

//string input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
string input = File.ReadAllText("input.txt");
long sum1 = 0;
long sum2 = 0;
foreach (var pair in input.Split(','))
{
    var bounds = pair.Split('-');
    var start = long.Parse(bounds[0]);
    var end = long.Parse(bounds[1]);
    for (long i = start; i <= end; i++)
    {
        if (!valid1(i))
        {
            sum1 += i;
        }

        if (!valid2(i))
        {
            sum2 += i;
        }
    }
}
Console.WriteLine(sum1);
Console.WriteLine(sum2);
