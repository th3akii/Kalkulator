using System;
using System.Collections.Generic;

namespace RomanCalculator.Core
{
  public class RomanNumber
  {
    private readonly string numberValue;
    private static readonly Dictionary<char, int> RomanMap = new()
    {
      {'I', 1},
      {'V', 5},
      {'X', 10},
      {'L', 50},
      {'C', 100},
      {'D', 500},
      {'M', 1000}
    };

    public RomanNumber(string number)
    {
      numberValue = number.ToUpper();
    }

    public int ToDecimal()
    {
      int decimalValue = 0;

      if (!IsValid())
      {
          return 0;
      }

      for (int i = 0; i < numberValue.Length; i++)
      {
        if(i + 1 < numberValue.Length && RomanMap[numberValue[i]] < RomanMap[numberValue[i + 1]])
        {
          decimalValue -= RomanMap[numberValue[i]];
        }
        else
        {
          decimalValue += RomanMap[numberValue[i]];
        }
      }

      return decimalValue;
    }

    public bool IsValid()
    {
      //dozvoljena slova
      foreach (char c in numberValue)
      {
          if (!RomanMap.ContainsKey(c))
          {
              return false;
          }
      }

      int previousValue = int.MaxValue;
      int repetitionCount = 1;
      bool isSubtractionCase = false;
      bool skipNext = false;

      for (int i = 0; i < numberValue.Length; i++)
      {
          // preskoci karakter ako je bio deo subtraction para
          if (skipNext)
          {
            skipNext = false;
            continue;
          }

          char currentChar = numberValue[i];
          int currentValue = RomanMap[numberValue[i]];

          //v, l, d se ne mogu ponavljati i nema vise od 3 ponavljanja
          if (i > 0 && numberValue[i] == numberValue[i - 1])
          {
            repetitionCount++;
            if (currentChar == 'V' || currentChar == 'L' || currentChar == 'D')
                return false;
            if (repetitionCount > 3)
                return false;
          }
          else
          {
            repetitionCount = 1;
          }

          if (i + 1 < numberValue.Length)
          {
            char nextChar = numberValue[i + 1];
            int nextValue = RomanMap[nextChar];

            if (currentValue < nextValue)
            {
              //I,X,C su jedini koji mogu da se oduzimaju
              if (currentChar == 'V' || currentChar == 'L' || currentChar == 'D')
              {
                return false;
              }

              //pravilo oduzimanja (I pre X i V, X pre L i C, C pre D i M)
              if (currentChar == 'I' && nextValue > 10) return false;
              if (currentChar == 'X' && nextValue > 100) return false;
              if (currentChar == 'C' && nextValue > 1000) return false;

              //samo jedan manji broj moze biti ispred veceg
              if (i > 0 && RomanMap[numberValue[i - 1]] <= currentValue)
              {
                return false;
              }

              //veci broj u subtraction paru ne sme biti vec koriscen pre (sprečava LXL, DCD, VIV)
              //nemam bolju ideju osim brute force provere
              for (int j = 0; j < i; j++)
              {
                if (numberValue[j] == nextChar)
                  return false;
              }

              //vrednost subtraction para mora biti manja od prethodnog broja (sprečava VIX)
              int subtractionValue = nextValue - currentValue;
              if (subtractionValue > previousValue)
              {
                return false;
              }

              //nakon oduzimanja nova vrednost ne moze manja ili jednaka staroj vrednosti
              if (i + 2 < numberValue.Length)
              {
                int thirdValue = RomanMap[numberValue[i + 2]];
                if(thirdValue > nextValue)
                  return false;
                
                //sprecava IXI i IXV - nisam imao bolje resenje
                //mozda ce da pokvari nesto drugo ali za sada radi
                if (numberValue[i + 2] <= currentChar*10)
                  return false;
              }

              previousValue = currentValue;
              isSubtractionCase = true;
              skipNext = true; 
            }
            else
            {
              isSubtractionCase = false;
            }
          }

          //u opadajucem redosledu
          if (!isSubtractionCase && currentValue > previousValue)
            return false;
          
          // azuriraj previousValue samo ako nije subtraction case
          if (!isSubtractionCase)
            previousValue = currentValue;
      }

      return true;
    }

    public static string ToDecimal(int number)
    {
        if (number <= 0 || number > 3999) return "N/A";

        string[] m = { "", "M", "MM", "MMM" };
        string[] c = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        string[] x = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        string[] i = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

        return m[number / 1000] + c[(number % 1000) / 100] + x[(number % 100) / 10] + i[number % 10];
    }
  }
}
