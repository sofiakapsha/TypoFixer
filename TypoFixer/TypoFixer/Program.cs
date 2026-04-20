using System.Text;
using TypoFixer;

var dictionary = DictionaryLoader.LoadDictionary("words_list.txt");
var filteredDictionary = dictionary.Order().ToList();
var suggestor = new Suggestor();
var completer = new AutoCompleter();

Console.WriteLine("Write your sentence: ");

var stringBuilder = new StringBuilder();

while (true)
{
    var button = Console.ReadKey(intercept: true);

    if (button.Key == ConsoleKey.Tab)
    {
        string[] text = stringBuilder.ToString().Split(new[]{' '}, StringSplitOptions.RemoveEmptyEntries);
        var lastWord = text.LastOrDefault();

        if (!string.IsNullOrEmpty(lastWord) && lastWord.Length >= 3)
        {
            var completion = completer.GetWord(filteredDictionary, lastWord);
            if (!string.IsNullOrEmpty(completion))
            {
                stringBuilder.Append(completion);
                Console.ResetColor();
                Console.Write(completion);
                
                Console.Write("                    ");
                for (int i = 0; i < 20; i++) Console.Write("\b");
            }
            
        }
    }
    else if (button.Key == ConsoleKey.Enter)
    {
        Console.Write("                    ");
        Console.WriteLine();
        break;
    }
    else if (button.Key == ConsoleKey.Backspace)
    {
        if (stringBuilder.Length > 0)
        {
            Console.Write("\b \b"); 
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
        }
    }
    else if (button.Key == ConsoleKey.Spacebar)
    {
        string[] text = stringBuilder.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var lastWord = text.LastOrDefault();

        if (!string.IsNullOrEmpty(lastWord) && !dictionary.Contains(lastWord.ToLower()))
        {
            for (int i = 0; i < lastWord.Length; i++)
            {
                Console.Write("\b \b");
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(lastWord);
            Console.ResetColor();

            var hints = suggestor.GetSuggestions(lastWord.ToLower(), dictionary);

            if (hints.Count > 0)
            {
                string menu = " [";
                for (int i = 0; i < hints.Count; i++)
                {
                    menu += $"{i + 1}: {hints[i]}, ";
                }

                menu = menu.TrimEnd() + "]";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(menu);
                Console.ResetColor();

                var choiceKey = Console.ReadKey(intercept: true);
                
                for (int i = 0; i < menu.Length; i++) Console.Write("\b \b");

                if (char.IsDigit(choiceKey.KeyChar))
                {
                    int choiceIndex = int.Parse(choiceKey.KeyChar.ToString()) - 1;
                    if (choiceIndex >= 0 && choiceIndex < hints.Count)
                    {
                        string chosenWord = hints[choiceIndex];
                        for (int i = 0; i < lastWord.Length; i++) Console.Write("\b \b");

                        stringBuilder.Remove(stringBuilder.Length - lastWord.Length, lastWord.Length);

                        stringBuilder.Append(chosenWord);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(chosenWord);
                        Console.ResetColor();
                    }
                }
            }
        }
        Console.Write(' ');
        stringBuilder.Append(' ');
    }
    else
    {
        stringBuilder.Append(button.KeyChar);
        Console.Write(button.KeyChar);
        
        string[] text = stringBuilder.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var lastWord = text.LastOrDefault();

        if (!string.IsNullOrEmpty(lastWord) && lastWord.Length >= 3)
        {
            var completion = completer.GetWord(filteredDictionary, lastWord);

            int charsToStepBack = 0;

            if (!string.IsNullOrEmpty(completion))
            {
                completer.Complete(completion);
                charsToStepBack += completion.Length;
            }
            Console.Write("                    ");
            charsToStepBack += 20;

            for (int i = 0; i < charsToStepBack; i++)
            {
                Console.Write("\b");
            }
        }
    }
}

string[] inputWords = stringBuilder.ToString().Split(new[] { ' ', ',', '.', '!' }, StringSplitOptions.RemoveEmptyEntries);

foreach (var word in inputWords)
{
    if (!dictionary.Contains(word))
    {
        Console.WriteLine($"\nFound the mistake: '{word}'");
        
        var hints = suggestor.GetSuggestions(word, dictionary);
        
        Console.WriteLine("Maybe you meant: " + string.Join(", ", hints));
    }
}