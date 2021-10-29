using CrawlerDemo02.Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerDemo02 {
  public static class Uol {
    public static async Task<MainNotice> GoScan() {
      try {
        var url = "https://www.uol.com.br/";
        var document = new HtmlDocument();

        using(var wc = new WebClient()) {
          var html = await Task.FromResult(wc.DownloadString(url));
          document.LoadHtml(html);
        }

        var otherNotices = document.DocumentNode.SelectNodes("//article[@aria-labelledby]").ToList();
        var notices = new List<Notice>();

        foreach(var notice in otherNotices) {
          var newNotice = new Notice {
            Link = $"{notice?.Descendants("a")?.FirstOrDefault()?.Attributes["href"].Value}",
            Title = $"{notice?.Descendants("h3")?.FirstOrDefault()?.InnerText.Trim()}",
            Description = $"{notice?.Descendants("p")?.FirstOrDefault()?.InnerText.Trim()}"
          };

          if(newNotice.Description.Equals("")) {
            newNotice.Description = "A noticia não possui uma descrição ou subtitulo.";
          }

          if(!newNotice.Title.Equals("")) {
            notices.Add(newNotice);
          }
        }

        var mainNotice = document.DocumentNode.SelectNodes("//article[@class='headlineMain section__grid__main__highlight__item']").FirstOrDefault();
        var newsletter = new MainNotice() {
          Description = $"{mainNotice?.Descendants("p")?.FirstOrDefault()?.InnerText.Trim()}",
          Link = mainNotice?.Descendants("a")?.FirstOrDefault()?.Attributes["href"].Value,
          Title = mainNotice?.Descendants("h3")?.FirstOrDefault()?.InnerText.Trim(),
          Notices = notices
        };

        return newsletter;
      } catch(Exception ex) {
        Console.WriteLine("ops... Algo deu errado!");
        return null;
      }
    }
  }
}
