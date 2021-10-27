using CrawlerDemo;
using System;
using System.Threading.Tasks;
namespace BootCrawler {
  class Program {
    static void Main(string[] args) {
      ScanBMW();
    }

    private static async Task ScanBMW() {
      await ScanBmw.GoScan();
    }

    private static async Task Other() {

    }
  }
}
