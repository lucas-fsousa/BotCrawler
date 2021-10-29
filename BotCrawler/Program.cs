using CrawlerDemo;
using CrawlerDemo02;
using CrawlerDemo02.Entities;
using SeleniumDemo;
using System;
using System.Threading.Tasks;
namespace BootCrawler {
  class Program {
    static void Main(string[] args) {
      //ScanBMW();
      ScanUOL();
      //Selenium();
    }

    private static async Task ScanBMW() {
      await ScanBmw.GoScan();
    }

    private static async Task ScanUOL() {
      var rec = await Uol.GoScan();
      Console.WriteLine("====================================================================");
      Console.WriteLine($"Destaque da UOL agora:\n\t{rec.Title}\n\t\t{rec.Description}\n >>{rec.Link}");
      Console.WriteLine("====================================================================\n");
      Console.WriteLine("Outras noticias também em destaque: \n\n");

      foreach(var notice in rec.Notices) {
        Console.WriteLine($"\t{notice.Title}\n\t\t{notice.Description}\n >> {notice.Link}\n");
        Console.WriteLine("====================================================================");
      }
    }

    private static async Task Selenium() {
      await GoTest.GG();
    }
  }
}
