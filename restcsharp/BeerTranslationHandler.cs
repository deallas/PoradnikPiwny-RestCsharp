using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using restcsharp.entity;

namespace restcsharp.rest
{
    public class BeerTranslationHandler
    {
        public string msg
        {
            set;
            get;
        }

        public BeerTranslation translation
        {
            set;
            get;
        }
    }
}
