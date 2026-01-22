using Spectre.Console;
using RomanCalculator.Core;
using RomanCalculator.Tests;

namespace RomanCalculator.UI;

public class TerminalUI
{
	public void Run()
	{
		while (true)
		{
			AnsiConsole.Clear();
			
			AnsiConsole.Write(
					new FigletText("Kalkulator")
						.Centered()
						.Color(Color.Blue));

			AnsiConsole.Write(new Rule("[yellow]Izaberite mod kalkulatora[/]").Centered());

			var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.PageSize(5)
					.AddChoices(new[]
					{
						"Rimski Brojevi",
						"Kalkulator Velikih Brojeva",
						"Dev",
						"Izlaz",
					})
					.HighlightStyle(new Style(Color.Blue, decoration: Decoration.Bold))
			);

			if (choice == "Izlaz")
			{
				AnsiConsole.MarkupLine("[blue]Doviđenja![/]");
				break;
			}

			Handle(choice);
		}
	}

	private void Handle(string choice)
	{
		var romanUI = new RomanNumbersUI();
		switch (choice)
		{
			case "Rimski Brojevi":
				romanUI.ShowRomanMenu();
				break;
			case "Kalkulator Velikih Brojeva":
				AnsiConsole.Clear();
				AnsiConsole.Write(new Rule($"[white bold]Izabran mod: {choice}[/]").Centered());
				AnsiConsole.MarkupLine("\nOva funkcionalnost još uvek nije implementirana.");
				AnsiConsole.MarkupLine("\nPritisnite bilo koji taster za povratak na glavni meni...");
				Console.ReadKey();
				break;
			case "Developer Mode":
				ShowDevMenu();
				break;
		}
	}

	private void ShowDevMenu()
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
				case "Nazad":
					return;
			}
		}
	}
}
