using AdventOfCode.Solutions;

namespace AdventOfCode
{

    class Program
    {

        public static readonly Config Config = Config.Get("config.json");
        static readonly SolutionCollector Solutions = new SolutionCollector(Config.Year, Config.Days);

        static void Main(string[] args)
        {
            foreach(var solution in Solutions)
            {
                solution.Solve();
            }
        }
    }
}
