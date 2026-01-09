public interface IDelimiterParser 
{
    (List<string> delimiters, string numbersPart) Parse(string input);
    IEnumerable<string> Split(string numbersPart, List<string> delimiters);
}