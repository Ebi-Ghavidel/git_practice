using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ebi_Cucumber_Back.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ToWord()
        {
            string isNegative = "";
            try
            {
                string name = Request.Form["Name"];
                string moneyInputString = Request.Form["Money"];
                var number = Convert.ToDecimal(moneyInputString).ToString();

                if (number.Contains("-"))
                {
                    isNegative = "Minus ";
                    number = number.Substring(1, number.Length - 1);
                }
                if (number == "0")
                {
                    ViewBag.Message = "The number in currency fomat is \nZero Only";
                }
                else
                {
                    ViewBag.Message = "Hello " + name + "! <br/>";
                    
                    //Ebi: Calling extension method: String.ToWords();
                    string output = number.ToWords().ToUpper();

                    //Ebi: There was a small bug in the program... for a number greater that Trillion it returns null string. I fixed it as follows:

                    ViewBag.Message += (output.Length < "Cents".Length) ? "It's very large amount of money that I couldn't calculate it!" : "The number in currency fomat is: <br />" + isNegative + output;
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }
            
            //Ebi: I used Sleep(1000) to delay 1 second to see the bauty of Ajax. You can remove it  :)
            System.Threading.Thread.Sleep(1000);

            return PartialView("_ToWord");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}