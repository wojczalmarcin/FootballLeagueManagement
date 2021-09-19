using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class ResponseData<T>
    {
        public ResponseStatus ResponseStatus { get; set; }

        public List<string> ValidationResult { get; set; }

        public T Data { get; set; }
    }
}
