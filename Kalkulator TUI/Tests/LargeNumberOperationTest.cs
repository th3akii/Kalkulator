using System;
using System.Numerics;
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
  }
}