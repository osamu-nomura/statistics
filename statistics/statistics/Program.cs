using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hsb.Statistics;

namespace statistics
{
    class Program
    {
        static void Example_1_p74()
        {
            var data = new int[] { 47, 27, 25, 22, 18, 19, 20 };
            var average = Math.Round(Statistics.Average(data, n => (double)n), 1, MidpointRounding.AwayFromZero);
            var median = Math.Round(Statistics.Median(data, n => (double)n), 1, MidpointRounding.AwayFromZero);
            var trimMean = Math.Round(Statistics.TrimedMean(data, n => (double)n, 0.3), 1, MidpointRounding.AwayFromZero);

            Console.WriteLine($"average:{average} median:{median} trimMean:{trimMean}");
        }


        static void Main(string[] args)
        {
            Example_1_p74();
        }
    }
}
