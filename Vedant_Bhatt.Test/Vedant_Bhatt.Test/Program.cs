internal class Program
{
    private static void Main(string[] args)
    {

        string[] weatherData = File.ReadAllLines("weather.dat");
        decimal minRange = int.MaxValue;
        int minDay = 0;

        foreach (string row in weatherData)
        {
            List<string> test = GetRowValues(row);

            if (test.Any() && int.TryParse(test[0], out int day))
            {
                decimal diff = decimal.Parse(test[1].Replace("*", "")) - decimal.Parse(test[2].Replace("*", ""));
                if (diff < minRange)
                {
                    minRange = diff;
                    minDay = day;
                }
            }
        }

        Console.WriteLine("The day that have minimum temprature difference: " + minDay);

        string[] footballData = File.ReadAllLines("football.dat");
        decimal minGoalDiff = int.MaxValue;
        string minGapTeam = string.Empty;
        foreach (string row in footballData)
        {
            List<string> test = GetRowValues(row);

            if (test.Any() && test.Count > 1 && int.TryParse(test[6], out int forGoal) && test[7] == "-")
            {
                int oppositeGoal = int.Parse(test[8]);
                int range;
                if (forGoal > oppositeGoal) range = forGoal - oppositeGoal;
                else if (oppositeGoal > forGoal) range = oppositeGoal - forGoal;
                else range = 0;

                if (range < minGoalDiff)
                {
                    minGoalDiff = range;
                    minGapTeam = test[1];
                }
            }
        }

        Console.WriteLine("Team name that have smallest difference in ‘for’ and ‘against’ goals: " + minGapTeam);
    }

    private static List<string> GetRowValues(string line)
    {
        var test = line.Split(" ").ToList();
        test.RemoveAll(x => string.IsNullOrWhiteSpace(x));
        return test;
    }
}