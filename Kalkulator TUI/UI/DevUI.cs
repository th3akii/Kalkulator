using Spectre.Console;
using Calculator.Tests;

namespace Calculator.UI;

public class DevUI
{
  public void ShowDevMenu()
  {
    while (true)
    {
      AnsiConsole.Clear();
      AnsiConsole.Write(new Rule("[yellow]Developer Mode[/]").Centered());

      var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
          .AddChoices(new[]
          {
            "Test Validacije Rimskih Brojeva",
            "Test Operacija Velikih Brojeva",
            "Nazad"
          })
          .HighlightStyle(new Style(Color.Yellow, decoration: Decoration.Bold))
      );

      switch (choice)
      {
        case "Test Validacije Rimskih Brojeva":
          AnsiConsole.Clear();
          RomanNumberValidationTest.RunTests();
          AnsiConsole.MarkupLine("\nPritisnite bilo koji taster za povratak...");
          Console.ReadKey();
          break;
        case "Test Operacija Velikih Brojeva":
          AnsiConsole.Clear();
          LargeNumberTests.RunTestMenu();
          AnsiConsole.MarkupLine("\nPritisnite bilo koji taster za povratak...");
          Console.ReadKey();
          break;
        case "Nazad":
          return;
      }
    }
  }
}
