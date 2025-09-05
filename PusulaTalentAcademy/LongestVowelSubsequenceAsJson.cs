using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class Solution
{
    /// <summary>
    /// Belirtilen kelime listesi için her kelimenin en uzun ardışık sesli harf dizisini bulur
    /// ve JSON formatında döndürür.
    /// </summary>
    /// <param name="words">Kelime listesi</param>
    /// <returns>JSON string</returns>
    public static string LongestVowelSubsequenceAsJson(List<string> words)
    {
        if (words == null || words.Count == 0)
            return "[]";

        var results = new List<object>();
        var vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };

        foreach (var word in words)
        {
            string longestSeq = "";
            string currentSeq = "";

            foreach (char c in word)
            {
                if (vowels.Contains(char.ToLower(c)))
                {
                    currentSeq += c;
                    if (currentSeq.Length > longestSeq.Length)
                    {
                        longestSeq = currentSeq;
                    }
                }
                else
                {
                    currentSeq = "";
                }
            }

            results.Add(new
            {
                word = word,
                sequence = longestSeq,
                length = longestSeq.Length
            });
        }

        return JsonSerializer.Serialize(results);
    }
}