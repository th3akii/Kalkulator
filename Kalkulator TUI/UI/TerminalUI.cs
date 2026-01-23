using Spectre.Console;
using Calculator.Core;
using Calculator.Tests;

namespace Calculator.UI;

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
		var devUI = new DevUI();
		var largeUI = new LargeNumbersUI();
		switch (choice)
		{
			case "Rimski Brojevi":
				romanUI.ShowRomanMenu();
				break;
			case "Kalkulator Velikih Brojeva":
				largeUI.ShowLargeNumbersMenu();
				break;
			case "Dev":
				devUI.ShowDevMenu();
				break;
		}
	}
}
