// <copyright file="StringDistance.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer.Utils
{
    using System;
    using System.Linq;

    public class StringDistance
    {
        /// <summary>
        /// Levenshteins the distance.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <param name="t">The t.</param>
        /// <returns></returns>
        public static int LevenshteinDistance(string s, string t)
        {
            s = RemoveNonAlphaNumeric(s);
            t = RemoveNonAlphaNumeric(t);

            int n = s.Length;
            int m = t.Length;

            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
            // Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            // Step 7
            return d[n, m];
        }

        public static string RemoveNonAlphaNumeric(string s)
        {
            return new string(s.Where(c => char.IsLetterOrDigit(c)).ToArray()).ToLower();
        }
    }
}