using System.Globalization;

public static class Arrays
{
    public static double[] MultiplesOf(double number, int length)
    {
        //initialize the array with the defined length
        double[] multiples = new double[length];

        //Fills the array with the multiples
        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1); // (i + 1) ensures it starts at the first multiple
        }

        return []; // replace this return statement with your own
    }

    public static void RotateListRight(List<int> data, int amount)
    {
        int n = data.Count;
        
        for (int i = 0; i < amount; i++)
        {
            //saves the value of the last index
            int lastIndex = n - 1;
            int lastValue = data[lastIndex];
            
            //rotate the elements to the right
            data.RemoveAt(lastIndex);
            
            //Puts the last value to the first index
            data.Insert(0, lastValue);
        }
    }
}
