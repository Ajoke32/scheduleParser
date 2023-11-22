


using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

var driver = new ChromeDriver();
var js = (IJavaScriptExecutor)driver;
driver.Url  = "https://cabinet.ztu.edu.ua/site/login";
const string login = ;
const string password = "";
Console.WriteLine("Wait when page loaded and press any key");
Console.ReadKey();
Console.WriteLine("Authorization started...");

var loginField = driver.FindElement(By.Id("loginform-username"));
var passwordField = driver.FindElement(By.Id("loginform-password"));
var rememberButton = driver.FindElement(By.Id("loginform-rememberme"));
loginField.SendKeys(login);
passwordField.SendKeys(password);
rememberButton.Click();
var loginButton = driver.FindElement(By.CssSelector(".btn.btn-primary"));
loginButton.Click();
Console.WriteLine("Wait when page loaded and press any key");
Console.ReadKey();
Console.WriteLine("Navigating to schedule");
var toSchedule = driver.FindElement(By.ClassName("top-menu"))
    .FindElements(By.TagName("a"))[1];
toSchedule.Click();

Console.WriteLine("Wait when page loaded and press any key");
Console.ReadKey();
Console.WriteLine("Parsing...");

var pairs = driver.FindElements(By.CssSelector(".pair"));

var ps = new List<string>();

var pd = new List<string>();
var pdTimes = new [] { "13:30-14:50","11:40-13:00"};
foreach (var pair in pairs)
{
    var dates = pair.FindElements(By.CssSelector(".date"));
    js.ExecuteScript("arguments[0].scrollIntoView();", pair);

    IWebElement? link = null;
    try
    {
       link = pair.FindElement(By.TagName("a"));
    }
    catch (Exception e)
    {
        continue;
    }
    
    foreach (var date in dates)
    {
        var titles = date.FindElements(By.CssSelector("div"));
        var time = titles[0].Text;
        var name = titles[1];
        var isLect = titles[2];
        var attr = link.GetAttribute("href");
        
        if (name.Text == "Психологія спілкування")
        {
            ps.Add($"{time} - ${attr} - ${isLect.Text}");
        }

        if (name.Text == "Правила дорожнього руху" && pdTimes.Contains(time))
        {
            pd.Add($"{time} - ${attr} - ${isLect.Text}");
        }
    }
    
}

foreach (var i in ps)
{
    Console.WriteLine(i);
}

Console.WriteLine("\n");

foreach (var j in pd)
{
    Console.WriteLine(j);
}
driver.Close();
driver.Quit();