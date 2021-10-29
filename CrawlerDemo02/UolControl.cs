using CrawlerDemo02.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrawlerDemo02 {
  public static class UolControl {
    public static async Task ExecuteReader() {
      for(int i = 1; i >= 1; i++) {
        var newNotices = await Uol.GoScan();
        Console.WriteLine("====================================================================");
        Console.WriteLine($"Destaque da UOL agora:\n\t{newNotices.Title}\n\t\t{newNotices.Description}\n >> {newNotices.Link}");
        Console.WriteLine("====================================================================\n");
        Console.WriteLine("Outras noticias também em destaque: \n");

        foreach(var notice in newNotices.Notices) {
          Console.WriteLine($"\t{notice.Title}\n\t\t{notice.Description}\n >> {notice.Link}\n");
          Console.WriteLine("====================================================================\n");
        }
        Thread.Sleep(3600000); // espera 1 hora
        Console.Clear();
      }
    }
  }
}
