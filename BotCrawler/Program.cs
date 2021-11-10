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
      var notices = Uol.GoScan().Result;
      foreach(var item in notices.Notices) {
        var rec = UolDetalhamentoDia_Dia.ReadByLink(item.Link);
        if(rec.PublishNotes.Trim().Length > 1) {
          Console.WriteLine($"{item.Title}\n\t{item.Description}\n");
          Console.WriteLine($"{rec.Autor} - {rec.AutorLocation} - {rec.PublishDate}\n");
          Console.WriteLine($"{rec.PublishNotes}");
          Console.WriteLine("\n=================================================================================\n");
          Thread.Sleep(2000);
        }
      }
    }

    private static void Selenium() {
      Google.SearchFor("Como tirar 10 sem estudar?");
    }
  }
}
