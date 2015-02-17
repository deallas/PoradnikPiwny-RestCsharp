using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace restcsharp.exception
{
    public class BeerNotFoundException : Exception
    {
        public BeerNotFoundException()
        {

        }
        
        public BeerNotFoundException(string msg)
            : base(msg)
        {

        }
    }
}
