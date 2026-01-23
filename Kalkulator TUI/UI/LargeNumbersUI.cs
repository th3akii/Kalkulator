using Spectre.Console;
using Calculator.Core;

namespace Calculator.UI
{
  public class LargeNumbersUI
  {
    public void ShowLargeNumbersMenu()
    {
      string resultText = "Format: BROJ1 + BROJ2 (npr. 12345 + 67890)";
      string? errorText = null;

      while (true)
      {
        AnsiConsole.Clear();
        AnsiConsole.Write(new FigletText("Veliki Brojevi").Centered().Color(Color.Cyan));
        
        DisplayPanel(resultText, errorText);

        var input = GetInput("Unesite izraz (npr. 100 + 200)");
        if (input == "q") break;

        var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 3)
        {
          errorText = "Format mora biti: BROJ + BROJ (razmaci su obavezni)";
          resultText = "";
          continue;
        }

        var n1 = new LargeNumber(parts[0]);
        var op = parts[1];
        var n2 = new LargeNumber(parts[2]);

        if (!n1.IsValid() || !n2.IsValid())
        {
          errorText = "Jedan od unetih brojeva nije validan!";
          resultText = "";
          continue;
        }

        LargeNumber result;
        switch (op)
        {
          case "+":
            result = n1 + n2;
            break;
          case "-":
            result = n1 - n2;
            break;
          case "*":
            result = n1 * n2;
            break;
          case "/":
            result = n1 / n2;
            if (result * n2 != n1)
            {
              var ostatak = n1 - (result * n2);
              resultText = $"Izraz: [bold white]{FormatNumber(parts[0])} {op} {FormatNumber(parts[2])}[/]\n" +
                          $"Rezultat: [bold cyan]{FormatNumber(result.GetValue())}[/]" +
                          $" Ostatak: [bold yellow]{FormatNumber(ostatak.GetValue())}[/]";
              errorText = null;
            }
            else
            {
              resultText = $"Izraz: [bold white]{FormatNumber(parts[0])} {op} {FormatNumber(parts[2])}[/]\n" +
                          $"Rezultat: [bold cyan]{FormatNumber(result.GetValue())}[/]";
            }
            errorText = null;
            continue;
          default:
            errorText = "Dostupano je samo sabiranje (+), oduzimanje (-) i množenje (*)!";
            resultText = "";
            continue;
        }

        var num1Formatted = FormatNumber(parts[0]);
        var num2Formatted = FormatNumber(parts[2]);
        var resultFormatted = FormatNumber(result.GetValue());

        resultText = $"Izraz: [bold white]{num1Formatted} {op} {num2Formatted}[/]\n" +
                     $"Rezultat: [bold cyan]{resultFormatted}[/]";
        errorText = null;
      }
    }

    private string FormatNumber(string number)
    {
      if (string.IsNullOrEmpty(number)) return number;
      
      var result = new System.Text.StringBuilder();
      int count = 0;

      for (int i = number.Length - 1; i >= 0; i--)
      {
        if (count > 0 && count % 3 == 0)
        {
          result.Insert(0, '\u200a');
        }
        result.Insert(0, number[i]);
        count++;
      }

      return result.ToString();
    }

    private void DisplayPanel(string resultText, string? errorText)
    {
      AnsiConsole.Write(new Rule().RuleStyle("grey"));
      var displayColor = errorText != null ? Color.Red : Color.Cyan;
      var displayHeader = errorText != null ? " [red]GREŠKA[/] " : " [blue]REZULTAT[/] ";
      var displayText = errorText ?? resultText;

      var displayPanel = new Panel(Align.Center(new Markup(displayText), VerticalAlignment.Middle))
        .Header(displayHeader)
        .BorderColor(displayColor)
        .BorderStyle(new Style(displayColor))
        .Padding(1, 1)
        .Expand();

      AnsiConsole.Write(displayPanel);
      AnsiConsole.Write(new Text("\n"));
    }

    private string GetInput(string promptText)
    {
      var instructionsPanel = new Panel(new Markup($"[grey]{promptText} (unesite 'q' za nazad)[/]"))
        .Header(" [yellow]INSTRUKCIJE[/] ")
        .Border(BoxBorder.Rounded)
        .BorderColor(Color.Yellow)
        .Expand();

      AnsiConsole.Write(instructionsPanel);

      var prompt = new TextPrompt<string>("[yellow]> [/]")
        .PromptStyle("white bold")
        .AllowEmpty();

      var input = prompt.Show(AnsiConsole.Console);
      AnsiConsole.WriteLine();

      return input?.ToLower().Trim() ?? "q";
    }
  }
}
