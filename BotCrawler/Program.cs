using CrawlerDemo;
using System;
using System.Threading.Tasks;
namespace BootCrawler {
  class Program {
    static void Main(string[] args) {
      LoadPage();
    }

    private static async Task LoadPage() {
      await ScanBmw.TestarCrawler();
    }
  }
}
