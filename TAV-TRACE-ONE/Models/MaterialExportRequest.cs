using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAV_TRACE_ONE.Models
{
    public sealed class MaterialExportRequest
    {
        public List<string> Plants { get; set; }
            = new List<string>();

        public List<string> Codes { get; set; }
            = new List<string>();

        public bool IncludeAttributes { get; set; }

        public bool IncludeNames { get; set; }

        public bool IncludeEaNs { get; set; }

        public bool IncludeCosts { get; set; }

        public bool IncludeGhgsClassifications
        {
            get;
            set;
        }

        public bool IncludeVendors { get; set; }

        public bool IncludeUfSets { get; set; }

        public bool IncludeUfAttributes
        {
            get;
            set;
        }

        public List<string> Attributes
        {
            get;
            set;
        } = new List<string>();

        public bool IncludeCustomers
        {
            get;
            set;
        }
    }

   
}
