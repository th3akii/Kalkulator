using System;
using System.Numerics;
using System.Globalization;
using Calculator.Core;
using Spectre.Console;

namespace Calculator.Tests
{
  public class LargeNumberTests
  {
    private static Random random = new Random();

    public static void RunTestMenu()
    {
      while (true)
      {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[yellow]Testiranje LargeNumber klase[/]").Centered());

        var choice = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
            .Title("Izaberite testove koje želite da pokrenete:")
            .PageSize(10)
            .AddChoices(new[]
            {
              "Sabiranje",
              "Oduzimanje",
              "Množenje",
              "Deljenje",
              "Poređenje",
              "Decimalno Sabiranje",
              "Decimalno Oduzimanje",
              "Decimalno Množenje",
              "Decimalno Deljenje",
              "Decimalno Poređenje",
              "Decimalni Testovi",
              "Svi Testovi",
              "Nazad"
            })
            .HighlightStyle(new Style(Color.Yellow, decoration: Decoration.Bold))
        );

        if (choice == "Nazad") break;

        AnsiConsole.Clear();
        switch (choice)
        {
          case "Sabiranje":
            TestAddition(100);
            break;
          case "Oduzimanje":
            TestSubtraction(100);
            break;
          case "Množenje":
            TestMultiplication(100);
            break;
          case "Deljenje":
            TestDivision(100);
            break;
          case "Poređenje":
            TestComparison(100);
            break;
          case "Decimalno Sabiranje":
            TestAdditionDecimals(100);
            break;
          case "Decimalno Oduzimanje":
            TestSubtractionDecimals(100);
            break;
          case "Decimalno Množenje":
            TestMultiplicationDecimals(100);
            break;
          case "Decimalno Deljenje":
            TestDivisionDecimals(100);
            break;
          case "Decimalno Poređenje":
            TestComparisonDecimals(100);
            break;
          case "Decimalni Testovi":
            RunAllDecimalTests();
            break;
          case "Svi Testovi":
            RunAllTests();
            break;
        }

        AnsiConsole.MarkupLine("\n[grey]Pritisnite bilo koji taster za povratak na meni testova...[/]");
        Console.ReadKey(true);
      }
    }

    public static void RunAllTests()
    {
      AnsiConsole.Write(new Rule("[yellow]Pokretanje svih testova[/]").Centered());

      TestAddition(100);
      TestSubtraction(100);
      TestMultiplication(100);
      TestDivision(100);
      TestComparison(100);

      TestAdditionDecimals(100);
      TestSubtractionDecimals(100);
      TestMultiplicationDecimals(100);
      TestDivisionDecimals(100);
      TestComparisonDecimals(100);

      AnsiConsole.MarkupLine("\n[bold green]========================================[/]");
      AnsiConsole.MarkupLine("[bold green]Svi testovi završeni![/]");
      AnsiConsole.MarkupLine("[bold green]========================================[/]");
    }

    private static string GenerateLargeNumber(int length)
    {
      if (length <= 0) return "0";

      string result = "";
      result += random.Next(1, 10).ToString();

      for (int i = 1; i < length; i++)
      {
        result += random.Next(0, 10).ToString();
      }

      return result;
    }

    private static void TestAddition(int count)
    {
      Console.WriteLine($"--- Test SABIRANJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int length1 = random.Next(10, 101);
        int length2 = random.Next(10, 101);

        string num1Str = GenerateLargeNumber(length1);
        string num2Str = GenerateLargeNumber(length2);

        // koristi BigInteger za proveru
        BigInteger expected = BigInteger.Parse(num1Str) + BigInteger.Parse(num2Str);

        LargeNumber ln1 = new LargeNumber(num1Str);
        LargeNumber ln2 = new LargeNumber(num2Str);
        LargeNumber result = ln1 + ln2;

        string resultStr = result.GetValue();
        string expectedStr = expected.ToString();

        if (resultStr == expectedStr)
        {
          passed++;
        }
        else
        {
          failed++;
          Console.WriteLine($"GREŠKA #{i + 1}:");
          Console.WriteLine($"  {num1Str} + {num2Str}");
          Console.WriteLine($"  Očekivano: {expectedStr}");
          Console.WriteLine($"  Dobijeno:  {resultStr}");
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }

    private static void TestSubtraction(int count)
    {
      Console.WriteLine($"--- Test ODUZIMANJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int length1 = random.Next(10, 101);
        int length2 = random.Next(10, length1);

        string num1Str = GenerateLargeNumber(length1);
        string num2Str = GenerateLargeNumber(length2);

        BigInteger big1 = BigInteger.Parse(num1Str);
        BigInteger big2 = BigInteger.Parse(num2Str);

        if (big2 > big1)
        {
          var temp = big1;
          big1 = big2;
          big2 = temp;
          var tempStr = num1Str;
          num1Str = num2Str;
          num2Str = tempStr;
        }

        BigInteger expected = big1 - big2;

        LargeNumber ln1 = new LargeNumber(num1Str);
        LargeNumber ln2 = new LargeNumber(num2Str);
        LargeNumber result = ln1 - ln2;

        string resultStr = result.GetValue();
        string expectedStr = expected.ToString();

        if (resultStr == expectedStr)
        {
          passed++;
        }
        else
        {
          failed++;
          Console.WriteLine($"GREŠKA #{i + 1}:");
          Console.WriteLine($"  {num1Str} - {num2Str}");
          Console.WriteLine($"  Očekivano: {expectedStr}");
          Console.WriteLine($"  Dobijeno:  {resultStr}");
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }

    private static void TestMultiplication(int count)
    {
      Console.WriteLine($"--- Test MNOŽENJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int length1 = random.Next(5, 51);
        int length2 = random.Next(5, 51);

        string num1Str = GenerateLargeNumber(length1);
        string num2Str = GenerateLargeNumber(length2);

        BigInteger expected = BigInteger.Parse(num1Str) * BigInteger.Parse(num2Str);

        LargeNumber ln1 = new LargeNumber(num1Str);
        LargeNumber ln2 = new LargeNumber(num2Str);
        LargeNumber result = ln1 * ln2;

        string resultStr = result.GetValue();
        string expectedStr = expected.ToString();

        if (resultStr == expectedStr)
        {
          passed++;
        }
        else
        {
          failed++;
          Console.WriteLine($"GREŠKA #{i + 1}:");
          Console.WriteLine($"  {num1Str} * {num2Str}");
          Console.WriteLine($"  Očekivano: {expectedStr}");
          Console.WriteLine($"  Dobijeno:  {resultStr}");
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }

    private static void TestDivision(int count)
    {
      Console.WriteLine($"--- Test DELJENJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int length1 = random.Next(10, 51);
        int length2 = random.Next(5, length1);

        string num1Str = GenerateLargeNumber(length1);
        string num2Str = GenerateLargeNumber(length2);

        BigInteger big1 = BigInteger.Parse(num1Str);
        BigInteger big2 = BigInteger.Parse(num2Str);

        if (big2 == 0)
        {
          big2 = 1;
          num2Str = "1";
        }

        BigInteger expected = big1 / big2;

        try
        {
          LargeNumber ln1 = new LargeNumber(num1Str);
          LargeNumber ln2 = new LargeNumber(num2Str);
          LargeNumber result = ln1 / ln2;

          string resultStr = result.GetValue();
          string expectedStr = expected.ToString();

          if (resultStr == expectedStr)
          {
            passed++;
          }
          else
          {
            failed++;
            Console.WriteLine($"GREŠKA #{i + 1}:");
            Console.WriteLine($"  {num1Str} / {num2Str}");
            Console.WriteLine($"  Očekivano: {expectedStr}");
            Console.WriteLine($"  Dobijeno:  {resultStr}");
          }
        }
        catch (Exception ex)
        {
          failed++;
          Console.WriteLine($"IZUZETAK #{i + 1}:");
          Console.WriteLine($"  {num1Str} / {num2Str}");
          Console.WriteLine($"  Poruka: {ex.Message}");
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }
    private static void TestComparison(int count)
    {
      Console.WriteLine($"--- Test POREĐENJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int length1 = random.Next(10, 51);
        int length2 = random.Next(10, 51);

        string num1Str = GenerateLargeNumber(length1);
        string num2Str = GenerateLargeNumber(length2);

        BigInteger big1 = BigInteger.Parse(num1Str);
        BigInteger big2 = BigInteger.Parse(num2Str);

        LargeNumber ln1 = new LargeNumber(num1Str);
        LargeNumber ln2 = new LargeNumber(num2Str);

        bool testPassed = true;

        if ((ln1 > ln2) != (big1 > big2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator >):");
          Console.WriteLine($"  {num1Str} > {num2Str}");
          Console.WriteLine($"  Očekivano: {big1 > big2}");
          Console.WriteLine($"  Dobijeno:  {ln1 > ln2}");
        }

        if ((ln1 < ln2) != (big1 < big2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator <):");
          Console.WriteLine($"  {num1Str} < {num2Str}");
          Console.WriteLine($"  Očekivano: {big1 < big2}");
          Console.WriteLine($"  Dobijeno:  {ln1 < ln2}");
        }

        if ((ln1 >= ln2) != (big1 >= big2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator >=):");
          Console.WriteLine($"  {num1Str} >= {num2Str}");
          Console.WriteLine($"  Očekivano: {big1 >= big2}");
          Console.WriteLine($"  Dobijeno:  {ln1 >= ln2}");
        }

        if ((ln1 <= ln2) != (big1 <= big2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator <=):");
          Console.WriteLine($"  {num1Str} <= {num2Str}");
          Console.WriteLine($"  Očekivano: {big1 <= big2}");
          Console.WriteLine($"  Dobijeno:  {ln1 <= ln2}");
        }

        if ((ln1 == ln2) != (big1 == big2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator ==):");
          Console.WriteLine($"  {num1Str} == {num2Str}");
          Console.WriteLine($"  Očekivano: {big1 == big2}");
          Console.WriteLine($"  Dobijeno:  {ln1 == ln2}");
        }

        if (testPassed)
        {
          passed++;
        }
        else
        {
          failed++;
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }
    private static string GenerateDecimalNumber(int intLength, int fracLength)
    {
      if (intLength <= 0) intLength = 1;
      if (fracLength < 0) fracLength = 0;

      string intPart = "";
      intPart += random.Next(1, 10).ToString();
      for (int i = 1; i < intLength; i++)
      {
        intPart += random.Next(0, 10).ToString();
      }

      string fracPart = "";
      for (int i = 0; i < fracLength; i++)
      {
        fracPart += random.Next(0, 10).ToString();
      }

      if (fracPart == "") return intPart;
      return intPart + "." + fracPart;
    }

    private static string NormalizeDecimalString(string s)
    {
      if (!s.Contains("."))
      {
        if (s == "-0") return "0";
        return s;
      }

      // ensure no scientific notation
      if (s.Contains("E") || s.Contains("e"))
      {
        s = decimal.Parse(s, CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
      }

      // remove trailing zeros
      s = s.TrimEnd('0');
      if (s.EndsWith(".")) s = s.TrimEnd('.');
      if (s == "-0") s = "0";
      return s;
    }

    private static void TestAdditionDecimals(int count)
    {
      Console.WriteLine($"--- Test DECIMALNO SABIRANJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int il = random.Next(1, 13);
        int fl = random.Next(1, 9);
        int jl = random.Next(1, 13);
        int fj = random.Next(1, 9);

        string num1Str = GenerateDecimalNumber(il, fl);
        string num2Str = GenerateDecimalNumber(jl, fj);

        decimal expected = decimal.Parse(num1Str, CultureInfo.InvariantCulture) + decimal.Parse(num2Str, CultureInfo.InvariantCulture);

        LargeNumber ln1 = new LargeNumber(num1Str);
        LargeNumber ln2 = new LargeNumber(num2Str);
        LargeNumber result = ln1 + ln2;

        string resultStr = result.GetValue();
        string expectedStr = NormalizeDecimalString(expected.ToString(CultureInfo.InvariantCulture));

        if (resultStr == expectedStr)
        {
          passed++;
        }
        else
        {
          failed++;
          Console.WriteLine($"GREŠKA #{i + 1}:");
          Console.WriteLine($"  {num1Str} + {num2Str}");
          Console.WriteLine($"  Očekivano: {expectedStr}");
          Console.WriteLine($"  Dobijeno:  {resultStr}");
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }

    private static void TestSubtractionDecimals(int count)
    {
      Console.WriteLine($"--- Test DECIMALNO ODUZIMANJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int il = random.Next(1, 13);
        int fl = random.Next(1, 9);
        int jl = random.Next(1, 13);
        int fj = random.Next(1, 9);

        string num1Str = GenerateDecimalNumber(il, fl);
        string num2Str = GenerateDecimalNumber(jl, fj);

        decimal expected = decimal.Parse(num1Str, CultureInfo.InvariantCulture) - decimal.Parse(num2Str, CultureInfo.InvariantCulture);

        LargeNumber ln1 = new LargeNumber(num1Str);
        LargeNumber ln2 = new LargeNumber(num2Str);
        LargeNumber result = ln1 - ln2;

        string resultStr = result.GetValue();
        string expectedStr = NormalizeDecimalString(expected.ToString(CultureInfo.InvariantCulture));

        if (resultStr == expectedStr)
        {
          passed++;
        }
        else
        {
          failed++;
          Console.WriteLine($"GREŠKA #{i + 1}:");
          Console.WriteLine($"  {num1Str} - {num2Str}");
          Console.WriteLine($"  Očekivano: {expectedStr}");
          Console.WriteLine($"  Dobijeno:  {resultStr}");
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }

    private static void TestMultiplicationDecimals(int count)
    {
      Console.WriteLine($"--- Test DECIMALNO MNOŽENJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int il = random.Next(1, 9);
        int fl = random.Next(1, 6);
        int jl = random.Next(1, 9);
        int fj = random.Next(1, 6);

        string num1Str = GenerateDecimalNumber(il, fl);
        string num2Str = GenerateDecimalNumber(jl, fj);

        decimal expected = decimal.Parse(num1Str, CultureInfo.InvariantCulture) * decimal.Parse(num2Str, CultureInfo.InvariantCulture);

        LargeNumber ln1 = new LargeNumber(num1Str);
        LargeNumber ln2 = new LargeNumber(num2Str);
        LargeNumber result = ln1 * ln2;

        string resultStr = result.GetValue();
        string expectedStr = NormalizeDecimalString(expected.ToString(CultureInfo.InvariantCulture));

        if (resultStr == expectedStr)
        {
          passed++;
        }
        else
        {
          failed++;
          Console.WriteLine($"GREŠKA #{i + 1}:");
          Console.WriteLine($"  {num1Str} * {num2Str}");
          Console.WriteLine($"  Očekivano: {expectedStr}");
          Console.WriteLine($"  Dobijeno:  {resultStr}");
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }

    private static void TestDivisionDecimals(int count)
    {
      Console.WriteLine($"--- Test DECIMALNO DELJENJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        // generate divisible pair so result is exact integer
        int baseLen = random.Next(1, 6);
        string rightBase = GenerateLargeNumber(baseLen);
        int k = random.Next(1, 101);
        BigInteger rightBaseBig = BigInteger.Parse(rightBase);
        BigInteger leftBaseBig = rightBaseBig * k;
        string leftBase = leftBaseBig.ToString();

        int decimalPlaces = random.Next(0, 4);
        string rightStr = InsertDecimalAtEnd(rightBase, decimalPlaces);
        string leftStr = InsertDecimalAtEnd(leftBase, decimalPlaces);

        BigInteger expectedBig = k;

        try
        {
          LargeNumber ln1 = new LargeNumber(leftStr);
          LargeNumber ln2 = new LargeNumber(rightStr);
          LargeNumber result = ln1 / ln2;

          string resultStr = result.GetValue();
          string expectedStr = expectedBig.ToString();

          if (resultStr == expectedStr)
          {
            passed++;
          }
          else
          {
            failed++;
            Console.WriteLine($"GREŠKA #{i + 1}:");
            Console.WriteLine($"  {leftStr} / {rightStr}");
            Console.WriteLine($"  Očekivano: {expectedStr}");
            Console.WriteLine($"  Dobijeno:  {resultStr}");
          }
        }
        catch (Exception ex)
        {
          failed++;
          Console.WriteLine($"IZUZETAK #{i + 1}:");
          Console.WriteLine($"  {leftStr} / {rightStr}");
          Console.WriteLine($"  Poruka: {ex.Message}");
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }

    private static string InsertDecimalAtEnd(string baseStr, int decimalPlaces)
    {
      if (decimalPlaces == 0) return baseStr;
      string s = baseStr.PadLeft(decimalPlaces + 1, '0');
      int pos = s.Length - decimalPlaces;
      return s.Insert(pos, ".");
    }

    private static void TestComparisonDecimals(int count)
    {
      Console.WriteLine($"--- Test DECIMALNO POREĐENJE ({count} testova) ---");
      int passed = 0;
      int failed = 0;

      for (int i = 0; i < count; i++)
      {
        int il = random.Next(1, 9);
        int fl = random.Next(1, 6);
        int jl = random.Next(1, 9);
        int fj = random.Next(1, 6);

        string num1Str = GenerateDecimalNumber(il, fl);
        string num2Str = GenerateDecimalNumber(jl, fj);

        decimal dec1 = decimal.Parse(num1Str, CultureInfo.InvariantCulture);
        decimal dec2 = decimal.Parse(num2Str, CultureInfo.InvariantCulture);

        LargeNumber ln1 = new LargeNumber(num1Str);
        LargeNumber ln2 = new LargeNumber(num2Str);

        bool testPassed = true;

        if ((ln1 > ln2) != (dec1 > dec2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator >):");
          Console.WriteLine($"  {num1Str} > {num2Str}");
          Console.WriteLine($"  Očekivano: {dec1 > dec2}");
          Console.WriteLine($"  Dobijeno:  {ln1 > ln2}");
        }

        if ((ln1 < ln2) != (dec1 < dec2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator <):");
          Console.WriteLine($"  {num1Str} < {num2Str}");
          Console.WriteLine($"  Očekivano: {dec1 < dec2}");
          Console.WriteLine($"  Dobijeno:  {ln1 < ln2}");
        }

        if ((ln1 >= ln2) != (dec1 >= dec2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator >=):");
          Console.WriteLine($"  {num1Str} >= {num2Str}");
          Console.WriteLine($"  Očekivano: {dec1 >= dec2}");
          Console.WriteLine($"  Dobijeno:  {ln1 >= ln2}");
        }

        if ((ln1 <= ln2) != (dec1 <= dec2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator <=):");
          Console.WriteLine($"  {num1Str} <= {num2Str}");
          Console.WriteLine($"  Očekivano: {dec1 <= dec2}");
          Console.WriteLine($"  Dobijeno:  {ln1 <= ln2}");
        }

        if ((ln1 == ln2) != (dec1 == dec2))
        {
          testPassed = false;
          Console.WriteLine($"GREŠKA #{i + 1} (operator ==):");
          Console.WriteLine($"  {num1Str} == {num2Str}");
          Console.WriteLine($"  Očekivano: {dec1 == dec2}");
          Console.WriteLine($"  Dobijeno:  {ln1 == ln2}");
        }

        if (testPassed)
        {
          passed++;
        }
        else
        {
          failed++;
        }
      }

      Console.WriteLine($"Prošlo: {passed}/{count}");
      Console.WriteLine($"Palo: {failed}/{count}\n");
    }

    private static void RunAllDecimalTests()
    {
      AnsiConsole.Write(new Rule("[yellow]Pokretanje svih decimalnih testova[/]").Centered());

      TestAdditionDecimals(100);
      TestSubtractionDecimals(100);
      TestMultiplicationDecimals(100);
      TestDivisionDecimals(100);
      TestComparisonDecimals(100);

      AnsiConsole.MarkupLine("\n[bold green]========================================[/]");
      AnsiConsole.MarkupLine("[bold green]Svi decimalni testovi završeni![/]");
      AnsiConsole.MarkupLine("[bold green]========================================[/]");
    }
  }
}