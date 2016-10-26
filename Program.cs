using System;
using System.Collections.Generic;

class Solution {
    public int solution(int[] A) {
        var discLeftBorder = GetBorders(A, (x, y) => x - y);
        var discRightBorder = GetBorders(A, (x, y) => x + y);
        var checkpoints = new SortedSet<int>();
        checkpoints.UnionWith(discLeftBorder.Keys);
        checkpoints.UnionWith(discRightBorder.Keys);

        int intersections = 0;
        var currDisks = new HashSet<int>();
        foreach (var i in checkpoints) {
            var len = currDisks.Count;
            if (discLeftBorder.ContainsKey(i)) {
                var currLeftBorders = discLeftBorder[i];
                var newLen = currLeftBorders.Count;
                intersections += len*newLen + (newLen - 1)*newLen/2;
                foreach (var discBorder in currLeftBorders) {
                    currDisks.Add(discBorder);
                }
            }
            if (discRightBorder.ContainsKey(i)) {
                foreach (var discBorder in discRightBorder[i]) {
                    len--;
                    currDisks.Remove(discBorder);
                }       
            }
            if (intersections > 10000000) {
                return -1;
            }
        }
        return intersections;
    }

    private IDictionary<int, List<int>> GetBorders(int[] A, Func<int, int, int> forBorderPos) {
        var borders = new Dictionary<int, List<int>>();
        for (int i = 0; i < A.Length; i++) {
            int borderPos = forBorderPos(i, A[i]);
            if (borders.ContainsKey(borderPos)) {
                borders[borderPos].Add(i);
            } else {
                borders.Add(borderPos, new List<int>() { i });
            }
        }
        return borders;
    }
}

class Program {
    static void Main(String[] args) {
        int[] A = { 1, 5, 2, 1, 4, 0 };

        Console.WriteLine(new Solution().solution(A));

        int[] b = {647, 100, 10, 1};
        Console.WriteLine(new Solution().solution(b));

        int[] c = {};
        Console.WriteLine(new Solution().solution(c));
        c = new int[] { 10 };
        Console.WriteLine(new Solution().solution(c));
    }
}