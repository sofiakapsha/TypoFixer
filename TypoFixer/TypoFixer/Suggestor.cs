namespace TypoFixer;

public class Suggestor
{
    public List<string> GetSuggestionsLCS(string user, IEnumerable<string> dictionary)
    {
        var filteredDictionary = dictionary.Where(w => Math.Abs(w.Length - user.Length) <= 2);
        
        return filteredDictionary
            .Select(word => new
            {
                Word = word,
                Score = LCSLengths.ComputeLCS(user, word)
            })
            .OrderByDescending(x => x.Score)
            .Take(5)
            .Select(x => x.Word)
            .ToList();
    }
    
    public List<string> GetSuggestions(string user, IEnumerable<string> dictionary)
    {
        var filteredDictionary = dictionary.Where(w => Math.Abs(w.Length - user.Length) <= 2);
        
        return filteredDictionary
            .Select(word => new
            {
                Word = word,
                LevenshteinScore = Levenshtein.ComputeLevenshtein(user, word),
                LCSScore = LCSLengths.ComputeLCS(user, word),
                LenDiff = Math.Abs(word.Length - user.Length)
            })
            .OrderBy(x => x.LevenshteinScore)
            .ThenBy(x => char.IsUpper(x.Word[0]))
            .ThenBy(x => x.LenDiff)
            .ThenByDescending(x => x.LCSScore)
            .ThenBy(x => x.Word.Length)
            .Take(5)
            .Select(x => x.Word)
            .ToList();
    }
}