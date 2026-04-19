using TypoFixer;

var dictionary = DictionaryLoader.LoadDictionary("words_list.txt");
Console.WriteLine("Write your sentence: ");
var input = Console.ReadLine();

string[] inputWords = input.Split(new[] { ' ', ',', '.', '!' }, StringSplitOptions.RemoveEmptyEntries);

var suggestor = new Suggestor();

foreach (var word in inputWords)
{
    if (!dictionary.Contains(word))
    {
        Console.WriteLine($"\nFound the mistake: '{word}'");
        
        var hints = suggestor.GetSuggestions(word, dictionary);
        
        Console.WriteLine("Maybe you meant: " + string.Join(", ", hints));
    }
}