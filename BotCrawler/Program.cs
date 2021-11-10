using CrawlerDemo;
using CrawlerDemo02;
using CrawlerDemo02.Entities;
using CrawlerDemo03;
using SeleniumDemo;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace BootCrawler {
  class Program {
    static void Main(string[] args) {

      new Thread(ScanUOL).Start();
      //new Thread(ScanBMW).Start();
      //new Thread(Selenium).Start();
      
    }

    private static void ScanBMW() {
      BmwControl.ExecuteReader();
    }

    private static void ScanUOL() {
      UolControl.ExecuteReader();
    }

    private static void Selenium() {
      Google.SearchFor("Como tirar 10 sem estudar?");
    }
  }
}
