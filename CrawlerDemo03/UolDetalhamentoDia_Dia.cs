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

    public static DetailsNotice ReadByLink(string link) {
      var details = new DetailsNotice();
      List<HtmlNode> text = null;
      try {
        var document = new HtmlDocument();
        using(var wc = new WebClient()) {
          document.LoadHtml(wc.DownloadString(link));
        }
        // checks nullable
        var divBase = document.DocumentNode.SelectNodes("//div[@class='  author  ']")?.FirstOrDefault();
        if($"{document.DocumentNode.SelectNodes("//div[@class='  author  ']")?.FirstOrDefault()}".Equals("")) {
          details.Autor = "UOL";
          details.PublishDate = $"{document.DocumentNode.SelectSingleNode("//time[@class='c-more-options__published-date']")?.InnerText}";
          details.AutorLocation = "SP - São Paulo";
        } else {
          details.Autor = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author"))?.FirstOrDefault()?.InnerText}".Trim();
          details.PublishDate = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author time"))?.FirstOrDefault()?.InnerText.ToString()}".Trim();
          details.AutorLocation = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author-local"))?.FirstOrDefault()?.InnerText.ToString()}".Trim();

          if(details.Autor.Equals("")) details.Autor = "Autor desconhecido / Colaboração Externa";
        }

        string divText = $"{document.DocumentNode.SelectNodes("//div[@class='text  ']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";
        string divTextSection = $"{ document.DocumentNode.SelectNodes("//div[@class='c-news__body']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";
        string divAlt = $"{document.DocumentNode.SelectNodes("//div[@id='modal-ready']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";
        string divTextAlt = $"{document.DocumentNode.SelectNodes("//div[@class='text']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";

        if(!divText.Equals("")) {
          text = document.DocumentNode.SelectNodes("//div[@class='text  ']").FirstOrDefault().Descendants("p").ToList();
        }else if(!divTextSection.Equals("")) {
          text = document.DocumentNode.SelectNodes("//div[@class='c-news__body']").FirstOrDefault().Descendants("p").ToList();
        }else if(!divAlt.Equals("")) {
          text = document.DocumentNode.SelectNodes("//div[@id='modal-ready']").FirstOrDefault().Descendants("p").ToList();
        }else if(!divTextAlt.Equals("")) {
          text = document.DocumentNode.SelectNodes("//div[@class='text']").FirstOrDefault().Descendants("p").ToList();
        }

        foreach(var p in text) {
          details.PublishNotes += $"{p.InnerText}\n";
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


  public class DetailsNotice {
    public string AutorLocation { get; set; }
    public string Autor { get; set; }
    public string PublishDate { get; set; }
    public string PublishNotes { get; set; }

  }

}
