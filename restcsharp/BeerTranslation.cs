using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restcsharp.entity
{
    public class BeerTranslation
    {
        public string Lang
        {
            get;
            set;
        }

        private string _description;

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value.Replace(System.Environment.NewLine, "");
            }
        }
    }
}
