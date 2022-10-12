using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCPassGenerator.Models
{
    public class PasswordGenerator
    {
        public int PasswordLength { get; set; }
        public string PasswordResult { get; set; } = "";
        public string ValidPass { get; set; } = "";
        public bool HasInteger { get; set; }
        public bool HasCaptial { get; set; }
        public bool HasLowerCase { get; set; }
        public bool HasSymbols { get; set; }
        public bool LengthToLong { get; set; }
        public bool HasBoxesEmpty { get; set;  }
        public string SymbolList { get; set; } = "";
    }
}
