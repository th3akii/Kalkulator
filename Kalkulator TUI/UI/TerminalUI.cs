using Spectre.Console;

namespace RomanCalculator.UI;

public class TerminalUI
{
    public void Run()
    {
        while (true)
        {
            AnsiConsole.Clear();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Kalkulator")
                    .AddChoices(
                        "Rimski",
                        "Veliki brojevi",
                        "Izlaz"
                    )
            );

            if (choice == "Izlaz")
                break;

            Handle(choice);
        }
    }

    private void Handle(string choice)
    {
        AnsiConsole.MarkupLine($"Izabrao si: [yellow]{choice}[/]");
        AnsiConsole.MarkupLine("\nPritisni taster...");
        Console.ReadKey();
    }
}
