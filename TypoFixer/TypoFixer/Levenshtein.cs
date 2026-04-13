namespace TypoFixer;

public static class Levenshtein
{
    public static int ComputeLevenshtein(string user, string dictionary)
    {
        int m = user.Length;
        int n = dictionary.Length;
        
        int[,] common = new int[m + 1, n + 1];

        for (var i = 0; i <= m; i++)
        {
            common[i, 0] = i;
        }
        
        for (var j = 0; j <= n; j++)
        {
            common[0, j] = j;
        }

        var cost = 0;

        for (var i = 1; i <= m; i++)
        {
            for (var j = 1; j <= n; j++)
            {
                if (user[i - 1] == dictionary[j - 1])
                {
                    cost = 0;
                }
                else
                {
                    cost = 1;
                }

                common[i, j] = Math.Min(Math.Min(common[i - 1, j] + 1, common[i, j - 1] + 1),
                    common[i - 1, j - 1] + cost);

                if (i > 1 && j > 1 && user[i - 1] == dictionary[j - 2] && user[i - 2] == dictionary[j - 1])
                {
                    common[i, j] = Math.Min(common[i, j], common[i - 2, j - 2] + cost);
                }
            }
        }

        return common[m, n];
    }
}