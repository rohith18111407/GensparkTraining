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

// https://www.hackerrank.com/challenges/day-of-the-programmer/problem?isFullScreen=true

class Result
{

    /*
     * Complete the 'dayOfProgrammer' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts INTEGER year as parameter.
     */

    public static string dayOfProgrammer(int year)
    {
        if (year == 1918)
        {
            // Transition year from Julian to Gregorian calendar
            return $"26.09.{year}";
        }
        else if ((year >= 1700 && year <= 1917 && year % 4 == 0) ||
                 (year >= 1919 && (year % 400 == 0 || (year % 4 == 0 && year % 100 != 0))))
        {
            // Leap year for Julian (before 1918) or Gregorian (after 1918)
            return $"12.09.{year}";
        }
        else
        {
            // Non-leap year
            return $"13.09.{year}";
        }
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int year = Convert.ToInt32(Console.ReadLine().Trim());

        string result = Result.dayOfProgrammer(year);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
