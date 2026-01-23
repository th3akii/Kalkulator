using Calculator.Core;

namespace Calculator.Tests
{
  public class RomanNumberValidationTest
  {
    public static void RunTests()
    {
      string[] invalidNumbers = { "IXIIXVC", "IIX", "VX", "LCI", "DMX", "ILV", "ICI", "XDC", "XMX",
      "VCI", "VVV", "LL", "DDIX", "IIII", "XXXXC", "CCCCIV", "MMMMX", "VIV", "LXLI", "DCD", "CMCXV",
      "XCXIC", "IVI", "IXV", "XVXIX", "IXIV", "VXI", "LCXC", "DM", "ILIV", "IC", "XDX", "XMI", "VC",
      "VVVC", "LLIX", "DD", "XXXXI", "CCCCXV", "MMMM", "VIVIC", "LXL", "DCDX", "CMCIV", "XCX",
      "IXVIX", "XVXC", "IIXI", "VXX", "LC", "DMI", "ICXC", "XDI", "XM", "VVIXV", "IIIIIC", "XXXXX",
      "CCCCV", "MMMMI", "LXLX", "DCDI", "CMCVC", "IVIIX", "XVX", "IXIIV", "IIXX", "VXC", "ILXV", "XD",
      "VCIXC", "DDI", "IIIIX", "XXXX", "MMMMC", "VIVX", "CMCV", "XCXIX", "IXVC", "XVXI", "IXIXV",
      "ILIVC", "XMIX", "LLI", "DDXC", "VIVI", "DCDC", "CMCIXV", "IXVI", "XVXX", "IIXIC", "LCX", "XDIX",
      "XMC", "VVIV", "LLX", "IIIII", "CCCCXVC", "DCDIX", "IVIIC", "IXVX", "DMC", "ICIX", "VVXV", "LLC",
      "LXLIXC", "XCXI", "IVIX", "IXIVC", "ILIXV", "VCX", "XXXXIC", "VIVIX", "XCXXC", "LCC", "DMIX",
      "VVIVC", "DDX", "MMMMIX", "VIVC", "IVII", "XVXXC", "LCIX", "ICIC", "IIIIC", "CCCCIXV", "VXIXC",
      "ICX", "VCC", "XXXXIX", "IXVIC", "IIXIX", "ILXVC", "DDC", "IIIIIX", "DCDIC", "XCXX", "IXIIXV",
      "IIXC", "XMXC", "CCCCIVC", "IVIC", "LLIXC", "LXLC", "XDIC", "XXXXXC", "XCXC", "LCIC", "VCIX",
      "VIVXC", "XVXIC", "ICC", "CMCIXVC", "MMMMIC", "IXVXC", "ILVC", "DDIC", "LXLIX", "IIXXC", "XMIC",
      "CCCCVC", "DMIXC", "IXIIVC", "XDXC", "VXIX", "VVXVC", "IIIIIXC", "VCIC", "MMMMXC", "DCDXC",
      "IVIIXC", "LXLIC", "IXIXVC", "ICIXC", "VXIC", "VCXC", "CCCCIXVC", "LLIC", "LXLXC", "XVXIXC",
      "CMCIVC", "VXXC" };

      int passed = 0;
      int failed = 0;
      List<string> failedNumbers = new List<string>();

      Console.WriteLine("═══════════════════════════════════════════════════════════");
      Console.WriteLine("         TEST ZA VALIDACIJU RIMSKIH BROJEVA");
      Console.WriteLine("═══════════════════════════════════════════════════════════\n");
      Console.WriteLine($"Testiram {invalidNumbers.Length} nevalidnih brojeva...\n");

      foreach (var numStr in invalidNumbers)
      {
        var romanNum = new RomanNumber(numStr);
        bool isValid = romanNum.IsValid();

        if (!isValid)
        {
          passed++;
          Console.ForegroundColor = ConsoleColor.Green;
          Console.Write("✓ ");
          Console.ResetColor();
          Console.Write($"{numStr,-10}");
          if (passed % 5 == 0) Console.WriteLine();
        }
        else
        {
          failed++;
          failedNumbers.Add(numStr);
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write("\n✗ ");
          Console.ResetColor();
          Console.Write($"{numStr,-10} <- GREŠKA: Prošao kao validan!\n");
        }
      }

      Console.WriteLine("\n\n═══════════════════════════════════════════════════════════");
      Console.WriteLine("                      REZULTATI");
      Console.WriteLine("═══════════════════════════════════════════════════════════");
      
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine($"Prošlo:  {passed}/{invalidNumbers.Length}");
      Console.ResetColor();
        
      if (failed > 0)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Palo:    {failed}/{invalidNumbers.Length}");
        Console.ResetColor();
        
        Console.WriteLine("\nNeispravni (prolaze kao validni):");
        foreach (var num in failedNumbers)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.WriteLine($"  • {num}");
          Console.ResetColor();
        }
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nSVI TESTOVI PROŠLI!");
        Console.ResetColor();
      }

      Console.WriteLine("═══════════════════════════════════════════════════════════\n");
    }
  }
}
