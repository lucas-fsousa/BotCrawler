using CrawlerDemo.Entidade;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CrawlerDemo {
  public static class ScanBmw {
    public static async Task<List<Car>> GoScan() {
      string url = "http://automobile.tn/neuf/bmw.3/";
      var document = new HtmlDocument();

      using(var wc = new WebClient()) {
        var html = await Task.FromResult(wc.DownloadString(url));
        document.LoadHtml(html);
      }

      var divs = document.DocumentNode.Descendants("div")
        .Where(node => node.GetAttributeValue("class", "")
        .Equals("versions-item")).ToList();

      var listCar = new List<Car>();
      foreach(var div in divs) {
        var car = new Car {
          Model = div?.Descendants("h2")?.FirstOrDefault()?.InnerText.Trim(),
          Price = div?.Descendants("div")?.FirstOrDefault()?.InnerText.Trim(),
          ImageUrl = div?.Descendants("img")?.FirstOrDefault()?.ChildAttributes("src").FirstOrDefault()?.Value.Trim(),
          DirectLink = div?.Descendants("a")?.FirstOrDefault()?.ChildAttributes("href").FirstOrDefault()?.Value.Trim()
        };
        listCar.Add(car);
      }
      return listCar;
    }
  }
}
