using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAV_TRACE_ONE.Models
{
    public sealed class ApiResult
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string ReasonPhrase { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }
        public bool BusinessSuccess { get; set; }

        public string BusinessError { get; set; }
    }
}
