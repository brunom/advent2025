string[] lines = {
    "..@@.@@@@.",
    "@@@.@.@.@@",
    "@@@@@.@.@@",
    "@.@@@@..@.",
    "@@.@@@@.@@",
    ".@@@@@@@.@",
    ".@.@.@.@@@",
    "@.@@@.@@@@",
    ".@@@@@@@@.",
    "@.@.@@@.@.",
};
lines = File.ReadAllLines("input.txt");
int rows = lines.Length;
int cols = lines[0].Length;
int count = 0;
var adjacents = new (int, int)[]
{
    (-1, -1), (-1, +0), (-1, +1),
    (+0, -1),           (+0, +1),
    (+1, -1), (+1, +0), (+1, +1),
};
for (int row = 0; row < rows; ++row)
{
    for (int col = 0; col < cols; ++col)
    {
        char currentChar = lines[row][col];
        if (currentChar != '@')
        {
            continue;
        }

        int localCount = 0;
        foreach (var (dr, dc) in adjacents)
        {
            int nr = row + dr;
            int nc = col + dc;
            if (nr < 0 || nr >= rows || nc < 0 || nc >= cols)
            {
                continue;
            }
            if (lines[nr][nc] == '@')
            {
                ++localCount;
            }
        }
        if (localCount < 4)
        {
            ++count;
        }
    }
}
Console.WriteLine(new { count });
