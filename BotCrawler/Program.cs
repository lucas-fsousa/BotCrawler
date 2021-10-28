using CrawlerDemo;
using CrawlerDemo02;
using System;
using System.Threading.Tasks;
namespace BootCrawler {
  class Program {
    static void Main(string[] args) {
      //ScanBMW();
      ScanReddit();
    }

    private static async Task ScanBMW() {
      await ScanBmw.GoScan();
    }

    private static async Task ScanReddit() {
     await Reddit.GoScan();
    }
  }
}
