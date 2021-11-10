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
  public static class UolHomePage {
    public static async Task<List<Notice>> GoScan() {
      var notices = new List<Notice>();
      try {
        var url = "https://www.uol.com.br/";
        var document = new HtmlDocument();

        using(var wc = new WebClient()) {
          var html = await Task.FromResult(wc.DownloadString(url));
          document.LoadHtml(html);
        }

        foreach(var notice in document.DocumentNode.SelectNodes("//article[@aria-labelledby]").ToList()) {
          var newNotice = new Notice {
            Link = $"{notice?.Descendants("a")?.FirstOrDefault()?.Attributes["href"].Value}",
            Title = $"{notice?.Descendants("h3")?.FirstOrDefault()?.InnerText.Trim()}",
            Description = $"{notice?.Descendants("p")?.FirstOrDefault()?.InnerText.Trim()}"
          };

          if(newNotice.Description.Equals("")) {
            newNotice.Description = "A noticia não possui uma descrição ou subtitulo.";
          }

          foreach(var valid in "ao-vivo,aovivo,ao_vivo".Split(",")) {
            var validLink = $"{newNotice.Link.Split("/").Where(x => x.IndexOf(valid, 0).Equals(0)).FirstOrDefault()}";
            if(!validLink.Equals("")) {
              newNotice.Link = "";
              break;
            }
          }
          if(!newNotice.Title.Equals("") && !newNotice.Link.Equals("")) {
            notices.Add(newNotice);
          }
        }

        // ===========================================
        List<HtmlNode> text = null;
        foreach(var item in notices) {
          using(var wc = new WebClient()) {
            document.LoadHtml(wc.DownloadString(item.Link));
          }
          // checks nullable
          var divBase = document.DocumentNode.SelectNodes("//div[@class='  author  ']")?.FirstOrDefault();
          if($"{document.DocumentNode.SelectNodes("//div[@class='  author  ']")?.FirstOrDefault()}".Equals("")) {
            item.Autor = "UOL";
            item.PublishDate = $"{document.DocumentNode.SelectSingleNode("//time[@class='c-more-options__published-date']")?.InnerText}";
            item.AutorLocation = "SP - São Paulo";
          } else {
            item.Autor = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author"))?.FirstOrDefault()?.InnerText}".Trim();
            item.PublishDate = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author time"))?.FirstOrDefault()?.InnerText.ToString()}".Trim();
            item.AutorLocation = $"{divBase.Descendants("p")?.Where(x => x.Attributes["class"].Value.Equals("p-author-local"))?.FirstOrDefault()?.InnerText.ToString()}".Trim();

            if(item.Autor.Equals("")) item.Autor = "Autor desconhecido / Colaboração Externa";
          }

          string divText = $"{document.DocumentNode.SelectNodes("//div[@class='text  ']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";
          string divTextSection = $"{document.DocumentNode.SelectNodes("//div[@class='c-news__body']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";
          string divAlt = $"{document.DocumentNode.SelectNodes("//div[@id='modal-ready']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";
          string divTextAlt = $"{document.DocumentNode.SelectNodes("//div[@class='text']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";
          string divTextAlt2 = $"{document.DocumentNode.SelectNodes("//div[@class='text has-image ']")?.FirstOrDefault()?.Descendants("p")?.ToList()}";


          if(!divText.Equals("")) {
            text = document.DocumentNode.SelectNodes("//div[@class='text  ']").FirstOrDefault().Descendants("p").ToList();
          } else if(!divTextSection.Equals("")) {
            text = document.DocumentNode.SelectNodes("//div[@class='c-news__body']").FirstOrDefault().Descendants("p").ToList();
          } else if(!divAlt.Equals("")) {
            text = document.DocumentNode.SelectNodes("//div[@id='modal-ready']").FirstOrDefault().Descendants("p").ToList();
          } else if(!divTextAlt.Equals("")) {
            text = document.DocumentNode.SelectNodes("//div[@class='text']").FirstOrDefault().Descendants("p").ToList();
          } else if(!divTextAlt2.Equals("")) {
            text = document.DocumentNode.SelectNodes("//div[@class='text has-image ']").FirstOrDefault().Descendants("p").ToList();
          } else {
            text = new List<HtmlNode>();
            text.Add(HtmlNode.CreateNode("<p></p>"));
          }

          foreach(var p in text) {
            item.PublishNotes += $"{p.InnerText}\n";
          }
        }
        return notices;
      } catch(Exception ex) {
        Console.WriteLine("ops... Algo deu errado!");
        return null;
      }
    }
  }
}
