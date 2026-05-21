using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAV_TRACE_ONE.Models
{
    public sealed class ApiResponseRoot
    {
        public List<ResponseMessage>
            ResponseMessages
        {
            get;
            set;
        }
    }

    public sealed class ResponseMessage
    {
        public string RqstId { get; set; }

        public string Operation { get; set; }

        public string Status { get; set; }

        public string ErrorMessage { get; set; }
    }
}
