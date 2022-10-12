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
            if (passLength.PasswordLength > 0)
            {
                //Checks if the checkboxes are not selected
                if (passLength.HasInteger == false && passLength.HasCaptial == false && passLength.HasLowerCase == false && passLength.HasSymbols == false)
                {
                    passLength.HasBoxesEmpty = true;
                    passLength.ValidPass = " ";
                }
            }
            //
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
                {
                    passLength.ValidPass = " ";
                }
            }
            else
            {
                //Checks if the length is too long
                passLength.ValidPass = " ";
                passLength.LengthToLong = true;
            }
           
            string validChar = passLength.ValidPass;
            StringBuilder pass = new StringBuilder();
            Random randNum = new Random();
            while (0 < passLength.PasswordLength--)
             {
                    pass.Append(validChar[randNum.Next(validChar.Length)]); 
             }
   
            passLength.PasswordResult = pass.ToString();
            return View(passLength);
        }
        public void CopyText(PasswordGenerator passwordCopy) {
            //Copies result to clipboard
           TextCopy.ClipboardService.SetText(passwordCopy.PasswordResult);
            
        }
       
    }
}
