using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerDemo02.Entities {
  public class Notice {
    public string Link { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
  }

  public class MainNotice {
    public string Link { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<Notice> Notices { get; set; }
  }
}
