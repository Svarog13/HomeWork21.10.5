using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the text:");
        string inputText = Console.ReadLine();

        Console.WriteLine("Enter a list of forbidden words separated by commas:");
        string forbiddenWordsInput = Console.ReadLine();
        string[] forbiddenWords = forbiddenWordsInput.Split(',').Select(word => word.Trim()).ToArray();

        string censoredText = CensorText(inputText, forbiddenWords, out int replacedWordCount);

        Console.WriteLine("Censored Text:");
        Console.WriteLine(censoredText);

        Console.WriteLine($"Statistics: {replacedWordCount} word(s) replaced.");
    }

    static string CensorText(string text, string[] forbiddenWords, out int replacedWordCount)
    {
        replacedWordCount = 0;
        string censoredText = text;
        foreach (string forbiddenWord in forbiddenWords)
        {
            string pattern = $@"\b{Regex.Escape(forbiddenWord)}\b";
            censoredText = Regex.Replace(censoredText, pattern, MatchEvaluator, RegexOptions.IgnoreCase);
            replacedWordCount += Regex.Matches(text, pattern, RegexOptions.IgnoreCase).Count;
        }
        return censoredText;
    }

    static string MatchEvaluator(Match match)
    {
        return new string('*', match.Value.Length);
    }
}
