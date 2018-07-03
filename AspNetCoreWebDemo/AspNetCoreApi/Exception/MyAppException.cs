using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApi.Exception
{
    public class MyAppException : System.Exception
    {
        public MyAppException()
        { }

        public MyAppException(string message)
            : base(message)
        { }

        public MyAppException(string message, System.Exception innerException)
            : base(message, innerException)
        { }
    }
}
