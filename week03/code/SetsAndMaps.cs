using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
{
    var seenWords = new HashSet<string>();
    var pairs = new List<string>();

    foreach (var word in words)
    {
        // Reverse the 2-character word
        string reversed = $"{word[1]}{word[0]}";

        // If the reversed word is in the set, we found a symmetric pair.
        // Because there are no duplicates, words like "aa" will never match an earlier entry.
        if (seenWords.Contains(reversed))
        {
            pairs.Add($"{word} & {reversed}");
        }
        else
        {
            seenWords.Add(word);
        }
    }

    return pairs.ToArray();
}

    public static Dictionary<string, int> SummarizeDegrees(string filename)
{
    var degrees = new Dictionary<string, int>();
    foreach (var line in File.ReadLines(filename))
    {
        var fields = line.Split(",");
        string degree = fields[3].Trim();

        if (degrees.ContainsKey(degree))
        {
            degrees[degree]++;
        }
        else
        {
            degrees[degree] = 1;
        }
    }

    return degrees;
}


    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
{
    var letterCounts = new Dictionary<char, int>();

    // Count characters in word1
    foreach (char c in word1.ToLower())
    {
        if (c == ' ') continue;

        if (letterCounts.ContainsKey(c))
            letterCounts[c]++;
        else
            letterCounts[c] = 1;
    }

    // Subtract characters found in word2
    foreach (char c in word2.ToLower())
    {
        if (c == ' ') continue;

        if (!letterCounts.ContainsKey(c) || letterCounts[c] == 0)
        {
            return false;
        }

        letterCounts[c]--;
    }

    // Verify all counts are back to zero
    foreach (var count in letterCounts.Values)
    {
        if (count != 0)
        {
            return false;
        }
    }

    return true;
}


    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
{
    const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
    using var client = new HttpClient();
    using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
    using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
    using var reader = new StreamReader(jsonStream);
    var json = reader.ReadToEnd();
    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

    var summaries = new List<string>();
    if (featureCollection?.Features != null)
    {
        foreach (var feature in featureCollection.Features)
        {
            summaries.Add($"{feature.Properties.Place} - Mag {feature.Properties.Mag}");
        }
    }

    return summaries.ToArray();
}
}