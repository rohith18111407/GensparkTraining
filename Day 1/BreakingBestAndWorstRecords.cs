using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

// https://www.hackerrank.com/challenges/breaking-best-and-worst-records/problem?isFullScreen=true

class Result
{

    /*
     * Complete the 'breakingRecords' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts INTEGER_ARRAY scores as parameter.
     */

    public static List<int> breakingRecords(List<int> scores)
    {
        int maxRecord_break = 0;
        int minRecord_break = 0;

        int minRecord_value = scores[0];
        int maxRecord_value = scores[0];

        foreach (int score in scores.Skip(1))
        {
            //maxRecord check
            if (maxRecord_value < score)
            {
                maxRecord_value = score;
                maxRecord_break++;
            }

            //minRecord check
            if (minRecord_value > score)
            {
                minRecord_value = score;
                minRecord_break++;
            }
        }

        // Console.WriteLine($"{maxRecord_break} {minRecord_break}");

        List<int> MaxMin = new List<int>();
        MaxMin.Add(maxRecord_break);
        MaxMin.Add(minRecord_break);

        return MaxMin;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> scores = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(scoresTemp => Convert.ToInt32(scoresTemp)).ToList();

        List<int> result = Result.breakingRecords(scores);

        textWriter.WriteLine(String.Join(" ", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
