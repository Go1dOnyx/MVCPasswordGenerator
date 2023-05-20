using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;
using MVCPassGenerator.Models;
using System.Text;
using TextCopy;

namespace MVCPassGenerator.Controllers
{
    public class PasswordGeneratorController : Controller
    {
        public IActionResult Index(PasswordGenerator passLength)
        {
            ViewBag.PasswordIsValid = false;

            //Validation
            if (passLength.PasswordLength <= 180) {

                passLength.LengthToLong = false;
                passLength.HasBoxesEmpty = false;

                if (passLength.HasInteger)
                    passLength.ValidPass += "1234567890";
                if (passLength.HasCaptial)
                    passLength.ValidPass +="ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                if (passLength.HasLowerCase)
                    passLength.ValidPass +="abcdefghijklmnopqrstuvwxyz";
                if (passLength.HasSymbols)
                    passLength.ValidPass += passLength.SymbolList;
                if (passLength.HasSymbols && passLength.SymbolList == null)
                    passLength.ValidPass = " ";
            }
            else
            {
                //Else if the length is too long
                passLength.ValidPass = " ";
                passLength.LengthToLong = true;
            }

            if (passLength.PasswordLength > 0)
            {
                //Checks if the checkboxes are not selected
                if (passLength.HasInteger == false && passLength.HasCaptial == false && passLength.HasLowerCase == false && passLength.HasSymbols == false)
                {
                    passLength.HasBoxesEmpty = true;
                    passLength.ValidPass = " ";
                }
            }
            //------------------------------------------

            string validChar = passLength.ValidPass; 
            StringBuilder pass = new StringBuilder();
            Random randNum = new Random();
            while (0 < passLength.PasswordLength--)
             {
                pass.Append(validChar[randNum.Next(validChar.Length)]); //Creates random chars and adds to string builder object
             }
   
            passLength.PasswordResult = pass.ToString();
            if (passLength.PasswordResult.Length >= 8 && passLength.PasswordResult.Any(char.IsDigit))
            {
                if (passLength.PasswordResult.Any(char.IsUpper) && passLength.PasswordResult.Any(char.IsLower) && passLength.PasswordResult.Any(char.IsSymbol)) {
                    ViewBag.PasswordIsValid = true;
                }
            }
            return View(passLength);
        }



        [HttpPost]
        public IActionResult resetForm(PasswordGenerator passReset) {
            ModelState.Clear();
            PasswordGenerator newResetForm = new PasswordGenerator()
            {
                PasswordLength = 0,
                SymbolList = string.Empty,
                PasswordResult = string.Empty,
                ValidPass = string.Empty,
                HasInteger = false,
                HasCaptial = false,
                HasBoxesEmpty = false,
                HasLowerCase = false,
                HasSymbols = false,
                LengthToLong = false,

        };
            return View("Index", newResetForm);
        }

    }
}
