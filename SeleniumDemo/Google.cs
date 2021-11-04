using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumDemo {
  public static class Google {
    public static void SearchFor(string inputForSearch) {
      IWebDriver driver = new ChromeDriver();
      driver.Url = "https://www.google.com.br/";
      Thread.Sleep(4000);
      var inputSearch = driver.FindElement(By.Name("q"));
      Thread.Sleep(4000);
      inputSearch.SendKeys(inputForSearch);
      Thread.Sleep(4000);
      inputSearch.Submit();
    }
  }
}
