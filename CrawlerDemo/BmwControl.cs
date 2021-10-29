using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrawlerDemo {
  public static class BmwControl {
    public static void ExecuteReader() {
      var cars = ScanBmw.GoScan();
      foreach(var car in cars.Result) {
        Console.WriteLine("===================================================================\n");
        Console.WriteLine($" Model: {car.Model}\n Price: {car.Price}\n ImageUrl: {car.ImageUrl}\n Link: {car.DirectLink}\n");
      }
    } 
  }
}
