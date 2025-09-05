using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public class Solution
{
    public static string MaxIncreasingSubarrayAsJson(List<int> numbers)
    {
        if (numbers == null || numbers.Count == 0)
            return JsonSerializer.Serialize(new List<int>());

        List<int> maxSubarray = new List<int>();
        List<int> currentSubarray = new List<int> { numbers[0] };
        int maxSum = numbers[0];
        int currentSum = numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                currentSubarray.Add(numbers[i]);
                currentSum += numbers[i];
            }
            else
            {
                if (currentSum > maxSum || 
                   (currentSum == maxSum && currentSubarray.Count > maxSubarray.Count))
                {
                    maxSum = currentSum;
                    maxSubarray = new List<int>(currentSubarray);
                }

                currentSubarray = new List<int> { numbers[i] };
                currentSum = numbers[i];
            }
        }

        // Final kontrol (döngü sonrası son alt diziyi kontrol et)
        if (currentSum > maxSum || 
            (currentSum == maxSum && currentSubarray.Count > maxSubarray.Count))
        {
            maxSubarray = new List<int>(currentSubarray);
        }

        return JsonSerializer.Serialize(maxSubarray);
    }
}