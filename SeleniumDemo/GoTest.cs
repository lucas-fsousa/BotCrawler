using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading.Tasks;

namespace SeleniumDemo {
  public static class GoTest {
    public static async Task GG() {
      IWebDriver driver = new ChromeDriver();
      driver.Url = "https://www.google.com.br/";
      var inputSearch = driver.FindElement(By.Name("q"));

      inputSearch.SendKeys("Hello Word?");
      inputSearch.Submit();
    }
  }
}
