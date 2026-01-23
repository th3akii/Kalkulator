using Spectre.Console;
using Calculator.Core;

namespace Calculator.UI
{
	public class RomanNumbersUI
	{
		public void ShowRomanMenu()
		{
			while (true)
			{
				AnsiConsole.Clear();
				AnsiConsole.Write(new FigletText("Rimski Brojevi").Centered().Color(Color.SpringGreen3));
				AnsiConsole.Write(new Rule("[yellow]Izaberite opciju[/]").Centered());

				var choice = AnsiConsole.Prompt(
				new SelectionPrompt<string>()
					.PageSize(5)
					.AddChoices(new[]
					{
						"Rimski u Decimalni",
						"Decimalni u Rimski",
						"Rimski Kalkulator",
						"Nazad",
					})
					.HighlightStyle(new Style(Color.SpringGreen3, decoration: Decoration.Bold))
				);

				if (choice == "Nazad") break;

				switch (choice)
				{
					case "Rimski u Decimalni":
						RunRomanToDecimal();
						break;
					case "Decimalni u Rimski":
						RunDecimalToRoman();
						break;
					case "Rimski Kalkulator":
						RunRomanCalculator();
						break;
				}
			}
		}

		public void RunRomanToDecimal()
		{
			string resultText = "Unesite rimski broj za konverziju.";
			string? errorText = null;

			while (true)
			{
				AnsiConsole.Clear();
				AnsiConsole.Write(new FigletText("Rimski -> Dec").Centered().Color(Color.SpringGreen3));

				DisplayPanel(resultText, errorText);

				var input = GetInput("Unesite rimski broj (I, V, X...)");
				if (input == "q") break;

				if (string.IsNullOrWhiteSpace(input))
				{
					errorText = "Unos ne može biti prazan!";
					resultText = "";
					continue;
				}

				var romanNumber = new RomanNumber(input);
				if (romanNumber.IsValid())
				{
					resultText = $"Broj: [bold white]{input.ToUpper()}[/]  =>  Vrednost: [bold springgreen3]{romanNumber.ToDecimal()}[/]";
					errorText = null;
				}
				else
				{
					errorText = $"[bold red]{input.ToUpper()}[/] nije validan rimski broj!";
					resultText = "";
				}
			}
		}

		public void RunDecimalToRoman()
		{
			string resultText = "Unesite dekadni broj (1-3999).";
			string? errorText = null;

			while (true)
			{
				AnsiConsole.Clear();
				AnsiConsole.Write(new FigletText("Dec -> Rimski").Centered().Color(Color.SpringGreen3));

				DisplayPanel(resultText, errorText);

				var input = GetInput("Unesite dekadni broj");
				if (input == "q") break;

				if (int.TryParse(input, out int decValue))
				{
					if (decValue > 0 && decValue < 4000)
					{
						var romanStr = RomanNumber.ToDecimal(decValue);
						resultText = $"Decimalni: [bold white]{decValue}[/]  =>  Rimski: [bold springgreen3]{romanStr}[/]";
						errorText = null;
					}
					else
					{
						errorText = "Broj mora biti između 1 i 3999!";
						resultText = "";
					}
				}
				else
				{
					errorText = "Nevalidan unos! Unesite ceo broj.";
					resultText = "";
				}
			}
		}

		public void RunRomanCalculator()
		{
			string resultText = "Format: BROJ1 OPERACIJA BROJ2 (npr. XII + IV)";
			string? errorText = null;

			while (true)
			{
				AnsiConsole.Clear();
				AnsiConsole.Write(new FigletText("Rimski Kalk").Centered().Color(Color.SpringGreen3));

				DisplayPanel(resultText, errorText);

				var input = GetInput("Unesite izraz (npr. X + V)");
				if (input == "q") break;

				var parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
				if (parts.Length != 3)
				{
					errorText = "Format mora biti: BROJ OPERACIJA BROJ";
					resultText = "";
					continue;
				}

				var r1 = new RomanNumber(parts[0]);
				var op = parts[1];
				var r2 = new RomanNumber(parts[2]);

				if (!r1.IsValid() || !r2.IsValid())
				{
					errorText = "Jedan od unetih brojeva nije validan!";
					resultText = "";
					continue;
				}

				int val1 = r1.ToDecimal();
				int val2 = r2.ToDecimal();
				int res = 0;

				switch (op)
				{
					case "+": res = val1 + val2; break;
					case "-": res = val1 - val2; break;
					case "*": res = val1 * val2; break;
					case "/": res = val2 != 0 ? val1 / val2 : 0; break;
					default:
						errorText = "Nepoznata operacija! (+, -, *, /)";
						resultText = "";
						continue;
				}

				if (res <= 0 || res >= 4000)
				{
					var decRes = res;
					resultText = $"Rezultat (Decimalno): [bold white]{decRes}[/]\n[yellow]Rimski brojevi ne podržavaju ovaj rezultat.[/]";
					errorText = null;
				}
				else
				{
					resultText = $"Izraz: [bold white]{parts[0].ToUpper()} {op} {parts[2].ToUpper()}[/]\nRezultat: [bold springgreen3]{RomanNumber.ToDecimal(res)}[/] ({res})";
					errorText = null;
				}
			}
		}

		private void DisplayPanel(string resultText, string? errorText)
		{
			AnsiConsole.Write(new Rule().RuleStyle("grey"));
			var displayColor = errorText != null ? Color.Red : Color.SpringGreen3;
			var displayHeader = errorText != null ? " [red]GREŠKA[/] " : " [blue]STATUS / REZULTAT[/] ";
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