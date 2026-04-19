namespace TypoFixer;

public class DictionaryLoader
{
    public static HashSet<string> LoadDictionary(string line)
    {
        var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        try
        {
            foreach (var word in File.ReadLines(line))
            {
                string clearedWord = word.Trim();

                if (!string.IsNullOrWhiteSpace(clearedWord))
                {
                    words.Add(clearedWord);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error during reading the file: {e}");
        }

        return words;

    }
}