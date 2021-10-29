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
      var inputSearch = driver.FindElement(By.Name("q"));
      inputSearch.SendKeys(inputForSearch);
      inputSearch.Submit();
    }
  }
}
