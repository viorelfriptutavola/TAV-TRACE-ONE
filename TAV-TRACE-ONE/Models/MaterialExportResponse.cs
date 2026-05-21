using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAV_TRACE_ONE.Models
{
    public sealed class MaterialExportResponse
    {
        public List<MaterialDto> Materials
        {
            get;
            set;
        } = new List<MaterialDto>();

        public List<JobDto> JobList
        {
            get;
            set;
        } = new List<JobDto>();
    }

    public sealed class MaterialDto
    {
        public string Plant { get; set; }

        public string Code { get; set; }

        public string Prefix { get; set; }

        public string Description { get; set; }

        public string Type { get; set; }

        public MaterialAttributesDto Attributes
        {
            get;
            set;
        }
    }

    public sealed class MaterialAttributesDto
    {
        public List<ScalarAttributeDto>
            ScalarAttributes
        {
            get;
            set;
        } = new List<ScalarAttributeDto>();

        public List<TableAttributeDto>
            TableAttributes
        {
            get;
            set;
        } = new List<TableAttributeDto>();
    }

    public sealed class ScalarAttributeDto
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public sealed class TableAttributeDto
    {
        public string Name { get; set; }

        public List<TableColumnDto> Columns
        {
            get;
            set;
        } = new List<TableColumnDto>();

        public List<TableRowDto> Rows
        {
            get;
            set;
        } = new List<TableRowDto>();
    }

    public sealed class TableColumnDto
    {
        public string Name { get; set; }
    }

    public sealed class TableRowDto
    {
        public int RowNumber { get; set; }

        public int Index { get; set; }

        public List<TableCellDto> Cells
        {
            get;
            set;
        } = new List<TableCellDto>();
    }

    public sealed class TableCellDto
    {
        public string Value { get; set; }
    }

    public sealed class JobDto
    {
        public string JobId { get; set; }

        public string Status { get; set; }
    }
}
