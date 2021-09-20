using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ResponseData<T>
    {
        public HttpStatusCode ResponseStatus { get; set; }

        public List<string> ValidationErrors { get; set; }

        public T Data { get; set; }
    }
}
