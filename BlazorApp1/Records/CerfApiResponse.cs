namespace English.Records
{
    public record CerfApiResponse (int Percentage, int CountOfWordsInText, string CERF, Dictionary<string, int> ContageOfCERF)
    {
    }
}