using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using TacoCatMVC.Models;

namespace TacoCatMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet] // get
        public IActionResult Reverse()
        {
            Palindrome model = new();
            return View(model);
        }

        [HttpPost] // post
        [ValidateAntiForgeryToken] // validate for forgery
        public IActionResult Reverse(Palindrome palindrome)
        {
            string inputWord = palindrome.InputWord; // grab the posted word the user submitted
            string revWord = "";

            for (int i = inputWord.Length - 1; i >= 0; i--) // start at end of word and work backwards
            {
                revWord += inputWord[i]; // add to revWord from last char to first
            }

            palindrome.RevWord = revWord; // set the RevWord of the palidrome object

            // regular expressions to remove non-letter characters
            revWord = Regex.Replace(revWord.ToLower(), "[^a-zA-Z0-9]+", "");
            inputWord = Regex.Replace(inputWord.ToLower(), "[^a-zA-Z0-9]+", "");

            if (revWord == inputWord)
            {
                palindrome.IsPalindrome = true; // set palindrome
                palindrome.Message = $"Success! {palindrome.InputWord} is a Palindrome!"; // set message
            }
            else
            {
                palindrome.IsPalindrome = false; // set palindrome
                palindrome.Message = $"Sorry... {palindrome.InputWord} is not a Palindrome."; // set message
            }

            return View(palindrome);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}