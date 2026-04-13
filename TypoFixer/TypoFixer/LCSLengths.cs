namespace TypoFixer;

public static class LCSLengths
{
    public static int ComputeLCS(string user, string dictionary)
    {
        int m = user.Length;
        int n = dictionary.Length;
        
        int[,] common = new int[m + 1, n + 1];

        for (var i = 1; i <= m; i++)
        {
            for (var j = 1; j <= n; j++)
            {
                if (user[i - 1] == dictionary[j - 1])
                {
                    common[i, j] = common[i - 1, j - 1] + 1;
                }
                else
                {
                    common[i, j] = Math.Max(common[i, j - 1], common[i - 1, j]);
                }
            }
        }

        return common[m, n];
    }
}