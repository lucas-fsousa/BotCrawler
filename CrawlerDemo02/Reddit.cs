using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerDemo02 {
  public static class Reddit {
    public static async Task GoScan() {
      try {
        var url = "https://www.reddit.com/";
        var document = new HtmlDocument();
        using(var wc = new WebClient()) {
          var html = await Task.FromResult(wc.DownloadString(url));
          document.LoadHtml(html);
        }
        var container = document.DocumentNode.Descendants("div").ToList();
        foreach(var div in container) {
          var nome = div.Attributes.Select(x => x.Name).Where(x => x.Equals("id"));
          var valor = div.Attributes.Select(x => x.Value).Where(x => x.Equals("Teste"));
        }
        if(container.Equals(null)) {
          Console.WriteLine("F");
        }
        //foreach(var div in divs) {
        //  var interno = div.DescendantsAndSelf();
        //  //if(div.GetAttributeValue("class", "cslJXYWf-T5weuzAPoO3X").Any()) Console.WriteLine(div.GetAttributeValue("class", "cslJXYWf-T5weuzAPoO3X"));
        //}
        string a = "a";

          
       
      } catch(Exception ex) {
        Console.WriteLine("ops... Algo deu errado!");
      }
     
    }
  }
}
