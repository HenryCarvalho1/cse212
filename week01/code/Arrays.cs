using System.Globalization;

public static class Arrays
{
    public static double[] MultiplesOf(double number, int length)
    {
        // 1. Allocate a floating-point array (double) in memory.
        // The size of the array must be exactly equal to the 'length' parameter.
        double[] multiples = new double[length];

        // 2. Loop through the array from index 0 up to index (length - 1).
        // Each cycle of the loop populates a specific position in the array.
        for (int i = 0; i < length; i++)
        {
            // 3. Multiply the base number by the current multiplier factor.
            // We use '(i + 1)' because array indices start at 0, but multiples start at 1 
            // (e.g., if the number is 5, the first multiple is 5 * 1, the second is 5 * 2, etc.).
            multiples[i] = number * (i + 1);
        }

        // 4. Return the populated array containing all the generated multiples.
        return multiples;
    }

    public static void RotateListRight(List<int> data, int amount)
    {
        // 1. Store the total number of elements in the list for reference and readability.
        int n = data.Count;
        
        // 2. Execute the rotation 'amount' times. Each cycle shifts the entire list one step to the right.
        for (int i = 0; i < amount; i++)
        {
            // 3. Identify the last valid index of the list (total elements minus 1).
            int lastIndex = n - 1;
            
            // 4. Temporarily save the value of the very last element. 
            // We must store it before removing it from the list so the data isn't lost.
            int lastValue = data[lastIndex];
            
            // 5. Remove the last element from the list, temporarily reducing its size by 1.
            data.RemoveAt(lastIndex);
            
            // 6. Insert the saved value back, but at index 0 (the very beginning of the list).
            // This action automatically shifts all other existing elements one position to the right.
            data.Insert(0, lastValue);
        }
    }
}
