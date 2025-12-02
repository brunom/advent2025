<Query Kind="Statements" />

int curr = 50;
int password1 = 0;
int password2 = 0;
foreach (var line in File.ReadLines("input.txt"))
{
	int distance = int.Parse(line.Substring(1));
	int step;
	if (line[0] == 'L')
	{
		step = -1;
	}
	else
	{
		step = +1;
	}
	for (int i = 0; i < distance; ++i)
	{
		curr += step;
		curr = curr % 100;
		if (curr == 0)
		{
			password2 += 1;
		}
	}

	if (curr == 0)
	{
		password1 += 1;
	}
}
password1.Dump();
password2.Dump();
