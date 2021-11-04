using CrawlerDemo02.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrawlerDemo03 {
  public static class UolDetalhamentoDia_Dia {

    public static Teste ReadByLink(string link) {
      var details = new Teste();
      try {
        var document = new HtmlDocument();
        using(var wc = new WebClient()) {
          document.LoadHtml(wc.DownloadString(link));
        }
        // checks nullable
        var check = $"{document.DocumentNode.SelectNodes("//div[@class='  author  ']")?.FirstOrDefault()}";
        
        var divBase = document.DocumentNode.SelectNodes("//div[@class='  author  ']")?.FirstOrDefault();
        if(check.Equals("")) {
          details.Autor = "UOL";
          details.PublishDate = $"{document.DocumentNode.SelectSingleNode("//time[@class='c-more-options__published-date']")?.InnerText}";
          details.AutorLocation = "SP - São Paulo";
        } else {
          details.Autor = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author"))?.FirstOrDefault()?.InnerText}";
          details.PublishDate = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author time"))?.FirstOrDefault()?.InnerText.ToString()}";
          details.AutorLocation = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author-local"))?.FirstOrDefault()?.InnerText.ToString()}";
          if(details.Autor.Equals("")) details.Autor = "Autor desconhecido / Colaboração Externa";
          
          foreach(var p in document.DocumentNode.SelectNodes("//div[@class='text  ']").FirstOrDefault().Descendants("p").ToList()) {
            details.PublishNotes += $"{p.InnerText}\n";
          }
        }
      } catch(Exception ex) {
        Console.BackgroundColor = ConsoleColor.Red;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Ops... Ocorreu um erro. ");
        Console.ResetColor();
        Console.WriteLine($"\n{ex.Message}");
        details = null;
      }
      return details;
    }
  }


  public class Teste {
    public string AutorLocation { get; set; }
    public string Autor { get; set; }
    public string PublishDate { get; set; }
    public string PublishNotes { get; set; }

  }

}
