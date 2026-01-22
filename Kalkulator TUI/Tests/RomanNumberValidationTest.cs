using RomanCalculator.Core;

namespace RomanCalculator.Tests
{
  public class RomanNumberValidationTest
  {
    public static void RunTests()
    {
      string[] invalidNumbers = { "IXIIXVC", "IIX", "VX", "LCI", "DMX", "ILV", "ICI", "XDC", "XMX",
      "VCI", "VVV", "LL", "DDIX", "IIII", "XXXXC", "CCCCIV", "MMMMX", "VIV", "LXLI", "DCD", "CMCXV",
      "XCXIC", "IVI", "IXV", "XVXIX", "IXIV", "IIX", "VXI", "LCXC", "DM", "ILIV", "IC", "XDX", "XMI",
      "VC", "VVVC", "LLIX", "DD", "IIII", "XXXXI", "CCCCXV", "MMMM", "VIVIC", "LXL", "DCDX", "CMCIV",
      "XCX", "IVI", "IXVIX", "XVXC", "IXIV", "IIXI", "VXX", "LC", "DMI", "ILV", "ICXC", "XDI", "XM",
      "VC", "VVIXV", "LL", "DD", "IIIIIC", "XXXXX", "CCCCV", "MMMMI", "VIV", "LXLX", "DCDI", "CMCVC",
      "XCX", "IVIIX", "IXV", "XVX", "IXIIV", "IIXX", "VXC", "LCI", "DM", "ILXV", "ICI", "XD", "XM",
      "VCIXC", "VVV", "LL", "DDI", "IIIIX", "XXXX", "CCCCIV", "MMMMC", "VIVX", "LXLI", "DCD", "CMCV",
      "XCXIX", "IVI", "IXVC", "XVXI", "IXIXV", "IIX", "VXI", "LC", "DMX", "ILIVC", "IC", "XD", "XMIX",
      "VC", "VVV", "LLI", "DDXC", "IIII", "XXXXI", "CCCCV", "MMMMX", "VIVI", "LXL", "DCDC", "CMCIXV",
      "XCX", "IVI", "IXVI", "XVXX", "IXIV", "IIXIC", "VX", "LCX", "DMI", "ILV", "IC", "XDIX", "XMC",
      "VC", "VVIV", "LLX", "DD", "IIIII", "XXXX", "CCCCXVC", "MMMMI", "VIV", "LXL", "DCDIX", "CMCV",
      "XCX", "IVIIC", "IXVX", "XVX", "IXIIV", "IIX", "VXX", "LCI", "DMC", "ILV", "ICIX", "XD", "XM",
      "VCI", "VVXV", "LLC", "DDI", "IIII", "XXXXX", "CCCCIV", "MMMM", "VIV", "LXLIXC", "DCD", "CMCV",
      "XCXI", "IVIX", "IXV", "XVXI", "IXIVC", "IIXX", "VXI", "LC", "DM", "ILIXV", "IC", "XDC", "XMI",
      "VCX", "VVV", "LLI", "DD", "IIIIX", "XXXXIC", "CCCCV", "MMMM", "VIVIX", "LXL", "DCD", "CMCIV",
      "XCXXC", "IVI", "IXVI", "XVX", "IXIXV", "IIXI", "VX", "LCC", "DMIX", "ILV", "IC", "XDI", "XMX",
      "VC", "VVIVC", "LL", "DDX", "IIIII", "XXXX", "CCCCV", "MMMMIX", "VIVC", "LXL", "DCDI", "CMCXV",
      "XCX", "IVII", "IXV", "XVXXC", "IXIIV", "IIX", "VX", "LCIX", "DM", "ILV", "ICIC", "XDX", "XM",
      "VCI", "VVV", "LLX", "DDI", "IIIIC", "XXXX", "CCCCIXV", "MMMM", "VIV", "LXLI", "DCDX", "CMCVC",
      "XCXI", "IVI", "IXVX", "XVXI", "IXIV", "IIX", "VXIXC", "LC", "DM", "ILIV", "ICX", "XD", "XMI",
      "VCC", "VVXV", "LLI", "DD", "IIII", "XXXXIX", "CCCCV", "MMMMC", "VIVI", "LXLX", "DCD", "CMCIV",
      "XCX", "IVIX", "IXVIC", "XVX", "IXIV", "IIXIX", "VX", "LC", "DMI", "ILXVC", "IC", "XDI", "XM",
      "VCX", "VVIV", "LL", "DDC", "IIIIIX", "XXXX", "CCCCV", "MMMMI", "VIVX", "LXL", "DCDIC", "CMCV",
      "XCXX", "IVII", "IXV", "XVX", "IXIIXV", "IIXC", "VX", "LCI", "DMX", "ILV", "ICI", "XD", "XMXC",
      "VCI", "VVV", "LL", "DDIX", "IIII", "XXXX", "CCCCIVC", "MMMMX", "VIV", "LXLI", "DCD", "CMCXV",
      "XCXI", "IVIC", "IXV", "XVXIX", "IXIV", "IIX", "VXI", "LCX", "DMC", "ILIV", "IC", "XDX", "XMI",
      "VC", "VVV", "LLIXC", "DD", "IIII", "XXXXI", "CCCCXV", "MMMM", "VIVI", "LXLC", "DCDX", "CMCIV",
      "XCX", "IVI", "IXVIX", "XVX", "IXIVC", "IIXI", "VXX", "LC", "DMI", "ILV", "ICX", "XDIC", "XM",
      "VC", "VVIXV", "LL", "DD", "IIIII", "XXXXXC", "CCCCV", "MMMMI", "VIV", "LXLX", "DCDI", "CMCV",
      "XCXC", "IVIIX", "IXV", "XVX", "IXIIV", "IIXX", "VX", "LCIC", "DM", "ILXV", "ICI", "XD", "XM",
      "VCIX", "VVVC", "LL", "DDI", "IIIIX", "XXXX", "CCCCIV", "MMMM", "VIVXC", "LXLI", "DCD", "CMCV", 
      "XCXIX", "IVI", "IXV", "XVXIC", "IXIXV", "IIX", "VXI", "LC", "DMX", "ILIV", "ICC", "XD", "XMIX",
      "VC", "VVV", "LLI", "DDX", "IIIIC", "XXXXI", "CCCCV", "MMMMX", "VIVI", "LXL", "DCD", "CMCIXVC",
      "XCX", "IVI", "IXVI", "XVXX", "IXIV", "IIXI", "VXC", "LCX", "DMI", "ILV", "IC", "XDIX", "XM",
      "VCC", "VVIV", "LLX", "DD", "IIIII", "XXXX", "CCCCXV", "MMMMIC", "VIV", "LXL", "DCDIX", "CMCV",
      "XCX", "IVII", "IXVXC", "XVX", "IXIIV", "IIX", "VXX", "LCI", "DM", "ILVC", "ICIX", "XD", "XM",
      "VCI", "VVXV", "LL", "DDIC", "IIII", "XXXXX", "CCCCIV", "MMMM", "VIV", "LXLIX", "DCDC", "CMCV",
      "XCXI", "IVIX", "IXV", "XVXI", "IXIV", "IIXXC", "VXI", "LC", "DM", "ILIXV", "IC", "XD", "XMIC",
      "VCX", "VVV", "LLI", "DD", "IIIIX", "XXXXI", "CCCCVC", "MMMM", "VIVIX", "LXL", "DCD", "CMCIV",
      "XCXX", "IVIC", "IXVI", "XVX", "IXIXV", "IIXI", "VX", "LC", "DMIXC", "ILV", "IC", "XDI", "XMX",
      "VC", "VVIV", "LLC", "DDX", "IIIII", "XXXX", "CCCCV", "MMMMIX", "VIV", "LXLC", "DCDI", "CMCXV",
      "XCX", "IVII", "IXV", "XVXX", "IXIIVC", "IIX", "VX", "LCIX", "DM", "ILV", "ICI", "XDXC", "XM",
      "VCI", "VVV", "LLX", "DDI", "IIII", "XXXXC", "CCCCIXV", "MMMM", "VIV", "LXLI", "DCDX", "CMCV",
      "XCXIC", "IVI", "IXVX", "XVXI", "IXIV", "IIX", "VXIX", "LCC", "DM", "ILIV", "ICX", "XD", "XMI",
      "VC", "VVXVC", "LLI", "DD", "IIII", "XXXXIX", "CCCCV", "MMMM", "VIVIC", "LXLX", "DCD", "CMCIV",
      "XCX", "IVIX", "IXVI", "XVXC", "IXIV", "IIXIX", "VX", "LC", "DMI", "ILXV", "ICC", "XDI", "XM",
      "VCX", "VVIV", "LL", "DD", "IIIIIXC", "XXXX", "CCCCV", "MMMMI", "VIVX", "LXL", "DCDI", "CMCVC",
      "XCXX", "IVII", "IXV", "XVX", "IXIIXV", "IIX", "VXC", "LCI", "DMX", "ILV", "ICI", "XD", "XMX",
      "VCIC", "VVV", "LL", "DDIX", "IIII", "XXXX", "CCCCIV", "MMMMXC", "VIV", "LXLI", "DCD", "CMCXV",
      "XCXI", "IVI", "IXVC", "XVXIX", "IXIV", "IIX", "VXI", "LCX", "DM", "ILIVC", "IC", "XDX", "XMI",
      "VC", "VVV", "LLIX", "DDC", "IIII", "XXXXI", "CCCCXV", "MMMM", "VIVI", "LXL", "DCDXC", "CMCIV",
      "XCX", "IVI", "IXVIX", "XVX", "IXIV", "IIXIC", "VXX", "LC", "DMI", "ILV", "ICX", "XDI", "XMC",
      "VC", "VVIXV", "LL", "DD", "IIIII", "XXXXX", "CCCCVC", "MMMMI", "VIV", "LXLX", "DCDI", "CMCV", 
      "XCX", "IVIIXC", "IXV", "XVX", "IXIIV", "IIXX", "VX", "LCI", "DMC", "ILXV", "ICI", "XD", "XM",
      "VCIX", "VVV", "LLC", "DDI", "IIIIX", "XXXX", "CCCCIV", "MMMM", "VIVX", "LXLIC", "DCD", "CMCV",
      "XCXIX", "IVI", "IXV", "XVXI", "IXIXVC", "IIX", "VXI", "LC", "DMX", "ILIV", "IC", "XDC", "XMIX",
      "VC", "VVV", "LLI", "DDX", "IIII", "XXXXIC", "CCCCV", "MMMMX", "VIVI", "LXL", "DCD", "CMCIXV",
      "XCXC", "IVI", "IXVI", "XVXX", "IXIV", "IIXI", "VX", "LCXC", "DMI", "ILV", "IC", "XDIX", "XM",
      "VC", "VVIVC", "LLX", "DD", "IIIII", "XXXX", "CCCCXV", "MMMMI", "VIVC", "LXL", "DCDIX", "CMCV",
      "XCX", "IVII", "IXVX", "XVXC", "IXIIV", "IIX", "VXX", "LCI", "DM", "ILV", "ICIXC", "XD", "XM",
      "VCI", "VVXV", "LL", "DDI", "IIIIC", "XXXXX", "CCCCIV", "MMMM", "VIV", "LXLIX", "DCD", "CMCVC",
      "XCXI", "IVIX", "IXV", "XVXI", "IXIV", "IIXX", "VXIC", "LC", "DM", "ILIXV", "IC", "XD", "XMI",
      "VCXC", "VVV", "LLI", "DD", "IIIIX", "XXXXI", "CCCCV", "MMMMC", "VIVIX", "LXL", "DCD", "CMCIV",
      "XCXX", "IVI", "IXVIC", "XVX", "IXIXV", "IIXI", "VX", "LC", "DMIX", "ILVC", "IC", "XDI", "XMX",
      "VC", "VVIV", "LL", "DDXC", "IIIII", "XXXX", "CCCCV", "MMMMIX", "VIV", "LXL", "DCDIC", "CMCXV",
      "XCX", "IVII", "IXV", "XVXX", "IXIIV", "IIXC", "VX", "LCIX", "DM", "ILV", "ICI", "XDX", "XMC",
      "VCI", "VVV", "LLX", "DDI", "IIII", "XXXX", "CCCCIXVC", "MMMM", "VIV", "LXLI", "DCDX", "CMCV",
      "XCXI", "IVIC", "IXVX", "XVXI", "IXIV", "IIX", "VXIX", "LC", "DMC", "ILIV", "ICX", "XD", "XMI",
      "VC", "VVXV", "LLIC", "DD", "IIII", "XXXXIX", "CCCCV", "MMMM", "VIVI", "LXLXC", "DCD", "CMCIV",
      "XCX", "IVIX", "IXVI", "XVX", "IXIVC", "IIXIX", "VX", "LC", "DMI", "ILXV", "IC", "XDIC", "XM",
      "VCX", "VVIV", "LL", "DD", "IIIIIX", "XXXXC", "CCCCV", "MMMMI", "VIVX", "LXL", "DCDI", "CMCV",
      "XCXXC", "IVII", "IXV", "XVX", "IXIIXV", "IIX", "VX", "LCIC", "DMX", "ILV", "ICI", "XD", "XMX",
      "VCI", "VVVC", "LL", "DDIX", "IIII", "XXXX", "CCCCIV", "MMMMX", "VIVC", "LXLI", "DCD", "CMCXV",
      "XCXI", "IVI", "IXV", "XVXIXC", "IXIV", "IIX", "VXI", "LCX", "DM", "ILIV", "ICC", "XDX", "XMI",
      "VC", "VVV", "LLIX", "DD", "IIIIC", "XXXXI", "CCCCXV", "MMMM", "VIVI", "LXL", "DCDX", "CMCIVC",
      "XCX", "IVI", "IXVIX", "XVX", "IXIV", "IIXI", "VXXC", "LC", "DMI", "ILV", "ICX", "XDI", "XM",
      "VCC", "VVIXV", "LL", "DD", "IIIII", "XXXXX", "CCCCV", "MMMMIC", "VIV", "LXLX", "DCDI", "CMCV",
      "XCX", "IVIIX", "IXVC", "XVX", "IXIIV", "IIXX", "VX", "LCI", "DM", "ILXVC", "ICI", "XD", "XM",
      "VCIX", "VVV", "LL", "DDIC", "IIIIX", "XXXX", "CCCCIV", "MMMM", "VIVX", "LXLI", "DCDC", "CMCV",
      "XCXIX", "IVI", "IXV", "XVXI" };

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
