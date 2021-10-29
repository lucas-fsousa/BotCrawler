using CrawlerDemo;
using CrawlerDemo02;
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
     await Uol.GoScan();
    }

    private static async Task Selenium() {
      await GoTest.GG();
    }
  }
}
