using System;
class Program
{
    static void Main()
    {
        String inputDollars;
        String inputCents;
        Int32 dollars;
        Int32 cents;
        Boolean isValid;
        Boolean isValidCents;
        
        do
        {
            Console.Write("Enter dollars : ");
            inputDollars = Console.ReadLine();
            isValid = Int32.TryParse(inputDollars, out dollars);
            if (!isValid && dollars > 2_000_000_000)
                Console.WriteLine("\n  Incorrect dollars value, please try again\n");
            else
            {
                Console.Write("Enter cents : ");
                inputCents = Console.ReadLine();
                isValidCents = Int32.TryParse(inputCents, out cents);
                if (!isValidCents || cents < 0 || cents > 99)
                {
                    Console.WriteLine("\n  Incorrect cents value, please try again\n");
                }
                else
                {
                    Console.WriteLine("\n  {0}\n", NumbersToText(dollars, cents));
                }
            }
        }
        while (!(isValid && dollars == 0));
        Console.WriteLine("\nProgram ended");
    }

    public static string NumbersToText(Int32 dollars, Int32 cents)
    {
        if (dollars == 0 && cents == 0) return "Zero";
        String and = "and "; 
        Int32[] num = new Int32[4];
        Int32 first = 0;
        Int32 u, h, t;
        Int32 unitsCents, tensCents;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        if (dollars < 0)
        {
            sb.Append("Minus ");
            dollars = -dollars;
        }
        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
        string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
        string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
        string[] words3 = { "Thousand ", "Million ", "Billion " };
        num[0] = dollars % 1000;           // units
        num[1] = dollars / 1000;
        num[2] = dollars / 1000000;
        num[1] = num[1] - 1000 * num[2];  // thousands
        num[3] = dollars / 1000000000;     // billions
        num[2] = num[2] - 1000 * num[3];  // millions
        for (Int32 i = 3; i > 0; i--)
        {
            if (num[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (Int32 i = first; i >= 0; i--)
        {
            if (num[i] == 0) continue;
            u = num[i] % 10;              // ones
            t = num[i] / 10;
            h = num[i] / 100;             // hundreds
            t = t - 10 * h;               // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i < first) sb.Append(and);
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }

        if (cents > 0)
        {
            sb.Append(and);
            unitsCents = cents % 10;
            tensCents = cents / 10;
            if (tensCents == 0)
                sb.Append(words0[unitsCents]);
            else if (tensCents == 1)
                sb.Append(words1[unitsCents]);
            else
                sb.Append(words2[tensCents - 2] + words0[unitsCents]);
            sb.Append("cents");
        }
        return sb.ToString().TrimEnd();
    }
}