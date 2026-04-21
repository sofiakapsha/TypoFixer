namespace TypoFixer;

public class AutoCompleter
{
    public string GetWord(List<string> filteredDictionary, string word)
    {
        var index = BinarySearch(filteredDictionary, word);
        if (index < filteredDictionary.Count && filteredDictionary[index].StartsWith(word))
        {
            var complete = filteredDictionary[index].Substring((word.Length));
            return complete;
        }

        return "";
    }

    private int BinarySearch(List<string> filteredDictionary, string word)
    {
        int left = 0;
        int right = filteredDictionary.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            int comperison = string.Compare(filteredDictionary[mid], word, StringComparison.OrdinalIgnoreCase);

            if (comperison == 0)
                return mid;
            if (comperison < 0)
                left = mid + 1;
            else
                right = mid - 1;
        }

        return left;
    }
}