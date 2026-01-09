using System.Text.RegularExpressions;

public class DelimiterParser : IDelimiterParser 
{
    public (List<string> delimiters, string numbersPart) Parse(string input)
    {
        var delimiters = new List<string> { ",", "\n" };
        
        if (!input.StartsWith("//"))
            return (delimiters, input);

        var newlineIndex = input.IndexOf('\n');
        if (newlineIndex == -1)
            return (delimiters, input);

        var delimiterSection = input.Substring(2, newlineIndex - 2);
        var numbers = input[(newlineIndex + 1)..];

        var matches = Regex.Matches(delimiterSection, @"\[(.*?)\]");
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
                delimiters.Add(match.Groups[1].Value);
        }
        else
        {
            delimiters.Add(delimiterSection);
        }

        return (delimiters, numbers);
    }

    public IEnumerable<string> Split(string numbersPart, List<string> delimiters)
    {
        var pattern = string.Join("|", delimiters.Select(Regex.Escape));
        return Regex.Split(numbersPart, pattern);
    }
}
