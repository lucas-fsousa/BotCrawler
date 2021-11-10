using CrawlerDemo02.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrawlerDemo02 {
  public static class UolControl {
    public static async Task ExecuteReader() {
      while(true) {
        var newNotices = await UolHomePage.GoScan();
        foreach(var notice in newNotices) {
          if(notice.PublishNotes.Length > 1) {
            Console.WriteLine($"\t{notice.Title}\n\t\t{notice.Description}\n >> {notice.Link}\n");
            Console.WriteLine($"  {notice.Autor} - {notice.AutorLocation} - {notice.PublishDate}");
            Console.WriteLine($"\t{notice.PublishNotes}");
            Console.WriteLine("====================================================================\n");
            Thread.Sleep(1000); // espera 1 segundo
            Console.Clear(); // limpa o console
          }
        }
      }
    }
  }
}
