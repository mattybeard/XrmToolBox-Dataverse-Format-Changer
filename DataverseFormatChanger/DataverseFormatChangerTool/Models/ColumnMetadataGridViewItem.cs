using Microsoft.Xrm.Sdk.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataverseFormatChangerTool.Models
{
    public class ColumnMetadataGridViewItem
    {
        // TODO: make this read off the metadata field
        public string LogicalName { get; set; }
        public string DisplayName { get; set; }
        public string ColumnType { get; set; }
        public StringAttributeMetadata StringMetadata { get; internal set; }
        public MemoAttributeMetadata MemoMetadata { get; internal set; }
    }
}
