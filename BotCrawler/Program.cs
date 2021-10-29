using CrawlerDemo;
using CrawlerDemo02;
using CrawlerDemo02.Entities;
using SeleniumDemo;
using System;
using System.Threading;
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
      for(int i = 1; i >= 1; i++) {
        var newNotices = await Uol.GoScan();
        Console.WriteLine("====================================================================");
        Console.WriteLine($"Destaque da UOL agora:\n\t{newNotices.Title}\n\t\t{newNotices.Description}\n >>{newNotices.Link}");
        Console.WriteLine("====================================================================\n");
        Console.WriteLine("Outras noticias também em destaque: \n\n");

        foreach(var notice in newNotices.Notices) {
          Console.WriteLine($"\t{notice.Title}\n\t\t{notice.Description}\n >> {notice.Link}\n");
          Console.WriteLine("====================================================================");
        }
        Thread.Sleep(3600000); // espera 1 hora
        Console.Clear();
      }
    }

    private static async Task Selenium() {
      await GoTest.GG();
    }
  }
}
